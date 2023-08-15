using MyGame;
using MyGame.interfaces;
using MyUnits.interfaces;
using System.Collections.Generic; // for List

namespace MyDialogs.interfaces
{
	interface IDialog
	{
        void OnDraw(IMyGraphic myGraphic);
        bool OnClickMouse(IMyGraphic myGraphic, int xMouse, int yMouse);
    }
}
