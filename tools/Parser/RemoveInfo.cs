namespace Parsing
{
	public class RemoveInfo
	{
		public readonly Pos Pos;
		public readonly char SubstanceType;

		public RemoveInfo(char substanceType, Pos pos)
		{
			SubstanceType = substanceType;
			Pos = pos;
		}

		public RemoveInfo(Reader r)
		{
			r.Ensure("-");
			SubstanceType = r.ReadChar();
			r.Ensure(" ");
			Pos = new Pos(r);
		}
	}
}