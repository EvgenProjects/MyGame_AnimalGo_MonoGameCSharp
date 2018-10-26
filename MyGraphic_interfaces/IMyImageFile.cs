namespace MyGraphic_interfaces
{
	interface IMyImageFile
	{
		MySize sizeSource { get; }
		bool IsLoaded { get; }
		int ImageID { get; }
		MyRectangle[] SpriteOffsets { get; set; }
	}
}