// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	class MyUnit_KrolikStayAndFire : MyUnit_FireOnDistanceIfSeeEnemyUnit
	{
		// constructor
		public MyUnit_KrolikStayAndFire(IMyGraphic myGraphic, int playerID, int xCenterSource, int yCenterSource)
			: base(30 /*life*/, playerID /*playerID*/, 1500 /*1.5 second (time to make damage near)*/, new MyPicture(null, xCenterSource, yCenterSource, enImageAlign.CenterX_CenterY))
		{
			MyPicture.ImageFile = myGraphic.FindImage((int)enImageType.Heroes_krolik_s_lukom);
			Trajectory = new MyTrajectory(0.0f/*x step*/, 0.0f/*y step*/);
		}

		public override bool CanFireOnThisUnit(IMyUnit unit, IMyGraphic myGraphic, IMyLevel gameLevel)
		{
			// is team
			if (gameLevel.IsTeam(PlayerID, unit.PlayerID))
				return false;

			// is same row
			if ((gameLevel as MyLevelImpl).GetRow(MyPicture.GetSourceRect()) == (gameLevel as MyLevelImpl).GetRow((unit as MyUnit).MyPicture.GetSourceRect()))
			{
				// has enemy unit on right
				if ((gameLevel as MyLevelImpl).GetCol(MyPicture.GetSourceRect()) <= (gameLevel as MyLevelImpl).GetCol((unit as MyUnit).MyPicture.GetSourceRect()))
					return true;
			}
			return false;
		}

		public override void NeedMakeFire(IMyGraphic myGraphic, IMyLevel gameLevel)
		{
			// base
			base.NeedMakeFire(myGraphic, gameLevel);

			MyRectangle rectSource = MyPicture.GetSourceRect();
			gameLevel.Fires.Add(new MyFire_Dog(myGraphic,
								PlayerID,
								rectSource.X + rectSource.Width / 2,
								rectSource.Y + rectSource.Height / 2
			));
		}
	}
}
