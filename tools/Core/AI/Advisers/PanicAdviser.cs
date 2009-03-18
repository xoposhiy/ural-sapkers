using System.Collections.Generic;
using Core.PathFinding;
using Core.StateCalculations;

namespace Core.AI.Advisers
{
	internal class PanicAdviser : IAdviser
	{
		public IEnumerable<Decision> Advise(GameState state, IPath[,] paths)
		{
			yield return new Decision(null, state.MyCell, false, 1, 0, "Panic");
		}
	}
}