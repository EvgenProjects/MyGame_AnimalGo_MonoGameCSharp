// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;
using MyGame_classes;

namespace MyGame_classes
{
	class MyUnit_StopAndHitIfNearOtherUnit : MyUnit
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
			if (Trajectory != null && CollisionWithUnit == null)
				Trajectory.Move(ref MyPicture.PosSource);
		}

		public override void OnNextTurn(long timeInMilliseconds, IMyGraphic myGraphic, IMyLevel gameLevel)
		{
			// base
			base.OnNextTurn(timeInMilliseconds, myGraphic, gameLevel);

			// check is valid 
			if (CollisionWithUnit != null)
			{
				IMyUnit unitFindCollision = gameLevel.Units.Find(unit => unit == CollisionWithUnit);
				if (unitFindCollision == null)
					CollisionWithUnit = null;
			}

			// move
			MoveByTrajectory();

			// get Rect
			MyRectangle rectSource = MyPicture.GetSourceRect();

			// find collision with my Unit
			IMyUnit unitFind = gameLevel.Units.Find(item =>
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

			// collision with unit
			if (unitFind != null)
			{
				if (TimeToMakeDamageNear != 0 && timeInMilliseconds > (LastTimeWhenMakeDamageInMilliseconds + TimeToMakeDamageNear))
				{
					// last time
					LastTimeWhenMakeDamageInMilliseconds = timeInMilliseconds;

					// collision
					CollisionWithUnit = unitFind;

					// other unit
					MyUnit_StopAndHitIfNearOtherUnit otherUnit = (unitFind as MyUnit_StopAndHitIfNearOtherUnit);
					if (otherUnit != null)
						otherUnit.CollisionWithUnit = this;

					// near damage
					NeedMakeDamageToUnit(unitFind, myGraphic, gameLevel);
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
