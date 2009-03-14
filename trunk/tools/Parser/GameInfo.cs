namespace Parsing
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
	}
}