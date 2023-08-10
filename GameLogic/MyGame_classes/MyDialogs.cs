// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	class MyDialogStartGame : MyDialogAbstract
	{
		protected override bool IsDrawRectangle { get { return true; } }
		
		// Constructor
		public MyDialogStartGame(IMyGame myGame) : base()
		{
			enImageType type = enImageType.Button_level_first; // picture

			// make dialog size same as picture
			IMyImageFile myImageFile = myGame.Graphic.FindImage((int)type);
			WidthSource = myImageFile.sizeSource.Width;
			HeightSource = myImageFile.sizeSource.Height;

			// add picture
			IMyDialogItem dlgItem = AddPictureToCenterDialog(myGame.Graphic, type);

			// handler click mouse
			dlgItem.OnHandlerClickMouse = (xMouse, yMouse, myGraphic, myDialog) =>
			{
				// close dialog
				this.CloseDialog(myGame);

				// start game
				myGame.LoadFirstLevel();

				return true;
			};
		}
	}

	class MyDialogLevelNext : MyDialogAbstract
	{
		protected override bool IsDrawRectangle { get { return true; } }

		// Constructor
		public MyDialogLevelNext(IMyGame myGame) : base()
		{
			enImageType type = enImageType.Button_level_next; // picture

			// make dialog size same as picture
			IMyImageFile myImageFile = myGame.Graphic.FindImage((int)type);
			WidthSource = myImageFile.sizeSource.Width;
			HeightSource = myImageFile.sizeSource.Height;

			// add picture
			IMyDialogItem dlgItem = AddPictureToCenterDialog(myGame.Graphic, type);

			// handler click mouse
			dlgItem.OnHandlerClickMouse = (xMouse, yMouse, myGraphic, myDialog) =>
			{
				// close dialog
				this.CloseDialog(myGame);

				// next level
				myGame.LoadNextLevel();

				return true;
			};
		}
	}
}