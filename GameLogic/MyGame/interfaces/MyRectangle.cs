namespace MyGame.interfaces
{
	public struct MyRectangle
	{
		public int X;
		public int Y;
		public int Width;
		public int Height;

		public MyRectangle(int x, int y, int width, int height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

        public MyRectangle(float x, float y, float width, float height)
        {
            X = (int)x;
            Y = (int)y;
            Width = (int)width;
            Height = (int)height;
        }
        
		public MyRectangle Inflate(int size)
		{
			X -= size;
			Y -= size;
			Width += size*2;
			Height += size*2;
			return this;
		}

		public bool Contains(int x, int y)
		{
			if (Width > 0 && Height > 0)
			{
				if (x >= X && x <= (X + Width))
				{
					if (y >= Y && y <= (Y + Height))
						return true;
				}
			}
			return false;
		}

		public bool IntersectsWith(MyRectangle rect)
		{
			if (Contains(rect.X, rect.Y))
				return true;
			if (Contains(rect.X + rect.Width, rect.Y + rect.Height))
				return true;

			// is rect bigger than this
			if (rect.X < X && (rect.X + rect.Width) > (X + Width) && rect.Y < Y && (rect.Y + rect.Height) > (Y + Height))
				return true;
			return false;
		}
	}
}
