// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;
using MyGame_classes;

namespace MyGame_classes
{
	class MyUnit_StopAndHitIfNearOtherUnit : MyUnitAbstract
	{
		// weapon info
		public IMyWeaponInfo WeaponInfo { get; protected set; }

		// time to hit
		protected long LastTimeWhenMakeDamageInMilliseconds = 0;
		protected long TimeToMakeDamageNear = 0;

		// image id when hit
		protected int ImageTypeWhenDamage = 0;

		// collision
		public IMyUnit CollisionWithUnit { get; protected set; }

		// constructor
		public MyUnit_StopAndHitIfNearOtherUnit(int life, int playerID, long timeToMakeDamageNear, int imageTypeWhenDamage, MyPicture myPicture) :
			base(life, playerID, myPicture)
		{
			CollisionWithUnit = null;

			// Damage
			TimeToMakeDamageNear = timeToMakeDamageNear;
			ImageTypeWhenDamage = imageTypeWhenDamage;
		}

		public override void MoveByTrajectory()
		{
			// Trajectory
			if (Trajectory != null)
				Trajectory.Move(ref MyPicture.PosSource);
		}

		public override void OnNextTurn(long timeInMilliseconds, IMyGraphic myGraphic, IMyLevel gameLevel, bool bMove)
		{
			// find collision by Enemy
			IMyUnit findCollisionWithUnit = gameLevel.Units.Find(unit =>
			{
				MyUnit_StopAndHitIfNearOtherUnit unitTemp = (unit as MyUnit_StopAndHitIfNearOtherUnit);
				if (unitTemp != null)
				{
					return unitTemp.CollisionWithUnit==this;
				}
				return false;
			});

			// find collision by Enemy rect
			if (findCollisionWithUnit == null)
			{
				// get Rect
				MyRectangle rectSource = MyPicture.GetSourceRect();

				// find collision with my Unit
				findCollisionWithUnit = gameLevel.Units.Find(item =>
				{
					// not team
					if (!gameLevel.IsTeam(PlayerID, item.PlayerID))
					{
						//is intersect
						if (item.GetSourceRect().IntersectsWith(rectSource))
							return true;
					}
					return false;
				});
			}

			// set
			CollisionWithUnit = findCollisionWithUnit;

			// base
			bool bNeedMove = CollisionWithUnit == null;
			base.OnNextTurn(timeInMilliseconds, myGraphic, gameLevel, bNeedMove);

			// make damage
			if (CollisionWithUnit != null)
			{
				if (TimeToMakeDamageNear != 0 && timeInMilliseconds > (LastTimeWhenMakeDamageInMilliseconds + TimeToMakeDamageNear))
				{
					// last time
					LastTimeWhenMakeDamageInMilliseconds = timeInMilliseconds;

					// damage
					NeedMakeDamageToUnit(CollisionWithUnit, myGraphic, gameLevel);
				}
			}
		}

		public override void NeedMakeDamageToUnit(IMyUnit unit, IMyGraphic myGraphic, IMyLevel gameLevel)
		{
			// base
			base.NeedMakeDamageToUnit(unit, myGraphic, gameLevel);

			// do damage
			unit.Life -= WeaponInfo.Damage;

			// animation
			if (ImageTypeWhenDamage != 0)
			{
				MyRectangle rectSource = MyPicture.GetSourceRect();
				gameLevel.Animations.Add(
									new MyAnimation(
										300 /* 0.3 second*/,
										new MyPicture(
											myGraphic.FindImage(ImageTypeWhenDamage),
											rectSource.X + rectSource.Width / 2,
											rectSource.Y + rectSource.Height / 2,
											enImageAlign.CenterX_CenterY
										)
									)
								);
			}
		}
	}
}
