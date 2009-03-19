using System.Collections.Generic;
using System.Diagnostics;
using Core.AI.Advisers;
using Core.AI.Experts;
using Core.PathFinding;
using Core.StateCalculations;
using log4net;

namespace Core.AI
{
	public class Chief
	{
		private static readonly IList<IAdviser> advisers = new List<IAdviser>();
		private static readonly IList<IExpert> experts = new List<IExpert>();
		private static readonly ILog log = LogManager.GetLogger(typeof (Chief));
		private readonly IInversionDetector inversionDetector;
		private readonly IPath[,] paths;
		private readonly GameState state;

		static Chief()
		{
			advisers.Add(new RunAwayAdviser());
			advisers.Add(new DestroyWallsAdviser());
			advisers.Add(new BonusAdviser());
			experts.Add(new DontGoToDeadlyCell());
			experts.Add(new TargetShouldHaveSense());
		}

		public Chief(GameState state, IInversionDetector inversionDetector)
		{
			this.state = state;
			this.inversionDetector = inversionDetector;
			var finder = new PathFinder();
			finder.SetMap(state.Map, state.CellSize);
			if (state.Sapkas[state.Me] != null && !state.Sapkas[state.Me].IsDead)
			{
				var sw = Stopwatch.StartNew();
				paths = finder.FindPaths(
					state.Sapkas[state.Me].Pos.X, state.Sapkas[state.Me].Pos.Y, state.Time,
					state.Sapkas[state.Me].Speed, Commons.Radius);
				sw.Stop();
				if (sw.ElapsedMilliseconds > 50)
					log.Warn(state.RoundNumber + " " + state.Time +
					         "FindPaths spends too much time: " + sw.ElapsedMilliseconds + " ms (should <= 50)");
			}
		}

		public Decision MakeDecision()
		{
			if (state.Sapkas[state.Me].IsDead)
			{
				if (inversionDetector.Inverted)
					log.Info(state.RoundNumber + " " + state.Time + " SAPKA DIED INVERTED!");
				return Decision.DoNothing;
			}
			Decision best = null;
			double bestBeauty = int.MinValue;
			if (state.Sapkas[state.Me] == null)
			{
				return Decision.DoNothing;
			}
			foreach (var expert in experts)
			{
				expert.OnNextMove();
			}
			foreach (IAdviser adviser in advisers)
			{
				foreach (Decision decision in adviser.Advise(state, paths))
				{
					log.Debug(state.RoundNumber + " " + state.Time + " " + DecisionLogString(decision));
					double beauty = CalculateBeauty(decision);
					if (beauty > bestBeauty)
					{
						best = decision;
						bestBeauty = beauty;
					}
				}
			}
			Decision d = best ?? Decision.DoNothing;
			log.Info(state.RoundNumber + " " + state.Time + " chosen move: " + DecisionLogString(d));
			if (state.Sapkas[state.Me].BombsLeft == 0)
				d = new Decision(d.Path, d.Target, d.TargetPt, false, d.Duration, d.PotentialScore, d.Name, d.WillBomb);
			if (d.PutBomb)
			{
				state.UseBomb();
				log.Info(state.RoundNumber + " " + state.Time + " BOMB!");
			}
			inversionDetector.RegisterMove(state, d.Path == null ? 's' : d.Path.FirstMove());
			if (inversionDetector.Inverted)
			{
				log.Info(state.RoundNumber + " " + state.Time + " INVERTED!");
				d.Inverse = true;
			}
			return d;
		}

		private string DecisionLogString(Decision decision)
		{
			return "from " + state.MyCell + " " + state.Sapkas[state.Me].Pos + " " + decision +
			       " cost:" + decision.PotentialScore + "/" + decision.Duration + " = " +
			       ((double) decision.PotentialScore/decision.Duration);
		}

		private double CalculateBeauty(Decision decision)
		{
			Debug.Assert(decision.Duration > 0);
			// Эти фиговины нужно будет подобрать...
			const double expertWeight = 0.1;
			double result = (double) decision.PotentialScore/decision.Duration;
			foreach (IExpert expert in experts)
			{
				byte expertsEstimate = expert.EstimateDecisionDanger(state, paths, decision);
				if (expertsEstimate == byte.MaxValue)
				{
					result = int.MinValue; // Эксперт сказал «нет», значит «нет»!
					log.Info(state.RoundNumber + " " + state.Time + " " + expert.GetType().Name + " declined " + decision);
				}
				result -= expertsEstimate*expertWeight;
			}
			return result;
		}
	}
}