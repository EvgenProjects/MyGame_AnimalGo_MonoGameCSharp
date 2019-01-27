// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	// level1
	class MyLevel1 : MyLevelAbstract
	{
		public override int GetCols()
		{
			return 10;
		}

		public override int GetRows()
		{
			return 1;
		}

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
				new MyUnitWillAppear(1000 /*1 second*/, new MyUnit_EnemyZmeia(myGraphic, GetComputerPlayerID(), GetStartXPositionWhenUnitAppear(), GetStartYPositionWhenUnitAppear(1 /*row*/))),

				// 1 row
				new MyUnitWillAppear(3000 /*1 second*/, new MyUnit_EnemyZmeia(myGraphic, GetComputerPlayerID(), GetStartXPositionWhenUnitAppear(), GetStartYPositionWhenUnitAppear(1 /*row*/)))
			};
		}
	}
	
	// level2
	class MyLevel2 : MyLevelAbstract
	{
		public override int GetCols()
		{
			return 10;
		}

		public override int GetRows()
		{
			return 2;
		}

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
				new MyUnitWillAppear(1000 /*1 second*/, new MyUnit_EnemyZmeia(myGraphic, GetComputerPlayerID(), GetStartXPositionWhenUnitAppear(), GetStartYPositionWhenUnitAppear(1 /*row*/))),
				new MyUnitWillAppear(3000 /*1 second*/, new MyUnit_EnemyZmeia(myGraphic, GetComputerPlayerID(), GetStartXPositionWhenUnitAppear(), GetStartYPositionWhenUnitAppear(1 /*row*/))),
				new MyUnitWillAppear(5000 /*1 second*/, new MyUnit_EnemyZmeia(myGraphic, GetComputerPlayerID(), GetStartXPositionWhenUnitAppear(), GetStartYPositionWhenUnitAppear(1 /*row*/))),
				new MyUnitWillAppear(7000 /*1 second*/, new MyUnit_EnemyZmeia(myGraphic, GetComputerPlayerID(), GetStartXPositionWhenUnitAppear(), GetStartYPositionWhenUnitAppear(1 /*row*/))),

				// 2 row
				new MyUnitWillAppear(0000 /*1 second*/, new MyUnit_EnemyZmeia(myGraphic, GetComputerPlayerID(), GetStartXPositionWhenUnitAppear(), GetStartYPositionWhenUnitAppear(2 /*row*/))),
				new MyUnitWillAppear(2000 /*1 second*/, new MyUnit_EnemyZmeia(myGraphic, GetComputerPlayerID(), GetStartXPositionWhenUnitAppear(), GetStartYPositionWhenUnitAppear(2 /*row*/))),
				new MyUnitWillAppear(4000 /*1 second*/, new MyUnit_EnemyZmeia(myGraphic, GetComputerPlayerID(), GetStartXPositionWhenUnitAppear(), GetStartYPositionWhenUnitAppear(2 /*row*/))),
				new MyUnitWillAppear(8000 /*1 second*/, new MyUnit_EnemyZmeia(myGraphic, GetComputerPlayerID(), GetStartXPositionWhenUnitAppear(), GetStartYPositionWhenUnitAppear(2 /*row*/))),
			};
		}
	}

	// level3
	class MyLevel3 : MyLevelAbstract
	{
		public override int GetCols()
		{
			return 10;
		}

		public override int GetRows()
		{
			return 3;
		}

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
				new MyUnitWillAppear(1000 /*1 second*/, new MyUnit_EnemyZmeia(myGraphic, GetComputerPlayerID(), GetStartXPositionWhenUnitAppear(), GetStartYPositionWhenUnitAppear(1 /*row*/))),

				// 2 row
				new MyUnitWillAppear(3000 /*1 second*/, new MyUnit_EnemyPauk(myGraphic, GetComputerPlayerID(), GetStartXPositionWhenUnitAppear(), GetStartYPositionWhenUnitAppear(2 /*row*/))),

				// 3 row
				new MyUnitWillAppear(2000 /*1 second*/, new MyUnit_EnemyZmeia(myGraphic, GetComputerPlayerID(), GetStartXPositionWhenUnitAppear(), GetStartYPositionWhenUnitAppear(3 /*row*/))),
			};
		}
	}

	// level4
	class MyLevel4 : MyLevelAbstract
	{
		public override int GetCols()
		{
			return 10;
		}

		public override int GetRows()
		{
			return 3;
		}

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
				new MyUnitWillAppear(1000 /*1 second*/, new MyUnit_EnemyZmeia(myGraphic, GetComputerPlayerID(), GetStartXPositionWhenUnitAppear(), GetStartYPositionWhenUnitAppear(1 /*row*/))),
				new MyUnitWillAppear(3000 /*1 second*/, new MyUnit_EnemyZmeia(myGraphic, GetComputerPlayerID(), GetStartXPositionWhenUnitAppear(), GetStartYPositionWhenUnitAppear(1 /*row*/))),
				new MyUnitWillAppear(5000 /*1 second*/, new MyUnit_EnemyZmeia(myGraphic, GetComputerPlayerID(), GetStartXPositionWhenUnitAppear(), GetStartYPositionWhenUnitAppear(1 /*row*/))),
				new MyUnitWillAppear(7000 /*1 second*/, new MyUnit_EnemyZmeia(myGraphic, GetComputerPlayerID(), GetStartXPositionWhenUnitAppear(), GetStartYPositionWhenUnitAppear(1 /*row*/))),

				// 2 row
				new MyUnitWillAppear(3000 /*1 second*/, new MyUnit_EnemyPauk(myGraphic, GetComputerPlayerID(), GetStartXPositionWhenUnitAppear(), GetStartYPositionWhenUnitAppear(2 /*row*/))),

				// 3 row
				new MyUnitWillAppear(0000 /*1 second*/, new MyUnit_EnemyZmeia(myGraphic, GetComputerPlayerID(), GetStartXPositionWhenUnitAppear(), GetStartYPositionWhenUnitAppear(3 /*row*/))),
				new MyUnitWillAppear(2000 /*1 second*/, new MyUnit_EnemyZmeia(myGraphic, GetComputerPlayerID(), GetStartXPositionWhenUnitAppear(), GetStartYPositionWhenUnitAppear(3 /*row*/))),
				new MyUnitWillAppear(4000 /*1 second*/, new MyUnit_EnemyZmeia(myGraphic, GetComputerPlayerID(), GetStartXPositionWhenUnitAppear(), GetStartYPositionWhenUnitAppear(3 /*row*/))),
				new MyUnitWillAppear(8000 /*1 second*/, new MyUnit_EnemyZmeia(myGraphic, GetComputerPlayerID(), GetStartXPositionWhenUnitAppear(), GetStartYPositionWhenUnitAppear(3 /*row*/))),

				// 4 row
				new MyUnitWillAppear(12000 /*1 second*/, new MyUnit_EnemyPauk(myGraphic, GetComputerPlayerID(), GetStartXPositionWhenUnitAppear(), GetStartYPositionWhenUnitAppear(3 /*row*/))),
			};
		}
	}
}
