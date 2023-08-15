using MyDialogs.interfaces;
using MyGame;
using MyGame.interfaces;
using MyLevels.interfaces;
using System;
using System.Collections.Generic;

namespace MyDialogs
{
    class MyDialogStartGame : IDialog
    {
        public List<MyTexture2DAnimation> Buttons { get; protected set; }
		protected Action _actionClickOK;

        public MyDialogStartGame(Action actionClickOK)
		{
            Buttons = new List<MyTexture2DAnimation>();
            _actionClickOK = actionClickOK;
        }

        public virtual void OnDraw(IMyGraphic myGraphic)
        {
            for (int i = 0; i < Buttons.Count; i++)
                Buttons[i].OnDraw(myGraphic);
        }

        public virtual bool OnClickMouse(IMyGraphic myGraphic, int xMouse, int yMouse)
        {
            MyTexture2DAnimation? button = Buttons.Find(item => item.GetRectInScenaPoints(myGraphic).Contains(xMouse, yMouse));
			if (button != null)
			{
				_actionClickOK?.Invoke();
				return true;
            }
			return false;
        }
    }

    class MyDialogLevelNext
	{
		// Constructor
		public MyDialogLevelNext(IMyLevel myLevel)
		{
			/*
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
			*/
		}
	}
}