using MyGame;
using MyGame.interfaces;
using MyUnits.interfaces;
using System;
using System.Collections.Generic; // for List

namespace MyLevels.interfaces
{
    public struct LevelInfo
    {
        public int ColsCount;
        public int RowsCount;
        public enImageType[] Buttons;
        public Func<int, int, enImageType> FuncBackgroundImageType;
        public IUnitWillAppear[] UnitsWillAppear;
        public LevelInfo(int colsCount, int rowsCount, enImageType[] buttons, Func<int, int, enImageType> funcBackgroundImageType, IUnitWillAppear[] unitsWillAppear)
        {
            ColsCount = colsCount;
            RowsCount = rowsCount;
            Buttons = buttons;
            FuncBackgroundImageType = funcBackgroundImageType;
            UnitsWillAppear = unitsWillAppear;
        }
    }
    
    public interface IMyLevel
	{
        LevelInfo LevelInfo { get; }
        int LevelWidth { get; }
        int LevelHeight { get; }
        int LevelLeft { get; }
        int LevelTop { get; }

        int GetRow(MyRectangle rect);
        int GetCol(MyRectangle rect);
        
        int GetXCenterItemByColNumber(int col);
        int GetYCenterItemByRowNumber(int row);

        MyTexture2DAnimation? ActiveButtonInActionZone { get; set; }
        List<IUnit> MyUnits { get; }
        List<IUnit> EnemyUnits { get; }
        List<IFire> MyFires { get; }
        List<IFire> EnemyFires { get; }

        List<IBackgroundSquad> BackgroundPictures { get; }
		List<IUnitWillAppear> UnitsWillAppear { get; }
        List<IAnimPictureDieByTime> AnimPicturesDieByTime { get; }
        List<MyTexture2DAnimation?> ButtonsInActionZone { get; }
	}
}
