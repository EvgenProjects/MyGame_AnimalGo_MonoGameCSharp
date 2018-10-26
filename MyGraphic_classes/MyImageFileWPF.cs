#if WPF_APPLICATION // is wpf application

using System; // for Uri
using System.IO; // for File
using System.Windows.Media.Imaging; // for BitmapImage

// my namespaces
using MyGraphic_interfaces;

namespace MyGraphic_classes
{
	class MyImageFileWPF : IMyImageFile
	{
		// constructor
		public MyImageFileWPF(string pathImage, int ImageID)
		{
			try
			{
				// load image
				pathImage = pathImage.Replace('/', '\\');
				pathImage = "Images\\" + pathImage;
				if (!File.Exists(pathImage))
					pathImage = "..\\" + pathImage;
				if (!File.Exists(pathImage))
					pathImage = "..\\" + pathImage;
				if (File.Exists(pathImage))
					pathImage = Path.GetFullPath(pathImage);

				this.bitmap = new BitmapImage(new Uri(pathImage, UriKind.Relative));
				this.sizeSource = new MySize(bitmap.PixelWidth, bitmap.PixelHeight);
				this.IsLoaded = bitmap.PixelHeight > 0 && bitmap.PixelWidth > 0;
				this.ImageID = ImageID;

				// for partial
				Image = System.Drawing.Image.FromFile(pathImage);
			}
			catch (Exception exception)
			{
			}
		}

		// implement attributes
		public MySize sizeSource { get; set; }
		public bool IsLoaded { get; set; }
		public int ImageID { get; set; }
		public MyRectangle[] SpriteOffsets { get; set; }

		// my additional attributes
		public BitmapImage bitmap { get; set; }

		// for partial draw
		public System.Drawing.Image Image { get; set; }
	}
}

#endif