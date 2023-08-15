using MyGame.interfaces;
using MyGame;

namespace MyUnits
{
    class Unit_EnemyPauk : Unit_Template_StopAndHitIfNearOtherUnit
    {
        public Unit_EnemyPauk(IMyGraphic myGraphic, int playerID, int xCenterSource, int yCenterSource) :
            base(myGraphic,
                xCenterSource, yCenterSource,
                playerID /*playerID*/,
                400 /*life*/,
                10 /*hand damage*/,
                1500 /*1.5 second (time to make damage near)*/,
                enImageType.Heroes_spider_go /*image Type when Move*/,
                enImageType.Heroes_spider_hit /*image Type when Damage*/)
        {
            Trajectory = new Trajectory_Template(-0.2f /*x step*/, 0.0f/*y step*/);
        }
    }
}