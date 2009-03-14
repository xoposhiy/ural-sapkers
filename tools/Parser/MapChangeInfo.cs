namespace Parsing
{
	public class MapChangeInfo
	{
		public readonly int DangerLevel;
		public readonly SapkaInfo[] Sapkas;
		public readonly int Time;

		public MapChangeInfo(int time, SapkaInfo[] sapkas, int dangerLevel)
		{
			Time = time;
			Sapkas = sapkas;
			DangerLevel = dangerLevel;
		}
	}
}