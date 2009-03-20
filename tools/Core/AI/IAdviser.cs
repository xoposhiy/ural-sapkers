using System.Collections.Generic;
using Core.PathFinding;
using Core.StateCalculations;

namespace Core.AI
{
	internal interface IAdviser
	{
		IList<Decision> Advise(GameState state, IPath[,] paths);
	}
}