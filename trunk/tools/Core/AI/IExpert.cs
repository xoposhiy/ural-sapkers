using Core.PathFinding;
using Core.StateCalculations;

namespace Core.AI
{
	internal interface IExpert
	{
		// „ем больше, тем хуже решение. 0 Ч эксперт не имеет ничего против :)
		byte EstimateDecisionDanger(GameState state, IPath[,] paths, Decision decision);
	}
}