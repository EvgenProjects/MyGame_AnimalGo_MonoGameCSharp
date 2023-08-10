namespace MyGraphic_interfaces
{
	public enum enImageAlign
	{
		LeftTop = 1,
		CenterX_CenterY = 2,
	}

	static class ImageAlign
	{
		public static MyPoint CalculateLeftTopPos(MyPoint pos, enImageAlign align, MySize size)
		{
			MyPoint leftTop = pos;
			if (align == enImageAlign.CenterX_CenterY)
			{
				leftTop.X -= size.Width / 2;
				leftTop.Y -= size.Height / 2;
			}
			return leftTop;
		}

		public static MyPoint CalculateLeftTopPos(MyPointF pos, enImageAlign align, int width, int height)
		{
			MySize size = new MySize(width, height);
			MyPoint posTemp = new MyPoint((int)pos.X, (int)pos.Y);
			return CalculateLeftTopPos(posTemp, align, size);
		}
	}
}