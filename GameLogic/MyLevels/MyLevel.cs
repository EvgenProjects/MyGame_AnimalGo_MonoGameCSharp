using System.Collections.Generic; // for List
using System; // for Type
using MyUnits.interfaces;
using MyGame.interfaces;
using MyLevels.interfaces;
using MyUnits;
using MyGame;

namespace MyLevels
{
    enum enLevelTouch
    {
        Handled = 0,
        LoadNextLevel = 1
    }
    
	class MyLevel : IMyLevel
	{
		// width/height
		public virtual int LevelWidth => 680;
		public virtual int LevelHeight => 380;
		public virtual int LevelLeft => 16;
		public virtual int LevelTop => 18;
        public readonly MySize PlayZoneItemSize = new MySize(64, 80);

		// time statistic
		protected int HowMuchTimeInMilliseconds_TakeNextTurn = 0;
		protected int HowMuchTimeInMilliseconds_TakeDraw = 0;

        // dialogs
        public MyTexture2DAnimation? ButtonNextLevel { get; protected set; }

        // level info
        public virtual LevelInfo LevelInfo { get; protected set; }

        // objects
        public List<IUnit> MyUnits { get; protected set; }
        public List<IUnit> EnemyUnits { get; protected set; }
        public List<IFire> MyFires { get; protected set; }
        public List<IFire> EnemyFires { get; protected set; }
        public MyTexture2DAnimation? ActiveButtonInActionZone { get; set; }
		public List<IBackgroundSquad> BackgroundPictures { get; protected set; }
        public List<IUnitWillAppear> UnitsWillAppear { get; protected set; }
		public List<IAnimPictureDieByTime> AnimPicturesDieByTime { get; protected set; }
        public List<MyTexture2DAnimation?> ButtonsInActionZone { get; protected set; }

        // constructor
        public MyLevel()
		{
			MyUnits = new List<IUnit>();
            EnemyUnits = new List<IUnit>();
            MyFires = new List<IFire>();
            EnemyFires = new List<IFire>();
            BackgroundPictures = new List<IBackgroundSquad>();
	        ButtonsInActionZone = new List<MyTexture2DAnimation?>();
            UnitsWillAppear = new List<IUnitWillAppear>();
            AnimPicturesDieByTime = new List<IAnimPictureDieByTime>();
            ButtonNextLevel = null; // create when all enemy units destroied
		}

		// load
		public virtual void OnLoad(IMyGraphic myGraphic, int levelNumber)
		{
            LevelInfo = GetLevelInfo(levelNumber);

            ButtonNextLevel?.Unload();
            ButtonNextLevel = null;
            
			MyUnits.Clear();
            EnemyUnits.Clear();
            MyFires.Clear();
            EnemyFires.Clear();
            BackgroundPictures.Clear();
            ButtonsInActionZone.Clear();
            UnitsWillAppear.Clear();
            AnimPicturesDieByTime.Clear();

            LoadBackground(myGraphic);
			LoadButtons(myGraphic);
			LoadUnitsWillAppear(myGraphic);
		}

