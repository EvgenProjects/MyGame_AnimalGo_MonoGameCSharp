// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	class MyBackgroundImpl : MyBackground
	{
		public MyBackgroundImpl(MyPicture myPicture) : base(myPicture)
		{
		}

		public override bool OnClickMouse(int xMouse, int yMouse, IMyGraphic myGraphic, IMyLevel gameLevel)
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
			if (buttonType == enImageType.Button_zuma)
			{
				gameLevel.Units.Add(new MyUnit_DogZumaGo(myGraphic, gameLevel.GetMyPlayerID(), xCenter, yCenter));
			}
			else if (buttonType == enImageType.Button_marshal)
			{
				gameLevel.Units.Add(new MyUnit_DogMarshalGo(myGraphic, gameLevel.GetMyPlayerID(), xCenter, yCenter));
			}
			else if (buttonType == enImageType.Button_dog)
			{
				gameLevel.Units.Add(new MyUnit_DogGo(myGraphic, gameLevel.GetMyPlayerID(), xCenter, yCenter));
			}
			else if (buttonType == enImageType.Button_krolik_s_lukom)
			{
				gameLevel.Units.Add(new MyUnit_KrolikStayAndFire(myGraphic, gameLevel.GetMyPlayerID(), xCenter, yCenter));
			}

			// unfocus button
			button.Focus = false;

			// return
			return true;
		}
	}
}