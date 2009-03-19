using System;
using System.Collections.Generic;

namespace Core
{
	public class Commons
	{
		public const int BombTimeout = 39;
		public const int ExplosionDuration = 6;
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

		public static char ToDirChar(int dx, int dy)
		{
			if (dx > 0) return 'r';
			if (dx < 0) return 'l';
			if (dy > 0) return 'd';
			if (dy < 0) return 'u';
			return 's';
		}

		public static char InvertDir(char move)
		{
			switch (move)
			{
				case 'l': return 'r';
				case 'r': return 'l';
				case 'u': return 'd';
				case 'd': return 'u';
				default: throw new Exception(move.ToString());
			}
		}
	}
}