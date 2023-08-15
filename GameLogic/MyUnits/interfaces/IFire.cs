using MyGame.interfaces;
using MyLevels.interfaces;

namespace MyUnits.interfaces
{
	public interface IFire
	{
		bool IsNeedDelete { get; set; }

		// multiplayer
		int PlayerID { get; }

		// trajectory
		ITrajectory Trajectory { get; }

		// weapon
		int Damage { get; }

		void OnDraw(IMyGraphic myGraphic);

		// events
		void OnNextTurn(long timeInMilliseconds, IMyGraphic myGraphic, IMyLevel gameLevel);
		void FireMakingDamage(IUnit unit, IMyGraphic myGraphic, IMyLevel gameLevel);
	}
}
