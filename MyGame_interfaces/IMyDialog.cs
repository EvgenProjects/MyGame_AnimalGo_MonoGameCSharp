using System.Collections.Generic; // for List

// my namespaces
using MyGraphic_interfaces;

namespace MyGame_interfaces
{
	public interface IMyDialog
	{
		// width/height
		int Width { get; }
		int Height { get; }

		// objects
		List<IMyDialogItem> Items { get; }

		// events
		void OnDraw(object context, IMyGraphic myGraphic);
		bool OnClickMouse(int xMouse, int yMouse, IMyGraphic myGraphic, IMyGame myGame);
	}
}