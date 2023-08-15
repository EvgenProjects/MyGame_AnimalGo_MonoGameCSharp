using Microsoft.Xna.Framework.Graphics;
using MyGame.interfaces;

namespace MyGame
{
	public struct MyTexture2DAnimation
	{
		// image
		public MyPointF Position;

		public IMyTexture2D MyTexture2D { get; }

		// sprite
		int SpriteIndex;
		long LastTimeInMillisecondsWhenSpriteIndexChanged;

		public MyTexture2DAnimation(IMyTexture2D myTexture2D, int xCenter, int yCenter)
		{
			SpriteIndex = 0;
			LastTimeInMillisecondsWhenSpriteIndexChanged = 0;
			Position = new MyPointF(xCenter, yCenter);
            MyTexture2D = myTexture2D;
		}

		public void ChangeSpriteIndex(long timeInMilliseconds)
		{
			if ((timeInMilliseconds - LastTimeInMillisecondsWhenSpriteIndexChanged) > 100) // 1/10 second
			{
				SpriteIndex++;
				LastTimeInMillisecondsWhenSpriteIndexChanged = timeInMilliseconds;
			}
		}

		public void OnDraw(IMyGraphic myGraphic)
        {
            // is draw part
			if (MyTexture2D != null && MyTexture2D.SpriteOffsets != null && MyTexture2D.SpriteOffsets.Length > 0)
			{
				if (SpriteIndex < 0 || SpriteIndex >= MyTexture2D.SpriteOffsets.Length)
					SpriteIndex = 0;

				// draw part
				myGraphic.DrawPartImage(Position, MyTexture2D, MyTexture2D.SpriteOffsets[SpriteIndex]);
				return;
			}

			// draw whole picture
			myGraphic.DrawImage(Position, MyTexture2D);
		}

        public MyRectangle GetRectInScenaPoints(IMyGraphic myGraphic)
        {
            // is part
            if (MyTexture2D != null && MyTexture2D.SpriteOffsets != null && MyTexture2D.SpriteOffsets.Length > 0 && SpriteIndex < MyTexture2D.SpriteOffsets.Length)
            {
				return myGraphic.GetRectInScenaPoints(Position, MyTexture2D, MyTexture2D.SpriteOffsets[SpriteIndex]);
            }

            return myGraphic.GetRectInScenaPoints(Position, MyTexture2D, null);
        }
        
		public bool IsPictureInsideLevel(IMyGraphic myGraphic, int levelLeft, int levelTop, int levelWidth, int levelHeight)
		{
			// check bounds
			var drawRect = GetRectInScenaPoints(myGraphic);

            if (drawRect.Width > 0 && drawRect.Height > 0)
			{
				if ((drawRect.X + drawRect.Width) < levelLeft)
					return false;
				if (drawRect.X > (levelLeft + levelWidth))
					return false;
			}
			return true;
		}
	}
}
