using Core.PathFinding;
using Core.StateCalculations;

namespace Core.AI
{
	public class DontSleepNearBomb : IExpert
	{
		#region IExpert Members

		public byte EstimateDecisionDanger(GameState state, IPath[,] paths, Decision decision)
		{
			if (decision.Path.FirstMove() != 's') return 0;

			MapCell cell = state.Map[state.MyCell.X, state.MyCell.Y];
			if (cell.DeadlySince < state.Time) return 0;
			int timeToLive = cell.DeadlySince - state.Time;
			if (timeToLive < 10) return 255;
			if (timeToLive <= 2*state.CellSize) return 128;
			return 0;
		}

		public void OnNextMove()
		{
			
		}

		#endregion
	}
}