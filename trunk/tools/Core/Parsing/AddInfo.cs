namespace Core.Parsing
{
	public class AddInfo
	{
		public readonly int DamagingRange;
		public readonly bool HasDamageInfo;
		public readonly Pos Pos;
		public readonly char SubstanceType;
		public readonly int Time;

		public AddInfo(char substanceType, Pos pos, int damagingRange, int boomTime)
		{
			SubstanceType = substanceType;
			Pos = pos;
			DamagingRange = damagingRange;
			Time = boomTime;
		}

		public AddInfo(Reader r)
		{
			r.Ensure("+");
			SubstanceType = r.ReadChar();
			r.Ensure(" ");
			Pos = new Pos(r);
			if (r.Lookup() == ' ')
			{
				r.Ensure(" ");
				DamagingRange = r.ReadNumber();
				r.Ensure(" ");
				Time = r.ReadNumber();
				HasDamageInfo = true;
			}
			else
			{
				HasDamageInfo = false;
			}
		}
	}
}