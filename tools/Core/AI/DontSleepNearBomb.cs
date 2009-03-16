using System;
using System.Collections.Generic;
using System.Text;
using Core.PathFinding;
using Core.StateCalculations;

namespace Core.AI
{
	public class DontSleepNearBombExpert : IExpert
	{
		public byte EstimateDecisionDanger(GameState state, IPath[,] paths, Decision decision)
		{
			if (decision.Path.FirstMove() != 's') return 0;

			var cell = state.Map[state.MyCell.X, state.MyCell.Y];
			if(cell.DeadlySince < state.Time) return 0;
			var timeToLive = cell.DeadlySince - state.Time;
			if(timeToLive < 10) return 255;
			if(timeToLive <= 2*state.CellSize) return 128;
			return 0;
		}
	}
}
