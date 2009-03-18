using System.Collections.Generic;
using Core.PathFinding;
using Core.StateCalculations;

namespace Core.AI
{
	internal interface IAdviser
	{
		IEnumerable<Decision> Advise(GameState state, Paths paths);
	}
}