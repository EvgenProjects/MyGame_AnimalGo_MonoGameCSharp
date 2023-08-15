using MyGame;
using MyGame.interfaces;
using MyLevels.interfaces;
using MyUnits.interfaces;

namespace MyUnits
{
	class UnitWillAppear_Template : IUnitWillAppear
	{
		public long TimeWhenAppearInMilliseconds = 0;
		public bool IsNeedDelete { get; protected set; }

        private long TimeCreatedInMilliseconds = 0;
        private MyPointF _position;
        private int _playerID;
        private enImageType _imageType;

        // constructor
        public UnitWillAppear_Template(int timeWhenAppearInMilliseconds, int xPosition, int yPosition, int playerID, enImageType imageType)
		{
			TimeWhenAppearInMilliseconds = timeWhenAppearInMilliseconds;
            _position = new MyPointF(xPosition, yPosition);
			_playerID = playerID;
            _imageType = imageType;
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
			if (_imageType != enImageType.Unknown)
			{
				MySettings.CreateEnemyUnit(gameLevel, myGraphic, (int)_position.X, (int)_position.Y, _playerID, _imageType);
			}

			// clear
			_imageType = enImageType.Unknown;

            // delete ObjectToAppear
            IsNeedDelete = true;
		}
	}
}
