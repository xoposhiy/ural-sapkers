using System.Collections.Generic;
using Core.PathFinding;
using Core.StateCalculations;

namespace Core.AI.Advisers
{
	internal class PanicAdviser : IAdviser
	{
		public IEnumerable<Decision> Advise(GameState state, IPath[,] paths)
		{
			var decision = new Decision(null, state.MyCell, false, 1, 0);
			decision.Name = "Panic";
			yield return decision;
		}
	}
}