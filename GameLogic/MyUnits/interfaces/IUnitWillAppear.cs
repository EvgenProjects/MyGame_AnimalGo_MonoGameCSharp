using MyGame.interfaces;
using MyLevels.interfaces;

namespace MyUnits.interfaces
{
	public interface IUnitWillAppear
	{
		bool IsNeedDelete { get; }
		void OnNextTurn(long timeInMilliseconds, IMyGraphic myGraphic, IMyLevel gameLevel);
	}
}