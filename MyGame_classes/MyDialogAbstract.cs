using System.Collections.Generic; // for List

// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	abstract class MyDialogAbstract : IMyDialog
	{
		// width/height
		protected virtual int WidthSource { get; set; }
		protected virtual int HeightSource { get; set; }

		// is show rectangle
		protected virtual bool IsDrawRectangle { get { return false; } }
		protected MyColor BorderColorForRectangle { get { return new MyColor(0, 0, 255); } }
		protected MyColor BackgroundColorForRectangle { get { return new MyColor(0, 255, 255); } }
		protected int LineWidthForRectangle { get { return 1; } }
		
		// objects
		protected List<MyDialogItem> Items { get; private set; }

		// constructor
		public MyDialogAbstract()
		{
			Items = new List<MyDialogItem>();
		}

		// methods
		public IMyDialogItem AddPictureToCenterDialog(IMyGraphic myGraphic, enImageType type)
		{
			// center
			MyPoint posSource = new MyPoint(WidthSource/2, HeightSource/2); 

			// add picture
			return AddPictureToDialog(myGraphic, type, posSource, enImageAlign.CenterX_CenterY);
		}

		public IMyDialogItem AddPictureToDialog(IMyGraphic myGraphic, enImageType type, MyPoint posSource, enImageAlign align)
		{
			// find image
			IMyImageFile myImageFile = myGraphic.FindImage((int)type);

			// MyPicture
			MyPicture myPicture = new MyPicture(myImageFile, 0/* NOT USE (will be calculated when draw) */, 0/* NOT USE (will be calculated when draw) */, (enImageAlign)0/* NOT USE (will be calculated when draw) */);

			// calcualte
			MyPoint posSourceFromLeftTopDialog = ImageAlign.CalculateLeftTopPos(posSource, align, myPicture.ImageFile.sizeSource);
				
			// add dialog item
			MyDialogItem dlgItem = new MyDialogItem(myPicture, posSourceFromLeftTopDialog);
									
			// add
			Items.Add(dlgItem);

			// result
			return dlgItem;
		}

		public void CloseDialog(IMyGame myGame)
		{
			myGame.Dialog = null;
		}

		// events
		public virtual void OnDraw(object context, IMyGraphic myGraphic)
		{
			// calculate xDialogForDraw, yDialogForDraw
			MySize sizeDialogForDraw = myGraphic.ConvertSourceToScreen(new MySize(WidthSource, HeightSource));
			
			// center dialog on game scene !!!!!
			MyPoint posDialogForDraw = new MyPoint(myGraphic.ScreenWidth / 2 - sizeDialogForDraw.Width / 2, myGraphic.ScreenHeight / 2 - sizeDialogForDraw.Height / 2);

			// is draw rectangle
			if (IsDrawRectangle)
			{
				// draw rectangle
				myGraphic.DrawRectangle(
					context, 
					new MyRectangle(posDialogForDraw.X, posDialogForDraw.Y, sizeDialogForDraw.Width, sizeDialogForDraw.Height).Inflate(3), 
					BorderColorForRectangle,
					BackgroundColorForRectangle,
					LineWidthForRectangle
				);
			}
			
			// draw items
			MyPoint posDialogSource = myGraphic.ConvertScreenToSource(posDialogForDraw);
			for (int i = 0; i < Items.Count; i++)
			{
				if (Items[i] == null)
					continue;

				// add left top pos dialog
				Items[i].MyPicture.ImageAlign = enImageAlign.LeftTop;
				Items[i].MyPicture.PosSource.X = Items[i].PosSourceFromLeftTopDialog.X + posDialogSource.X;
				Items[i].MyPicture.PosSource.Y = Items[i].PosSourceFromLeftTopDialog.Y + posDialogSource.Y;

				// draw
				Items[i].OnDraw(context, myGraphic);
			}
		}

		public virtual bool OnClickMouse(int xMouse, int yMouse, IMyGraphic myGraphic, IMyGame myGame)
		{
			// find button by mouse x,y
			IMyDialogItem found = Items.Find(item => item.GetDrawRect().Contains(xMouse, yMouse));
			if (found == null)
				return false;
			
			// unfocus all items in gameLevel.Buttons
			for (int i = 0; i < Items.Count; i++)
				Items[i].Focus = false;

			// set focus
			found.Focus = true;

			// result
			if (found.OnHandlerClickMouse != null)
				return found.OnHandlerClickMouse(xMouse, yMouse, myGraphic, this);
			return false;
		}
	}
}