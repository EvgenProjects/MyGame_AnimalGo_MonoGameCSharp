// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	class MyAnimation : IMyAnimation
	{
		// delete Flag
		public bool IsNeedDelete { get; set; }

		// image
		public MyPicture MyPicture;

		public long TimeAnimationInMilliseconds = 0;
		private long TimeCreatedInMilliseconds = 0;

		public MyAnimation(long timeAnimationInMilliseconds, MyPicture myPicture)
		{
			MyPicture = myPicture;
			IsNeedDelete = false;
			TimeAnimationInMilliseconds = timeAnimationInMilliseconds;
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

		public virtual void OnNextTurn(long timeInMilliseconds)
		{
			if (TimeCreatedInMilliseconds == 0)
				TimeCreatedInMilliseconds = timeInMilliseconds;

			// check
			if (IsNeedDelete)
				return;

			// is elipsed
			if (timeInMilliseconds > (TimeCreatedInMilliseconds + TimeAnimationInMilliseconds))
			{
				IsNeedDelete = true;
			}
		}
	}
}
