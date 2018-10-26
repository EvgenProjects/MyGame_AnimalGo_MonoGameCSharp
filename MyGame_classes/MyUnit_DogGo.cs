// my namespaces
using MyGraphic_interfaces;
using MyGame_classes;

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
}
