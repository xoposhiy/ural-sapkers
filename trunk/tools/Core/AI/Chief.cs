using System;
using System.Collections.Generic;
using System.Diagnostics;
using Core;
using Core.Parsing;
using Core.PathFinding;
using Core.StateCalculations;

namespace Core.AI
{
	public class Chief
	{
		private readonly GameState state;
		private readonly IPath[,] paths;

		static Chief()
		{
			// Мусор надо будет удалить. Сейчас он для тестирования.
			advisers.Add(new SuicideAdviser());
			advisers.Add(new PanicAdviser());
			advisers.Add(new DestroyWallsAdviser());
			experts.Add(new CanNotRunFromBombExpert());
		}

		public Chief(GameState state)
		{
			this.state = state;
			var finder = new PathFinder();
			finder.SetMap(state.Map, state.CellSize);
			if(state.Sapkas[state.Me] != null && !state.Sapkas[state.Me].IsDead)
			{
				paths = finder.FindPaths(
					state.Sapkas[state.Me].Pos.X, state.Sapkas[state.Me].Pos.Y, state.Time,
					state.Sapkas[state.Me].Speed);
			}
		}

		public Decision MakeDecision()
		{
			if(state.Sapkas[state.Me].IsDead) return Decision.DoNothing;
			Decision best = null;
			double bestBeauty = int.MinValue;
			if (state.Sapkas[state.Me] == null)
			{
				return Decision.DoNothing;
			}
			foreach (var adviser in advisers)
			{
				foreach (var decision in adviser.Advise(state, paths))
				{
					double beauty = CalculateBeauty(decision);
					if (beauty > bestBeauty)
					{
						best = decision;
						bestBeauty = beauty;
					}
				}
			}
			var d = best ?? Decision.DoNothing;
			Console.WriteLine("Chief choose " + d);
			return d;
		}

		private double CalculateBeauty(Decision decision)
		{
			Debug.Assert(decision.Duration > 0);
			// Эти фиговины нужно будет подобрать...
			const double expertWeight = 0.1;
			double result = (double)decision.PotentialScore/decision.Duration;
			foreach (var expert in experts)
			{
				var expertsEstimate = expert.EstimateDecisionDanger(state, paths, decision);
				if(expertsEstimate == byte.MaxValue) 
				{
					result = int.MinValue; // Эксперт сказал «нет», значит «нет»!
					Console.WriteLine(expert.GetType().Name + " declined " + decision);
				}
				result -= expertsEstimate * expertWeight;
			}
			return result;
		}

		private static readonly IList<IAdviser> advisers = new List<IAdviser>();
		private static readonly IList<IExpert> experts = new List<IExpert>();
	}

	internal class CanNotRunFromBombExpert : IExpert
	{
		// TODO pe: Не учитывает собранные при отходе антибонусы.
		// TODO pe: Не учитывает взаимодетанирование бомб.
		public byte EstimateDecisionDanger(GameState state, IPath[,] paths, Decision decision)
		{
			Pos me = state.MyCell;
			SapkaInfo sapka = state.Sapkas[state.Me];
			if (!decision.PutBomb) return 0;
			int safePositions = 0;
			for(int xp=0; xp<paths.GetLength(0); xp++)
				for(int yp = 0; yp < paths.GetLength(1); yp++)
				{
					
					if (paths[xp, yp] != null 
						&& ((xp/10 != me.X && yp/10 != me.Y)
						|| Math.Abs(xp/10 - me.X) > sapka.BombsStrength
						|| Math.Abs(yp/10 - me.Y) > sapka.BombsStrength) 
						&& paths[xp,yp].Size() < Constants.BombTimeout) safePositions++;
				}
			if (safePositions == 0) return 255;
			if(safePositions < 4) return 128;
			return 0;
		}
	}

	internal class SuicideAdviser : IAdviser
	{
		public IEnumerable<Decision> Advise(GameState state, IPath[,] paths)
		{
			var decision = new Decision(null, state.MyCell, true, 1, -1000);
			decision.Name = "Suicide";
			yield return decision; //Убей себя! Выпей яду! Вгазенваген! Неудачнег! Лох — это судьба...
		}
	}

	internal class PanicAdviser : IAdviser
	{
		public IEnumerable<Decision> Advise(GameState state, IPath[,] paths)
		{
			var decision = new Decision(null, state.MyCell, false, 1, 0);
			decision.Name = "Panic";
			yield return decision;
		}
	}


	internal interface IExpert
	{
		// Чем больше, тем хуже решение. 0 — эксперт не имеет ничего против :)
		byte EstimateDecisionDanger(GameState state, IPath[,] paths, Decision decision);
	}
}
