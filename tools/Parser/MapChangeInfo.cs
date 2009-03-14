namespace Parsing
{
	public class MapChangeInfo
	{
		public readonly int DangerLevel;
		public readonly SapkaInfo[] Sapkas;
		public readonly AddInfo[] Adds;
		public readonly RemoveInfo[] Removes;
		public readonly int Time;

		public MapChangeInfo(int dangerLevel, SapkaInfo[] sapkas, AddInfo[] adds, RemoveInfo[] removes, int time)
		{
			DangerLevel = dangerLevel;
			Sapkas = sapkas;
			Adds = adds;
			Removes = removes;
			Time = time;
		}

		public MapChangeInfo(Reader r)
		{
			throw new System.NotImplementedException();
		}
	}
}