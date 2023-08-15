using MyGame.interfaces;

namespace MyUnits.interfaces
{
	public interface IAnimPictureDieByTime
	{
		void OnDraw(IMyGraphic myGraphic);

		bool IsNeedDelete { get; }

		void OnNextTurn(long timeInMilliseconds);
	}
}
