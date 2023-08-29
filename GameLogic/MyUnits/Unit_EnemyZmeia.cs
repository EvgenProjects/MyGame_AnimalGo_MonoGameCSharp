using MyGame.interfaces;
using MyGame;

namespace MyUnits
{
    class Unit_EnemyZmeia : Unit_Template_StopAndHitIfNearOtherUnit
    {
        public Unit_EnemyZmeia(IMyGraphic myGraphic, int xCenterSource, int yCenterSource) :
            base(myGraphic,
                xCenterSource, yCenterSource,
                false /*is my unit*/,
                40 /*life*/,
                20 /*hand damage*/,
                1000 /*1.5 second (time to make damage near)*/,
                enImageType.Heroes_zmeia_go /*image Type when Move*/,
                enImageType.Heroes_zmeia_zalit /*image Type when Damage*/)
        {
            Trajectory = new Trajectory_Template(-0.7f /*x step*/, 0.0f/*y step*/);
        }
    }
}