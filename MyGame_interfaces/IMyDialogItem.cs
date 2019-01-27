// my namespaces
using MyGraphic_interfaces;

namespace MyGame_interfaces
{
	public delegate bool HandlerClickMouse(int xMouse, int yMouse, IMyGraphic myGraphic, IMyDialog myDialog);

	public interface IMyDialogItem : IMyShape
	{
		// focus
		bool Focus { get; set; }

		HandlerClickMouse OnHandlerClickMouse { get; set; }
	}
}
