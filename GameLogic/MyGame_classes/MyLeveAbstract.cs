using System.Collections.Generic; // for List
using System; // for Type
using System.Reflection; // forConstructorInfo

// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	abstract class MyLevelAbstract : IMyLevel
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
		public List<IMyUnit> Units { get; protected set; }
		public List<IMyBackground> BackgroundPictures { get; protected set; }
		public List<IMyButton> Buttons { get; protected set; }
		public List<IMyUnitWillAppear> UnitsWillAppear { get; protected set; }
		public List<IMyFire> Fires { get; protected set; }
		public List<IMyAnimation> Animations { get; protected set; }

		// constructor
		public MyLevelAbstract()
		{
			Units = new List<IMyUnit>();
			BackgroundPictures = new List<IMyBackground>();
			Buttons = new List<IMyButton>();
			UnitsWillAppear = new List<IMyUnitWillAppear>();
			Fires = new List<IMyFire>();
			Animations = new List<IMyAnimation>();
		}

		// abstract
		public abstract int GetCols();
		public abstract int GetRows();
		public abstract IMyUnitWillAppear[] GetUnitsWillAppear(IMyGraphic myGraphic);
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
			IMyImageFile imageFile;

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
					imageFile = myGraphic.FindImage((int)imageType);

					// picture
					MyPicture myPicture = new MyPicture(imageFile, x, y, enImageAlign.CenterX_CenterY);

					//add backgroud
					BackgroundPictures.Add(new MyBackground(myPicture));
				}

				// add background bottom
				imageType = enImageType.Background_Bottom;
				imageFile = myGraphic.FindImage((int)imageType);
				int xLeftBackground = LevelLeft + imageFile.sizeSource.Width * (col - 1);
				int yBottomBackground = GetYCenterItemByRowNumber(GetRows()) + PlayZoneItemSize.Height / 2 + imageFile.sizeSource.Height;
				BackgroundPictures.Add(new MyBackground(new MyPicture(imageFile, xLeftBackground, yBottomBackground - imageFile.sizeSource.Height, enImageAlign.LeftTop)));
			}
		}

		public void LoadButtons(IMyGraphic myGraphic)
		{
			// get buttons
			enImageType[] buttonsID = GetButtons();

			// get x,y for buttons
			int x = LevelLeft;
			int y = GetYCenterItemByRowNumber(GetRows() + 1);

			// enum buttons
			foreach (enImageType buttonID in buttonsID)
			{
				// add buttons
				Buttons.Add(new MyButton(new MyPicture(myGraphic.FindImage((int)buttonID), x, y, enImageAlign.LeftTop)));

				// next position
				x += (int)((float)myGraphic.FindImage((int)buttonID).sizeSource.Width * 1.3); // 1.3 shift of size button
			}
		}

		public void LoadUnitsWillAppear(IMyGraphic myGraphic)
		{
			//clear
			UnitsWillAppear.Clear();

			// add
			UnitsWillAppear.AddRange(GetUnitsWillAppear(myGraphic));
		}

		public virtual void OnNextTurn(long timeInMilliseconds, IMyGame myGame)
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
					Units[i].OnNextTurn(timeInMilliseconds, myGame.Graphic, this, true/*move*/);
			}

			// Fires
			for (int i = (Fires.Count - 1); i >= 0; i--)
			{
				// delete
				if (Fires[i].IsNeedDelete)
					Fires.RemoveAt(i);
				else
					Fires[i].OnNextTurn(timeInMilliseconds, myGame.Graphic, this);
			}

			// Animations
			for (int i = (Animations.Count - 1); i >= 0; i--)
			{
				// delete
				if (Animations[i].IsNeedDelete)
					Animations.RemoveAt(i);
				else
					Animations[i].OnNextTurn(timeInMilliseconds);
			}

			// Units will appear
			for (int i = (UnitsWillAppear.Count - 1); i >= 0; i--)
			{
				UnitsWillAppear[i].OnNextTurn(timeInMilliseconds, myGame.Graphic, this);

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

		public virtual void OnDraw(object context, IMyGraphic myGraphic)
		{
			// Background picture
			for (int i = 0; i < BackgroundPictures.Count; i++)
				BackgroundPictures[i].OnDraw(context, myGraphic);

			// Units
			for (int i = 0; i < Units.Count; i++)
				Units[i].OnDraw(context, myGraphic);

			// Fires
			for (int i = 0; i < Fires.Count; i++)
				Fires[i].OnDraw(context, myGraphic);

			// Animations
			for (int i = 0; i < Animations.Count; i++)
				Animations[i].OnDraw(context, myGraphic);

			// Buttons
			for (int i = 0; i < Buttons.Count; i++)
				Buttons[i].OnDraw(context, myGraphic);
		
			// statistic
			string info = string.Format("NextTurn time={0} , Units count={1}", HowMuchTimeInMilliseconds_TakeNextTurn, Units.Count);
			myGraphic.DrawText(context, 10, 0, info, 16/*text size*/, new MyColor(128, 128, 128));
		}

		public virtual bool OnClickMouse(int xMouse, int yMouse, IMyGraphic myGraphic)
		{
			// find button by mouse x,y
			IMyButton button = Buttons.Find(item => item.GetDrawRect().Contains(xMouse, yMouse));
			if (button!=null)
				return button.OnClickMouse(xMouse, yMouse, myGraphic, this);

			// find background by mouse x,y
			IMyBackground background = BackgroundPictures.Find(item => item.GetDrawRect().Contains(xMouse, yMouse));
			if (background != null)
				return background.OnClickMouse(xMouse, yMouse, myGraphic, this);

			return false;
		}

		public virtual void OnCreateUnit_WhenDeleteUnitWillAppear(IMyGraphic myGraphic, IMyUnitWillAppear gameUnitWillAppear)
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

		public IMyUnitWillAppear AddEnemyUnit(IMyGraphic myGraphic, double timeInSeconds, int row, Type obj)
		{
			// time in seconds
			int timeInMilliseconds = (int)(timeInSeconds * 1000);

			// player ID
			int playerID = GetComputerPlayerID();

			// position
			int xPosition = GetStartXPositionWhenUnitAppear();
			int yPosition = GetStartYPositionWhenUnitAppear(row);

			// create object (reflection)
			ConstructorInfo info = obj.GetConstructor(new Type[] { typeof(IMyGraphic), typeof(int), typeof(int), typeof(int) });
			object myUnit = info.Invoke(new object[] { myGraphic, playerID, xPosition, yPosition });

			// create IMyUnitWillAppear
			IMyUnitWillAppear myUnitWillAppear = new MyUnitWillAppear(timeInMilliseconds, (IMyUnit)myUnit);
			
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
					IMyUnit unitEnemy = Units.Find(item =>
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
