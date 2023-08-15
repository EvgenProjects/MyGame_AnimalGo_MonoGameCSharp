using MyGame.interfaces;
using MyUnits.interfaces;

namespace MyUnits
{
	class Trajectory_Template : ITrajectory
	{
		readonly float XStep;
		readonly float YStep;

		public Trajectory_Template(float xStep = 0, float yStep = 0)
		{
			XStep = xStep;
			YStep = yStep;
		}

		public virtual void Move(ref MyPointF pt)
		{
			if (XStep == 0 && YStep == 0)
				return;

			pt.X += XStep;
			pt.Y += YStep;
		}
	}
}
