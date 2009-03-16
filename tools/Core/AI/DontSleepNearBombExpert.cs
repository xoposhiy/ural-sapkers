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
			int tx = decision.Target.X;
			int ty = decision.Target.Y;
			if(state.Map[tx, ty].IsDeadlyAt(state.Time + decision.Duration))
				return 255;
			return 0;
		}
	}
}
