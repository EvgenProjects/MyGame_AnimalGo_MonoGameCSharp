// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	class MyGame : IMyGame
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
			_gameLevels = new IMyLevel[] { new MyLevel1(), new MyLevel2(), new MyLevel3(), new MyLevel4() };
		}

		// methods
		public virtual void OnExit()
		{
		}

		public virtual void OnInit()
		{
			int imageType = (int)enImageType.Unknown;

			// load images (level)
			Graphic.LoadImageFromFile("level/field_grass1.png", (int)enImageType.Background_Level_Grass1);
			Graphic.LoadImageFromFile("level/field_grass2.png", (int)enImageType.Background_Level_Grass2);
			Graphic.LoadImageFromFile("level/field_bottom.png", (int)enImageType.Background_Bottom);
			Graphic.LoadImageFromFile("level/filed_left_baza.png", (int)enImageType.Background_left_baza);

			// load images (buttons)
			Graphic.LoadImageFromFile("buttons/button_eda.png", (int)enImageType.Button_eda);
			Graphic.LoadImageFromFile("buttons/button_krolik_s_lukom.png", (int)enImageType.Button_krolik_s_lukom);
			Graphic.LoadImageFromFile("buttons/button_restart.png", (int)enImageType.Button_restart);
			Graphic.LoadImageFromFile("buttons/button_dog.png", (int)enImageType.Button_dog);
			Graphic.LoadImageFromFile("buttons/shenachi_patrul/button_marshal.png", (int)enImageType.Button_marshal);
			Graphic.LoadImageFromFile("buttons/shenachi_patrul/button_zuma.png", (int)enImageType.Button_zuma);
			Graphic.LoadImageFromFile("buttons/button_level_first.png", (int)enImageType.Button_level_first);
			Graphic.LoadImageFromFile("buttons/button_level_next.png", (int)enImageType.Button_level_next);

			// load images (my heroes)
			Graphic.LoadImageFromFile("heroes/krolik/krolik_s_lukom.png", (int)enImageType.Heroes_krolik_s_lukom);

			Graphic.LoadImageFromFile("heroes/griadka/griadka.png", (int)enImageType.Heroes_griadka);
			Graphic.LoadImageFromFile("heroes/griadka/griadka1.png", (int)enImageType.Heroes_griadka1);
			Graphic.LoadImageFromFile("heroes/griadka/griadka2.png", (int)enImageType.Heroes_griadka2);
			Graphic.LoadImageFromFile("heroes/griadka/griadka3.png", (int)enImageType.Heroes_griadka3);
			Graphic.LoadImageFromFile("heroes/griadka/griadka4.png", (int)enImageType.Heroes_griadka4);
			Graphic.LoadImageFromFile("heroes/griadka/griadka5.png", (int)enImageType.Heroes_griadka5);

			Graphic.LoadImageFromFile("heroes/shenachi_patrul/marshal.png", (int)enImageType.Heroes_marshal);
			Graphic.LoadImageFromFile("heroes/shenachi_patrul/zuma.png", (int)enImageType.Heroes_zuma);

			// load image & offsets
			imageType = (int)enImageType.Heroes_sobachka_run;
			Graphic.LoadImageFromFile("heroes/sobachka_run/sobachka_run.png", imageType);
			Graphic.FindImage(imageType).SpriteOffsets = new MyRectangle[]{
//				new MyRectangle(4, 4, 52, 42),
//				new MyRectangle(65, 5, 59, 40),
//				new MyRectangle(127, 3, 56, 41),
//				new MyRectangle(189, 6, 55, 37),
				new MyRectangle(244, 3, 57, 42),
				new MyRectangle(304, 4, 54, 39),
				new MyRectangle(363, 4, 57, 38),
				new MyRectangle(420, 1, 55, 40),
				new MyRectangle(483, 4, 57, 36),
				new MyRectangle(542, 4, 57, 38),
			};

			// load images (enemy heroes)
			Graphic.LoadImageFromFile("heroes/zmeia/zmeia_go.png", (int)enImageType.Heroes_zmeia_go);
			Graphic.LoadImageFromFile("heroes/zmeia/zmeia_zalit.png", (int)enImageType.Heroes_zmeia_zalit);

			Graphic.LoadImageFromFile("heroes/spider/spider_go.png", (int)enImageType.Heroes_spider_go);
			Graphic.LoadImageFromFile("heroes/spider/spider_hit.png", (int)enImageType.Heroes_spider_hit);

			// load images (fire)
			Graphic.LoadImageFromFile("heroes/fire/morkovka.png", (int)enImageType.Fire_morkovka);
			Graphic.LoadImageFromFile("heroes/fire/morkovka_splash.png", (int)enImageType.Fire_morkovka_splash);

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
