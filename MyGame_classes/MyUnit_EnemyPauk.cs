// my namespaces
using MyGraphic_interfaces;
using MyGame_classes;

namespace MyGame_classes
{
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
}
