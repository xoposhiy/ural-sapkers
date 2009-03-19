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
			var finder = new PathFinder();
			var cellSize = state.CellSize;
			finder.SetMap(state.Map, cellSize);
			int x = state.MySapka.Pos.X;
			int y = state.MySapka.Pos.Y;
			for (int d = 0; d < 4; ++d)
			{
				finder.Move(ref x, ref y, state.Time, state.MySapka.Speed, d);
				yield return new Decision(new Path(null, PathFinder.Dir[d]), new Pos(x/cellSize, y/cellSize), new Pos(x, y), false, 1, -1, "RunAway");
			}
		}
	}
}