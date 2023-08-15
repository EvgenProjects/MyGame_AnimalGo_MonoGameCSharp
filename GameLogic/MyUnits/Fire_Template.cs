using Microsoft.Xna.Framework.Graphics;
using MyGame;
using MyGame.interfaces;
using MyLevels.interfaces;
using MyUnits.interfaces;

namespace MyUnits
{
	class Fire_Template : IFire
	{
		public readonly int TimeAnimationInMilliseconds = 300 /* 0.3 second*/;

		protected enImageType ImageTypeWhenDamage { get; set; }
        protected enImageType ImageTypeWhenMove { get; set; }

        public bool IsNeedDelete { get; set; }

        public int Damage { get; protected set; }

        // multiplayer
        public int PlayerID { get; protected set; }

		// image
		protected MyTexture2DAnimation MyTexture2DAnimation;

		// trajectory
		public ITrajectory Trajectory { get; protected set; }

		// weapon info
		public Fire_Template(IMyGraphic myGraphic, int xCenter, int yCenter, ITrajectory myTrajectory, int damage, int playerID, enImageType imageTypeWhenMove, enImageType imageTypeWhenDamage)
		{
            ImageTypeWhenMove = imageTypeWhenMove;
            ImageTypeWhenDamage = imageTypeWhenDamage;
			Damage = damage;
			IsNeedDelete = false;
			PlayerID = playerID;
			Trajectory = myTrajectory;

            IMyTexture2D myTexture2D = myGraphic.FindImage(enImageType.Fire_morkovka);
            MyTexture2DAnimation = new MyTexture2DAnimation(myTexture2D, xCenter, yCenter);
        }

        // events
        public virtual void OnDraw(IMyGraphic myGraphic)
		{
            MyTexture2DAnimation.OnDraw(myGraphic);
		}

		public virtual void FireMakingDamage(IUnit unit, IMyGraphic myGraphic, IMyLevel gameLevel)
		{
			// do damage
			unit.Life -= Damage;

			// animation
			if (ImageTypeWhenDamage != enImageType.Unknown)
			{
				MyRectangle rect = MyTexture2DAnimation.GetRectInScenaPoints(myGraphic);

				gameLevel.AnimPicturesDieByTime.Add(
									new AnimPictureDieByTime_Template(
										TimeAnimationInMilliseconds,
										new MyTexture2DAnimation(
											myGraphic.FindImage(ImageTypeWhenDamage),
											rect.X + rect.Width / 2,
											rect.Y + rect.Height / 2
										)
									)
								);
			}
		}

		public virtual void OnNextTurn(long timeInMilliseconds, IMyGraphic myGraphic, IMyLevel gameLevel)
		{
			// check bounds
			if (!MyTexture2DAnimation.IsPictureInsideLevel(myGraphic, gameLevel.LevelLeft, gameLevel.LevelTop, gameLevel.LevelWidth, gameLevel.LevelHeight))
				IsNeedDelete = true;

			// is valid
			if (IsNeedDelete)
				return;

			// Trajectory
			if (Trajectory != null)
				Trajectory.Move(ref MyTexture2DAnimation.Position);

			// get Rect
			MyRectangle rect = MyTexture2DAnimation.GetRectInScenaPoints(myGraphic);

			// find collision Fire & Unit
			IUnit unit = gameLevel.Units.Find(item =>
			{
				if (item.GetRectInScenaPoints(myGraphic).IntersectsWith(rect))
				{
					if (!gameLevel.IsTeam(PlayerID /*this player ID*/, item.PlayerID /*unit player ID*/))
						return true;
				}
				return false;
			});

			// fire damage on Unit
			if (unit != null)
			{
				// delete
				IsNeedDelete = true;

				FireMakingDamage(unit, myGraphic, gameLevel);
			}
		}
	}
}
