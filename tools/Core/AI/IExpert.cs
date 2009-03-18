using Core.PathFinding;
using Core.StateCalculations;

namespace Core.AI
{
	internal interface IExpert
	{
		// Чем больше, тем хуже решение. 0 — эксперт не имеет ничего против :)
		byte EstimateDecisionDanger(GameState state, Paths paths, Decision decision);
		void OnNextMove();
	}
}