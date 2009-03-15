namespace Parsing
{
	public class StartRoundInfo
	{
		public readonly MapInfo MapInfo;
		public readonly int RoundNumber;

		public StartRoundInfo(int roundNumber, MapInfo mapInfo)
		{
			RoundNumber = roundNumber;
			MapInfo = mapInfo;
		}

		public StartRoundInfo(Reader r)
		{
			r.Ensure("START");
			RoundNumber = r.ReadNumber();
			r.Ensure("&");
			MapInfo = new MapInfo(r);
		}
	}
}