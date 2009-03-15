namespace Core.Parsing
{
	public class GameInfo
	{
		public readonly MapInfo MapInfo;
		public readonly int PID;

		public GameInfo(int pid, MapInfo mapInfo)
		{
			PID = pid;
			MapInfo = mapInfo;
		}

		public GameInfo(Reader r)
		{
			r.Ensure("PID");
			PID = int.Parse(r.ReadChar().ToString());
			r.Ensure("&");
			MapInfo = new MapInfo(r);
		}
	}
}