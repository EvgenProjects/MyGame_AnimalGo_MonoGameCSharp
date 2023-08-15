namespace MyGame.interfaces
{
	public interface IMyGraphic
	{
		IMyTexture2D LoadImageFromFile(string pathImage, enImageType imageType);
        IMyTexture2D FindImage(enImageType imageType);

		MyRectangle GetRectInScenaPoints(MyPointF ptCenter, IMyTexture2D myTexture2D, MyRectangle? partRect);
		MyPointF ConvertMousePtToScenaPoints(float xMouse, float yMouse);

        void DrawImage(MyPointF ptCenter, IMyTexture2D myTexture2D);
		void DrawPartImage(MyPointF ptCenter, IMyTexture2D myTexture2D, MyRectangle partRect);

		void DrawRectangle(MyRectangle rect, MyColor color, MyColor bgColor, int lineWidth);
		void DrawText(int x, int y, string text, float size, MyColor color);

		// Stretch
		float XStretchCoef { get; set; }
		float YStretchCoef { get; set; }

		// width / height
		int ScreenWidth { get; set; }
		int ScreenHeight { get; set; }
	}
}