// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	class MyUnit_FireOnDistanceIfSeeEnemyUnit : MyUnit
	{
		// Fire preiod
		protected long LastTimeWhenMadeFireInMilliseconds = 0;
		protected long TimeToMakeFire = 0;

		// constructor
		public MyUnit_FireOnDistanceIfSeeEnemyUnit(int life, int playerID, long timeToMakeFire, MyPicture myPicture)
			: base(life, playerID, myPicture)
		{
			// Damage
			TimeToMakeFire = timeToMakeFire;
		}

		public virtual bool CanFireOnThisUnit(IMyUnit unit, IMyGraphic myGraphic, IMyLevel gameLevel)
		{
			return false;
		}

		public override void OnNextTurn(long timeInMilliseconds, IMyGraphic myGraphic, IMyLevel gameLevel)
		{
			// base
			base.OnNextTurn(timeInMilliseconds, myGraphic, gameLevel);

			// Trajectory
			if (Trajectory != null)
				Trajectory.Move(ref MyPicture.PosSource);

			// is time to make Fire?
			if ((timeInMilliseconds - LastTimeWhenMadeFireInMilliseconds) < TimeToMakeFire)
				return;

			// can fire for unit?
			IMyUnit enemyUnit = gameLevel.Units.Find(item => CanFireOnThisUnit(item, myGraphic, gameLevel));

			// found unit to fire
			if (enemyUnit == null)
				return;

			// make fire
			NeedMakeFire(myGraphic, gameLevel);

			// set last time for fire
			LastTimeWhenMadeFireInMilliseconds = timeInMilliseconds;
		}
	}
}
