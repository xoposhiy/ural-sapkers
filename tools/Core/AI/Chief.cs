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
		private readonly IList<IAdviser> advisers = new List<IAdviser>();
		private readonly IList<IExpert> experts = new List<IExpert>();
		private static readonly ILog log = LogManager.GetLogger(typeof (Chief));

		public Chief()
		{
			advisers.Add(new RunAwayAdviser());
			advisers.Add(new KillBillAdviser());
			advisers.Add(new DestroyWallsAdviser());
			advisers.Add(new BonusAdviser());
			experts.Add(new DontGoToDeadlyCell());
			experts.Add(new CanLiveAfterTarget());
		}

		public Decision MakeDecision(GameState state, IInversionDetector inversionDetector)
		{
			Stopwatch sw = Stopwatch.StartNew();
			var finder = new PathFinder();
			finder.SetMap(state.Map, state.CellSize);
			IPath[,] paths = null;
			if(state.Sapkas[state.Me] != null && !state.Sapkas[state.Me].IsDead)
			{
				paths = finder.FindPaths(
					state.Sapkas[state.Me].Pos.X, state.Sapkas[state.Me].Pos.Y, state.Time,
					state.Sapkas[state.Me].Speed, Commons.Radius);
			}
	
			if(state.Sapkas[state.Me].IsDead)
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
					log.Debug(state.RoundNumber + " " + state.Time + " " + DecisionLogString(decision, state));
					double beauty = CalculateBeauty(decision, bestBeauty, state, paths);
					if (beauty > bestBeauty)
					{
						best = decision;
						bestBeauty = beauty;
					}
					if (sw.ElapsedMilliseconds > 40)
					{
						log.Info("Отсечка по времени!");
						break;
					}
				}
				if(sw.ElapsedMilliseconds > 40)
				{
					log.Info("Отсечка по времени!");
					break;
				}
			}
			Decision d = best ?? Decision.DoNothing;
			if(bestBeauty == 0) d = Decision.DoNothing;
			log.Info(state.RoundNumber + " " + state.Time + " chosen move: " + DecisionLogString(d, state));
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
			sw.Stop();
			performanceLog.InfoFormat("{0}\t{1} ms", state.Time, sw.ElapsedMilliseconds);
			return d;
		}

		private static readonly ILog performanceLog = LogManager.GetLogger("performance");

		private string DecisionLogString(Decision decision, GameState state)
		{
			return "from " + state.MyCell + " " + state.Sapkas[state.Me].Pos + " " + decision +
			       " cost:" + decision.PotentialScore + "/" + decision.Duration + " = " +
			       (decision.PotentialScore/decision.Duration);
		}

		private double CalculateBeauty(Decision decision, double bestBeauty, GameState state, IPath[,] paths)
		{
			Debug.Assert(decision.Duration > 0);
			// Эти фиговины нужно будет подобрать...
			double result = decision.PotentialScore/decision.Duration;
			if (result <= bestBeauty) return 0;
			foreach (IExpert expert in experts)
			{
				byte expertsEstimate = expert.EstimateDecisionDanger(state, paths, decision);
				if (expertsEstimate == byte.MaxValue)
				{
					result = 0; // Эксперт сказал «нет», значит «нет»!
					log.Info(state.RoundNumber + " " + state.Time + " " + expert.GetType().Name + " declined " + decision);
				}
				result *= (255 - expertsEstimate);
				result /= 255;
				if(result <= bestBeauty) return 0;
			}
			return result;
		}
	}
}