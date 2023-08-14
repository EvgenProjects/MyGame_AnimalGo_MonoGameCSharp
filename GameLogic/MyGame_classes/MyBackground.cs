// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	class MyBackground : IMyBackground
	{
		// image
		protected MyPicture MyPicture;

		public MyBackground(MyPicture myPicture)
		{
			MyPicture = myPicture;
		}

		// events
		public virtual void OnDraw(object context, IMyGraphic myGraphic)
		{
			MyPicture.OnDraw(context, myGraphic);
		}

		public virtual MyRectangle GetSourceRect()
		{
			return MyPicture.GetSourceRect();
		}

		public virtual MyRectangle GetDrawRect()
		{
			return MyPicture.GetDrawRect();
		}

		public virtual bool OnClickMouse(int xMouse, int yMouse, IMyGraphic myGraphic, IMyLevel gameLevel)
		{
			// find active button
			IMyButton button = gameLevel.Buttons.Find(item => item.Focus);
			if (button == null)
				return false;

			// get my Level Play
			MyLevelAbstract myLevelAbstract = gameLevel as MyLevelAbstract;

			// find myHero here
			IMyUnit myUnit = gameLevel.Units.Find(item =>
			{
				// is team
				if (gameLevel.IsTeam(gameLevel.GetMyPlayerID(), item.PlayerID))
				{
					// is same row
					if (myLevelAbstract.GetRow(MyPicture.GetSourceRect()) == myLevelAbstract.GetRow((item as MyUnitAbstract).MyPicture.GetSourceRect()))
					{
						// has enemy unit on right
						if (myLevelAbstract.GetCol(MyPicture.GetSourceRect()) == myLevelAbstract.GetCol((item as MyUnitAbstract).MyPicture.GetSourceRect()))
							return true;
					}
				}

				return false;
			});
			if (myUnit != null)
				return true;

			// get Rect
			MyRectangle rectSource = MyPicture.GetSourceRect();
			int xCenter = rectSource.X + rectSource.Width / 2;
			int yCenter = rectSource.Y + rectSource.Height / 2;

			// get button type
			enImageType buttonType = (enImageType)(button as MyButton).MyPicture.ImageFile.ImageID;

			// create unit
			gameLevel.CreateMyUnitWhenClickButton(myGraphic, xCenter, yCenter, buttonType);

			// unfocus button
			button.Focus = false;

			// return
			return true;
		}
	}
}