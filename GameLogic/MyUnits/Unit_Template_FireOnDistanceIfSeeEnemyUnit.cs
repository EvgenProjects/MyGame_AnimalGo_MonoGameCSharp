using MyGame;
using MyGame.interfaces;
using MyLevels.interfaces;
using MyUnits.interfaces;
using System;

namespace MyUnits
{
	class Unit_Template_FireOnDistanceIfSeeEnemyUnit : IUnit
	{
        public MyTexture2DAnimation MyTexture2DAnimation;
        public int Life { get; set; }
        public bool IsNeedDelete { get; set; }
        public int HandDamage { get; protected set; }
        public ITrajectory Trajectory { get; protected set; }
        public bool IsMyUnit { get; }

        public Func<MyPoint, IFire> DelegateMakeFire;
        protected long LastTimeWhenMadeFireInMilliseconds = 0;
		protected long TimeToMakeFire = 0;

		// constructor
		public Unit_Template_FireOnDistanceIfSeeEnemyUnit(IMyGraphic myGraphic, int xCenter, int yCenter, bool isMyUnit, int life, long timeToMakeFire, enImageType imageTypeWhenStay)
        {
            Life = life;
            HandDamage = 0;
            Trajectory = null;
            IsNeedDelete = false;
            TimeToMakeFire = timeToMakeFire;
			IsMyUnit = isMyUnit;

            IMyTexture2D myTexture2D = myGraphic.FindImage(imageTypeWhenStay);
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
            if (Life <= 0)
                IsNeedDelete = true;

            // check bounds
            if (!MyTexture2DAnimation.IsPictureInsideLevel(myGraphic, gameLevel.LevelLeft, gameLevel.LevelTop, gameLevel.LevelWidth, gameLevel.LevelHeight))
                IsNeedDelete = true;

            // is valid
            if (IsNeedDelete)
                return;

            // move
            if (bMove)
            {
                if (Trajectory != null)
                    Trajectory.Move(ref MyTexture2DAnimation.Position);

                // change sprite index
                MyTexture2DAnimation.ChangeSpriteIndex(timeInMilliseconds);
            }

			// is time to make Fire?
			if ((timeInMilliseconds - LastTimeWhenMadeFireInMilliseconds) < TimeToMakeFire)
				return;

            // can fire for unit?
            var unitsForFight = IsMyUnit ? gameLevel.EnemyUnits : gameLevel.MyUnits;
            IUnit enemyUnit = unitsForFight.Find(item => CanFireOnThisUnit(item, myGraphic, gameLevel));

			// found unit to fire
			if (enemyUnit == null)
				return;

			// make fire
			NeedMakeFire(myGraphic, gameLevel);

			// set last time for fire
			LastTimeWhenMadeFireInMilliseconds = timeInMilliseconds;
		}
	
		public virtual bool CanFireOnThisUnit(IUnit unit, IMyGraphic myGraphic, IMyLevel gameLevel)
		{
			// is same row
			if (gameLevel.GetRow(MyTexture2DAnimation.GetRectInScenaPoints(myGraphic)) == gameLevel.GetRow(unit.GetRectInScenaPoints(myGraphic)))
			{
				// has enemy unit on right
				if (gameLevel.GetCol(MyTexture2DAnimation.GetRectInScenaPoints(myGraphic)) <= gameLevel.GetCol(unit.GetRectInScenaPoints(myGraphic)))
					return true;
			}
			return false;
		}

		public virtual void NeedMakeFire(IMyGraphic myGraphic, IMyLevel gameLevel)
		{
			MyRectangle rect = MyTexture2DAnimation.GetRectInScenaPoints(myGraphic);

			if (DelegateMakeFire!=null)
			{
				IFire myFire = DelegateMakeFire(new MyPoint(rect.X + rect.Width / 2, rect.Y + rect.Height / 2));
                var fires = IsMyUnit ? gameLevel.MyFires : gameLevel.EnemyFires;
                fires.Add(myFire);
			}
		}
        
		public virtual bool OnClickMouse(int xMouse, int yMouse, IMyGraphic myGraphic)
        {
            return false;
        }

		public virtual void NeedMakeDamageToUnit(IUnit unit, IMyGraphic myGraphic, IMyLevel gameLevel)
		{
		}
	}
}