		public void LoadBackground(IMyGraphic myGraphic)
		{
			enImageType imageType;
			IMyTexture2D myTexture2D;

            // add level background
            for (int col = 1; col <= LevelInfo.ColsCount; col++)
			{
				for (int row = 1; row <= LevelInfo.RowsCount; row++)
				{
					// image type
					imageType = LevelInfo.FuncBackgroundImageType(row, col);

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
				int yBottomBackground = GetYCenterItemByRowNumber(LevelInfo.RowsCount) + PlayZoneItemSize.Height / 2 + myTexture2D.sizeSource.Height;
				BackgroundPictures.Add(new BackgroundSquad_Template(new MyTexture2DAnimation(myTexture2D, xLeftBackground + myTexture2D.sizeSource.Width / 2, yBottomBackground - myTexture2D.sizeSource.Height/2)));
			}
		}

		public void LoadButtons(IMyGraphic myGraphic)
		{
			// get buttons
			enImageType[] buttonsID = LevelInfo.Buttons;

			// get x,y for buttons
			int xCenter = GetXCenterItemByColNumber(1);
            int yCenter = GetYCenterItemByRowNumber(LevelInfo.RowsCount + 1) + GetYCenterItemByRowNumber(1)/2;

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
			UnitsWillAppear.AddRange(LevelInfo.UnitsWillAppear);
		}

		public virtual void OnNextTurn(long timeInMilliseconds, IMyGraphic myGraphic)
		{
			// statistic: take time
			int time1 = System.Environment.TickCount;

			// check
			if (timeInMilliseconds == 0)
				return;

			// is level end
			if (UnitsWillAppear.Count == 0 && EnemyUnits.Count == 0)
			{
				if (ButtonNextLevel==null)
				{
					int xCenter = LevelWidth / 2;
					int yCenter = LevelHeight / 2;
                    ButtonNextLevel = new MyTexture2DAnimation(myGraphic.FindImage(enImageType.Button_level_next), xCenter, yCenter);
				}
				return;
            }

            // my Units
            for (int i = (MyUnits.Count - 1); i >= 0; i--)
			{
				// delete
				if (MyUnits[i].IsNeedDelete)
                    MyUnits.RemoveAt(i);
				else
                    MyUnits[i].OnNextTurn(timeInMilliseconds, myGraphic, this, true/*move*/);
			}

            // enemy Units
            for (int i = (EnemyUnits.Count - 1); i >= 0; i--)
            {
                // delete
                if (EnemyUnits[i].IsNeedDelete)
                    EnemyUnits.RemoveAt(i);
                else
                    EnemyUnits[i].OnNextTurn(timeInMilliseconds, myGraphic, this, true/*move*/);
            }
            
			// my Fires
            for (int i = (MyFires.Count - 1); i >= 0; i--)
			{
				// delete
				if (MyFires[i].IsNeedDelete)
                    MyFires.RemoveAt(i);
				else
                    MyFires[i].OnNextTurn(timeInMilliseconds, myGraphic, this);
			}

            // enemy Fires
            for (int i = (EnemyFires.Count - 1); i >= 0; i--)
            {
                // delete
                if (EnemyFires[i].IsNeedDelete)
                    EnemyFires.RemoveAt(i);
                else
                    EnemyFires[i].OnNextTurn(timeInMilliseconds, myGraphic, this);
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
            
			// my Units
            for (int i = 0; i < MyUnits.Count; i++)
                MyUnits[i].OnDraw(myGraphic);

            // enemy Units
            for (int i = 0; i < EnemyUnits.Count; i++)
                EnemyUnits[i].OnDraw(myGraphic);
            
			// my Fires
            for (int i = 0; i < MyFires.Count; i++)
                MyFires[i].OnDraw(myGraphic);

            // enemy Fires
            for (int i = 0; i < EnemyFires.Count; i++)
                EnemyFires[i].OnDraw(myGraphic);
            
			// Animations
            for (int i = 0; i < AnimPicturesDieByTime.Count; i++)
                AnimPicturesDieByTime[i].OnDraw(myGraphic);

			// Buttons in Action zone
			for (int i = 0; i < ButtonsInActionZone.Count; i++)
                ButtonsInActionZone[i]?.OnDraw(myGraphic);

            // Buttons
            ButtonNextLevel?.OnDraw(myGraphic);

            // statistic
            //string info = string.Format("NextTurn time={0} , Units count={1}", HowMuchTimeInMilliseconds_TakeNextTurn, Units.Count);
            //myGraphic.DrawText(10, 0, info, 16/*text size*/, new MyColor(128, 128, 128));
        }

        public virtual enLevelTouch OnTouch(IMyGraphic myGraphic, int xTouch, int yTouch)
		{
			// find button by mouse x,y
			MyTexture2DAnimation? button = ButtonsInActionZone.Find(item => item?.GetRectInScenaPoints(myGraphic).Contains(xTouch, yTouch) ?? false);
			if (button != null)
			{
				ActiveButtonInActionZone = button;
                return enLevelTouch.Handled;
			}

			// find background by mouse x,y
			IBackgroundSquad background = BackgroundPictures.Find(item => item.GetRectInScenaPoints(myGraphic).Contains(xTouch, yTouch));
			if (background != null)
			{
				background.OnClickMouse(xTouch, yTouch, myGraphic, this);
                return enLevelTouch.Handled;
            }

			// click dialog button
            if (ButtonNextLevel?.GetRectInScenaPoints(myGraphic).Contains(xTouch, yTouch) ?? false)
            {
                return enLevelTouch.LoadNextLevel;
            }

            return enLevelTouch.Handled;
        }

        public virtual int GetXCenterItemByColNumber(int col)
		{
			return (col - 1) * PlayZoneItemSize.Width + LevelLeft + PlayZoneItemSize.Width / 2;
		}

        public virtual int GetYCenterItemByRowNumber(int row)
		{
			return (row - 1) * PlayZoneItemSize.Height + LevelTop + PlayZoneItemSize.Height / 2;
		}

		public virtual int GetRow(MyRectangle rect)
		{
			int yCenter = (rect.Y + rect.Height / 2);
			float y = yCenter - LevelTop;
			float row = (y / (float)PlayZoneItemSize.Height) + 1;
			return (int)row;
		}

		public virtual int GetCol(MyRectangle rect)
		{
			int xCenter = (rect.X + rect.Width / 2);
			float x = xCenter - LevelLeft;
			float col = (x / (float)PlayZoneItemSize.Width) + 1;
			return (int)col;
		}

        LevelInfo GetLevelInfo(int levelNumber)
        {
            Func<int, int, enImageType> funcBackground = (row, col) => (row + col) % 2 == 0 ? enImageType.Background_Level_Grass1 : enImageType.Background_Level_Grass2;
            
			if (levelNumber==1)
			{
				return new LevelInfo(10, 1,
					new enImageType[] { enImageType.Button_krolik_s_lukom },
                    funcBackground,
                    new IUnitWillAppear[]
					{
                        new UnitWillAppear_Template(2000 /*ms*/, 1 /*row*/, (myGraphic, xAppear,yAppear) => new Unit_EnemyZmeia(myGraphic, xAppear, yAppear)),
                        new UnitWillAppear_Template(8000 /*ms*/, 1 /*row*/, (myGraphic, xAppear,yAppear) => new Unit_EnemyZmeia(myGraphic, xAppear, yAppear))
                    }
                );
			}

            else if (levelNumber == 2)
            {
                return new LevelInfo(10, 2,
                    new enImageType[] { enImageType.Button_krolik_s_lukom },
                    funcBackground,
                    new IUnitWillAppear[]
                    {
                        new UnitWillAppear_Template(1000 /*ms*/, 1 /*row*/, (myGraphic, xAppear,yAppear) => new Unit_EnemyZmeia(myGraphic, xAppear, yAppear)),
                        new UnitWillAppear_Template(8000 /*ms*/, 2 /*row*/, (myGraphic, xAppear,yAppear) => new Unit_EnemyBird1(myGraphic, xAppear, yAppear))
                    }
                );
            }

            else if (levelNumber == 3)
            {
                return new LevelInfo(10, 3,
                    new enImageType[] { enImageType.Button_krolik_s_lukom },
                    funcBackground,
                    new IUnitWillAppear[]
                    {
                        new UnitWillAppear_Template(1000 /*ms*/, 1 /*row*/, (myGraphic, xAppear,yAppear) => new Unit_EnemyZmeia(myGraphic, xAppear, yAppear)),
                        new UnitWillAppear_Template(2000 /*ms*/, 2 /*row*/, (myGraphic, xAppear,yAppear) => new Unit_EnemyPauk(myGraphic, xAppear, yAppear)),
                        new UnitWillAppear_Template(8000 /*ms*/, 3 /*row*/, (myGraphic, xAppear,yAppear) => new Unit_EnemyZmeia(myGraphic, xAppear, yAppear))
                    }
                );
            }
            
			return new LevelInfo(0, 0, new enImageType[] { }, funcBackground, new IUnitWillAppear[] { }); // empty
		}
	}
}