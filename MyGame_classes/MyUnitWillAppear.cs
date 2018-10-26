// my namespaces
using MyGraphic_interfaces;
using MyGame_interfaces;

namespace MyGame_classes
{
	class MyUnitWillAppear : IMyUnitWillAppear
	{
		public long TimeWhenAppearInMilliseconds = 0;
		private long TimeCreatedInMilliseconds = 0;
		public IMyUnit Unit { get; protected set; }
		public bool IsNeedDelete { get; protected set; }

		// constructor
		public MyUnitWillAppear(int timeWhenAppearInMilliseconds, IMyUnit unit)
		{
			TimeWhenAppearInMilliseconds = timeWhenAppearInMilliseconds;
			Unit = unit;
		}

		public virtual void OnNextTurn(long timeInMilliseconds, IMyGraphic myGraphic, IMyLevel gameLevel)
		{
			if (TimeCreatedInMilliseconds == 0)
				TimeCreatedInMilliseconds = timeInMilliseconds;

			// check
			if (IsNeedDelete)
				return;

			// is elipsed
			if (timeInMilliseconds < (TimeCreatedInMilliseconds + TimeWhenAppearInMilliseconds))
				return;

			// Create Unit from UnitWillAppear
			if (Unit != null)
				gameLevel.Units.Add(Unit);

			// clear
			Unit = null;

			// delete ObjectToAppear
			IsNeedDelete = true;
		}
	}
}
