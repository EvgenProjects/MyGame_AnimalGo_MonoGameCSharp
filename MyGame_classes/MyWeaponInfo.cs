// my namespaces
using MyGame_interfaces;

namespace MyGame_classes
{
	class MyWeaponInfo : IMyWeaponInfo
	{
		public MyWeaponInfo(int damage)
		{
			Damage = damage;
		}

		public int Damage { get; protected set; }
	}
}
