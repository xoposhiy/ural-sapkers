using Core.PathFinding;
using Core.StateCalculations;
using System.Collections.Generic;

namespace Core.AI.Advisers
{
	public class RunAwayAdviser : IAdviser
	{
		public RunAwayAdviser()
		{
		}
		
		public IEnumerable<Decision> Advise(GameState state, IPath[,] paths)
		{
			for (int d = 0; d < 4; ++d)
			{
				yield return new Decision(new Path(null, PathFinder.Dir[d]), state.MyCell, false, 1, -1, "RunAway");
			}
		}
	}
}