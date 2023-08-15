// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	class MyUnit_FireOnDistanceIfSeeEnemyUnit : MyUnitAbstract
	{
		public delegate IMyFire FireFactory(MyPoint posSourceCenter);

		// fire create
		public FireFactory DelegateMakeFire;

		// Fire preiod
		protected long LastTimeWhenMadeFireInMilliseconds = 0;
		protected long TimeToMakeFire = 0;

		// constructor
		public MyUnit_FireOnDistanceIfSeeEnemyUnit(int life, int playerID, long timeToMakeFire, MyPicture myPicture)
			: base(0 /*hand damage*/,life, playerID, myPicture)
		{
			// Damage
			TimeToMakeFire = timeToMakeFire;
		}

		public override void OnNextTurn(long timeInMilliseconds, IMyGraphic myGraphic, IMyLevel gameLevel, bool bMove)
		{
			// base
			base.OnNextTurn(timeInMilliseconds, myGraphic, gameLevel, bMove);

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
	
		public virtual bool CanFireOnThisUnit(IMyUnit unit, IMyGraphic myGraphic, IMyLevel gameLevel)
		{
			// is team
			if (gameLevel.IsTeam(PlayerID, unit.PlayerID))
				return false;

			// get my Level Play
			MyLevelAbstract myLevelPlayAbstract = gameLevel as MyLevelAbstract;

			// is same row
			if (myLevelPlayAbstract.GetRow(MyPicture.GetSourceRect()) == myLevelPlayAbstract.GetRow((unit as MyUnitAbstract).MyPicture.GetSourceRect()))
			{
				// has enemy unit on right
				if (myLevelPlayAbstract.GetCol(MyPicture.GetSourceRect()) <= myLevelPlayAbstract.GetCol((unit as MyUnitAbstract).MyPicture.GetSourceRect()))
					return true;
			}
			return false;
		}

		public override void NeedMakeFire(IMyGraphic myGraphic, IMyLevel gameLevel)
		{
			MyRectangle rectSource = MyPicture.GetSourceRect();

			if (DelegateMakeFire!=null)
			{
				IMyFire myFire = DelegateMakeFire(new MyPoint(rectSource.X + rectSource.Width / 2, rectSource.Y + rectSource.Height / 2));
				gameLevel.Fires.Add(myFire);
			}
		}
	}
}
