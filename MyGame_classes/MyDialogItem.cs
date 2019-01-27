// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	class MyDialogItem : IMyDialogItem
	{
		// focus
		public bool Focus { get; set; }

		// image
		public MyPicture MyPicture;

		// handler
		public HandlerClickMouse OnHandlerClickMouse { get; set; }

		// offset from Dialog
		public MyPointF PosSourceFromDialog { get; set; }

		public MyDialogItem(MyPicture myPicture)
		{
			PosSourceFromDialog = myPicture.PosSource;
			MyPicture = myPicture;
			Focus = false;
		}

		// events
		public virtual void OnDraw(object context, IMyGraphic myGraphic)
		{
			if (myGraphic == null)
				return;

			// focus rectangle
			if (Focus && myGraphic != null)
				myGraphic.DrawRectangle(context, MyPicture.GetDrawRect(), new MyColor(0, 0, 255), new MyColor(0, 255, 255), 1 /*line width*/);

			// image
			MyPicture.OnDraw(context, myGraphic);
		}

		public virtual MyRectangle GetSourceRect()
		{
			return MyPicture.GetSourceRect();
		}

		public virtual MyRectangle GetDrawRect()
		{
			return MyPicture.GetDrawRect();
		}
	}
}
