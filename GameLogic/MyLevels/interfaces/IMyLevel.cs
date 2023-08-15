using MyGame;
using MyGame.interfaces;
using MyUnits.interfaces;
using System.Collections.Generic; // for List

namespace MyLevels.interfaces
{
	public interface IMyLevel
	{
		// width/height
		int LevelWidth { get; }
		int LevelHeight { get; }
		int LevelLeft { get; }
		int LevelTop { get; }

        // objects
        MyTexture2DAnimation? ActiveButtonInActionZone { get; set; }
        List<IUnit> Units { get; }
		List<IBackgroundSquad> BackgroundPictures { get; }
		List<IUnitWillAppear> UnitsWillAppear { get; }
		List<IFire> Fires { get; }
		List<IAnimPictureDieByTime> AnimPicturesDieByTime { get; }
        List<MyTexture2DAnimation?> ButtonsInActionZone { get; }

        // events
        void OnLoad(IMyGraphic myGraphic);
		void OnDraw(IMyGraphic myGraphic);
		void OnNextTurn(long timeInMilliseconds, IMyGraphic myGraphic);
		bool OnClickMouse(int xMouse, int yMouse, IMyGraphic myGraphic);
		void OnCreateUnit_WhenDeleteUnitWillAppear(IMyGraphic myGraphic, IUnitWillAppear gameUnitWillAppear);

        int GetRow(MyRectangle rect);
        int GetCol(MyRectangle rect);

        // level end
        bool IsLevelEnd();

		// multiplayer
		int[] GetPlayerIDs();
		string GetPlayerName(int playerID);
		bool IsTeam(int playerID1, int playerID2);
		int GetMyPlayerID();
	}
}
