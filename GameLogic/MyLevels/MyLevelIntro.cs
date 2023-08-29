using System.Collections.Generic;
using MyGame.interfaces;
using MyGame;
using Microsoft.Xna.Framework.Input;

namespace MyLevels
{
    enum enLevelIntroTouch
	{
        Unknown = 0,
		LoadFirstLevel = 1
	}

    class MyLevelIntro
	{
        // width/height
        public readonly int LevelWidth = 680;
        public readonly int LevelHeight = 380;

		// dialogs
        public List<MyTexture2DAnimation?> Buttons { get; protected set; }

        // constructor
        public MyLevelIntro()
		{
	        Buttons = new List<MyTexture2DAnimation?>();
		}

		public virtual void OnLoad(IMyGraphic myGraphic)
        {
			int xCenter = LevelWidth / 2;
            int yCenter = LevelHeight / 2;
            Buttons.Add(new MyTexture2DAnimation(myGraphic.FindImage(enImageType.Button_level_first), xCenter, yCenter));
        }

		public virtual void OnNextTurn(long timeInMilliseconds, IMyGraphic myGraphic)
		{
		}

		public virtual void OnDraw(IMyGraphic myGraphic)
		{
			for (int i = 0; i < Buttons.Count; i++)
                Buttons[i]?.OnDraw(myGraphic);
		}

		public virtual enLevelIntroTouch OnTouch(IMyGraphic myGraphic, int xTouch, int yTouch)
		{
			MyTexture2DAnimation? button = Buttons.Find(item => item?.GetRectInScenaPoints(myGraphic).Contains(xTouch, yTouch) ?? false);
			if (button != null)
			{
                return enLevelIntroTouch.LoadFirstLevel;
			}
			return enLevelIntroTouch.Unknown;
		}
	}
}