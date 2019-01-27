// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	class MyDialogStartGame : MyDialogAbstract
	{
		// Constructor
		public MyDialogStartGame(IMyGame myGame)
			: base()
		{
			// show rectangle
			ShowRectangle = false;

			// add items
			IMyImageFile myImageFile = myGame.Graphic.FindImage((int)enImageType.Button_level_first);
			IMyDialogItem dlgItem = new MyDialogItem(new MyPicture(myImageFile, Width/2 /*x offset from dialog*/, Height/2 /*y offset from dialog*/, enImageAlign.CenterX_CenterY));
			Items.Add(dlgItem);

			// handler click mouse
			dlgItem.OnHandlerClickMouse = delegate(int xMouse, int yMouse, IMyGraphic myGraphic, IMyDialog myDialog)
			{
				myGame.Dialog = null;

				// start game
				myGame.LoadFirstLevel();

				return true;
			};
		}
	}

	class MyDialogLevelNext : MyDialogAbstract
	{
		// Constructor
		public MyDialogLevelNext(IMyGame myGame)
			: base()
		{
			// show rectangle
			ShowRectangle = false;

			// add items
			IMyImageFile myImageFile = myGame.Graphic.FindImage((int)enImageType.Button_level_next);
			IMyDialogItem dlgItem = new MyDialogItem(new MyPicture(myImageFile, Width / 2 /*x offset from dialog*/, Height / 2 /*y offset from dialog*/, enImageAlign.CenterX_CenterY));
			Items.Add(dlgItem);

			// handler click mouse
			dlgItem.OnHandlerClickMouse = delegate(int xMouse, int yMouse, IMyGraphic myGraphic, IMyDialog myDialog)
			{
				myGame.Dialog = null;

				// start game
				myGame.LoadNextLevel();

				return true;
			};
		}
	}
}