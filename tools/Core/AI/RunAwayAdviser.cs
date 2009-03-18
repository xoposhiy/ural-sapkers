using System;
using Core.AI;
using Core.PathFinding;
using Core.StateCalculations;
using System.Collections.Generic;

namespace Core
{
	public class RunAwayAdviser : IAdviser
	{
		public RunAwayAdviser()
		{
		}
		
		public IEnumerable<Decision> Advise(GameState state, Paths paths)
		{
			for (int d = 0; d < 4; ++d)
			{
				var decision = new Decision(new Path(null, PathFinder.Dir[d]), state.MyCell, false, 1, -1);
				decision.Name = "Run away";
				yield return decision;
			}
		}
	}
}
