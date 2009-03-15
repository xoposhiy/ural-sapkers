using System;
using System.Collections.Generic;
using Core;
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
			advisers.Add(new DestroyWallsAdviser());
		}
		public Chief(GameState state)
		{
			this.state = state;
			var finder = new PathFinder();
			finder.SetMap(state.Map, state.CellSize);
			if (state.Sapkas[state.Me] != null)
			{
				paths = finder.FindPaths(
					state.Sapkas[state.Me].Pos.X, state.Sapkas[state.Me].Pos.Y, state.Time,
					state.Sapkas[state.Me].Speed);
			}
		}

		public Decision MakeDecision()
		{
			if (state.Sapkas[state.Me] == null)
			{
				return Decision.DoNothing;
			}
			List<Decision> decisions = new List<Decision>();
			foreach (var adviser in advisers)
			{
				decisions.AddRange(adviser.Advise(state, paths));
			}
			
			// TODO сортировка — излишество... Нужен просто минимум.
			decisions.Sort((x,y) => y.PotentialScore / y.Duration - x.PotentialScore / x.Duration);
			if(decisions.Count == 0)
			{
				return Decision.DoNothing;
			}
			return decisions[0];
		}

		private static readonly IList<IAdviser> advisers = new List<IAdviser>();
	}



	internal interface IExpert
	{
		// Чем больше, тем хуже решение. 0 — эксперт не имеет ничего против :)
		byte EstimateDecisionDanger(GameState state, Decision decision);
	}
}