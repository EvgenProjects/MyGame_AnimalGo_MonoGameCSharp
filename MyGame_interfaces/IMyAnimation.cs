namespace MyGame_interfaces
{
	public interface IMyAnimation : IMyShape
	{
		bool IsNeedDelete { get; }

		void OnNextTurn(long timeInMilliseconds);
	}
}
