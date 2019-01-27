// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_interfaces
{
	public interface IMyButton : IMyShape
	{
		// focus
		bool Focus { get; set; }

		// events
		bool OnClickMouse(int xMouse, int yMouse, IMyGraphic myGraphic, IMyLevel gameLevel);
	}
}
