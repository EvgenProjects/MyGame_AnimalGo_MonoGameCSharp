using MyGame;
using MyGame.interfaces;
using MyLevels.interfaces;
using MyUnits.interfaces;

namespace MyUnits
{
	class BackgroundSquad_Template : IBackgroundSquad
	{
		// image
		protected MyTexture2DAnimation MyTexture2DAnimation;

		public BackgroundSquad_Template(MyTexture2DAnimation myTexture2DAnimation)
		{
            MyTexture2DAnimation = myTexture2DAnimation;
		}

        // events
        public virtual MyRectangle GetRectInScenaPoints(IMyGraphic myGraphic)
        {
            return MyTexture2DAnimation.GetRectInScenaPoints(myGraphic);
        }
        
		public virtual void OnDraw(IMyGraphic myGraphic)
		{
            MyTexture2DAnimation.OnDraw(myGraphic);
		}

		public virtual bool OnClickMouse(int xMouse, int yMouse, IMyGraphic myGraphic, IMyLevel gameLevel)
		{
			// find active button
			if (gameLevel.ActiveButtonInActionZone == null)
				return false;

			// find myHero here
			IUnit myUnit = gameLevel.Units.Find(item =>
			{
				// is team
				if (gameLevel.IsTeam(gameLevel.GetMyPlayerID(), item.PlayerID))
				{
					// is same row
					if (gameLevel.GetRow(MyTexture2DAnimation.GetRectInScenaPoints(myGraphic)) == gameLevel.GetRow(item.GetRectInScenaPoints(myGraphic)))
					{
						// has enemy unit on right
						if (gameLevel.GetCol(MyTexture2DAnimation.GetRectInScenaPoints(myGraphic)) == gameLevel.GetCol(item.GetRectInScenaPoints(myGraphic)))
							return true;
					}
				}

				return false;
			});
			if (myUnit != null)
				return true;

			// get Rect
			MyRectangle rect = MyTexture2DAnimation.GetRectInScenaPoints(myGraphic);
			int xCenter = rect.X + rect.Width / 2;
			int yCenter = rect.Y + rect.Height / 2;

			// create unit
			gameLevel.CreateMyUnitWhenClickButton(myGraphic, xCenter, yCenter, gameLevel.ActiveButtonInActionZone.Value.MyTexture2D.ImageType);

            // unfocus button
            gameLevel.ActiveButtonInActionZone = null;

			// return
			return true;
		}
	}
}