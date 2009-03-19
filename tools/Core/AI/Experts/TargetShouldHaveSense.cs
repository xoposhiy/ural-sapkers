using Core.Parsing;
using Core.PathFinding;
using Core.StateCalculations;

namespace Core.AI.Experts
{
	internal class TargetShouldHaveSense : AbstractExpert
	{
		public override byte EstimateDecisionDanger(GameState state, IPath[,] paths, Decision decision)
		{
			int tx = decision.Target.X;
			int ty = decision.Target.Y;
			int txp = decision.TargetPt.X;
			int typ = decision.TargetPt.Y;
			MapCell cell = state.Map[tx, ty];
			if(cell.DeadlyTill < state.Time) return 0;
			if(cell.DeadlySince - state.Time <= 45)
			{
				Bomb? b = null;
				if(decision.WillBomb)
				{
					b = new Bomb(tx, ty, state.MySapka.BombsStrength, state.Time + decision.Duration + Commons.BombTimeout);
					state.AddBomb(b.Value);
					state.RecalcDeadly();
				}

				var finder = new PathFinder();
				finder.SetMap(state.Map, state.CellSize);
				var canLive = finder.Live(txp, typ, state.Time + decision.Duration, state.MySapka.Speed);
				log.InfoFormat("{0}\t{1}. {2}", state.Time, canLive ? "Y" : "n", decision);
				if(b.HasValue)
				{
					state.RemoveBomb(b.Value);
					state.RecalcDeadly();
				}
				if (!canLive)
				{
					return 255;
				}
			}
			return 0;
		}
	}
}