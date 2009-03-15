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
			paths = finder.FindPaths(
				state.MyCell.X, state.MyCell.Y, state.Time,
				state.Sapkas[state.Me].Speed);
		}

		public Decision MakeDecision()
		{
			List<Decision> decisions = new List<Decision>();
			foreach (var adviser in advisers)
			{
				decisions.AddRange(adviser.Advise(state, paths));
			}
			
			// TODO ���������� � ����������... ����� ������ �������.
			decisions.Sort((x,y) => y.PotentialScore / y.Duration - x.PotentialScore / x.Duration);
			if(decisions.Count == 0) return Decision.DoNothing;
			return decisions[0];
		}

		private static readonly IList<IAdviser> advisers = new List<IAdviser>();
	}

	internal class DestroyWallsAdviser : IAdviser
	{
		public IEnumerable<Decision> Advise(GameState state, IPath[,] paths)
		{
			yield return new Decision(null, true, 1, -1000); //���� ����! ����� ���! �����������! ���������! ��� � ��� ������...
		}
	}

	internal interface IAdviser
	{
		IEnumerable<Decision> Advise(GameState state, IPath[,] paths);
	}

	internal interface IExpert
	{
		// ��� ������, ��� ���� �������. 0 � ������� �� ����� ������ ������ :)
		byte EstimateDecisionDanger(GameState state, Decision decision);
	}
}