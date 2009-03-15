
using System;

namespace TheSapka
{
	public class MapCell
	{
		public bool IsUnbreakableWall { get; private set; }
		public bool IsBreakableWall { get; private set; }
		public bool IsEmpty { get; private set; }
		public int DeadlySince { get; private set; }
		public int DeadlyTill { get; private set; }
		public int EmptySince { get; private set; }
		public char Bonus { get; private set; }
		public bool HasBomb { get; private set; }
		public int DamageRange { get; private set; }
		public int BoomTime { get; private set; }
	}
}
