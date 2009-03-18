using Core.PathFinding;
using Core.StateCalculations;
using log4net;

namespace Core.AI
{
	internal interface IExpert
	{
		// „ем больше, тем хуже решение. 0 Ч эксперт не имеет ничего против :)
		byte EstimateDecisionDanger(GameState state, IPath[,] paths, Decision decision);
		void OnNextMove();
	}

	abstract class AbstractExpert : IExpert
	{
		public abstract byte EstimateDecisionDanger(GameState state, IPath[,] paths, Decision decision);

		protected AbstractExpert()
		{
			log = LogManager.GetLogger(GetType());
		}

		protected ILog log;

		public void OnNextMove()
		{
		}
	}
}