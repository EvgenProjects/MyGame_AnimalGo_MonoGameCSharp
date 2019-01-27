using System.Collections.Generic; // for List

// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	abstract class MyDialogAbstract : IMyDialog
	{
		// width/height
		public virtual int Width { get { return 300; } }
		public virtual int Height { get { return 240; } }

		// objects
		public List<IMyDialogItem> Items { get; protected set; }

		// is show rectangle
		protected bool ShowRectangle { get; set; }

		// constructor
		public MyDialogAbstract()
		{
			Items = new List<IMyDialogItem>();
		}

		// events
		public virtual void OnDraw(object context, IMyGraphic myGraphic)
		{
			// center dialog for scene (set Dialog x,y)
			int xDest = myGraphic.ScreenWidth / 2 - Width / 2;
			float xSource = xDest / myGraphic.XStretchCoef;
			int yDest = myGraphic.ScreenHeight / 2 - Height / 2;
			float ySource = yDest / myGraphic.YStretchCoef;

			// is show rectangle
			if (ShowRectangle)
			{
				// width/height
				float widthDest = Width * myGraphic.XStretchCoef;
				float heightDest = Height * myGraphic.YStretchCoef;

				// draw rectangle
				myGraphic.DrawRectangle(context, new MyRectangle(xDest, yDest, (int)widthDest, (int)heightDest), new MyColor(0, 0, 255), new MyColor(0, 255, 255), 1 /*line width*/);
			}
			
			// enum items
			MyDialogItem item = null;
			for (int i = 0; i < Items.Count; i++)
			{
				// offset from dialog
				item = Items[i] as MyDialogItem;
				if (item != null)
				{
					item.MyPicture.PosSource.X = xSource + item.PosSourceFromDialog.X;
					item.MyPicture.PosSource.Y = ySource + item.PosSourceFromDialog.Y;
				}

				// draw item
				Items[i].OnDraw(context, myGraphic);
			}
		}

		public virtual bool OnClickMouse(int xMouse, int yMouse, IMyGraphic myGraphic, IMyGame myGame)
		{
			// find button by mouse x,y
			IMyDialogItem found = Items.Find(item => item.GetDrawRect().Contains(xMouse, yMouse));
			if (found != null)
			{
				// unfocus all items in gameLevel.Buttons
				for (int i = 0; i < Items.Count; i++)
					Items[i].Focus = false;

				// set focus
				found.Focus = true;

				// result
				if (found.OnHandlerClickMouse != null)
					return found.OnHandlerClickMouse(xMouse, yMouse, myGraphic, this);
			}

			return false;
		}
	}
}