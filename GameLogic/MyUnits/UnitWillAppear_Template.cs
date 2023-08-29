using MyGame;
using MyGame.interfaces;
using MyLevels;
using MyLevels.interfaces;
using MyUnits.interfaces;
using System;

namespace MyUnits
{
	class UnitWillAppear_Template : IUnitWillAppear
	{
		public long TimeWhenAppearInMilliseconds = 0;
		public bool IsNeedDelete { get; protected set; }

        private long TimeCreatedInMilliseconds = 0;
        private int _row = 0;
        private Func<IMyGraphic, int, int, IUnit> _funcCreateUnit;

        // constructor
        public UnitWillAppear_Template(int timeWhenAppearInMilliseconds, int row, Func<IMyGraphic, int, int, IUnit> funcCreateUnit)
		{
			TimeWhenAppearInMilliseconds = timeWhenAppearInMilliseconds;
            _funcCreateUnit = funcCreateUnit;
            _row = row;
        }

        public virtual void OnNextTurn(long timeInMilliseconds, IMyGraphic myGraphic, IMyLevel gameLevel)
		{
			if (TimeCreatedInMilliseconds == 0)
				TimeCreatedInMilliseconds = timeInMilliseconds;

			// check
			if (IsNeedDelete)
				return;

			// is elipsed
			if (timeInMilliseconds < (TimeCreatedInMilliseconds + TimeWhenAppearInMilliseconds))
				return;

            // Create Unit from UnitWillAppear
            int xAppear = gameLevel.GetXCenterItemByColNumber(gameLevel.LevelDetails.ColsCount+1);
            int yAppear = gameLevel.GetYCenterItemByRowNumber(_row);
            IUnit unit = _funcCreateUnit(myGraphic, xAppear, yAppear);
			gameLevel.EnemyUnits.Add(unit);

            // delete ObjectToAppear
            IsNeedDelete = true;
		}
	}
}