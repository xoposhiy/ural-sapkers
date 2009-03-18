using System.Collections.Generic;
using System.Text;
using Core.Parsing;
using Core.PathFinding;
using Core.StateCalculations;

namespace Core.AI.Advisers
{
	internal class BonusAdviser : IAdviser
	{
		public IEnumerable<Decision> Advise(GameState state, IPath[,] paths)
		{
			string goodBonus = "bvf?";
			int[] cost = new int[] {70, 50, 40, 25};
			IPath[,] ds = new Path[state.Map.GetLength(0), state.Map.GetLength(1)];
			for (int i = 0; i < paths.GetLength(0); ++i)
			{
				for (int j = 0; j < paths.GetLength(1); ++j)
				{
					int x = i / state.CellSize;
					int y = j / state.CellSize;
					if (paths[i, j] != null && (ds[x, y] == null || ds[x, y].CompareTo(paths[i, j]) > 0))
					{
						ds[x, y] = paths[i, j];
					}
				}
			}
			List<Decision> r = new List<Decision>();
			for (int i = 0; i < ds.GetLength(0); ++i)
			{
				for (int j = 0; j < ds.GetLength(1); ++j)
				{
					if (ds[i, j] == null)
					{
						continue;
					}
					char bonus = state.Map[i, j].Bonus;
					if (goodBonus.IndexOf(bonus) != -1)
					{
						r.Add(new Decision(ds[i, j], new Pos(i, j), false, ds[i, j].Size(), cost[goodBonus.IndexOf(bonus)], "Bonuser"));
					}
				}
			}
			return r;
		}

		private IPath AddStops(IPath path, int repeat)
		{
			for(int i=0; i<repeat; i++)
			{
				path = new Path((Path)path, 's');
			}
			return path;
		}
	}
}