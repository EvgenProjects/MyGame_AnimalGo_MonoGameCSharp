using MyDialogs;
using MyGame.interfaces;
using MyLevels.interfaces;

namespace MyGame
{
	public class MyGame
	{
		// graphic
		public IMyGraphic MyGraphic { get; protected set; }

		// levels
		private readonly IMyLevel[] _gameLevels;
		public IMyLevel[] Levels { get { return _gameLevels; } }
		public int CurrentLevelIndex { get; protected set; }

		// time
		public virtual int GetTimeStepInMilliseconds() { return 20; } // 20 miliseconds

		// constructor
		public MyGame(IMyGraphic myGraphic)
		{
            MyGraphic = myGraphic;
		
			// set levels
			_gameLevels = this.GetLevels();
		}

		// methods
		public virtual void OnExit()
		{
		}

		public virtual void OnInit()
		{
			this.LoadImages(MyGraphic);

			// open dialog
			//new MyDialogStartGame()).ClickStart(MyGraphic, this);
			LoadFirstLevel();
		}

		public virtual void LoadFirstLevel()
		{
			CurrentLevelIndex = 0;

			// get cur level
			IMyLevel myLevel = GetCurLevel();

			// load
			if (myLevel != null)
				myLevel.OnLoad(MyGraphic);
		}

		public virtual void LoadNextLevel()
		{
			CurrentLevelIndex++;

			// get cur level
			IMyLevel myLevel = GetCurLevel();

			// load
			if (myLevel!=null)
				myLevel.OnLoad(MyGraphic);
		}

		public virtual IMyLevel GetCurLevel()
		{
			// check
			if (Levels == null || CurrentLevelIndex < 0 || CurrentLevelIndex >= Levels.Length)
				return null;

			// result
			return Levels[CurrentLevelIndex];
		}

		public virtual void OnNextTurn(long timeInMilliseconds)
		{
			// get cur level
			IMyLevel myLevel = GetCurLevel();

			if (myLevel != null)
			{
				// next turn
				myLevel.OnNextTurn(timeInMilliseconds, MyGraphic);

				// is level end
				if (myLevel.IsLevelEnd())
				{
					// open dialog
					//OpenDialog(new MyDialogLevelNext(MyGraphic));
				}
			}
		}

		public virtual void OnChangeWindowSize(int screenWidth, int screenHeight)
		{
            MyGraphic.ScreenWidth = screenWidth;
            MyGraphic.ScreenHeight = screenHeight;

            // calculate stratch
            MyGraphic.XStretchCoef = (float)MyGraphic.ScreenWidth / Levels[CurrentLevelIndex].LevelWidth;
            MyGraphic.YStretchCoef = (float)MyGraphic.ScreenHeight / Levels[CurrentLevelIndex].LevelHeight;
		}

		public virtual void OnDraw()
		{
			// get cur level
			IMyLevel myLevel = GetCurLevel();

			// draw
			if (myLevel != null)
				myLevel.OnDraw(MyGraphic);
		}

		public virtual bool OnClickMouse(int xMouse, int yMouse)
		{
			// get cur level
			IMyLevel myLevel = GetCurLevel();

            MyPointF ptMouseInScenaPoints = MyGraphic.ConvertMousePtToScenaPoints(xMouse, yMouse);

            // click mouse
            if (myLevel != null && myLevel.OnClickMouse((int)ptMouseInScenaPoints.X, (int)ptMouseInScenaPoints.Y, MyGraphic))
			{
				return true;
			}
			return false;
		}
	}
}
