// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	class MyLevelImpl : MyLevel
	{
		public override int LevelWidth { get { return 680; } }
		public override int LevelHeight { get { return 276; } }
		public override int LevelLeft { get { return 16; } }
		public override int LevelTop { get { return 8; } }

		MySize PlayZoneItemSize = new MySize(64, 80); ///// !!!!!
		protected int Rows = 2; ///// !!!!!
		protected int Cols = 10; ///// !!!!!

		// time statistic
		protected int HowMuchTimeInMilliseconds_TakeNextTurn = 0;
		protected int HowMuchTimeInMilliseconds_TakeDraw = 0;

		// constructor
		public MyLevelImpl() :	base()
		{
		}

		// multiplayer
		public int GetComputerPlayerID()
		{
			return 1000; // any number
		}

		public override int[] GetPlayerIDs()
		{
			return null;
		}

		public override string GetPlayerName(int playerID)
		{
			return "";
		}

		public override bool IsTeam(int playerID1, int playerID2)
		{
			if (playerID1 != playerID2 && (playerID1 == GetComputerPlayerID() || playerID2 == GetComputerPlayerID()))
				return false;
			return true;
		}

		public override int GetMyPlayerID()
		{
			return 0;
		}

		// next
		public override void OnNextTurn(long timeInMilliseconds, IMyGame myGame)
		{
			// statistic: take time
			int time1 = System.Environment.TickCount;

			// base OnNextTurn
			base.OnNextTurn(timeInMilliseconds, myGame);

			// statistic: take time
			int time2 = System.Environment.TickCount;
			HowMuchTimeInMilliseconds_TakeNextTurn = time2 - time1;
		}

		public override void OnDraw(object context, IMyGraphic myGraphic)
		{
			// base
			base.OnDraw(context, myGraphic);

			// 
			myGraphic.DrawText(context, 10, 0, "Units:" + Units.Count, 16/*text size*/, new MyColor(128, 128, 128));
		}

		public override void OnLoad(IMyGraphic myGraphic)
		{
			// add level background
			enImageType imageType = 0;
			int xLeftBackground = 0;
			int yBottomBackground = 0;
			for (int colNumber = 1; colNumber <= Cols; colNumber++)
			{
				for (int rowNumber = 1; rowNumber <= Rows; rowNumber++)
				{
					// image type
					imageType = (rowNumber + colNumber) % 2 == 0 ? enImageType.Background_Level_Grass1 : enImageType.Background_Level_Grass2;

					//add backgroud in scene
					BackgroundPictures.Add(new MyBackgroundImpl(new MyPicture(myGraphic.FindImage((int)imageType), GetXCenterItemByColNumber(colNumber), GetYCenterItemByRowNumber(rowNumber), enImageAlign.CenterX_CenterY)));
				}

				// add background bottom
				imageType = enImageType.Background_Bottom;
				IMyImageFile imageFile = myGraphic.FindImage((int)imageType);
				xLeftBackground = LevelLeft + imageFile.sizeSource.Width * (colNumber - 1);
				yBottomBackground = GetYCenterItemByRowNumber(Rows) + PlayZoneItemSize.Height / 2 + imageFile.sizeSource.Height;
				BackgroundPictures.Add(new MyBackground(new MyPicture(imageFile, xLeftBackground, yBottomBackground - imageFile.sizeSource.Height, enImageAlign.LeftTop)));
			}

			// add background left
			//imageType = enImageType.Background_left_baza;
			//x = leftTopPlayZone.X - myGraphic.FindImage((int)imageType).sizeSource.Width;
			//y = leftTopPlayZone.Y;
			//BackgroundPictures.Add(new BackgroundItem(myGraphic, imageType, x, y));

			// add buttons
			int x = LevelLeft;
			int y = yBottomBackground;
			enImageType[] buttonsID = { enImageType.Button_marshal, enImageType.Button_zuma, enImageType.Button_dog, enImageType.Button_krolik_s_lukom };
			foreach (enImageType buttonID in buttonsID)
			{
				Buttons.Add(new MyButton(new MyPicture(myGraphic.FindImage((int)buttonID), x, y, enImageAlign.LeftTop)));

				// next position
				x += (int)((float)myGraphic.FindImage((int)buttonID).sizeSource.Width * 1.3); // 1.3 shift of size button
			}

			// units will appear
			UnitsWillAppear.Add(new MyUnitWillAppear(1000 /*1 second*/, new MyUnit_EnemyZmeia(myGraphic, GetComputerPlayerID(), GetXCenterItemByColNumber(Cols) + PlayZoneItemSize.Width / 2, GetYCenterItemByRowNumber(1 /*row*/))));
			UnitsWillAppear.Add(new MyUnitWillAppear(3000 /*1 second*/, new MyUnit_EnemyPauk(myGraphic, GetComputerPlayerID(), GetXCenterItemByColNumber(Cols) + PlayZoneItemSize.Width / 2, GetYCenterItemByRowNumber(2 /*row*/))));
		}

		protected int GetXCenterItemByColNumber(int col)
		{
			return (col - 1) * PlayZoneItemSize.Width + LevelLeft + PlayZoneItemSize.Width / 2;
		}

		protected int GetYCenterItemByRowNumber(int row)
		{
			return (row - 1) * PlayZoneItemSize.Height + LevelTop + PlayZoneItemSize.Height / 2;
		}

		public int GetRow(MyRectangle rect)
		{
			int yCenter = (rect.Y + rect.Height / 2);
			float y = yCenter - LevelTop;
			float row = (y / (float)PlayZoneItemSize.Height) + 1;
			return (int)row;
		}

		public int GetCol(MyRectangle rect)
		{
			int xCenter = (rect.X + rect.Width / 2);
			float x = xCenter - LevelLeft;
			float col = (x / (float)PlayZoneItemSize.Width) + 1;
			return (int)col;
		}
	}
}
