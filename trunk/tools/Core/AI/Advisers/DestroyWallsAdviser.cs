using System.Collections.Generic;
using Core.Parsing;
using Core.PathFinding;
using Core.StateCalculations;

namespace Core.AI.Advisers
{
	internal class DestroyWallsAdviser : IAdviser
	{
		public IEnumerable<Decision> Advise(GameState state, IPath[,] paths)
		{
			var dx = new[] {1, -1, 0, 0};
			var dy = new[] {0, 0, 1, -1};
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
					int countWalls = 0;
					for (int d = 0; d < 4; ++d)
					{
						int x = i;
						int y = j;
						for (int it = 0; it < state.Sapkas[state.Me].BombsStrength; ++it)
						{
							if (x < 0 || x >= ds.GetLength(0) ||
							    y < 0 || y >= ds.GetLength(1) ||
							    state.Map[x, y].IsEmpty ||
							    state.Map[x, y].IsBreakableWall &&
							    state.Time >= state.Map[x, y].EmptySince)
							{
								x += dx[d];
								y += dy[d];
							}
						}
						if (x >= 0 && x < ds.GetLength(0) && 
						    y >= 0 && y < ds.GetLength(1) && 
						    state.Map[x, y].IsBreakableWall && 
						    state.Map[x, y].EmptySince == int.MaxValue)
						{
							++countWalls;
						}
					}
					if (countWalls > 0)
					{
						var decision = new Decision(ds[i, j], new Pos(i, j), ds[i, j].Size() == 0, ds[i, j].Size() + 1, countWalls * 10, "WallBreaker", true);
						if(state.GetWaitForBombTime() <= decision.Duration)
							r.Add(decision);
						else
						{
							var cell = state.Map[decision.Target.X, decision.Target.Y];
							if (cell.DeadlySince == int.MaxValue || cell.DeadlySince < state.Time + decision.Duration)
							{
								decision = new Decision(AddStops(decision.Path, state.GetWaitForBombTime() - decision.Duration), decision.Target, decision.PutBomb, state.GetWaitForBombTime(), decision.PotentialScore, decision.Name, true);
								r.Add(decision);
							}
						}
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
