using System.Collections.Generic; // for List

// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	class MyLevel : IMyLevel
	{
		// width/height
		public virtual int LevelWidth { get; protected set; }
		public virtual int LevelHeight { get; protected set; }
		public virtual int LevelLeft { get; protected set; }
		public virtual int LevelTop { get; protected set; }

		// objects
		public List<IMyUnit> Units { get; protected set; }
		public List<IMyBackground> BackgroundPictures { get; protected set; }
		public List<IMyButton> Buttons { get; protected set; }
		public List<IMyUnitWillAppear> UnitsWillAppear { get; protected set; }
		public List<IMyFire> Fires { get; protected set; }
		public List<IMyAnimation> Animations { get; protected set; }

		public MyLevel()
		{
			Units = new List<IMyUnit>();
			BackgroundPictures = new List<IMyBackground>();
			Buttons = new List<IMyButton>();
			UnitsWillAppear = new List<IMyUnitWillAppear>();
			Fires = new List<IMyFire>();
			Animations = new List<IMyAnimation>();
		}

		// multiplayer
		public virtual int[] GetPlayerIDs()
		{
			return null;
		}

		public virtual string GetPlayerName(int playerID)
		{
			return "";
		}

		public virtual bool IsTeam(int playerID1, int playerID2)
		{
			return false;
		}

		public virtual int GetMyPlayerID()
		{
			return 0;
		}

		// load
		public virtual void OnLoad(IMyGraphic myGraphic)
		{

		}

		public virtual void OnNextTurn(long timeInMilliseconds, IMyGame myGame)
		{
			if (timeInMilliseconds == 0)
				return;

			// Units
			for (int i = (Units.Count - 1); i >= 0; i--)
			{
				// delete
				if (Units[i].IsNeedDelete)
					Units.RemoveAt(i);
				else
					Units[i].OnNextTurn(timeInMilliseconds, myGame.Graphic, this);
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
	}
}
