// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	public enum enImageType
	{
		Unknown = 0,

		// background
		Background_Level_Grass1,
		Background_Level_Grass2,
		Background_Bottom,
		Background_left_baza,

		// buttons
		Button_eda,
		Button_krolik_s_lukom,
		Button_restart,
		Button_marshal,
		Button_zuma,
		Button_dog,
		Button_level_first,
		Button_level_next,

		// my heroes
		Heroes_krolik_s_lukom,
		Heroes_griadka,
		Heroes_griadka1,
		Heroes_griadka2,
		Heroes_griadka3,
		Heroes_griadka4,
		Heroes_griadka5,

		Heroes_marshal,
		Heroes_zuma,
		Heroes_sobachka_run,

		// enemy heroes
		Heroes_zmeia_go,
		Heroes_zmeia_zalit,

		Heroes_spider_go,
		Heroes_spider_hit,

		Heroes_bird1_fly,

		// fire
		Fire_morkovka,
		Fire_morkovka_splash,
	}
	
	// settings
	public static class MySettings
	{
		// load images
		public static void LoadImages(this MyGame myGame, IMyGraphic Graphic)
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
				new MyRectangle(244, 3, 57, 42),
				new MyRectangle(304, 4, 54, 39),
				new MyRectangle(363, 4, 57, 38),
				new MyRectangle(420, 1, 55, 40),
				new MyRectangle(483, 4, 57, 36),
				new MyRectangle(542, 4, 57, 38),
			};

			// load image & offsets
			imageType = (int)enImageType.Heroes_bird1_fly;
			Graphic.LoadImageFromFile("heroes/bird/bird1.png", imageType);
			Graphic.FindImage(imageType).SpriteOffsets = new MyRectangle[]{
				new MyRectangle(0, 1, 76, 52),
				new MyRectangle(85, 0, 81, 45),
				new MyRectangle(177, 0, 77, 46),
				new MyRectangle(45, 65, 75, 53),
				new MyRectangle(130, 61, 76, 62),
				new MyRectangle(0, 128, 77, 78),
				new MyRectangle(130, 61, 76, 62),
				new MyRectangle(45, 65, 75, 53),
				new MyRectangle(177, 0, 77, 46),
				new MyRectangle(85, 0, 81, 45),
			};

			// load images (enemy heroes)
			Graphic.LoadImageFromFile("heroes/zmeia/zmeia_go.png", (int)enImageType.Heroes_zmeia_go);
			Graphic.LoadImageFromFile("heroes/zmeia/zmeia_zalit.png", (int)enImageType.Heroes_zmeia_zalit);

			Graphic.LoadImageFromFile("heroes/spider/spider_go.png", (int)enImageType.Heroes_spider_go);
			Graphic.LoadImageFromFile("heroes/spider/spider_hit.png", (int)enImageType.Heroes_spider_hit);

			// load images (fire)
			Graphic.LoadImageFromFile("heroes/fire/morkovka.png", (int)enImageType.Fire_morkovka);
			Graphic.LoadImageFromFile("heroes/fire/morkovka_splash.png", (int)enImageType.Fire_morkovka_splash);
		}

		// get levels
		public static IMyLevel[] GetLevels(this MyGame myGame)
		{
			return new IMyLevel[] { new MyLevel1(), new MyLevel2(), new MyLevel3(), new MyLevel4() };
		}

		// create my unit by button type
		public static void CreateMyUnitWhenClickButton(this IMyLevel gameLevel, IMyGraphic myGraphic, int xCenter, int yCenter, enImageType buttonType)
		{
			if (buttonType == enImageType.Button_zuma)
			{
				gameLevel.Units.Add(new MyUnit_DogZumaGo(myGraphic, gameLevel.GetMyPlayerID(), xCenter, yCenter));
			}
			else if (buttonType == enImageType.Button_marshal)
			{
				gameLevel.Units.Add(new MyUnit_DogMarshalGo(myGraphic, gameLevel.GetMyPlayerID(), xCenter, yCenter));
			}
			else if (buttonType == enImageType.Button_dog)
			{
				gameLevel.Units.Add(new MyUnit_DogGo(myGraphic, gameLevel.GetMyPlayerID(), xCenter, yCenter));
			}
			else if (buttonType == enImageType.Button_krolik_s_lukom)
			{
				gameLevel.Units.Add(new MyUnit_KrolikStayAndFire(myGraphic, gameLevel.GetMyPlayerID(), xCenter, yCenter));
			}
		}
	}

	// level1
	class MyLevel1 : MyLevelAbstract
	{
		public override int GetCols(){	return 10;	}
		public override int GetRows(){	return 1;	}

		public override enImageType GetBackgroundImageType(int row, int col)
		{
			return (row + col) % 2 == 0 ? enImageType.Background_Level_Grass1 : enImageType.Background_Level_Grass2;
		}

		public override enImageType[] GetButtons()
		{
			return new enImageType[]
			{ 
				enImageType.Button_krolik_s_lukom 
			};
		}

		public override IMyUnitWillAppear[] GetUnitsWillAppear(IMyGraphic myGraphic)
		{
			return new IMyUnitWillAppear[]
			{
				// 1 row
				AddEnemyUnit(myGraphic, 0.5  /*second*/, 1 /*row*/, typeof(MyUnit_EnemyZmeia)),
				AddEnemyUnit(myGraphic, 4    /*second*/, 1 /*row*/, typeof(MyUnit_EnemyZmeia)),
				AddEnemyUnit(myGraphic, 7.6  /*second*/, 1 /*row*/, typeof(MyUnit_EnemyBird1)),
			};
		}
	}
	
	// level2
	class MyLevel2 : MyLevelAbstract
	{
		public override int GetCols() { return 10; }
		public override int GetRows() { return 2; }

		public override enImageType GetBackgroundImageType(int row, int col)
		{
			return (row + col) % 2 == 0 ? enImageType.Background_Level_Grass1 : enImageType.Background_Level_Grass2;
		}

		public override enImageType[] GetButtons()
		{
			return new enImageType[]
			{ 
		//		enImageType.Button_marshal, 
		//		enImageType.Button_zuma, 
		//		enImageType.Button_dog,
				enImageType.Button_krolik_s_lukom
			};
		}

		public override IMyUnitWillAppear[] GetUnitsWillAppear(IMyGraphic myGraphic)
		{
			return new IMyUnitWillAppear[]
			{
				// 1 row
				AddEnemyUnit(myGraphic, 1.0  /*second*/, 1 /*row*/, typeof(MyUnit_EnemyZmeia)),
				AddEnemyUnit(myGraphic, 3.0  /*second*/, 1 /*row*/, typeof(MyUnit_EnemyBird1)),
				AddEnemyUnit(myGraphic, 5.0  /*second*/, 1 /*row*/, typeof(MyUnit_EnemyZmeia)),
				AddEnemyUnit(myGraphic, 7.0  /*second*/, 1 /*row*/, typeof(MyUnit_EnemyZmeia)),

				// 2 row
				AddEnemyUnit(myGraphic, 0.0  /*second*/, 2 /*row*/, typeof(MyUnit_EnemyZmeia)),
				AddEnemyUnit(myGraphic, 2.0  /*second*/, 2 /*row*/, typeof(MyUnit_EnemyZmeia)),
				AddEnemyUnit(myGraphic, 4.0  /*second*/, 2 /*row*/, typeof(MyUnit_EnemyBird1)),
				AddEnemyUnit(myGraphic, 8.0  /*second*/, 2 /*row*/, typeof(MyUnit_EnemyZmeia)),
			};
		}
	}

	// level3
	class MyLevel3 : MyLevelAbstract
	{
		public override int GetCols() { return 10; }
		public override int GetRows() { return 3; }

		public override enImageType GetBackgroundImageType(int row, int col)
		{
			return (row + col) % 2 == 0 ? enImageType.Background_Level_Grass1 : enImageType.Background_Level_Grass2;
		}

		public override enImageType[] GetButtons()
		{
			return new enImageType[]
			{ 
		//		enImageType.Button_marshal, 
		//		enImageType.Button_zuma, 
				enImageType.Button_dog,
				enImageType.Button_krolik_s_lukom
			};
		}

		public override IMyUnitWillAppear[] GetUnitsWillAppear(IMyGraphic myGraphic)
		{
			return new IMyUnitWillAppear[]
			{
				// 1 row
				AddEnemyUnit(myGraphic, 1.0  /*second*/, 1 /*row*/, typeof(MyUnit_EnemyZmeia)),

				// 2 row
				AddEnemyUnit(myGraphic, 3.0  /*second*/, 2 /*row*/, typeof(MyUnit_EnemyPauk)),

				// 3 row
				AddEnemyUnit(myGraphic, 2.0  /*second*/, 3 /*row*/, typeof(MyUnit_EnemyPauk)),
			};
		}
	}

	// level4
	class MyLevel4 : MyLevelAbstract
	{
		public override int GetCols() { return 10; }
		public override int GetRows() { return 3; }

		public override enImageType GetBackgroundImageType(int row, int col)
		{
			return (row + col) % 2 == 0 ? enImageType.Background_Level_Grass1 : enImageType.Background_Level_Grass2;
		}

		public override enImageType[] GetButtons()
		{
			return new enImageType[]
			{ 
		//		enImageType.Button_marshal, 
		//		enImageType.Button_zuma, 
				enImageType.Button_dog,
				enImageType.Button_krolik_s_lukom
			};
		}

		public override IMyUnitWillAppear[] GetUnitsWillAppear(IMyGraphic myGraphic)
		{
			return new IMyUnitWillAppear[]
			{
				// 1 row
				AddEnemyUnit(myGraphic, 1.0  /*second*/, 1 /*row*/, typeof(MyUnit_EnemyZmeia)),
				AddEnemyUnit(myGraphic, 3.0  /*second*/, 1 /*row*/, typeof(MyUnit_EnemyZmeia)),
				AddEnemyUnit(myGraphic, 5.0  /*second*/, 1 /*row*/, typeof(MyUnit_EnemyZmeia)),
				AddEnemyUnit(myGraphic, 7.0  /*second*/, 1 /*row*/, typeof(MyUnit_EnemyZmeia)),

				// 2 row
				AddEnemyUnit(myGraphic, 3.0  /*second*/, 2 /*row*/, typeof(MyUnit_EnemyPauk)),

				// 3 row
				AddEnemyUnit(myGraphic, 0.0  /*second*/, 3 /*row*/, typeof(MyUnit_EnemyZmeia)),
				AddEnemyUnit(myGraphic, 2.0  /*second*/, 3 /*row*/, typeof(MyUnit_EnemyZmeia)),
				AddEnemyUnit(myGraphic, 4.0  /*second*/, 3 /*row*/, typeof(MyUnit_EnemyZmeia)),
				AddEnemyUnit(myGraphic, 8.0  /*second*/, 3 /*row*/, typeof(MyUnit_EnemyZmeia)),

				// 4 row
				AddEnemyUnit(myGraphic, 12.0  /*second*/, 4 /*row*/, typeof(MyUnit_EnemyPauk)),
			};
		}
	}

	// Units
	class MyUnit_DogGo : MyUnit_StopAndHitIfNearOtherUnit
	{
		public MyUnit_DogGo(IMyGraphic myGraphic, int playerID, int xCenterSource, int yCenterSource) :
			base(40 /*life*/, playerID /*playerID*/, 1500 /*1.5 second (time to make damage near)*/, (int)enImageType.Heroes_sobachka_run/*image Type when Damage*/, new MyPicture(null, xCenterSource, yCenterSource, enImageAlign.CenterX_CenterY))
		{
			MyPicture.ImageFile = myGraphic.FindImage((int)enImageType.Heroes_sobachka_run);
			Trajectory = new MyTrajectory(0.7f/*x step*/, 0.0f/*y step*/);
			WeaponInfo = new MyWeaponInfo(20/*damage*/);
		}
	}

	class MyUnit_DogMarshalGo : MyUnit_StopAndHitIfNearOtherUnit
	{
		public MyUnit_DogMarshalGo(IMyGraphic myGraphic, int playerID, int xCenterSource, int yCenterSource) :
			base(40 /*life*/, playerID /*playerID*/, 1500 /*1.5 second (time to make damage near)*/, (int)enImageType.Heroes_marshal/*image Type when Damage*/, new MyPicture(null, xCenterSource, yCenterSource, enImageAlign.CenterX_CenterY))
		{
			MyPicture.ImageFile = myGraphic.FindImage((int)enImageType.Heroes_marshal);
			Trajectory = new MyTrajectory(0.7f/*x step*/, 0.0f/*y step*/);
			WeaponInfo = new MyWeaponInfo(20/*damage*/);
		}
	}

	class MyUnit_DogZumaGo : MyUnit_StopAndHitIfNearOtherUnit
	{
		public MyUnit_DogZumaGo(IMyGraphic myGraphic, int playerID, int xCenterSource, int yCenterSource) :
			base(40 /*life*/, playerID /*playerID*/, 1500 /*1.5 second (time to make damage near)*/, (int)enImageType.Heroes_zuma/*image Type when Damage*/, new MyPicture(null, xCenterSource, yCenterSource, enImageAlign.CenterX_CenterY))
		{
			MyPicture.ImageFile = myGraphic.FindImage((int)enImageType.Heroes_zuma);
			Trajectory = new MyTrajectory(0.7f/*x step*/, 0.0f/*y step*/);
			WeaponInfo = new MyWeaponInfo(20/*damage*/);
		}
	}

	class MyUnit_EnemyPauk : MyUnit_StopAndHitIfNearOtherUnit
	{
		public MyUnit_EnemyPauk(IMyGraphic myGraphic, int playerID, int xCenterSource, int yCenterSource) :
			base(400 /*life*/, playerID /*playerID*/, 1500 /*1.5 second (time to make damage near)*/, (int)enImageType.Heroes_spider_hit/*image Type when Damage*/, new MyPicture(null, xCenterSource, yCenterSource, enImageAlign.CenterX_CenterY))
		{
			MyPicture.ImageFile = myGraphic.FindImage((int)enImageType.Heroes_spider_go);
			Trajectory = new MyTrajectory(-0.2f/*x step*/, 0.0f/*y step*/);
			WeaponInfo = new MyWeaponInfo(10/*damage*/);
		}
	}

	class MyUnit_EnemyBird1 : MyUnit_StopAndHitIfNearOtherUnit
	{
		public MyUnit_EnemyBird1(IMyGraphic myGraphic, int playerID, int xCenterSource, int yCenterSource) :
			base(180 /*life*/, playerID /*playerID*/, 1000 /*1.5 second (time to make damage near)*/, (int)enImageType.Heroes_bird1_fly/*image Type when Damage*/, new MyPicture(null, xCenterSource, yCenterSource, enImageAlign.CenterX_CenterY))
		{
			MyPicture.ImageFile = myGraphic.FindImage((int)enImageType.Heroes_bird1_fly);
			Trajectory = new MyTrajectory(-1.8f/*x step*/, 0.0f/*y step*/);
			WeaponInfo = new MyWeaponInfo(40/*damage*/);
		}
	}

	class MyUnit_EnemyZmeia : MyUnit_StopAndHitIfNearOtherUnit
	{
		public MyUnit_EnemyZmeia(IMyGraphic myGraphic, int playerID, int xCenterSource, int yCenterSource) :
			base(40 /*life*/, playerID /*playerID*/, 1500 /*1.5 second (time to make damage near)*/, (int)enImageType.Heroes_zmeia_zalit/*image Type when Damage*/, new MyPicture(null, xCenterSource, yCenterSource, enImageAlign.CenterX_CenterY))
		{
			MyPicture.ImageFile = myGraphic.FindImage((int)enImageType.Heroes_zmeia_go);
			Trajectory = new MyTrajectory(-0.7f/*x step*/, 0.0f/*y step*/);
			WeaponInfo = new MyWeaponInfo(20/*damage*/);
		}
	}

	class MyUnit_KrolikStayAndFire : MyUnit_FireOnDistanceIfSeeEnemyUnit
	{
		// constructor
		public MyUnit_KrolikStayAndFire(IMyGraphic myGraphic, int playerID, int xCenterSource, int yCenterSource)
			: base(30 /*life*/, playerID /*playerID*/, 1500 /*1.5 second (time to make damage near)*/, new MyPicture(null, xCenterSource, yCenterSource, enImageAlign.CenterX_CenterY))
		{
			MyPicture.ImageFile = myGraphic.FindImage((int)enImageType.Heroes_krolik_s_lukom);
			Trajectory = new MyTrajectory(0.0f/*x step*/, 0.0f/*y step*/);

			DelegateMakeFire = (posSourceCenter) =>
			{
				IMyTrajectory trajectory = new MyTrajectory(4.7f/*x step*/, 0.0f/*y step*/);
				IMyWeaponInfo weaponInfo = new MyWeaponInfo(10/*damage*/);
				enImageType imageTypeWhenDamage = enImageType.Fire_morkovka_splash;
				IMyImageFile myImageFile = myGraphic.FindImage((int)enImageType.Fire_morkovka);

				MyPicture myPicture = new MyPicture(myImageFile, posSourceCenter.X, posSourceCenter.Y, enImageAlign.CenterX_CenterY);

				MyFire myFire = new MyFire(weaponInfo, trajectory, playerID, myPicture, imageTypeWhenDamage);
				return myFire;
			};
		}
	}
}