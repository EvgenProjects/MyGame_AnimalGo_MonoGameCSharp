using MyGame;
using MyGame.interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace MyUnits
{
	class Unit_EnemyBird1 : Unit_Template_StopAndHitIfNearOtherUnit
	{
		public Unit_EnemyBird1(IMyGraphic myGraphic, int xCenterSource, int yCenterSource, int playerID) :
			base(myGraphic,
                xCenterSource, yCenterSource,
                playerID /*playerID*/,
                180 /*life*/,
                40/*hand damage*/,
                1000 /*1.5 second (time to make damage near)*/,
                enImageType.Heroes_bird1_fly /*image Type when Move*/,
                enImageType.Heroes_bird1_fly /*image Type when Damage*/) // ??????
        {
			Trajectory = new Trajectory_Template(-1.8f/*x step*/, 0.0f/*y step*/);

            // enImageType.Heroes_bird1_fly
            myGraphic.LoadImageFromFile("heroes/bird/bird1.png", enImageType.Heroes_bird1_fly);
            MyTexture2DAnimation.MyTexture2D.SpriteOffsets = new MyRectangle[]{
                new MyRectangle(0, 1, 76, 52),
                new MyRectangle(85, 0, 81, 45),
                new MyRectangle(177, 0, 77, 46),
                new MyRectangle(45, 65, 75, 53),
                new MyRectangle(130, 61, 76, 62),
                new MyRectangle(0, 128, 77, 78),
                new MyRectangle(130, 61, 76, 62),
                new MyRectangle(45, 65, 75, 53),
                new MyRectangle(177, 0, 77, 46),
                new MyRectangle(85, 0, 81, 45),
            };

        }
    }
}