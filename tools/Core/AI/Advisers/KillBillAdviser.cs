using System.Collections.Generic;
using Core.Parsing;
using Core.PathFinding;
using Core.StateCalculations;

namespace Core.AI.Advisers
{
	internal class KillBillAdviser : IAdviser
	{

		public IList<Decision> Advise(GameState state, IPath[,] paths)
		{
			if (state.GetWaitForBombTime() != 0 || state.Time < 2000) //слишком рано
			{
				return new List<Decision>();
			}
			var dx = new[] {1, -1, 0, 0, 0};
			var dy = new[] {0, 0, 1, -1, 0};
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
			List<Decision> r = new List<Decision>();
			for (int it = 0; it < state.Sapkas.Length; ++it)
			{
				if (it == state.Me)
				{
					continue;
				}
				Pos p = state.Sapkas[it].Pos;
				if (p == null)
				{
					continue;
				}
				int x = p.X / state.CellSize;
				int y = p.Y / state.CellSize;
				for (int d = 0; d < 5; ++d)
				{
					int i = x + dx[d];
					int j = y + dy[d];
					if (i < 0 || i >= state.Map.GetLength(0) ||
					    j < 0 || j >= state.Map.GetLength(1) ||
					    ds[i, j] == null || state.Map[i, j].IsBomb)
					{
						continue;
					}
					var target = new Pos(i, j);
					var decision = new Decision(ds[i, j], target, targetsPt[i,j], ds[i, j].Size() == 0, ds[i, j].Size(), 1000, "KillBill", true);
					if(state.GetWaitForBombTime() <= decision.Duration)
						r.Add(decision);
					else
					{
						var cell = state.Map[decision.Target.X, decision.Target.Y];
						if (cell.DeadlySince == int.MaxValue || cell.DeadlySince < state.Time + decision.Duration)
						{
							decision = new Decision(
								AddStops(decision.Path, state.GetWaitForBombTime() - decision.Duration),
								decision.Target, targetsPt[decision.Target.X, decision.Target.Y], 
								decision.PutBomb, state.GetWaitForBombTime(), decision.PotentialScore, 
								decision.Name, true);
							r.Add(decision);
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
