// my namespaces
using MyGraphic_interfaces;

namespace MyGame_interfaces
{
	public interface IMyBackground : IMyShape
	{
		// events
		bool OnClickMouse(int xMouse, int yMouse, IMyGraphic myGraphic, IMyLevel gameLevel);
	}
}
