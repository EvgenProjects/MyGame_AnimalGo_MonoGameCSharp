using MyGame;
using MyGame.interfaces;
using MyUnits.interfaces;

namespace MyUnits
{
	class AnimPictureDieByTime_Template : IAnimPictureDieByTime
	{
		// delete Flag
		public bool IsNeedDelete { get; set; }

		// image
		public MyTexture2DAnimation MyTexture2DAnimation;

		public long TimeAnimationInMilliseconds = 0;
		private long TimeCreatedInMilliseconds = 0;

		public AnimPictureDieByTime_Template(long timeAnimationInMilliseconds, MyTexture2DAnimation myTexture2DAnimation)
		{
            MyTexture2DAnimation = myTexture2DAnimation;
			IsNeedDelete = false;
			TimeAnimationInMilliseconds = timeAnimationInMilliseconds;
		}

		// events
		public virtual void OnDraw(IMyGraphic myGraphic)
		{
            MyTexture2DAnimation.OnDraw(myGraphic);
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
