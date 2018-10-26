// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	class MyFire_Dog : MyFire
	{
		public MyFire_Dog(IMyGraphic myGraphic, int playerID, int xCenterSource, int yCenterSource)
			: base(10 /*damage*/, playerID, new MyPicture(null, xCenterSource, yCenterSource, enImageAlign.CenterX_CenterY))
		{
			MyPicture.ImageFile = myGraphic.FindImage((int)enImageType.Fire_morkovka);
			Trajectory = new MyTrajectory(4.7f/*x step*/, 0.0f/*y step*/);
		}

		public override void FireMakingDamage(IMyUnit unit, IMyGraphic myGraphic, IMyLevel gameLevel)
		{
			// do damage
			unit.Life -= WeaponInfo.Damage;

			// animation
			MyRectangle rectSource = MyPicture.GetSourceRect();
			gameLevel.Animations.Add(
									new MyAnimation(
										300 /* 0.3 second*/,
										new MyPicture(
											myGraphic.FindImage((int)enImageType.Fire_morkovka_splash),
											rectSource.X + rectSource.Width / 2,
											rectSource.Y + rectSource.Height / 2,
											enImageAlign.CenterX_CenterY
										)
									)
								);
		}
	}
}