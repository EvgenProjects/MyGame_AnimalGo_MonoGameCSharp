namespace MyGraphic_interfaces
{
	public interface IMyGraphic
	{
		IMyImageFile LoadImageFromFile(string pathImage, int ImageID);
		IMyImageFile FindImage(int imageID);

		MyRectangle DrawImage(object context, MyPointF pt, IMyImageFile imageFile, enImageAlign imageAlign, ref MyRectangle RectDraw);
		MyRectangle DrawPartImage(object context, MyPointF pt, IMyImageFile imageFile, MyRectangle part, enImageAlign imageAlign, ref MyRectangle RectDraw);

		void DrawRectangle(object context, MyRectangle rect, MyColor color, MyColor bgColor, int lineWidth);
		void DrawText(object context, int x, int y, string text, float size, MyColor color);

		void SendMessageToReDraw();

		MySize ConvertSourceToScreen(MySize sizeSource);
		MyPoint ConvertScreenToSource(MyPoint posScreen);

		// Stretch
		float XStretchCoef { get; set; }
		float YStretchCoef { get; set; }

		// width / height
		int ScreenWidth { get; set; }
		int ScreenHeight { get; set; }
	}
}