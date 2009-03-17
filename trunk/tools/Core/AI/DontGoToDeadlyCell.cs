using Core.Parsing;
using Core.PathFinding;
using Core.StateCalculations;
using log4net;

namespace Core.AI
{
	internal class DontGoToDeadlyCell : IExpert
	{
		public byte EstimateDecisionDanger(GameState state, IPath[,] paths, Decision decision)
		{
			int tx = decision.Target.X;
			int ty = decision.Target.Y;
			MapCell cell = state.Map[tx, ty];
			if(cell.DeadlyTill < state.Time) return 0;
			if(cell.DeadlySince - state.Time <= 45)
			{
				var finder = new PathFinder();
				finder.SetMap(state.Map, state.CellSize);
				var targetX = tx * state.CellSize + state.CellSize / 2;
				var targetY = ty * state.CellSize + state.CellSize / 2;
				var canLive = finder.Live(targetX, targetY, state.Time + decision.Duration, state.Sapkas[state.Me].Speed);
				if (!canLive)
				{
					log.Debug(state.Time + " ���� �� " + state.MyCell + "� " + decision.Target + " ���������! ��� ������ ����� " + (cell.DeadlySince - state.Time));
					return 255;
				}
				log.Debug(state.Time + " ���� �� " + state.MyCell + "� " + decision.Target + " �������� � ��� ����� ������...");
			}
			log.Debug(state.Time + " ���� �� " + state.MyCell + "� " + decision.Target + " ����. " + cell.DeadlySince);
			return 0;
		}

		private static readonly ILog log = LogManager.GetLogger(typeof (DontGoToDeadlyCell));

	}
}