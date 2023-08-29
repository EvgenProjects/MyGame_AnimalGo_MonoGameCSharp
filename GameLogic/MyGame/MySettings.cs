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
		public static void LoadImages(IMyGraphic Graphic)
		{
            // Popup windows
            Graphic.LoadImageFromFile("popup_windows/window_level_first.png", enImageType.PopupWindow_level_first);
            Graphic.LoadImageFromFile("popup_windows/window_level_next.png", enImageType.PopupWindow_level_next);
            Graphic.LoadImageFromFile("popup_windows/window_all_levels_finished.png", enImageType.PopupWindow_all_levels_finished);

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
			Graphic.LoadImageFromFile("heroes/sobachka_run/sobachka_run.png", enImageType.Heroes_sobachka_run);
			Graphic.FindImage(enImageType.Heroes_sobachka_run).SpriteOffsets = new MyRectangle[]{
				new MyRectangle(244, 3, 57, 42),
				new MyRectangle(304, 4, 54, 39),
				new MyRectangle(363, 4, 57, 38),
				new MyRectangle(420, 1, 55, 40),
				new MyRectangle(483, 4, 57, 36),
				new MyRectangle(542, 4, 57, 38),
			};

			// load image & offsets
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

		// create my unit by button type
		public static void CreateMyUnitWhenClickButton(this IMyLevel gameLevel, IMyGraphic myGraphic, int xCenter, int yCenter, enImageType buttonType)
		{
			if (buttonType == enImageType.Button_dog)
			{
				gameLevel.MyUnits.Add(new Unit_DogGo(myGraphic, xCenter, yCenter));
			}
			else if (buttonType == enImageType.Button_krolik_s_lukom)
			{
				gameLevel.MyUnits.Add(new Unit_KrolikStayAndFire(myGraphic, xCenter, yCenter));
			}
		}
    }
}