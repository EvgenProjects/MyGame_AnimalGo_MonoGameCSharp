// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	class MyGame : IMyGame
	{
		// graphic
		public IMyGraphic Graphic { get; protected set; }

		// level
		protected IMyLevel GameLevel;

		// time
		public virtual int GetTimeStepInMilliseconds() { return 20; } // 20 miliseconds

		// constructor
		public MyGame(IMyGraphic myGraphic)
		{
			Graphic = myGraphic;
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

			// set level
			GameLevel = new MyLevelImpl();
			GameLevel.OnLoad(Graphic);
		}

		public virtual void OnNextTurn(long timeInMilliseconds)
		{
			if (GameLevel != null)
				GameLevel.OnNextTurn(timeInMilliseconds, this);
		}

		public virtual void OnChangeWindowSize(int screenWidth, int screenHeight)
		{
			Graphic.ScreenWidth = screenWidth;
			Graphic.ScreenHeight = screenHeight;

			// calculate stratch
			Graphic.XStretchCoef = (float)Graphic.ScreenWidth / GameLevel.LevelWidth;
			Graphic.YStretchCoef = (float)Graphic.ScreenHeight / GameLevel.LevelHeight;
		}

		public virtual void OnDraw(object context)
		{
			if (GameLevel != null)
				GameLevel.OnDraw(context, Graphic);
		}

		public virtual bool OnClickMouse(int xMouse, int yMouse)
		{
			bool result = false;
			if (GameLevel != null)
			{
				result = GameLevel.OnClickMouse(xMouse, yMouse, Graphic);
				if (result)
					Graphic.SendMessageToReDraw();
			}
			return result;
		}
	}
}
