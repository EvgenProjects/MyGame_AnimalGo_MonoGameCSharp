using MyGame.interfaces;
using MyLevels.interfaces;

namespace MyUnits.interfaces
{
	public interface IBackgroundSquad
	{
		void OnDraw(IMyGraphic myGraphic);
        MyRectangle GetRectInScenaPoints(IMyGraphic myGraphic);

        // events
        bool OnClickMouse(int xMouse, int yMouse, IMyGraphic myGraphic, IMyLevel gameLevel);
	}
}
