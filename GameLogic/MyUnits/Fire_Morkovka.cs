using MyGame;
using MyGame.interfaces;
using MyUnits.interfaces;

namespace MyUnits
{
	class Fire_Morkovka : Fire_Template
	{
		public Fire_Morkovka(IMyGraphic myGraphic, int xCenter, int yCenter)
			: base(myGraphic, 
				xCenter, yCenter,
                true /*is my fire*/,
                new Trajectory_Template(4.7f/*x step*/, 0.0f/*y step*/),
				10 /* damage */,
                enImageType.Fire_morkovka,
                enImageType.Fire_morkovka_splash)
		{
		}
	}
}
