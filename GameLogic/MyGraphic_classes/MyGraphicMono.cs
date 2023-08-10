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

			// rectangle
			TextureRectangleBorder_MonoGame = new Microsoft.Xna.Framework.Graphics.Texture2D(graphicsDevice, 1, 1);
			TextureRectangleFill_MonoGame = new Microsoft.Xna.Framework.Graphics.Texture2D(graphicsDevice, 1, 1);

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
			MyPoint posLeftTopSource = ImageAlign.CalculateLeftTopPos(pt, imageAlign, imageFile.sizeSource.Width, imageFile.sizeSource.Height);

			// calculate x,y,w,h Draw
			float xDraw = posLeftTopSource.X * XStretchCoef;
			float yDraw = posLeftTopSource.Y * YStretchCoef;
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
			RectSource.X = posLeftTopSource.X;
			RectSource.Y = posLeftTopSource.Y;
			RectSource.Width = (int)imageFile.sizeSource.Width;
			RectSource.Height = (int)imageFile.sizeSource.Height;
			return RectSource;
		}

		public MyRectangle DrawPartImage(object context, MyPointF pt, IMyImageFile imageFile, MyRectangle part, enImageAlign imageAlign, ref MyRectangle RectDraw)
		{
			// calculate source x,y
			MyPoint posLeftTopSource = ImageAlign.CalculateLeftTopPos(pt, imageAlign, part.Width, part.Height);

			// calculate x,y,w,h Draw
			float xDraw = posLeftTopSource.X * XStretchCoef;
			float yDraw = posLeftTopSource.Y * YStretchCoef;
			float wDraw = (float)part.Width * XStretchCoef;
			float hDraw = (float)part.Height * YStretchCoef;

			// source rectangle
			Microsoft.Xna.Framework.Point sourceSize = new Microsoft.Xna.Framework.Point((int)part.Width, (int)part.Height);
			Microsoft.Xna.Framework.Point sourcePosition = new Microsoft.Xna.Framework.Point((int)part.X, (int)part.Y);
			Microsoft.Xna.Framework.Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle(sourcePosition, sourceSize);

			// dest rectangle
			Microsoft.Xna.Framework.Point destSize = new Microsoft.Xna.Framework.Point((int)wDraw, (int)hDraw);
			Microsoft.Xna.Framework.Point destPosition = new Microsoft.Xna.Framework.Point((int)xDraw, (int)yDraw);
			Microsoft.Xna.Framework.Rectangle destRectangle = new Microsoft.Xna.Framework.Rectangle(destPosition, destSize);

			// draw
			spriteBatch_MonoGame.Draw((imageFile as MyImageFileMono).texture2D_MonoGame, destRectangle, sourceRectangle, MyGraphicMono.GetDefaultBgColor());

			// result Draw
			RectDraw.X = (int)xDraw;
			RectDraw.Y = (int)yDraw;
			RectDraw.Width = (int)wDraw;
			RectDraw.Height = (int)hDraw;

			// return Source
			MyRectangle RectSource = new MyRectangle();
			RectSource.X = posLeftTopSource.X;
			RectSource.Y = posLeftTopSource.Y;
			RectSource.Width = (int)part.Width;
			RectSource.Height = (int)part.Height;
			return RectSource;
		}

		public void DrawRectangle(object context, MyRectangle rect, MyColor color, MyColor bgColor, int lineWidth)
		{
			Microsoft.Xna.Framework.Point position = new Microsoft.Xna.Framework.Point(rect.X, rect.Y);
			Microsoft.Xna.Framework.Point size = new Microsoft.Xna.Framework.Point(rect.Width, rect.Height);

			// rectangle border
			Microsoft.Xna.Framework.Rectangle rectangleBorder = new Microsoft.Xna.Framework.Rectangle(position, size);

			// draw border
			Microsoft.Xna.Framework.Color xnaColorBorder = new Microsoft.Xna.Framework.Color(color.R, color.G, color.B);
			TextureRectangleBorder_MonoGame.SetData(new[] { xnaColorBorder });
			spriteBatch_MonoGame.Draw(this.TextureRectangleBorder_MonoGame, rectangleBorder, MyGraphicMono.GetDefaultBgColor());

			// rectangle fill
			Microsoft.Xna.Framework.Color xnaColorFill = new Microsoft.Xna.Framework.Color(bgColor.R, bgColor.G, bgColor.B);
			TextureRectangleFill_MonoGame.SetData(new[] { xnaColorFill });
			Microsoft.Xna.Framework.Rectangle rectangleFill = new Microsoft.Xna.Framework.Rectangle(position, size);
			//rectangleFill.Inflate(10, 10);

			// draw fill
			spriteBatch_MonoGame.Draw(this.TextureRectangleFill_MonoGame, rectangleFill, MyGraphicMono.GetDefaultBgColor());
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

		public MySize ConvertSourceToScreen(MySize sizeSource)
		{
			MySize sizeScreen = new MySize((int)((float)sizeSource.Width * XStretchCoef), (int)((float)sizeSource.Height * YStretchCoef));
			return sizeScreen;
		}

		public MyPoint ConvertScreenToSource(MyPoint posScreen)
		{
			MyPoint posSource = new MyPoint((int)((float)posScreen.X / XStretchCoef), (int)((float)posScreen.Y / YStretchCoef));
			return posSource;
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

		// draw rectangle
		Microsoft.Xna.Framework.Graphics.Texture2D TextureRectangleBorder_MonoGame;
		Microsoft.Xna.Framework.Graphics.Texture2D TextureRectangleFill_MonoGame;

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