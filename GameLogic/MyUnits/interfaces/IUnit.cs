using MyGame.interfaces;
using MyLevels.interfaces;

namespace MyUnits.interfaces
{
	public interface IUnit
	{
		bool IsNeedDelete { get; set; }

		// trajectory
		ITrajectory Trajectory { get; }

		bool IsMyUnit { get; }

		// Life
		int Life { get; set; }

		void OnDraw(IMyGraphic myGraphic);
		MyRectangle GetRectInScenaPoints(IMyGraphic myGraphic);

		// events
		void OnNextTurn(long timeInMilliseconds, IMyGraphic myGraphic, IMyLevel gameLevel, bool bMove);
		bool OnClickMouse(int xMouse, int yMouse, IMyGraphic myGraphic);
		void NeedMakeDamageToUnit(IUnit unit, IMyGraphic myGraphic, IMyLevel gameLevel);
		void NeedMakeFire(IMyGraphic myGraphic, IMyLevel gameLevel);
	}
}
