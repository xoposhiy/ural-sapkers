using Core.Parsing;
using Core.PathFinding;
using Core.StateCalculations;
using log4net;

namespace Core.AI.Experts
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
					bomb = new Bomb(tx / state.CellSize, ty / state.CellSize, state.Sapkas[state.Me].BombsStrength, state.Time + Commons.BombTimeout);
					state.AddBomb(bomb.Value);
				}
				MapCell backup = null;
				int speed = state.Sapkas[state.Me].Speed;
				if (dir != -1)
				{
					if (!finder.Move(ref tx, ref ty, state.Time, speed, dir))
					{
						cache[dir + 1, bom] = 2;
					}
					char bonus = state.Map[tx / state.CellSize, ty / state.CellSize].Bonus;
					if (bonus == 's' || bonus == '?')
					{
						speed = 1;
					}
					string badBonus = "rsuo?";
					if (badBonus.IndexOf(bonus) != -1 && state.Sapkas[state.Me].Infected)
					{
						//Две инфекции почти всегда фатальны
						//cache[dir + 1, bom] = 2;
					}
					backup = state.Map[tx / state.CellSize, ty / state.CellSize];
					state.Map[tx / state.CellSize, ty / state.CellSize] =
						new MapCell(
                              backup.IsUnbreakableWall,
                              backup.IsBreakableWall,
                              backup.IsEmpty,
                              int.MaxValue,
                              int.MaxValue,
                              int.MaxValue,
                              '.');
				}
				state.RecalcDeadly();
				if (cache[dir + 1, bom] == 0)
				{
					cache[dir + 1, bom] = finder.Live(tx, ty, state.Time + 1, speed) ? 1 : 2;
				}
				if (bomb != null)
				{
					state.RemoveBomb(bomb.Value);
				}
				if (backup != null)
				{
					state.Map[tx / state.CellSize, ty / state.CellSize] = backup;
				}
				state.RecalcDeadly();
			}
			return (byte)(cache[dir + 1, bom] == 1 ? 0 : 255);
		}

		public void OnNextMove()
		{
			cache = new int[5, 2];
		}

		private static readonly ILog log = LogManager.GetLogger(typeof (DontGoToDeadlyCell));

	}
}