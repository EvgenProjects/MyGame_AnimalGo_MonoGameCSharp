// my namespaces
using MyGraphic_interfaces;

namespace MyGame_interfaces
{
	public interface IMyFire : IMyShape
	{
		bool IsNeedDelete { get; set; }

		// multiplayer
		int PlayerID { get; }

		// trajectory
		IMyTrajectory Trajectory { get; }

		// weapon
		int Damage { get; }

		// events
		void OnNextTurn(long timeInMilliseconds, IMyGraphic myGraphic, IMyLevel gameLevel);
		void FireMakingDamage(IMyUnit unit, IMyGraphic myGraphic, IMyLevel gameLevel);
	}
}
