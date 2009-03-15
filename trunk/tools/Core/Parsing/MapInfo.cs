using System.Collections.Generic;

namespace Core.Parsing
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

		public MapInfo(Reader r)
		{
			MapCellSize = int.Parse(r.ReadLine());
			string line = r.ReadLine();
			var m = new List<string>();
			while (line != ";")
			{
				m.Add(line);
				line = r.ReadLine();
			}
			int width = m.Count > 0 ? m[0].Length : 0;

			int height = m.Count;
			Map = new char[width,height];
			for (int x = 0; x < width; x++)
				for (int y = 0; y < height; y++)
				{
					Map[x, y] = m[y][x];
				}
		}
	}
}