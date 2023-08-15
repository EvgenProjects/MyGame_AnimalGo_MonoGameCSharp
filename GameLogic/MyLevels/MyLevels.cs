using MyGame;
using MyGame.interfaces;
using MyUnits;
using MyUnits.interfaces;

namespace MyLevels
{
	// level1
	class MyLevel1 : MyLevel
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

		public override IUnitWillAppear[] GetUnitsWillAppear(IMyGraphic myGraphic)
		{
			return new IUnitWillAppear[]
			{
				// 1 row
				AddEnemyUnit(myGraphic, 2  /*second*/, 1 /*row*/, enImageType.Heroes_zmeia_go),
				AddEnemyUnit(myGraphic, 4    /*second*/, 1 /*row*/, enImageType.Heroes_zmeia_go),
				AddEnemyUnit(myGraphic, 5  /*second*/, 1 /*row*/, enImageType.Heroes_bird1_fly),
			};
		}
	}
	
	// level2
	class MyLevel2 : MyLevel
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

		public override IUnitWillAppear[] GetUnitsWillAppear(IMyGraphic myGraphic)
		{
			return new IUnitWillAppear[]
			{
				// 1 row
				AddEnemyUnit(myGraphic, 1.0  /*second*/, 1 /*row*/, enImageType.Heroes_zmeia_go),
				AddEnemyUnit(myGraphic, 3.0  /*second*/, 1 /*row*/, enImageType.Heroes_bird1_fly),
				AddEnemyUnit(myGraphic, 5.0  /*second*/, 1 /*row*/, enImageType.Heroes_zmeia_go),
				AddEnemyUnit(myGraphic, 7.0  /*second*/, 1 /*row*/, enImageType.Heroes_zmeia_go),

				// 2 row
				AddEnemyUnit(myGraphic, 0.0  /*second*/, 2 /*row*/, enImageType.Heroes_zmeia_go),
				AddEnemyUnit(myGraphic, 2.0  /*second*/, 2 /*row*/, enImageType.Heroes_zmeia_go),
				AddEnemyUnit(myGraphic, 4.0  /*second*/, 2 /*row*/, enImageType.Heroes_bird1_fly),
				AddEnemyUnit(myGraphic, 8.0  /*second*/, 2 /*row*/, enImageType.Heroes_zmeia_go),
			};
		}
	}

	// level3
	class MyLevel3 : MyLevel
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

		public override IUnitWillAppear[] GetUnitsWillAppear(IMyGraphic myGraphic)
		{
			return new IUnitWillAppear[]
			{
				// 1 row
				AddEnemyUnit(myGraphic, 1.0  /*second*/, 1 /*row*/, enImageType.Heroes_zmeia_go),

				// 2 row
				AddEnemyUnit(myGraphic, 3.0  /*second*/, 2 /*row*/, enImageType.Heroes_spider_go),

				// 3 row
				AddEnemyUnit(myGraphic, 2.0  /*second*/, 3 /*row*/, enImageType.Heroes_spider_go),
			};
		}
	}

	// level4
	class MyLevel4 : MyLevel
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

		public override IUnitWillAppear[] GetUnitsWillAppear(IMyGraphic myGraphic)
		{
			return new IUnitWillAppear[]
			{
				// 1 row
				AddEnemyUnit(myGraphic, 1.0  /*second*/, 1 /*row*/, enImageType.Heroes_zmeia_go),
				AddEnemyUnit(myGraphic, 3.0  /*second*/, 1 /*row*/, enImageType.Heroes_zmeia_go),
				AddEnemyUnit(myGraphic, 5.0  /*second*/, 1 /*row*/, enImageType.Heroes_zmeia_go),
				AddEnemyUnit(myGraphic, 7.0  /*second*/, 1 /*row*/, enImageType.Heroes_zmeia_go),

				// 2 row
				AddEnemyUnit(myGraphic, 3.0  /*second*/, 2 /*row*/, enImageType.Heroes_spider_go),

				// 3 row
				AddEnemyUnit(myGraphic, 0.0  /*second*/, 3 /*row*/, enImageType.Heroes_zmeia_go),
				AddEnemyUnit(myGraphic, 2.0  /*second*/, 3 /*row*/, enImageType.Heroes_zmeia_go),
				AddEnemyUnit(myGraphic, 4.0  /*second*/, 3 /*row*/, enImageType.Heroes_zmeia_go),
				AddEnemyUnit(myGraphic, 8.0  /*second*/, 3 /*row*/, enImageType.Heroes_zmeia_go),

				// 4 row
				AddEnemyUnit(myGraphic, 12.0  /*second*/, 4 /*row*/, enImageType.Heroes_spider_go),
			};
		}
	}
}