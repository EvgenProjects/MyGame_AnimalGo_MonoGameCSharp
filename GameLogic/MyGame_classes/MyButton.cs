// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	class MyButton : IMyButton
	{
		// focus
		public bool Focus { get; set; }

		// image
		public MyPicture MyPicture;

		public MyButton(MyPicture myPicture)
		{
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
				myGraphic.DrawRectangle(context, MyPicture.GetDrawRect().Inflate(3), new MyColor(0, 0, 255), new MyColor(0, 255, 255), 3 /*line width*/);

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

		public virtual bool OnClickMouse(int xMouse, int yMouse, IMyGraphic myGraphic, IMyLevel gameLevel)
		{
			// unfocus all items in gameLevel.Buttons
			for (int i=0; i< gameLevel.Buttons.Count; i++)
				gameLevel.Buttons[i].Focus = false;

			// set focus
			Focus = true;

			// result
			return true;
		}
	}
}
