using System.Collections.Generic; // for List
using System; // for Type
using MyUnits.interfaces;
using MyGame.interfaces;
using MyLevels.interfaces;
using MyUnits;
using MyGame;

namespace MyLevels
{
	abstract class MyLevel : IMyLevel
	{
		// width/height
		public virtual int LevelWidth { get { return 680; } }
		public virtual int LevelHeight { get { return 380; } }
		public virtual int LevelLeft { get { return 16; } }
		public virtual int LevelTop { get { return 18; } }

		protected MySize PlayZoneItemSize = new MySize(64, 80); ///// !!!!!

		// time statistic
		protected int HowMuchTimeInMilliseconds_TakeNextTurn = 0;
		protected int HowMuchTimeInMilliseconds_TakeDraw = 0;

        // objects
        public MyTexture2DAnimation? ActiveButtonInActionZone { get; set; }
        public List<IUnit> Units { get; protected set; }
		public List<IBackgroundSquad> BackgroundPictures { get; protected set; }
        public List<IUnitWillAppear> UnitsWillAppear { get; protected set; }
		public List<IFire> Fires { get; protected set; }
		public List<IAnimPictureDieByTime> AnimPicturesDieByTime { get; protected set; }
        public List<MyTexture2DAnimation?> ButtonsInActionZone { get; protected set; }

        // constructor
        public MyLevel()
		{
			Units = new List<IUnit>();
			BackgroundPictures = new List<IBackgroundSquad>();
	        ButtonsInActionZone = new List<MyTexture2DAnimation?>();
            UnitsWillAppear = new List<IUnitWillAppear>();
			Fires = new List<IFire>();
            AnimPicturesDieByTime = new List<IAnimPictureDieByTime>();
		}

		// abstract
		public abstract int GetCols();
		public abstract int GetRows();
		public abstract IUnitWillAppear[] GetUnitsWillAppear(IMyGraphic myGraphic);
		public abstract enImageType GetBackgroundImageType(int row, int col);
		public abstract enImageType[] GetButtons();

		// multiplayer
		public virtual int[] GetPlayerIDs()
		{
			return null;
		}

		public virtual string GetPlayerName(int playerID)
		{
			return "";
		}

		public virtual int GetMyPlayerID()
		{
			return 0;
		}

		public int GetComputerPlayerID()
		{
			return 1000; // any number
		}

		public virtual bool IsTeam(int playerID1, int playerID2)
		{
			if (playerID1 != playerID2 && (playerID1 == GetComputerPlayerID() || playerID2 == GetComputerPlayerID()))
				return false;
			return true;
		}

		// load
		public virtual void OnLoad(IMyGraphic myGraphic)
		{
			LoadBackground(myGraphic);
			LoadButtons(myGraphic);
			LoadUnitsWillAppear(myGraphic);
		}

		public void LoadBackground(IMyGraphic myGraphic)
		{
			enImageType imageType;
			IMyTexture2D myTexture2D;

			// add level background
			for (int col = 1; col <= GetCols(); col++)
			{
				for (int row = 1; row <= GetRows(); row++)
				{
					// image type
					imageType = GetBackgroundImageType(row, col);

					// x,y
					int x = GetXCenterItemByColNumber(col);
					int y = GetYCenterItemByRowNumber(row);

                    // image file
                    myTexture2D = myGraphic.FindImage(imageType);

                    // picture
                    MyTexture2DAnimation myTexture2DAnimation = new MyTexture2DAnimation(myTexture2D, x, y);

					//add backgroud
					BackgroundPictures.Add(new BackgroundSquad_Template(myTexture2DAnimation));
				}

				// add background bottom
				imageType = enImageType.Background_Bottom;
                myTexture2D = myGraphic.FindImage(imageType);
				int xLeftBackground = LevelLeft + myTexture2D.sizeSource.Width * (col - 1);
				int yBottomBackground = GetYCenterItemByRowNumber(GetRows()) + PlayZoneItemSize.Height / 2 + myTexture2D.sizeSource.Height;
				BackgroundPictures.Add(new BackgroundSquad_Template(new MyTexture2DAnimation(myTexture2D, xLeftBackground + myTexture2D.sizeSource.Width / 2, yBottomBackground - myTexture2D.sizeSource.Height/2)));
			}
		}

		public void LoadButtons(IMyGraphic myGraphic)
		{
			// get buttons
			enImageType[] buttonsID = GetButtons();

			// get x,y for buttons
			int xCenter = GetXCenterItemByColNumber(1);
            int yCenter = GetYCenterItemByRowNumber(GetRows() + 1);

			// enum buttons
			foreach (enImageType buttonID in buttonsID)
			{
                // add buttons
                ButtonsInActionZone.Add( new MyTexture2DAnimation(myGraphic.FindImage(buttonID), xCenter, yCenter) );

				// next position
				xCenter += (int)((float)myGraphic.FindImage(buttonID).sizeSource.Width * 1.3); // 1.3 shift of size button
			}
		}

		public void LoadUnitsWillAppear(IMyGraphic myGraphic)
		{
			//clear
			UnitsWillAppear.Clear();

			// add
			UnitsWillAppear.AddRange(GetUnitsWillAppear(myGraphic));
		}

