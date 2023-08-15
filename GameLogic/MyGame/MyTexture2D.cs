using System; // for Exception
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MyGame.interfaces;

namespace MyGame
{
	class MyTexture2D : IMyTexture2D
    {
        public MySize sizeSource { get; protected set; }
        public MyRectangle[] SpriteOffsets { get; set; }
        public Texture2D Texture2D { get; protected set; }
        public enImageType ImageType { get; protected set; }

        protected bool IsLoaded { get; set; }
                
		// constructor
        public MyTexture2D(ContentManager contentManager_MonoGame, string pathImage, enImageType imageType)
		{
			try
			{
				// change path
				pathImage = pathImage.Replace("\\", "/").Replace("//", "/").Replace(".png", "");

                // load image
                Texture2D = contentManager_MonoGame.Load<Texture2D>(pathImage);

				// set info
				sizeSource = new MySize(Texture2D.Bounds.Width, Texture2D.Bounds.Height);
				IsLoaded = sizeSource.Width > 0 && sizeSource.Height > 0;
				this.ImageType = imageType;
			}
			catch (Exception exception)
			{
			}
		}
	}
}