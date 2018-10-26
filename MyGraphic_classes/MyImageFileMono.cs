#if !WPF_APPLICATION // is NOT wpf application

using System; // for Exception

// my namespaces
using MyGraphic_interfaces;

namespace MyGraphic_classes
{
	class MyImageFileMono : IMyImageFile
	{
		// constructor
		public MyImageFileMono(Microsoft.Xna.Framework.Content.ContentManager contentManager_MonoGame, string pathImage, int ImageID)
		{
			try
			{
				// change path
				pathImage = pathImage.Replace("\\", "/").Replace("//", "/").Replace(".png", "");

				// load image
				texture2D_MonoGame = contentManager_MonoGame.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(pathImage);

				// set info
				sizeSource = new MySize(texture2D_MonoGame.Bounds.Width, texture2D_MonoGame.Bounds.Height);
				IsLoaded = sizeSource.Width > 0 && sizeSource.Height > 0;
				this.ImageID = ImageID;
			}
			catch (Exception exception)
			{
			}
		}

		// implement attributes
		public MySize sizeSource { get; protected set; }
		public bool IsLoaded { get; protected set; }
		public int ImageID { get; protected set; }
		public MyRectangle[] SpriteOffsets { get; set; }

		// my additional attributes
		public Microsoft.Xna.Framework.Graphics.Texture2D texture2D_MonoGame { get; protected set; }
	}
}

#endif