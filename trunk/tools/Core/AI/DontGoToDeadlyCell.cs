using Core.Parsing;
using Core.PathFinding;
using Core.StateCalculations;
using log4net;

namespace Core.AI
{
	internal class DontGoToDeadlyCell : IExpert
	{
		private int[,] cache;
		
		public DontGoToDeadlyCell()
		{
			cache = new int[5,2];
		}
		
		public byte EstimateDecisionDanger(GameState state, IPath[,] paths, Decision decision)
		{
			int dir = PathFinder.Dir.IndexOf(decision.Path.FirstMove());
			int bom = decision.PutBomb ? 1 : 0;
			if (cache[dir + 1, bom] == 0)
			{
				int tx = state.Sapkas[state.Me].Pos.X;
				int ty = state.Sapkas[state.Me].Pos.Y;
				var finder = new PathFinder();
				finder.SetMap(state.Map, state.CellSize);
				Bomb? bomb = null;
				if (bom == 1)
				{
					bomb = new Bomb(tx / state.CellSize, ty / state.CellSize, state.Sapkas[state.Me].BombsStrength, state.Time + Constants.BombTimeout);
					state.AddBomb(bomb.Value);
					state.RecalcDeadly();
				}
				if (dir != -1)
				{
					if (!finder.Move(ref tx, ref ty, state.Time, state.Sapkas[state.Me].Speed, dir))
					{
						cache[dir + 1, bom] = 1;
					}
				}
				if (cache[dir + 1, bom] == 0)
				{
					cache[dir + 1, bom] = finder.Live(tx, ty, state.Time + 1, state.Sapkas[state.Me].Speed) ? 1 : 2;
				}
				if (bomb != null)
				{
					state.RemoveBomb(bomb.Value);
					state.RecalcDeadly();
				}
			}
			return (byte)(cache[dir + 1, bom] == 1 ? 0 : 255);
			/*int tx = decision.Target.X;
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
					log.Debug(state.Time + " путь из " + state.MyCell + "в " + decision.Target + " ОТВЕРГНУТ! Там рванет через " + (cell.DeadlySince - state.Time));
					return 255;
				}
				log.Debug(state.Time + " путь из " + state.MyCell + "в " + decision.Target + " оправдан — там можно выжить...");
			}
			log.Debug(state.Time + " путь из " + state.MyCell + "в " + decision.Target + " чист. " + cell.DeadlySince);
			return 0;*/
		}

		public void OnNextMove()
		{
			cache = new int[5, 2];
		}

		private static readonly ILog log = LogManager.GetLogger(typeof (DontGoToDeadlyCell));

	}
}