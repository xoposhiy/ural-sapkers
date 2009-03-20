using Core.PathFinding;
using Core.StateCalculations;

namespace Core.AI.Experts
{
	internal class CanLiveAfterTarget : AbstractExpert
	{
		public override byte EstimateDecisionDanger(GameState state, IPath[,] paths, Decision decision)
		{
			if (decision.Name == "RunAway")
			{
				return 0;
			}
			bool canLive = CalculateCanLive(decision, state, decision.WillBomb);
			if(canLive) return 0;
			if(decision.WillBomb)
			{
				canLive = CalculateCanLive(decision, state, false);
				if(canLive) return 128;
			}
			return 255;
		}

		private bool CalculateCanLive(Decision decision, GameState state, bool putBomb)
		{
			int tx = decision.Target.X;
			int ty = decision.Target.Y;
			int txp = decision.TargetPt.X;
			int typ = decision.TargetPt.Y;
			Bomb? b = null;
			if(putBomb)
			{
				b = new Bomb(tx, ty, state.MySapka.BombsStrength, state.Time + decision.Duration + Commons.BombTimeout);
				state.AddBomb(b.Value);
				state.RecalcDeadly();
			}

			var finder = new PathFinder();
			finder.SetMap(state.Map, state.CellSize);
			bool canLive = finder.Live(txp, typ, state.Time + decision.Duration, state.MySapka.Speed);
			log.InfoFormat("{0}\t{1}. {2} bomb? = {3}", state.Time, canLive ? "Y" : "n", decision, putBomb);
			if (b.HasValue)
			{
				state.RemoveBomb(b.Value);
				state.RecalcDeadly();
			}
			return canLive;
		}
	}
}