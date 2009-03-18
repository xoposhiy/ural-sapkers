using System.Collections.Generic;
using System.Text;
using Core.Parsing;
using Core.PathFinding;
using Core.StateCalculations;

namespace Core.AI
{
	internal class BonusAdviser : IAdviser
	{
		public IEnumerable<Decision> Advise(GameState state, Paths paths)
		{
			string goodBonus = "bvf?";
			int[] cost = new int[] {70, 50, 40, 25};
			IPath[,] ds = new Path[state.Map.GetLength(0), state.Map.GetLength(1)];
			for (int x = 0; x < state.Map.GetLength(0); ++x)
			{
				for (int y = 0; y < state.Map.GetLength(1); ++y)
				{
					int besti = -1, bestj = -1, bestt = -1, bestd = int.MaxValue;
					for (int i = 0; i < state.CellSize; ++i)
					{
						for (int j = 0; j < state.CellSize; ++j)
						{
							int ii = x * state.CellSize + i;
							int jj = y * state.CellSize + j;
							int tt = paths.MinTime[ii, jj];
							if (tt != -1 && paths.Dist[ii, jj, tt] < bestd)
							{
								besti = ii;
								bestj = jj;
								bestt = tt;
								bestd = paths.Dist[ii, jj, tt];
							}
						}
					}
					if (bestd != int.MaxValue)
					{
						ds[x, y] = paths.GetPath(besti, bestj, bestt);
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
						r.Add(new Decision(ds[i, j], new Pos(i, j), false, ds[i, j].Size(), cost[goodBonus.IndexOf(bonus)]));
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