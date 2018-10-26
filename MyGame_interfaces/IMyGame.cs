// my namespaces
using MyGraphic_interfaces;

namespace MyGame_interfaces
{
	interface IMyGame
	{
		// graphic
		IMyGraphic Graphic { get; }

		// time
		int GetTimeStepInMilliseconds();

		// events
		void OnInit();
		void OnDraw(object context);
		void OnNextTurn(long timeInMilliseconds);
		void OnExit();
		bool OnClickMouse(int xMouse, int yMouse);
		void OnChangeWindowSize(int windowWidth, int windowHeight);
	}
}
