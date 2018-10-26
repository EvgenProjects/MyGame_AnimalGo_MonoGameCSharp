// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	class MyTrajectory : IMyTrajectory
	{
		readonly float XStep;
		readonly float YStep;

		public MyTrajectory(float xStep = 0, float yStep = 0)
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
