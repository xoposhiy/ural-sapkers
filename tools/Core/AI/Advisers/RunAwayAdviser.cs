using Core.Parsing;
using Core.PathFinding;
using Core.StateCalculations;
using System.Collections.Generic;

namespace Core.AI.Advisers
{
	public class RunAwayAdviser : IAdviser
	{
		public IEnumerable<Decision> Advise(GameState state, IPath[,] paths)
		{
			IPath[,] ds = new Path[state.Map.GetLength(0), state.Map.GetLength(1)];
			Pos[,] targetsPt = new Pos[state.Map.GetLength(0), state.Map.GetLength(1)];

			for (int i = 0; i < paths.GetLength(0); ++i)
			{
				for (int j = 0; j < paths.GetLength(1); ++j)
				{
					int x = i / state.CellSize;
					int y = j / state.CellSize;
					if (paths[i, j] != null && (ds[x, y] == null || ds[x, y].CompareTo(paths[i, j]) > 0))
					{
						ds[x, y] = paths[i, j];
						targetsPt[x, y] = new Pos(i, j);
					}
				}
			}
			for (int i = 0; i < ds.GetLength(0); ++i)
			{
				for (int j = 0; j < ds.GetLength(1); ++j)
				{
					if (ds[i, j] != null && state.Map[i, j].DeadlySince == int.MaxValue)
					{
						yield return new Decision(ds[i, j], new Pos(i, j), targetsPt[i, j], false, ds[i, j].Size() + 1, 1.0, "RunAway");
					}
				}
			}
			var finder = new PathFinder();
			var cellSize = state.CellSize;
			finder.SetMap(state.Map, cellSize);
			for (int d = 0; d < 4; ++d)
			{
				int x = state.MySapka.Pos.X;
				int y = state.MySapka.Pos.Y;
				if (finder.Move(ref x, ref y, state.Time, state.MySapka.Speed, d))
				{
					yield return new Decision(new Path(null, PathFinder.Dir[d]), new Pos(x/cellSize, y/cellSize), new Pos(x, y), false, 1, 0.000000001, "RunAway");
				}
			}
		}
	}
}