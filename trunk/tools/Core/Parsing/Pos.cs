using System;

namespace Core.Parsing
{
	public class Pos : IEquatable<Pos>
	{
		public readonly int X;
		public readonly int Y;

		public Pos(int x, int y)
		{
			X = x;
			Y = y;
		}

		public Pos(Reader r)
		{
			X = r.ReadNumber();
			r.Ensure(" ");
			Y = r.ReadNumber();
		}

		public override string ToString()
		{
			return "(" + X + ", " + Y +")";
		}

		public bool Equals(Pos obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.X == X && obj.Y == Y;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (X*397) ^ Y;
			}
		}
	}
}