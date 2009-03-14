namespace Parsing
{
	public class AddInfo
	{
		public readonly int BoomTime;
		public readonly int DamagingRange;
		public readonly Pos Pos;
		public readonly char SubstanceType;

		public AddInfo(char substanceType, Pos pos, int damagingRange, int boomTime)
		{
			SubstanceType = substanceType;
			Pos = pos;
			DamagingRange = damagingRange;
			BoomTime = boomTime;
		}
	}
}