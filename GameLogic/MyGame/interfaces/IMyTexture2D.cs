using Microsoft.Xna.Framework.Graphics;

namespace MyGame.interfaces
{
	public interface IMyTexture2D
	{
		MySize sizeSource { get; }
	//	bool IsLoaded { get; }
		enImageType ImageType { get; }
		MyRectangle[] SpriteOffsets { get; set; }
        public Texture2D Texture2D { get; }
    }
}