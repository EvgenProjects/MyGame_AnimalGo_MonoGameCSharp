using MyGame.interfaces;
using MyLevels;
using System.Threading;

namespace MyGame
{
    enum enLevelType
    {
        Intro = 0,
        LevelStart = 1,
        LevelEnd = 1000,
    };
    
	public class MyGame
	{
        public readonly int TimeStepInMilliseconds = 20;
		private readonly IMyGraphic _myGraphic;
        private enLevelType _levelType;
        private MyLevelIntro _myLevelIntro;
        private MyLevel _myLevel;

        // constructor
        public MyGame(IMyGraphic myGraphic)
		{
            _myGraphic = myGraphic;
			_myLevel = new MyLevel();
            _myLevelIntro = new MyLevelIntro();
		}

        public virtual void OnInit()
        {
            MySettings.LoadImages(_myGraphic);
            LoadLevelIntro();
        }
        
		public virtual void OnExit()
		{
		}

        void LoadLevelIntro()
        {
            _levelType = enLevelType.Intro;
            _myLevelIntro.OnLoad(_myGraphic);
        }
        
        public virtual void LoadFirstLevel()
		{
            _levelType = enLevelType.LevelStart;
            _myLevel.OnLoad(_myGraphic, (int)_levelType);
        }

        public virtual void LoadNextLevel()
		{
            _levelType++;
            _myLevel.OnLoad(_myGraphic, (int)_levelType);
        }

        public virtual void OnChangeWindowSize(int screenWidth, int screenHeight)
		{
            _myGraphic.ScreenWidth = screenWidth;
            _myGraphic.ScreenHeight = screenHeight;

            int levelWidth = 1;
            int levelHeight = 1;
            if (_levelType == enLevelType.Intro)
            {
                levelWidth = _myLevelIntro.LevelWidth;
                levelHeight = _myLevelIntro.LevelHeight;
            }
            else if (_levelType >= enLevelType.LevelStart && _levelType <= enLevelType.LevelEnd)
            {
                levelWidth = _myLevel.LevelWidth;
                levelHeight = _myLevel.LevelHeight;
            }

            // calculate
            _myGraphic.XStretchCoef = (float)_myGraphic.ScreenWidth / levelWidth;
            _myGraphic.YStretchCoef = (float)_myGraphic.ScreenHeight / levelHeight;
		}

        public virtual void OnNextTurn(long timeInMilliseconds)
        {
            if (_levelType == enLevelType.Intro)
                _myLevelIntro.OnNextTurn(timeInMilliseconds, _myGraphic);
            else if (_levelType >= enLevelType.LevelStart && _levelType <= enLevelType.LevelEnd)
                _myLevel.OnNextTurn(timeInMilliseconds, _myGraphic);
        }
        
        public virtual void OnDraw()
		{
            if (_levelType == enLevelType.Intro)
                _myLevelIntro.OnDraw(_myGraphic);
            else if (_levelType >= enLevelType.LevelStart && _levelType <= enLevelType.LevelEnd)
                _myLevel.OnDraw(_myGraphic);
        }

        public virtual void OnTouch(int xTouch, int yTouch)
        {
            MyPointF ptMouseInScenaPoints = _myGraphic.ConvertMousePtToScenaPoints(xTouch, yTouch);

            if (_levelType == enLevelType.Intro)
            {
                if (_myLevelIntro.OnTouch(_myGraphic, (int)ptMouseInScenaPoints.X, (int)ptMouseInScenaPoints.Y) == enLevelIntroTouch.LoadFirstLevel)
                    LoadFirstLevel();
            }

            else if (_levelType >= enLevelType.LevelStart && _levelType <= enLevelType.LevelEnd)
            {
                enLevelTouch levelTouch = _myLevel.OnTouch(_myGraphic, (int)ptMouseInScenaPoints.X, (int)ptMouseInScenaPoints.Y);
                if (levelTouch == enLevelTouch.LoadNextLevel)
                    LoadNextLevel();
                else if (levelTouch == enLevelTouch.LoadLevelIntro)
                    LoadLevelIntro();
            }
		}
	}
}