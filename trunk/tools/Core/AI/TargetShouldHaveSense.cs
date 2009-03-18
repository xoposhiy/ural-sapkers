using Core.Parsing;
using Core.PathFinding;
using Core.StateCalculations;

namespace Core.AI
{
	internal class TargetShouldHaveSense : AbstractExpert
	{
		public override byte EstimateDecisionDanger(GameState state, IPath[,] paths, Decision decision)
		{
			int tx = decision.Target.X;
			int ty = decision.Target.Y;
			MapCell cell = state.Map[tx, ty];
			if(cell.DeadlyTill < state.Time) return 0;
			if(cell.DeadlySince - state.Time <= 45)
			{
				var targetX = tx * state.CellSize + state.CellSize / 2;
				var targetY = ty * state.CellSize + state.CellSize / 2;
				
				var b = new Bomb(tx, ty, state.MySapka.BombsStrength, state.Time + decision.Duration + Constants.BombTimeout);
				state.AddBomb(b);
				state.RecalcDeadly();
				
				var finder = new PathFinder();
				finder.SetMap(state.Map, state.CellSize);
				var canLive = finder.Live(targetX, targetY, state.Time + decision.Duration, state.MySapka.Speed);
				log.InfoFormat("{0}\t{1}. {2}", state.Time, canLive ? "Y" : "n", decision);
				state.RemoveBomb(b);
				state.RecalcDeadly();
				
				if (!canLive)
				{
					return 255;
				}
			}
			return 0;	
		}
	}
}