		public virtual void OnNextTurn(long timeInMilliseconds, IMyGraphic myGraphic)
		{
			// statistic: take time
			int time1 = System.Environment.TickCount;

			// check
			if (timeInMilliseconds == 0)
				return;

			// Units
			for (int i = (Units.Count - 1); i >= 0; i--)
			{
				// delete
				if (Units[i].IsNeedDelete)
					Units.RemoveAt(i);
				else
					Units[i].OnNextTurn(timeInMilliseconds, myGraphic, this, true/*move*/);
			}

			// Fires
			for (int i = (Fires.Count - 1); i >= 0; i--)
			{
				// delete
				if (Fires[i].IsNeedDelete)
					Fires.RemoveAt(i);
				else
					Fires[i].OnNextTurn(timeInMilliseconds, myGraphic, this);
			}

			// Animations
			for (int i = (AnimPicturesDieByTime.Count - 1); i >= 0; i--)
			{
				// delete
				if (AnimPicturesDieByTime[i].IsNeedDelete)
                    AnimPicturesDieByTime.RemoveAt(i);
				else
                    AnimPicturesDieByTime[i].OnNextTurn(timeInMilliseconds);
			}

			// Units will appear
			for (int i = (UnitsWillAppear.Count - 1); i >= 0; i--)
			{
				UnitsWillAppear[i].OnNextTurn(timeInMilliseconds, myGraphic, this);

				// delete IGameUnitWillAppear and create IGameUnit
				if (UnitsWillAppear[i].IsNeedDelete)
				{
					// delete UnitWillAppear
					UnitsWillAppear.RemoveAt(i);
				}
			}

			// statistic: take time
			int time2 = System.Environment.TickCount;
			HowMuchTimeInMilliseconds_TakeNextTurn = time2 - time1;
		}

		public virtual void OnDraw(IMyGraphic myGraphic)
		{
			// Background picture
			for (int i = 0; i < BackgroundPictures.Count; i++)
				BackgroundPictures[i].OnDraw(myGraphic);

            // draw active button
            if (ActiveButtonInActionZone != null)
                myGraphic.DrawRectangle(ActiveButtonInActionZone.Value.GetRectInScenaPoints(myGraphic).Inflate(3), new MyColor(0, 0, 255), new MyColor(0, 255, 255), 3 /*line width*/);
            
			// Units
            for (int i = 0; i < Units.Count; i++)
				Units[i].OnDraw(myGraphic);

			// Fires
			for (int i = 0; i < Fires.Count; i++)
				Fires[i].OnDraw(myGraphic);

			// Animations
			for (int i = 0; i < AnimPicturesDieByTime.Count; i++)
                AnimPicturesDieByTime[i].OnDraw(myGraphic);

			// Buttons
			for (int i = 0; i < ButtonsInActionZone.Count; i++)
                ButtonsInActionZone[i]?.OnDraw(myGraphic);

            // statistic
            string info = string.Format("NextTurn time={0} , Units count={1}", HowMuchTimeInMilliseconds_TakeNextTurn, Units.Count);
			myGraphic.DrawText(10, 0, info, 16/*text size*/, new MyColor(128, 128, 128));
		}

		public virtual bool OnClickMouse(int xMouse, int yMouse, IMyGraphic myGraphic)
		{
			// find button by mouse x,y
			MyTexture2DAnimation? button = ButtonsInActionZone.Find(item => item?.GetRectInScenaPoints(myGraphic).Contains(xMouse, yMouse) ?? false);
			if (button != null)
			{
				ActiveButtonInActionZone = button;
                return true;
			}

			// find background by mouse x,y
			IBackgroundSquad background = BackgroundPictures.Find(item => item.GetRectInScenaPoints(myGraphic).Contains(xMouse, yMouse));
			if (background != null)
				return background.OnClickMouse(xMouse, yMouse, myGraphic, this);

			return false;
		}

		public virtual void OnCreateUnit_WhenDeleteUnitWillAppear(IMyGraphic myGraphic, IUnitWillAppear gameUnitWillAppear)
		{
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

		public IUnitWillAppear AddEnemyUnit(IMyGraphic myGraphic, double timeInSeconds, int row, enImageType imageType)
		{
			// time in seconds
			int timeInMilliseconds = (int)(timeInSeconds * 1000);

			// player ID
			int playerID = GetComputerPlayerID();

			// position
			int xPosition = GetStartXPositionWhenUnitAppear();
			int yPosition = GetStartYPositionWhenUnitAppear(row);

			// create IMyUnitWillAppear
			IUnitWillAppear myUnitWillAppear = new UnitWillAppear_Template(timeInMilliseconds, xPosition, yPosition, playerID, imageType);
			
			// result
			return myUnitWillAppear;
		}

		public int GetStartXPositionWhenUnitAppear()
		{
			return GetXCenterItemByColNumber(GetCols()) + PlayZoneItemSize.Width / 2;
		}

		public int GetStartYPositionWhenUnitAppear(int row)
		{
			return GetYCenterItemByRowNumber(row);
		}
		
		public virtual bool IsLevelEnd()
		{
			// check
			if (UnitsWillAppear != null)
			{
				// check units will appear
				if (UnitsWillAppear.Count == 0)
				{
					IUnit unitEnemy = Units.Find(item =>
					{
						return !IsTeam(item.PlayerID, GetMyPlayerID());
					});

					if (unitEnemy == null)
						return true; // end level
				}
			}
			return false;
		}
	}
}
