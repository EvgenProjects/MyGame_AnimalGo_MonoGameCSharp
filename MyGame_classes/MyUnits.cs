// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	class MyUnit_DogGo : MyUnit_StopAndHitIfNearOtherUnit
	{
		public MyUnit_DogGo(IMyGraphic myGraphic, int playerID, int xCenterSource, int yCenterSource) :
			base(40 /*life*/, playerID /*playerID*/, 1500 /*1.5 second (time to make damage near)*/, (int)enImageType.Heroes_sobachka_run/*image Type when Damage*/, new MyPicture(null, xCenterSource, yCenterSource, enImageAlign.CenterX_CenterY))
		{
			MyPicture.ImageFile = myGraphic.FindImage((int)enImageType.Heroes_sobachka_run);
			Trajectory = new MyTrajectory(0.7f/*x step*/, 0.0f/*y step*/);
			WeaponInfo = new MyWeaponInfo(20/*damage*/);
		}
	}

	class MyUnit_DogMarshalGo : MyUnit_StopAndHitIfNearOtherUnit
	{
		public MyUnit_DogMarshalGo(IMyGraphic myGraphic, int playerID, int xCenterSource, int yCenterSource) :
			base(40 /*life*/, playerID /*playerID*/, 1500 /*1.5 second (time to make damage near)*/, (int)enImageType.Heroes_marshal/*image Type when Damage*/, new MyPicture(null, xCenterSource, yCenterSource, enImageAlign.CenterX_CenterY))
		{
			MyPicture.ImageFile = myGraphic.FindImage((int)enImageType.Heroes_marshal);
			Trajectory = new MyTrajectory(0.7f/*x step*/, 0.0f/*y step*/);
			WeaponInfo = new MyWeaponInfo(20/*damage*/);
		}
	}

	class MyUnit_DogZumaGo : MyUnit_StopAndHitIfNearOtherUnit
	{
		public MyUnit_DogZumaGo(IMyGraphic myGraphic, int playerID, int xCenterSource, int yCenterSource) :
			base(40 /*life*/, playerID /*playerID*/, 1500 /*1.5 second (time to make damage near)*/, (int)enImageType.Heroes_zuma/*image Type when Damage*/, new MyPicture(null, xCenterSource, yCenterSource, enImageAlign.CenterX_CenterY))
		{
			MyPicture.ImageFile = myGraphic.FindImage((int)enImageType.Heroes_zuma);
			Trajectory = new MyTrajectory(0.7f/*x step*/, 0.0f/*y step*/);
			WeaponInfo = new MyWeaponInfo(20/*damage*/);
		}
	}

	class MyUnit_EnemyPauk : MyUnit_StopAndHitIfNearOtherUnit
	{
		public MyUnit_EnemyPauk(IMyGraphic myGraphic, int playerID, int xCenterSource, int yCenterSource) :
			base(400 /*life*/, playerID /*playerID*/, 1500 /*1.5 second (time to make damage near)*/, (int)enImageType.Heroes_spider_hit/*image Type when Damage*/, new MyPicture(null, xCenterSource, yCenterSource, enImageAlign.CenterX_CenterY))
		{
			MyPicture.ImageFile = myGraphic.FindImage((int)enImageType.Heroes_spider_go);
			Trajectory = new MyTrajectory(-0.2f/*x step*/, 0.0f/*y step*/);
			WeaponInfo = new MyWeaponInfo(10/*damage*/);
		}
	}

	class MyUnit_EnemyZmeia : MyUnit_StopAndHitIfNearOtherUnit
	{
		public MyUnit_EnemyZmeia(IMyGraphic myGraphic, int playerID, int xCenterSource, int yCenterSource) :
			base(40 /*life*/, playerID /*playerID*/, 1500 /*1.5 second (time to make damage near)*/, (int)enImageType.Heroes_zmeia_zalit/*image Type when Damage*/, new MyPicture(null, xCenterSource, yCenterSource, enImageAlign.CenterX_CenterY))
		{
			MyPicture.ImageFile = myGraphic.FindImage((int)enImageType.Heroes_zmeia_go);
			Trajectory = new MyTrajectory(-0.7f/*x step*/, 0.0f/*y step*/);
			WeaponInfo = new MyWeaponInfo(20/*damage*/);
		}
	}

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