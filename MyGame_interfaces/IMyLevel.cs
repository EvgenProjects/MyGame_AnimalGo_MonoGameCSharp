using System.Collections.Generic; // for List

// my namespaces
using MyGraphic_interfaces;

namespace MyGame_interfaces
{
	public interface IMyLevel
	{
		// width/height
		int LevelWidth { get; }
		int LevelHeight { get; }
		int LevelLeft { get; }
		int LevelTop { get; }

		// objects
		List<IMyUnit> Units { get; }
		List<IMyBackground> BackgroundPictures { get; }
		List<IMyButton> Buttons { get; }
		List<IMyUnitWillAppear> UnitsWillAppear { get; }
		List<IMyFire> Fires { get; }
		List<IMyAnimation> Animations { get; }

		// events
		void OnLoad(IMyGraphic myGraphic);
		void OnDraw(object context, IMyGraphic myGraphic);
		void OnNextTurn(long timeInMilliseconds, IMyGame game);
		bool OnClickMouse(int xMouse, int yMouse, IMyGraphic myGraphic);
		void OnCreateUnit_WhenDeleteUnitWillAppear(IMyGraphic myGraphic, IMyUnitWillAppear gameUnitWillAppear);

		// level end
		bool IsLevelEnd();

		// multiplayer
		int[] GetPlayerIDs();
		string GetPlayerName(int playerID);
		bool IsTeam(int playerID1, int playerID2);
		int GetMyPlayerID();
	}
}
