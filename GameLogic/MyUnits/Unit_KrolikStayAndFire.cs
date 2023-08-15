using MyGame;
using MyGame.interfaces;

namespace MyUnits
{
	class Unit_KrolikStayAndFire : Unit_Template_FireOnDistanceIfSeeEnemyUnit
	{
		// constructor
		public Unit_KrolikStayAndFire(IMyGraphic myGraphic, int xCenterSource, int yCenterSource, int playerID)
			: base(myGraphic,
				  xCenterSource, yCenterSource, 
				  playerID /*playerID*/,
                  30 /*life*/,
                  1500 /*1.5 second (time to make damage near)*/,
                  enImageType.Heroes_krolik_s_lukom)
		{
			DelegateMakeFire = (posSourceCenter) =>
			{
				return new Fire_Morkovka(myGraphic, posSourceCenter.X, posSourceCenter.Y, playerID);
			};
		}
	}
}