// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	abstract class MyUnitAbstract : IMyUnit
	{
		// picture
		public MyPicture MyPicture;

		// multiplayer
		public virtual int PlayerID { get; protected set; }

		// life
		public int Life { get; set; }

		// delete flag
		public bool IsNeedDelete { get; set; }

        public int HandDamage { get; protected set; }
        
		// trajectory
        public IMyTrajectory Trajectory { get; protected set; }

		public MyUnitAbstract(int handDamage, int life, int playerID, MyPicture myPicture)
		{
			MyPicture = myPicture;
			PlayerID = playerID;
			Life = life;
            HandDamage = handDamage;

            // trajectory
            Trajectory = null;

			// delete
			IsNeedDelete = false;
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

		public virtual void MoveByTrajectory()
		{
			// Trajectory
			if (Trajectory != null)
				Trajectory.Move(ref MyPicture.PosSource);
		}

		public virtual void OnNextTurn(long timeInMilliseconds, IMyGraphic myGraphic, IMyLevel gameLevel, bool bMove)
		{
			// delete unit
			if (Life <= 0)
				IsNeedDelete = true;

			// check bounds
			if (!MyPicture.IsPictureInsideLevel(gameLevel.LevelLeft, gameLevel.LevelTop, gameLevel.LevelWidth, gameLevel.LevelHeight))
				IsNeedDelete = true;

			// is valid
			if (IsNeedDelete)
				return;

			// move
			if (bMove)
			{
				MoveByTrajectory();

				// change sprite index
				MyPicture.ChangeSpriteIndex(timeInMilliseconds);
			}
		}

		public virtual bool OnClickMouse(int xMouse, int yMouse, IMyGraphic myGraphic)
		{
			return false;
		}

		public virtual void NeedMakeDamageToUnit(IMyUnit unit, IMyGraphic myGraphic, IMyLevel gameLevel)
		{
		}

		public virtual void NeedMakeFire(IMyGraphic myGraphic, IMyLevel gameLevel)
		{
		}
	}
}
