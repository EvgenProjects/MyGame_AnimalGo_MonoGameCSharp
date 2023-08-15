// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	class MyFire : IMyFire
	{
		public readonly int TimeAnimationInMilliseconds = 300 /* 0.3 second*/;

		public enImageType ImageTypeWhenDamage { get; protected set; }

		public bool IsNeedDelete { get; set; }

        public int Damage { get; protected set; }

        // multiplayer
        public int PlayerID { get; protected set; }

		// image
		protected MyPicture MyPicture;

		// trajectory
		public IMyTrajectory Trajectory { get; protected set; }

		// weapon info
		public MyFire(int damage, IMyTrajectory myTrajectory, int playerID, MyPicture myPicture, enImageType imageTypeWhenDamage)
		{
			ImageTypeWhenDamage = imageTypeWhenDamage;
			Damage = damage;
			IsNeedDelete = false;
			PlayerID = playerID;
			MyPicture = myPicture;
			Trajectory = myTrajectory;
		}

		// events
		public virtual void OnDraw(object context, IMyGraphic myGraphic)
		{
			MyPicture.OnDraw(context, myGraphic);
		}

		public virtual MyRectangle GetSourceRect()
		{
			return MyPicture.GetSourceRect();
		}

		public virtual MyRectangle GetDrawRect()
		{
			return MyPicture.GetDrawRect();
		}

		public virtual void FireMakingDamage(IMyUnit unit, IMyGraphic myGraphic, IMyLevel gameLevel)
		{
			// do damage
			unit.Life -= Damage;

			// animation
			if (ImageTypeWhenDamage != enImageType.Unknown)
			{
				MyRectangle rectSource = MyPicture.GetSourceRect();

				gameLevel.Animations.Add(
									new MyAnimation(
										TimeAnimationInMilliseconds,
										new MyPicture(
											myGraphic.FindImage((int)ImageTypeWhenDamage),
											rectSource.X + rectSource.Width / 2,
											rectSource.Y + rectSource.Height / 2,
											enImageAlign.CenterX_CenterY
										)
									)
								);
			}
		}

		public virtual void OnNextTurn(long timeInMilliseconds, IMyGraphic myGraphic, IMyLevel gameLevel)
		{
			// check bounds
			if (!MyPicture.IsPictureInsideLevel(gameLevel.LevelLeft, gameLevel.LevelTop, gameLevel.LevelWidth, gameLevel.LevelHeight))
				IsNeedDelete = true;

			// is valid
			if (IsNeedDelete)
				return;

			// Trajectory
			if (Trajectory != null)
				Trajectory.Move(ref MyPicture.PosSource);

			// get Rect
			MyRectangle rectSource = MyPicture.GetSourceRect();

			// find collision Fire & Unit
			IMyUnit unit = gameLevel.Units.Find(item =>
			{
				if (item.GetSourceRect().IntersectsWith(rectSource))
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
