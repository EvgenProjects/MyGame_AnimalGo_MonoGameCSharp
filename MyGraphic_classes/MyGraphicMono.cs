#if !WPF_APPLICATION // is NOT wpf application

using System.Collections.Generic;// for Dictionary

// my namespaces
using MyGraphic_interfaces;

namespace MyGraphic_classes
{
	class MyGraphicMono : IMyGraphic
	{
		public MyGraphicMono(Microsoft.Xna.Framework.Content.ContentManager contentManager_MonoGame, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_MonoGame, Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice)
		{
			this.contentManager_MonoGame = contentManager_MonoGame;
			this.spriteBatch_MonoGame = spriteBatch_MonoGame;
			this.graphicsDevice = graphicsDevice;

			// images
			Images = new Dictionary<int, IMyImageFile>();

			// Stretch default
			XStretchCoef = 1.0f;
			YStretchCoef = 1.0f;
		}

		public IMyImageFile LoadImageFromFile(string pathImage, int imageID)
		{
			MyImageFileMono image = new MyImageFileMono(contentManager_MonoGame, pathImage, imageID);

			Images.Add(imageID, image);

			return image;
		}

		public MyRectangle DrawImage(object context, MyPointF pt, IMyImageFile imageFile, enImageAlign imageAlign, ref MyRectangle RectDraw)
		{
			// calculate source x,y
			float xSource = pt.X;
			float ySource = pt.Y;
			if (imageAlign == enImageAlign.CenterX_CenterY)
			{
				xSource = xSource - imageFile.sizeSource.Width / 2;
				ySource = ySource - imageFile.sizeSource.Height / 2;
			}

			// calculate x,y,w,h Draw
			float xDraw = xSource * XStretchCoef;
			float yDraw = ySource * YStretchCoef;
			float wDraw = (float)imageFile.sizeSource.Width * XStretchCoef;
			float hDraw = (float)imageFile.sizeSource.Height * YStretchCoef;

			// rectangle
			Microsoft.Xna.Framework.Point size = new Microsoft.Xna.Framework.Point((int)wDraw, (int)hDraw);
			Microsoft.Xna.Framework.Point position = new Microsoft.Xna.Framework.Point((int)xDraw, (int)yDraw);
			Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle(position, size);

			// draw
			spriteBatch_MonoGame.Draw((imageFile as MyImageFileMono).texture2D_MonoGame, rectangle, MyGraphicMono.GetDefaultBgColor());

			// result Draw
			RectDraw.X = (int)xDraw;
			RectDraw.Y = (int)yDraw;
			RectDraw.Width = (int)wDraw;
			RectDraw.Height = (int)hDraw;

			// return Source
			MyRectangle RectSource = new MyRectangle();
			RectSource.X = (int)xSource;
			RectSource.Y = (int)ySource;
			RectSource.Width = (int)imageFile.sizeSource.Width;
			RectSource.Height = (int)imageFile.sizeSource.Height;
			return RectSource;
		}

		public MyRectangle DrawPartImage(object context, MyPointF pt, IMyImageFile imageFile, MyRectangle part, enImageAlign imageAlign, ref MyRectangle RectDraw)
		{
			return new MyRectangle();
		}

		public void DrawRectangle(object context, MyRectangle rect, MyColor color, MyColor bgColor, int lineWidth)
		{

		}

		public void DrawText(object context, int x, int y, string text, float size, MyColor color)
		{

		}

		public void SendMessageToReDraw()
		{
			// no need
		}

		public IMyImageFile FindImage(int imageID)
		{
			IMyImageFile image;
			if (Images.TryGetValue(imageID, out image))
				return image;
			return null;
		}

		// my additional attributes
		protected Microsoft.Xna.Framework.Content.ContentManager contentManager_MonoGame { get; set; }
		public Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_MonoGame { get; set; }
		public Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice { get; set; }

		// my static attributes
		public static Microsoft.Xna.Framework.Color GetDefaultBgColor()
		{
			return new Microsoft.Xna.Framework.Color(255, 255, 255);
		}

		// images
		public Dictionary<int, IMyImageFile> Images { get; protected set; }

		// Stretch
		public float XStretchCoef { get; set; }
		public float YStretchCoef { get; set; }

		// width / height
		public int ScreenWidth { get; set; }
		public int ScreenHeight { get; set; }
	}
}

#endif