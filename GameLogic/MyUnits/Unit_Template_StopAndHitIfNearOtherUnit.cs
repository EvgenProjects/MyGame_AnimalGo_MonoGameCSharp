using MyGame;
using MyGame.interfaces;
using MyLevels.interfaces;
using MyUnits.interfaces;

namespace MyUnits
{
	class Unit_Template_StopAndHitIfNearOtherUnit : IUnit
	{
        public MyTexture2DAnimation MyTexture2DAnimation;
        public int Life { get; set; }
        public bool IsNeedDelete { get; set; }
        public int HandDamage { get; protected set; }
        public ITrajectory Trajectory { get; protected set; }
		public bool IsMyUnit { get; }

        protected long LastTimeWhenMakeDamageInMilliseconds = 0;
		protected long TimeToMakeDamageNear = 0;
		protected enImageType ImageTypeWhenDamage = 0;

		public IUnit CollisionWithUnit { get; protected set; }

        // constructor
        public Unit_Template_StopAndHitIfNearOtherUnit(IMyGraphic myGraphic, int xCenter, int yCenter, bool isMyUnit, int life, int handDamage, long timeToMakeDamageNear, enImageType imageTypeWhenMove, enImageType imageTypeWhenDamage)
        {
            CollisionWithUnit = null;
            Life = life;
            HandDamage = handDamage;
            Trajectory = null;
            IsNeedDelete = false;
            TimeToMakeDamageNear = timeToMakeDamageNear;
            ImageTypeWhenDamage = imageTypeWhenDamage;
			IsMyUnit = isMyUnit;

            IMyTexture2D myTexture2D = myGraphic.FindImage(imageTypeWhenMove);
            MyTexture2DAnimation = new MyTexture2DAnimation(myTexture2D, xCenter, yCenter);
        }

        public virtual void OnDraw(IMyGraphic myGraphic)
        {
            MyTexture2DAnimation.OnDraw(myGraphic);
        }

        public virtual MyRectangle GetRectInScenaPoints(IMyGraphic myGraphic)
        {
            return MyTexture2DAnimation.GetRectInScenaPoints(myGraphic);
        }

		public virtual void OnNextTurn(long timeInMilliseconds, IMyGraphic myGraphic, IMyLevel gameLevel, bool bMove)
		{
			var unitsForFight = IsMyUnit ? gameLevel.EnemyUnits : gameLevel.MyUnits;

            // find collision with Enemy
            IUnit findCollisionWithUnit = unitsForFight.Find(unit =>
			{
                Unit_Template_StopAndHitIfNearOtherUnit unitTemp = (unit as Unit_Template_StopAndHitIfNearOtherUnit);
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
				MyRectangle rect = MyTexture2DAnimation.GetRectInScenaPoints(myGraphic);

				// find collision with my Unit
				findCollisionWithUnit = unitsForFight.Find(item =>
				{
					//is intersect
					if (item.GetRectInScenaPoints(myGraphic).IntersectsWith(rect))
						return true;
					return false;
				});
			}

			// set
			CollisionWithUnit = findCollisionWithUnit;
            if (Life <= 0)
                IsNeedDelete = true;

            // check bounds
            if (!MyTexture2DAnimation.IsPictureInsideLevel(myGraphic, gameLevel.LevelLeft, gameLevel.LevelTop, gameLevel.LevelWidth, gameLevel.LevelHeight))
                IsNeedDelete = true;

            // is valid
            if (IsNeedDelete)
                return;

            // move
            if (CollisionWithUnit == null)
            {
                if (Trajectory != null)
                    Trajectory.Move(ref MyTexture2DAnimation.Position);

                // change sprite index
                MyTexture2DAnimation.ChangeSpriteIndex(timeInMilliseconds);
            }

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

		public virtual void NeedMakeDamageToUnit(IUnit unit, IMyGraphic myGraphic, IMyLevel gameLevel)
		{
			// do damage
			unit.Life -= HandDamage;

			// animation
			if (ImageTypeWhenDamage != 0)
			{
				MyRectangle rect = MyTexture2DAnimation.GetRectInScenaPoints(myGraphic);
				gameLevel.AnimPicturesDieByTime.Add(
									new AnimPictureDieByTime_Template(
										300 /* 0.3 second*/,
										new MyTexture2DAnimation(
											myGraphic.FindImage(ImageTypeWhenDamage),
                                            rect.X + rect.Width / 2,
                                            rect.Y + rect.Height / 2
										)
									)
								);
			}
        }

		public virtual void NeedMakeFire(IMyGraphic myGraphic, IMyLevel gameLevel)
		{
		}

		public virtual bool OnClickMouse(int xMouse, int yMouse, IMyGraphic myGraphic)
		{
			return false;
		}
	}
}
