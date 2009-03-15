using Core.Parsing;

namespace Core.StateCalculations
{
	public class GameState : IParserListener
	{
		public int CellSize { get; private set; }
		public int DangerLevel { get; private set; }
		public MapCell[,] Map { get; private set; }
		public int Me { get; private set; }
		public SapkaInfo[] Sapkas { get; private set; }
		public int Time { get; private set; }
		public int RoundNumber { get; private set; }

		public Pos MyCell
		{
			get
			{
				if (Sapkas[Me].IsDead) return new Pos(0,0); // Мертвые на небесах!
				Pos pointsPos = Sapkas[Me].Pos;
				return new Pos(pointsPos.X/CellSize, pointsPos.Y/CellSize);
			}
		}

		#region IParserListener Members

		public void OnGameStart(GameInfo gameInfo)
		{
			DangerLevel = 0;
			Me = gameInfo.PID;
			CellSize = gameInfo.MapInfo.MapCellSize;
			InitMapFrom(gameInfo.MapInfo.Map);
		}

		public void OnRoundStart(StartRoundInfo startRoundInfo)
		{
			DangerLevel = 0;
			RoundNumber = startRoundInfo.RoundNumber;
			CellSize = startRoundInfo.MapInfo.MapCellSize;
			InitMapFrom(startRoundInfo.MapInfo.Map);
		}

		public void OnMapChange(MapChangeInfo info)
		{
			DangerLevel = info.HasDangerLevel ? info.DangerLevel : 0;
			Time = info.Time;
			Sapkas = info.Sapkas;
			foreach (AddInfo add in info.Adds)
			{
				Pos p = add.Pos;
				if (add.SubstanceType == '*')
				{
					int start = Time + add.Time;
					int end = start + MapCell.BoomDuration;
					Map[p.X, p.Y] = Map[p.X, p.Y].AddBomb(start, end);
					RecalcDeadly(p.X, p.Y, add.DamagingRange, start, end);
				}
				else if (add.SubstanceType == '#')
				{
					int start = Time;
					int end = Time + add.Time;
					Map[p.X, p.Y] = Map[p.X, p.Y].MakeDeadly(Time, Time + add.Time);
					RecalcDeadly(p.X, p.Y, add.DamagingRange, start, end);
				}
				else if (add.SubstanceType == 'w')
					Map[p.X, p.Y] = Map[p.X, p.Y].AddBreakableWall();
				else if (add.SubstanceType == 'X')
					Map[p.X, p.Y] = Map[p.X, p.Y].AddUnbreakableWall();
				else
				{
					Map[p.X, p.Y].AddBonus(add.SubstanceType);
				}
			}
		}

		public void OnFinishRound(int score)
		{
		}

		public void OnFinishGame(PlayerResult[] playerResults)
		{
		}

		#endregion

		private void RecalcDeadly(int x, int y, int range, int startTime, int endTime)
		{
			var dx = new[] {1, -1, 0, 0};
			var dy = new[] {0, 0, 1, -1};
			for (int dir = 0; dir < 4; dir++)
			{
				for (int r = 1; r <= range; r++)
				{
					//TODO Не учитывается взаимодетонация бомб
					int xx = x + dx[dir] * r;
					int yy = y + dy[dir] * r;
					if(xx < 0 || xx >= Map.GetLength(0) ||
					   yy < 0 || yy >= Map.GetLength(1) || 
					   Map[xx, yy].IsUnbreakableWall) break;
					if(Map[xx, yy].IsBreakableWall)
					{
						Map[xx, yy] = Map[xx, yy].MakeDeadly(startTime, endTime);
						break;
					}
					if (Map[xx, yy].IsEmpty)
					{
						Map[xx, yy] = Map[xx, yy].MakeDeadly(startTime, endTime);
					}
				}
			}
		}

		private void InitMapFrom(char[,] m)
		{
			Map = new MapCell[m.GetLength(0),m.GetLength(1)];
			for (int x = 0; x < m.GetLength(0); x++)
				for (int y = 0; y < m.GetLength(1); y++)
					Map[x, y] = new MapCell(m[x, y]);
		}
	}
}
