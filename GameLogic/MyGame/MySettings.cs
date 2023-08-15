// my namespaces
using MyGame.interfaces;
using MyLevels;
using MyLevels.interfaces;
using MyUnits;

namespace MyGame
{
	public static class MySettings
	{
		// load images
		public static void LoadImages(this MyGame myGame, IMyGraphic Graphic)
		{
			enImageType imageType = enImageType.Unknown;

			// load images (level)
			Graphic.LoadImageFromFile("level/field_grass1.png", enImageType.Background_Level_Grass1);
			Graphic.LoadImageFromFile("level/field_grass2.png", enImageType.Background_Level_Grass2);
			Graphic.LoadImageFromFile("level/field_bottom.png", enImageType.Background_Bottom);
			Graphic.LoadImageFromFile("level/filed_left_baza.png", enImageType.Background_left_baza);

			// load images (buttons)
			Graphic.LoadImageFromFile("buttons/button_eda.png", enImageType.Button_eda);
			Graphic.LoadImageFromFile("buttons/button_krolik_s_lukom.png", enImageType.Button_krolik_s_lukom);
			Graphic.LoadImageFromFile("buttons/button_restart.png", enImageType.Button_restart);
			Graphic.LoadImageFromFile("buttons/button_dog.png", enImageType.Button_dog);
			Graphic.LoadImageFromFile("buttons/button_level_first.png", enImageType.Button_level_first);
			Graphic.LoadImageFromFile("buttons/button_level_next.png", enImageType.Button_level_next);

			// load images (my heroes)
			Graphic.LoadImageFromFile("heroes/krolik/krolik_s_lukom.png", enImageType.Heroes_krolik_s_lukom);

			Graphic.LoadImageFromFile("heroes/griadka/griadka.png", enImageType.Heroes_griadka);
			Graphic.LoadImageFromFile("heroes/griadka/griadka1.png", enImageType.Heroes_griadka1);
			Graphic.LoadImageFromFile("heroes/griadka/griadka2.png", enImageType.Heroes_griadka2);
			Graphic.LoadImageFromFile("heroes/griadka/griadka3.png", enImageType.Heroes_griadka3);
			Graphic.LoadImageFromFile("heroes/griadka/griadka4.png", enImageType.Heroes_griadka4);
			Graphic.LoadImageFromFile("heroes/griadka/griadka5.png", enImageType.Heroes_griadka5);

			Graphic.LoadImageFromFile("heroes/shenachi_patrul/marshal.png", enImageType.Heroes_marshal);
			Graphic.LoadImageFromFile("heroes/shenachi_patrul/zuma.png", enImageType.Heroes_zuma);

			// load image & offsets
			imageType = enImageType.Heroes_sobachka_run;
			Graphic.LoadImageFromFile("heroes/sobachka_run/sobachka_run.png", imageType);
			Graphic.FindImage(imageType).SpriteOffsets = new MyRectangle[]{
				new MyRectangle(244, 3, 57, 42),
				new MyRectangle(304, 4, 54, 39),
				new MyRectangle(363, 4, 57, 38),
				new MyRectangle(420, 1, 55, 40),
				new MyRectangle(483, 4, 57, 36),
				new MyRectangle(542, 4, 57, 38),
			};

			// load image & offsets
			imageType = enImageType.Heroes_bird1_fly;
			Graphic.LoadImageFromFile("heroes/bird/bird1.png", enImageType.Heroes_bird1_fly);

			// load images (enemy heroes)
			Graphic.LoadImageFromFile("heroes/zmeia/zmeia_go.png", enImageType.Heroes_zmeia_go);
			Graphic.LoadImageFromFile("heroes/zmeia/zmeia_zalit.png", enImageType.Heroes_zmeia_zalit);

			Graphic.LoadImageFromFile("heroes/spider/spider_go.png", enImageType.Heroes_spider_go);
			Graphic.LoadImageFromFile("heroes/spider/spider_hit.png", enImageType.Heroes_spider_hit);

			// load images (fire)
			Graphic.LoadImageFromFile("heroes/fire/morkovka.png", enImageType.Fire_morkovka);
			Graphic.LoadImageFromFile("heroes/fire/morkovka_splash.png", enImageType.Fire_morkovka_splash);
		}

		// get levels
		public static IMyLevel[] GetLevels(this MyGame myGame)
		{
			return new IMyLevel[] { new MyLevel1(), new MyLevel2(), new MyLevel3(), new MyLevel4() };
		}

		// create my unit by button type
		public static void CreateMyUnitWhenClickButton(this IMyLevel gameLevel, IMyGraphic myGraphic, int xCenter, int yCenter, enImageType buttonType)
		{
			if (buttonType == enImageType.Button_dog)
			{
				gameLevel.Units.Add(new Unit_DogGo(myGraphic, xCenter, yCenter, gameLevel.GetMyPlayerID()));
			}
			else if (buttonType == enImageType.Button_krolik_s_lukom)
			{
				gameLevel.Units.Add(new Unit_KrolikStayAndFire(myGraphic, xCenter, yCenter, gameLevel.GetMyPlayerID()));
			}
		}

        public static void CreateEnemyUnit(this IMyLevel gameLevel, IMyGraphic myGraphic, int xCenter, int yCenter, int playerID, enImageType imageType)
        {
            if (imageType == enImageType.Heroes_zmeia_go)
            {
                gameLevel.Units.Add(new Unit_EnemyZmeia(myGraphic, xCenter, yCenter, playerID));
            }
            else if (imageType == enImageType.Heroes_bird1_fly)
            {
                gameLevel.Units.Add(new Unit_EnemyBird1(myGraphic, xCenter, yCenter, playerID));
            }
        }
    }
}