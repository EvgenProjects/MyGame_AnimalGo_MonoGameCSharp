// my namespaces
using MyGraphic_interfaces;

namespace MyGame_interfaces
{
	public interface IMyGame
	{
		// graphic
		IMyGraphic Graphic { get; }

		// levels
		IMyLevel[] Levels { get; }
		int CurrentLevelIndex { get; }

		// dialog
		IMyDialog Dialog { get; set; }

		// time
		int GetTimeStepInMilliseconds();

		// events
		void OnInit();
		void OnDraw(object context);
		void OnNextTurn(long timeInMilliseconds);
		void OnExit();
		bool OnClickMouse(int xMouse, int yMouse);
		void OnChangeWindowSize(int windowWidth, int windowHeight);

		// methods
		void LoadFirstLevel();
		void LoadNextLevel();
	}
}
