using MyGame;
using MyGame.interfaces;
using MyUnits.interfaces;

namespace MyUnits
{
	class Fire_Morkovka : Fire_Template
	{
		public Fire_Morkovka(IMyGraphic myGraphic, int xCenter, int yCenter, int playerID)
			: base(myGraphic, 
				  xCenter, yCenter,
                  new Trajectory_Template(4.7f/*x step*/, 0.0f/*y step*/),
				  10 /* damage */,
				  playerID,
                  enImageType.Fire_morkovka,
                  enImageType.Fire_morkovka_splash)
		{
		}
	}
}
