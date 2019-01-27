// my namespaces
using MyGraphic_interfaces;

namespace MyGame_interfaces
{
	public interface IMyShape
	{
		void OnDraw(object context, IMyGraphic myGraphic);
		MyRectangle GetSourceRect();
		MyRectangle GetDrawRect();
	}
}
