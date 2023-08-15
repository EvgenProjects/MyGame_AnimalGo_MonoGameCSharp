using System.Collections.Generic;// for Dictionary
using Microsoft.Xna.Framework.Graphics;

using MyGame.interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace MyGame
{
	class MyGraphic : IMyGraphic
	{
        // draw rectangle
        Texture2D TextureRectangleBorder_MonoGame;
        Texture2D TextureRectangleFill_MonoGame;

        // images
        public Dictionary<enImageType, IMyTexture2D> Images { get; protected set; }

        // Stretch
        public float XStretchCoef { get; set; }
        public float YStretchCoef { get; set; }

        // width / height
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        
		public MyGraphic(Microsoft.Xna.Framework.Content.ContentManager contentManager, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
		{
			this.contentManager_MonoGame = contentManager;
			this.spriteBatch_MonoGame = spriteBatch;
			this.graphicsDevice = graphicsDevice;

			// rectangle
			TextureRectangleBorder_MonoGame = new Texture2D(graphicsDevice, 1, 1);
			TextureRectangleFill_MonoGame = new Texture2D(graphicsDevice, 1, 1);

			// images
			Images = new Dictionary<enImageType, IMyTexture2D>();

			// Stretch default
			XStretchCoef = 1.0f;
			YStretchCoef = 1.0f;
		}

        public IMyTexture2D FindImage(enImageType imageType)
        {
            IMyTexture2D image;
            if (Images.TryGetValue(imageType, out image))
                return image;
            return null;
        }
        
		public IMyTexture2D LoadImageFromFile(string pathImage, enImageType imageType)
		{
			MyTexture2D image = null;

            if (!Images.ContainsKey(imageType))
			{
				image = new MyTexture2D(contentManager_MonoGame, pathImage, imageType);

				Images.Add(imageType, image);
			}

			return image;
		}

		public void DrawImage(MyPointF ptCenter, IMyTexture2D myTexture2D)
		{
			if (myTexture2D == null)
				return;

            MyRectangle drawRect = ConvertScenaRectToDrawingRect(
                                new MyRectangle(ptCenter.X - myTexture2D.sizeSource.Width / 2,
                                               ptCenter.Y - myTexture2D.sizeSource.Height / 2,
                                               myTexture2D.sizeSource.Width,
                                               myTexture2D.sizeSource.Height));

			// rectangle
			Microsoft.Xna.Framework.Point size = new Microsoft.Xna.Framework.Point(drawRect.Width, drawRect.Height);
			Microsoft.Xna.Framework.Point position = new Microsoft.Xna.Framework.Point(drawRect.X, drawRect.Y);
			Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle(position, size);

			// draw
			spriteBatch_MonoGame.Draw(myTexture2D.Texture2D, rectangle, GetDefaultBgColor());
		}

        public void DrawPartImage(MyPointF ptCenter, IMyTexture2D myTexture2D, MyRectangle partRect)
		{
            if (myTexture2D == null)
                return;

            MyRectangle drawRect = ConvertScenaRectToDrawingRect(
                                new MyRectangle(ptCenter.X - partRect.Width / 2,
                                               ptCenter.Y - partRect.Height / 2,
                                               partRect.Width,
                                               partRect.Height));

            // source rectangle
            Microsoft.Xna.Framework.Point sourceSize = new Microsoft.Xna.Framework.Point(partRect.Width, partRect.Height);
			Microsoft.Xna.Framework.Point sourcePosition = new Microsoft.Xna.Framework.Point(partRect.X, partRect.Y);
			Microsoft.Xna.Framework.Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle(sourcePosition, sourceSize);

			// dest rectangle
			Microsoft.Xna.Framework.Point destSize = new Microsoft.Xna.Framework.Point(drawRect.Width, drawRect.Height);
			Microsoft.Xna.Framework.Point destPosition = new Microsoft.Xna.Framework.Point(drawRect.X, drawRect.Y);
			Microsoft.Xna.Framework.Rectangle destRectangle = new Microsoft.Xna.Framework.Rectangle(destPosition, destSize);

			// draw
			spriteBatch_MonoGame.Draw(myTexture2D.Texture2D, destRectangle, sourceRectangle, GetDefaultBgColor());
		}

        public MyPointF ConvertMousePtToScenaPoints(float xMouse, float yMouse)
        {
            MyPointF ptResult = new MyPointF();
			ptResult.X = xMouse / XStretchCoef;
            ptResult.Y = yMouse / YStretchCoef;
            return ptResult;
        }

        public MyRectangle GetRectInScenaPoints(MyPointF ptCenter, IMyTexture2D myTexture2D, MyRectangle? partRect)
		{
            MyRectangle rect = new MyRectangle();

            if (partRect == null)
            {
                rect.X = (int)(ptCenter.X - myTexture2D.sizeSource.Width / 2);
                rect.Y = (int)(ptCenter.Y - myTexture2D.sizeSource.Height / 2);
                rect.Width = myTexture2D.sizeSource.Width;
                rect.Height = myTexture2D.sizeSource.Height;
            }
            else
            {
                rect.X = (int)(ptCenter.X - partRect.Value.Width / 2);
                rect.Y = (int)(ptCenter.Y - partRect.Value.Height / 2);
                rect.Width = partRect.Value.Width;
                rect.Height = partRect.Value.Height;
            }
            return rect;
        }

        protected MyRectangle ConvertScenaRectToDrawingRect(MyRectangle scenaRect)
        {
            MyRectangle rect = new MyRectangle();
            rect.X = (int)(scenaRect.X * XStretchCoef);
            rect.Y = (int)(scenaRect.Y * YStretchCoef);
            rect.Width = (int)(scenaRect.Width * XStretchCoef);
            rect.Height = (int)(scenaRect.Height * YStretchCoef);
            return rect;
        }

		public void DrawRectangle(MyRectangle rect, MyColor color, MyColor bgColor, int lineWidth)
		{
            MyRectangle drawRect = ConvertScenaRectToDrawingRect(rect);
            
			Microsoft.Xna.Framework.Point position = new Microsoft.Xna.Framework.Point(drawRect.X, drawRect.Y);
			Microsoft.Xna.Framework.Point size = new Microsoft.Xna.Framework.Point(drawRect.Width, drawRect.Height);

			// rectangle border
			Microsoft.Xna.Framework.Rectangle rectangleBorder = new Microsoft.Xna.Framework.Rectangle(position, size);

			// draw border
			Microsoft.Xna.Framework.Color xnaColorBorder = new Microsoft.Xna.Framework.Color(color.R, color.G, color.B);
			TextureRectangleBorder_MonoGame.SetData(new[] { xnaColorBorder });
			spriteBatch_MonoGame.Draw(this.TextureRectangleBorder_MonoGame, rectangleBorder, MyGraphic.GetDefaultBgColor());

			// rectangle fill
			Microsoft.Xna.Framework.Color xnaColorFill = new Microsoft.Xna.Framework.Color(bgColor.R, bgColor.G, bgColor.B);
			TextureRectangleFill_MonoGame.SetData(new[] { xnaColorFill });
			Microsoft.Xna.Framework.Rectangle rectangleFill = new Microsoft.Xna.Framework.Rectangle(position, size);
			//rectangleFill.Inflate(10, 10);

			// draw fill
			spriteBatch_MonoGame.Draw(this.TextureRectangleFill_MonoGame, rectangleFill, GetDefaultBgColor());
		}

		public void DrawText(int x, int y, string text, float size, MyColor color)
		{

		}

		// my additional attributes
		protected Microsoft.Xna.Framework.Content.ContentManager contentManager_MonoGame { get; set; }
		public SpriteBatch spriteBatch_MonoGame { get; set; }
		public GraphicsDevice graphicsDevice { get; set; }

		// my static attributes
		public static Microsoft.Xna.Framework.Color GetDefaultBgColor()
		{
			return new Microsoft.Xna.Framework.Color(255, 255, 255);
		}
	}
}