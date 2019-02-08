#if WPF_APPLICATION // is wpf application

using System.Windows; // for FlowDirection
using System.Windows.Media; // for Typeface
using System.IO; // for MemoryStream
using System.Windows.Controls; // for Canvas
using System.Collections.Generic;// for Dictionary
using System.Windows.Media.Imaging; // for BitmapImage

// my namespaces
using MyGraphic_interfaces;

namespace MyGraphic_classes
{
	class MyGraphicWPF : Canvas, IMyGraphic
	{
		public MyGraphicWPF()
		{
			// images
			Images = new Dictionary<int, IMyImageFile>();

			// Stretch default
			XStretchCoef = 1.0f;
			YStretchCoef = 1.0f;
		}

		public IMyImageFile LoadImageFromFile(string pathImage, int imageID)
		{
			MyImageFileWPF image = new MyImageFileWPF(pathImage, imageID);

			Images.Add(imageID, image);

			return image;
		}

		public MyRectangle DrawPartImage(object context, MyPointF pt, IMyImageFile imageFile, MyRectangle part, enImageAlign imageAlign, ref MyRectangle RectDraw)
		{
			// Вырезаем выбранный кусок картинки
			System.Drawing.Bitmap bmpPart = new System.Drawing.Bitmap(part.Width, part.Height);
			using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmpPart))
			{
				g.DrawImage((imageFile as MyImageFileWPF).Image, new System.Drawing.Rectangle(0, 0, part.Width, part.Height), (float)part.X, (float)part.Y, (float)part.Width, (float)part.Height, System.Drawing.GraphicsUnit.Pixel);
				//g.Save();
			}

			// convert to image source
			MemoryStream ms = new MemoryStream();
			((System.Drawing.Bitmap)bmpPart).Save(ms, System.Drawing.Imaging.ImageFormat.Png);
			BitmapImage bmpImage = new BitmapImage();
			bmpImage.BeginInit();
			ms.Seek(0, SeekOrigin.Begin);
			bmpImage.StreamSource = ms;
			bmpImage.EndInit();

			// draw image
			return DrawImage(context, pt, bmpImage, part.Width, part.Height, imageAlign, ref RectDraw);
		}

		public MyRectangle DrawImage(object context, MyPointF pt, ImageSource imgSource, int imgWidth, int imgHeight, enImageAlign imageAlign, ref MyRectangle RectDraw)
		{
			// calculate source x,y
			MyPoint posLeftTopSource = ImageAlign.CalculateLeftTopPos(pt, imageAlign, imgWidth, imgHeight);

			// calculate x,y,w,h Draw
			float xDraw = posLeftTopSource.X * XStretchCoef;
			float yDraw = posLeftTopSource.Y * YStretchCoef;
			float wDraw = (float)imgWidth * XStretchCoef;
			float hDraw = (float)imgHeight * YStretchCoef;

			// draw
			(context as DrawingContext).DrawImage(imgSource, new System.Windows.Rect(xDraw, yDraw, wDraw, hDraw));

			// result Draw
			RectDraw.X = (int)xDraw;
			RectDraw.Y = (int)yDraw;
			RectDraw.Width = (int)wDraw;
			RectDraw.Height = (int)hDraw;

			// return Source
			MyRectangle RectSource = new MyRectangle();
			RectSource.X = (int)posLeftTopSource.X;
			RectSource.Y = (int)posLeftTopSource.Y;
			RectSource.Width = (int)imgWidth;
			RectSource.Height = (int)imgHeight;
			return RectSource;
		}

		public MyRectangle DrawImage(object context, MyPointF pt, IMyImageFile imageFile, enImageAlign imageAlign, ref MyRectangle RectDraw)
		{
			return DrawImage(context, pt, (imageFile as MyImageFileWPF).bitmap, imageFile.sizeSource.Width, imageFile.sizeSource.Height, imageAlign, ref RectDraw);
		}

		public void DrawRectangle(object context, MyRectangle rect, MyColor color, MyColor bgColor, int lineWidth)
		{
			// brush for fill
			BrushForFillRectangle.Color = System.Windows.Media.Color.FromRgb(bgColor.R, bgColor.G, bgColor.B);

			// brush for border
			BrushForBorderRectangle.Color = System.Windows.Media.Color.FromRgb(color.R, color.G, color.B);

			// pen for border
			PenForBorderRectangle.Thickness = lineWidth;
			PenForBorderRectangle.Brush = BrushForBorderRectangle;

			// rect
			RectForRectangle.X = rect.X;
			RectForRectangle.Y = rect.Y;
			RectForRectangle.Width = rect.Width;
			RectForRectangle.Height = rect.Height;

			// draw Rectangle
			(context as DrawingContext).DrawRectangle(BrushForFillRectangle, PenForBorderRectangle, RectForRectangle);
		}

		public void DrawText(object context, int x, int y, string text, float size, MyColor color)
		{
			FormattedText formattedText = new FormattedText(text,
						CultureInfoForText,
						FlowDirection.LeftToRight,
						TypefaceForText, size, SolidColorBrushForText);

			// pos
			PosForText.X = x;
			PosForText.Y = y;

			// draw Text
			(context as DrawingContext).DrawText(formattedText, PosForText);
		}

		protected override void OnRender(DrawingContext drawingContext)
		{
			if (DrawAll != null)
				DrawAll(drawingContext);
		}

		public void SendMessageToReDraw()
		{
			// InvalidateVisual() says Canvas to call OnRender 
			InvalidateVisual();
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
		
		// Rectangle cashe
		protected System.Windows.Media.SolidColorBrush BrushForFillRectangle = new System.Windows.Media.SolidColorBrush();
		protected System.Windows.Media.SolidColorBrush BrushForBorderRectangle = new System.Windows.Media.SolidColorBrush();
		protected System.Windows.Media.Pen PenForBorderRectangle = new System.Windows.Media.Pen();
		protected System.Windows.Rect RectForRectangle = new System.Windows.Rect();

		// Text cashe
		protected System.Windows.Point PosForText = new System.Windows.Point();
		protected System.Globalization.CultureInfo CultureInfoForText = System.Globalization.CultureInfo.CurrentCulture;
		protected Typeface TypefaceForText = new Typeface("Calibri");
		protected SolidColorBrush SolidColorBrushForText = new SolidColorBrush(Colors.Black);

		// images
		public Dictionary<int, IMyImageFile> Images { get; protected set; }

		// draw all
		public delegate void OnDrawAll(object context);
		public OnDrawAll DrawAll; // delegate

		// Stretch
		public float XStretchCoef { get; set; }
		public float YStretchCoef { get; set; }

		// width / height
		public int ScreenWidth { get; set; }
		public int ScreenHeight { get; set; }
	}
}
#endif