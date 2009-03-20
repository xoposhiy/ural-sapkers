using System;
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
		private static readonly ILog log = LogManager.GetLogger(typeof (Chief));
		private static readonly ILog performanceLog = LogManager.GetLogger("performance");
		private readonly IList<IAdviser> advisers = new List<IAdviser>();
		private readonly IList<IExpert> experts = new List<IExpert>();
		private readonly IDictionary<Type, Stat> performanceStats = new Dictionary<Type, Stat>();
		private int timeBreaks = 0;
		private int moves = 0;
		public int skippedMoves;

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
			if (state.Time % 100 == 0) WriteStats();
			moves++;
			Stopwatch sw = Stopwatch.StartNew();
			var finder = new PathFinder();
			finder.SetMap(state.Map, state.CellSize);
			IPath[,] paths = null;
			if (state.Sapkas[state.Me] != null && !state.Sapkas[state.Me].IsDead)
			{
				paths = finder.FindPaths(
					state.Sapkas[state.Me].Pos.X, state.Sapkas[state.Me].Pos.Y, state.Time,
					state.Sapkas[state.Me].Speed, Commons.Radius);
			}

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
				foreach (Decision decision in GetDecisions(state, adviser, paths))
				{
					log.Debug(state.RoundNumber + " " + state.Time + " " + DecisionLogString(decision, state));
					double beauty = CalculateBeauty(decision, bestBeauty, state, paths);
					if (beauty > bestBeauty)
					{
						best = decision;
						bestBeauty = beauty;
					}
					if (sw.ElapsedMilliseconds > 40) break;
				}
				if (sw.ElapsedMilliseconds > 40)
				{
					timeBreaks++;
					break;
				}
			}
			Decision d = best ?? Decision.DoNothing;
			if (bestBeauty == 0) d = Decision.DoNothing;
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
			if (sw.ElapsedMilliseconds > 50)
				performanceLog.InfoFormat("{0}\tВылезли за 50 :(  {1} ms", state.Time, sw.ElapsedMilliseconds);
			return d;
		}

		private void WriteStats()
		{
			var list = new List<Type>(performanceStats.Keys);
			list.Sort((x,y) => x.Name.CompareTo(y.Name));
			foreach (var type in list)
			{
				performanceLog.Info(type.Name + "\t" + performanceStats[type]);
			}
			performanceLog.InfoFormat("Ходов: {0}, отсечек по времени {1}, пропущено {2}", moves, timeBreaks, skippedMoves);
		}

		private byte GetExpertsEstimate(Decision decision, IExpert expert, GameState state, IPath[,] paths)
		{
			Stopwatch sw = Stopwatch.StartNew();
			var res = expert.EstimateDecisionDanger(state, paths, decision);
			sw.Stop();
			Type t = expert.GetType();
			if (!performanceStats.ContainsKey(t))
				performanceStats.Add(t, new Stat());
			performanceStats[t].Add(sw.ElapsedMilliseconds);
			return res;
		}

		private IEnumerable<Decision> GetDecisions(GameState state, IAdviser adviser, IPath[,] paths)
		{
			Stopwatch sw = Stopwatch.StartNew();
			IList<Decision> res = adviser.Advise(state, paths);
			sw.Stop();
			Type t = adviser.GetType();
			if (!performanceStats.ContainsKey(t))
				performanceStats.Add(t, new Stat());
			performanceStats[t].Add(sw.ElapsedMilliseconds);
			return res;
		}

		private string DecisionLogString(Decision decision, GameState state)
		{
			return "from " + state.MyCell + " " + state.Sapkas[state.Me].Pos + " " + decision +
			       " cost:" + decision.PotentialScore + "/" + decision.Duration + " = " +
			       (decision.PotentialScore/decision.Duration);
		}

		private double CalculateBeauty(Decision decision, double bestBeauty, GameState state, IPath[,] paths)
		{
			Debug.Assert(decision.Duration > 0, decision.ToString());
			// Эти фиговины нужно будет подобрать...
			double result = decision.PotentialScore/decision.Duration;
			if (result <= bestBeauty) return 0;
			foreach (IExpert expert in experts)
			{
				byte expertsEstimate = GetExpertsEstimate(decision, expert, state, paths);
				if (expertsEstimate == byte.MaxValue)
				{
					result = 0; // Эксперт сказал «нет», значит «нет»!
					log.Info(state.RoundNumber + " " + state.Time + " " + expert.GetType().Name + " declined " + decision);
				}
				result *= (255 - expertsEstimate);
				result /= 255;
				if (result <= bestBeauty) return 0;
			}
			return result;
		}


		#region Nested type: Stat

		private class Stat
		{
			public long Count;
			public long TotalTime;

			public double AvTime
			{
				get { return Count > 0 ? (double)TotalTime/Count : 0; }
			}

			public void Add(long time)
			{
				Count++;
				TotalTime += time;
			}

			public override string ToString()
			{
				return string.Format("Total {0} = {1} ms * {2} launches.", TotalTime, AvTime, Count);
			}
		}

		#endregion
	}
}