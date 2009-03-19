using Core.Parsing;

namespace Core.StateCalculations
{
	public class MapCell
	{
		public bool IsBomb { get {
				return DeadlySince != int.MaxValue && EmptySince == DeadlySince;
			}
		}

		public MapCell(bool isUnbreakableWall, bool isBreakableWall, bool isEmpty, int deadlySince, int deadlyTill,
		               int emptySince, char bonus)
		{
			IsUnbreakableWall = isUnbreakableWall;
			IsBreakableWall = isBreakableWall;
			IsEmpty = isEmpty;
			DeadlySince = deadlySince;
			DeadlyTill = deadlyTill;
			EmptySince = emptySince;
			Bonus = bonus;
		}

		public MapCell(char c)
		{
			IsUnbreakableWall = c == Cell.Indestructable;
			IsBreakableWall = c == Cell.Destructable;
			IsEmpty = !IsUnbreakableWall && !IsBreakableWall;
			DeadlySince = int.MaxValue;
			DeadlyTill = int.MaxValue;
			EmptySince = int.MaxValue;
			Bonus = IsBreakableWall || IsUnbreakableWall ? '.' : c;
		}

		public bool IsUnbreakableWall { get; private set; }
		public bool IsBreakableWall { get; private set; }
		public bool IsEmpty { get; private set; }
		public int DeadlySince { get; private set; }
		public int DeadlyTill { get; private set; }
		public int EmptySince { get; private set; }
		public char Bonus { get; private set; }

		public MapCell AddBonus(char substanceType)
		{
			return new MapCell(
				IsUnbreakableWall,
				IsBreakableWall,
				IsEmpty,
				DeadlySince,
				DeadlyTill,
				EmptySince,
				substanceType);
		}

		public MapCell AddBreakableWall()
		{
			return new MapCell(
				false, true, false,
				DeadlySince, DeadlyTill, EmptySince, Bonus);
		}

		public MapCell AddUnbreakableWall()
		{
			return new MapCell(
				true, false, false,
				DeadlySince, DeadlyTill, EmptySince, Bonus);
		}

		public bool IsDeadlyAt(int time)
		{
			return time >= DeadlySince-1 && time <= DeadlyTill+1;
		}
	}
}