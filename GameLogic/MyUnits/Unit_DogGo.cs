using MyGame;
using MyGame.interfaces;
using MyUnits.interfaces;

namespace MyUnits
{
    class Unit_DogGo : Unit_Template_StopAndHitIfNearOtherUnit
	{
        public Unit_DogGo(IMyGraphic myGraphic, int playerID, int xCenterSource, int yCenterSource) :
            base(myGraphic,
                xCenterSource, yCenterSource,
                playerID /*playerID*/,
                40 /*life*/,
                20 /*hand damage*/,
                1500 /*1.5 second (time to make damage near)*/,
                enImageType.Heroes_sobachka_run /*image Type when Move*/,
                enImageType.Heroes_spider_hit /*image Type when Damage*/) // ???????????
        {
            Trajectory = new Trajectory_Template(0.7f /*x step*/, 0.0f/*y step*/);
        }
    }
}