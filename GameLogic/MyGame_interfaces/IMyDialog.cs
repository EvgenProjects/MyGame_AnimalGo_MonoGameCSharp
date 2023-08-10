using System.Collections.Generic; // for List

// my namespaces
using MyGraphic_interfaces;

namespace MyGame_interfaces
{
	public interface IMyDialog
	{
		// events
		void OnDraw(object context, IMyGraphic myGraphic);
		bool OnClickMouse(int xMouse, int yMouse, IMyGraphic myGraphic, IMyGame myGame);
	}
}