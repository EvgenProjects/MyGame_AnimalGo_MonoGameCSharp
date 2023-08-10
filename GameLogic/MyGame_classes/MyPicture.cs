// my namespaces
using MyGraphic_interfaces;

namespace MyGame_classes
{
	struct MyPicture
	{
		// image
		public MyPointF PosSource;

		public IMyImageFile ImageFile;

		// sprite
		int SpriteIndex;
		long LastTimeInMillisecondsWhenSpriteIndexChanged;

		public enImageAlign ImageAlign;

		MyRectangle RectDraw;
		MyRectangle RectSource;

		public MyPicture(IMyImageFile imageFile, int x, int y, enImageAlign imageAlign)
		{
			// sprite
			SpriteIndex = 0;
			LastTimeInMillisecondsWhenSpriteIndexChanged = 0;

			RectDraw = new MyRectangle(0, 0, 0, 0);
			RectSource = new MyRectangle(0, 0, 0, 0);

			PosSource = new MyPointF();
			PosSource.X = (int)x;
			PosSource.Y = (int)y;
			ImageFile = imageFile;
			ImageAlign = imageAlign;
		}

		public void ChangeSpriteIndex(long timeInMilliseconds)
		{
			if ((timeInMilliseconds - LastTimeInMillisecondsWhenSpriteIndexChanged) > 100) // 1/10 second
			{
				SpriteIndex++;
				LastTimeInMillisecondsWhenSpriteIndexChanged = timeInMilliseconds;
			}
		}

		public void OnDraw(object context, IMyGraphic myGraphic)
		{
			// is draw part
			if (ImageFile != null && ImageFile.SpriteOffsets != null && ImageFile.SpriteOffsets.Length > 0)
			{
				if (SpriteIndex < 0 || SpriteIndex >= ImageFile.SpriteOffsets.Length)
					SpriteIndex = 0;

				// draw part
				RectSource = myGraphic.DrawPartImage(context, PosSource, ImageFile, ImageFile.SpriteOffsets[SpriteIndex], ImageAlign, ref RectDraw);
				return;
			}

			// draw whole picture
			RectSource = myGraphic.DrawImage(context, PosSource, ImageFile, ImageAlign, ref RectDraw);
		}

		public MyRectangle GetSourceRect()
		{
			return RectSource;
		}

		public MyRectangle GetDrawRect()
		{
			return RectDraw;
		}

		public bool IsPictureInsideLevel(int levelLeft, int levelTop, int levelWidth, int levelHeight)
		{
			// check bounds
			if (RectSource.Width > 0 && RectSource.Height > 0)
			{
				if ((RectSource.X + RectSource.Width) < levelLeft)
					return false;
				if (RectSource.X > (levelLeft + levelWidth))
					return false;
			}
			return true;
		}
	}
}
