namespace MyGame_interfaces
{
	interface IMyAnimation : IMyShape
	{
		bool IsNeedDelete { get; }

		void OnNextTurn(long timeInMilliseconds);
	}
}
