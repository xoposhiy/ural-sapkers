using System.Collections.Generic;

namespace Core
{
	public class Constants
	{
		public const int BombTimeout = 40;
		public const int ExplosionDuration = 3;
		public const int Radius = int.MaxValue;

		public static IDictionary<char, int> dx =
			new Dictionary<char, int>
				{
					{'u', 0},
					{'d', 0},
					{'l', -1},
					{'r', 1}
				};
		public static IDictionary<char, int> dirIndex =
			new Dictionary<char, int>
				{
					{'l', 0},
					{'r', 1},
					{'u', 2},
					{'d', 3}
				};

		public static IDictionary<char, int> dy =
			new Dictionary<char, int>
				{
					{'u', -1},
					{'d', 1},
					{'l', 0},
					{'r', 0}
				};
	}
}