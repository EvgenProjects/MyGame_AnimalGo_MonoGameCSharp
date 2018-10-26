// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_interfaces
{
	interface IMyUnitWillAppear
	{
		IMyUnit Unit { get; }
		bool IsNeedDelete { get; }
		void OnNextTurn(long timeInMilliseconds, IMyGraphic myGraphic, IMyLevel gameLevel);
	}
}