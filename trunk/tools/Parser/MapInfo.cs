namespace Parsing
{
	public class MapInfo
	{
		public readonly char[,] Map;
		public readonly int MapCellSize;

		public MapInfo(int mapCellSize, char[,] map)
		{
			MapCellSize = mapCellSize;
			Map = map;
		}
	}
}