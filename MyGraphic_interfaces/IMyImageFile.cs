namespace MyGraphic_interfaces
{
	public interface IMyImageFile
	{
		MySize sizeSource { get; }
		bool IsLoaded { get; }
		int ImageID { get; }
		MyRectangle[] SpriteOffsets { get; set; }
	}
}