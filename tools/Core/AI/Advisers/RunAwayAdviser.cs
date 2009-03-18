using System;
using Core.AI;
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
				var decision = new Decision(new Path(null, PathFinder.Dir[d], 1), state.MyCell, false, 1, -1);
				decision.Name = "Run away";
				yield return decision;
			}
		}
	}
}