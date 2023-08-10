// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	public class MyGame : IMyGame
	{
		// graphic
		public IMyGraphic Graphic { get; protected set; }

		// levels
		private readonly IMyLevel[] _gameLevels;
		public IMyLevel[] Levels { get { return _gameLevels; } }
		public int CurrentLevelIndex { get; protected set; }

		// dialog
		public IMyDialog Dialog { get; set; }

		// time
		public virtual int GetTimeStepInMilliseconds() { return 20; } // 20 miliseconds

		// constructor
		public MyGame(IMyGraphic myGraphic)
		{
			Graphic = myGraphic;
		
			// set levels
			_gameLevels = this.GetLevels();
		}

		// methods
		public virtual void OnExit()
		{
		}

		public virtual void OnInit()
		{
			this.LoadImages(Graphic);

			// open dialog
			OpenDialog(new MyDialogStartGame(this));
		}

		public void OpenDialog(IMyDialog myDialog)
		{
			Dialog = myDialog;
		}

		public virtual void LoadFirstLevel()
		{
			CurrentLevelIndex = 0;

			// get cur level
			IMyLevel myLevel = GetCurLevel();

			// load
			if (myLevel != null)
				myLevel.OnLoad(Graphic);
		}

		public virtual void LoadNextLevel()
		{
			CurrentLevelIndex++;

			// get cur level
			IMyLevel myLevel = GetCurLevel();

			// load
			if (myLevel!=null)
				myLevel.OnLoad(Graphic);
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
			// if Dialog skip next turn for level objects
			if (Dialog != null)
				return;

			// get cur level
			IMyLevel myLevel = GetCurLevel();

			if (myLevel != null)
			{
				// next turn
				myLevel.OnNextTurn(timeInMilliseconds, this);

				// is level end
				if (myLevel.IsLevelEnd())
				{
					// open dialog
					OpenDialog(new MyDialogLevelNext(this));
				}
			}
		}

		public virtual void OnChangeWindowSize(int screenWidth, int screenHeight)
		{
			Graphic.ScreenWidth = screenWidth;
			Graphic.ScreenHeight = screenHeight;

			// calculate stratch
			Graphic.XStretchCoef = (float)Graphic.ScreenWidth / Levels[CurrentLevelIndex].LevelWidth;
			Graphic.YStretchCoef = (float)Graphic.ScreenHeight / Levels[CurrentLevelIndex].LevelHeight;
		}

		public virtual void OnDraw(object context)
		{
			// get cur level
			IMyLevel myLevel = GetCurLevel();

			// draw
			if (myLevel != null)
				myLevel.OnDraw(context, Graphic);

			// draw dialog
			if (Dialog != null)
				Dialog.OnDraw(context, Graphic);
		}

		public virtual bool OnClickMouse(int xMouse, int yMouse)
		{
			// click on dialog
			if (Dialog != null)
			{
				if (Dialog.OnClickMouse(xMouse, yMouse, Graphic, this))
					return true;
			}

			// get cur level
			IMyLevel myLevel = GetCurLevel();

			// click mouse
			if (myLevel != null && myLevel.OnClickMouse(xMouse, yMouse, Graphic))
			{
				Graphic.SendMessageToReDraw();
				return true;
			}
			return false;
		}
	}
}
