namespace Core.Parsing
{
	public class Pos
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
	}
}