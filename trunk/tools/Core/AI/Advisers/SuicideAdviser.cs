using System.Collections.Generic;
using Core.PathFinding;
using Core.StateCalculations;

namespace Core.AI.Advisers
{
	internal class SuicideAdviser : IAdviser
	{
		public IEnumerable<Decision> Advise(GameState state, IPath[,] paths)
		{
			yield return new Decision(null, state.MyCell, true, 1, -1000, "Suicide"); 
		}
	}
}