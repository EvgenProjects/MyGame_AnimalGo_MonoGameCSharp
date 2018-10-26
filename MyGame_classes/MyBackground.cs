// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	class MyBackground : IMyBackground
	{
		// image
		protected MyPicture MyPicture;

		public MyBackground(MyPicture myPicture)
		{
			MyPicture = myPicture;
		}

		// events
		public virtual void OnDraw(object context, IMyGraphic myGraphic)
		{
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
			return false;
		}
	}
}
