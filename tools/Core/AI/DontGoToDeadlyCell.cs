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
				var fromTarget = finder.FindPaths(tx * state.CellSize + state.CellSize / 2, ty * state.CellSize + state.CellSize / 2, state.Time + decision.Duration, state.Sapkas[state.Me].Speed);
				for(int x=0; x<fromTarget.GetLength(0); x++)
				for(int y=0; y<fromTarget.GetLength(1); y++)
				{
					if(fromTarget[x, y] == null) continue;
					int cx = x/state.CellSize;
					int cy = y/state.CellSize;
					if (state.Map[cx, cy].DeadlySince == int.MaxValue)
					{
						log.Debug(state.Time + " путь из " + state.MyCell + "в " + decision.Target + " оправдан. »з него можно сбежать в " + new Pos(cx, cy));
						return 0;
					}
				}
				log.Debug(state.Time + " путь из " + state.MyCell + "в " + decision.Target + " ќ“¬≈–√Ќ”“! “ам рванет через " + (cell.DeadlySince - state.Time));
				return 255;
			}
			log.Debug(state.Time + " путь из " + state.MyCell + "в " + decision.Target + " чист. " + cell.DeadlySince);
			return 0;
		}

		private static readonly ILog log = LogManager.GetLogger(typeof (DontGoToDeadlyCell));

	}
}