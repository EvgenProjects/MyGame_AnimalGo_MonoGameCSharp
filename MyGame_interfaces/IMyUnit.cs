// my namespaces
using MyGraphic_interfaces;

namespace MyGame_interfaces
{
	public interface IMyUnit : IMyShape
	{
		bool IsNeedDelete { get; set; }

		// trajectory
		IMyTrajectory Trajectory { get; }

		// multiplayer
		int PlayerID { get; }

		// Life
		int Life { get; set; }

		// events
		void OnNextTurn(long timeInMilliseconds, IMyGraphic myGraphic, IMyLevel gameLevel, bool bMove);
		bool OnClickMouse(int xMouse, int yMouse, IMyGraphic myGraphic);
		void NeedMakeDamageToUnit(IMyUnit unit, IMyGraphic myGraphic, IMyLevel gameLevel);
		void NeedMakeFire(IMyGraphic myGraphic, IMyLevel gameLevel);
	}
}
