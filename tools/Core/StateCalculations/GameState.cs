using System;
using Core.Parsing;
using System.Collections.Generic;

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
		public SapkaInfo MySapka { get { return Sapkas[Me]; } }
		
		private List<Bomb> bombs;
		private int lastUsedBomb;
		private bool lastInverted;

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
			bombs = new List<Bomb>();
			Time = 0;
		}

		public void OnRoundStart(StartRoundInfo startRoundInfo)
		{
			DangerLevel = 0;
			RoundNumber = startRoundInfo.RoundNumber;
			CellSize = startRoundInfo.MapInfo.MapCellSize;
			InitMapFrom(startRoundInfo.MapInfo.Map);
			bombs = new List<Bomb>();
			Time = 0;
		}

		public MapChangeInfo LastInfo { get; private set;}
		
		public void OnMapChange(MapChangeInfo info)
		{
			LastInfo = info;
			DangerLevel = info.HasDangerLevel ? info.DangerLevel : 0;
			Time = info.Time;
			Sapkas = info.Sapkas;
			foreach (AddInfo add in info.Adds)
			{
				Pos p = add.Pos;
				if (add.SubstanceType == '*')
				{
					AddBomb(new Bomb(p.X, p.Y, add.DamagingRange, Time + Commons.BombTimeout - 1));
				}
				else if (add.SubstanceType == '#')
				{
				}
				else if (add.SubstanceType == 'w')
					Map[p.X, p.Y] = Map[p.X, p.Y].AddBreakableWall();
				else if (add.SubstanceType == 'X')
					Map[p.X, p.Y] = Map[p.X, p.Y].AddUnbreakableWall();
				else
				{
					Map[p.X, p.Y] = Map[p.X, p.Y].AddBonus(add.SubstanceType);
				}
			}
			foreach (RemoveInfo rem in info.Removes)
			{
				if (rem.SubstanceType == 'w')
				{
					Map[rem.Pos.X, rem.Pos.Y] = new MapCell(
				                                        false,
				                                        false,
				                                        true,
				                                        int.MaxValue,
				                                        int.MaxValue,
				                                        int.MaxValue,
				                                        Map[rem.Pos.X, rem.Pos.Y].Bonus);
				} else
				if (rem.SubstanceType != '*' && rem.SubstanceType != '#')
				{
					Map[rem.Pos.X, rem.Pos.Y] = Map[rem.Pos.X, rem.Pos.Y].AddBonus('.');
				}
			}
			RecalcDeadly();
		}

		public void AddBomb(Bomb b)
		{
			bombs.Add(b);
		}
		
		public void RemoveBomb(Bomb b)
		{
			bombs.Remove(b);
		}

		public void OnFinishRound(int score)
		{
		}

		public void OnFinishGame(PlayerResult[] playerResults)
		{
		}

		#endregion

		public void RecalcDeadly()
		{
			var dx = new[] {1, -1, 0, 0};
			var dy = new[] {0, 0, 1, -1};
			bombs.RemoveAll(Expired);
			int[,] bf = new int[Map.GetLength(0), Map.GetLength(1)];
			for (int i = 0; i < Map.GetLength(0); ++i)
			{
				for (int j = 0; j < Map.GetLength(1); ++j)
				{
					Map[i, j] = new MapCell(
				                        Map[i, j].IsUnbreakableWall,
				                        Map[i, j].IsBreakableWall,
				                        Map[i, j].IsEmpty,
				                        int.MaxValue,
				                        int.MaxValue,
				                        int.MaxValue,
				                        Map[i, j].Bonus);
					bf[i, j] = -1;
				}
			}
			int[] r = new int[bombs.Count];
			int[] p = new int[bombs.Count];
			for (int i = 0; i < bombs.Count; ++i)
			{
				p[i] = i;
				bf[bombs[i].X, bombs[i].Y] = i;
			}
			for (int i = 0; i < bombs.Count; ++i)
			{
				p[i] = i;
				bf[bombs[i].X, bombs[i].Y] = i;
			}
			for (int i = 0; i < bombs.Count; ++i)
			{
				Bomb b = bombs[i];
				for (int d = 0; d < 4; ++d)
				{
					int x = b.X;
					int y = b.Y;
					for (int it = 0; it < b.DamagingRange; ++it)
					{
						if (x >= 0 && x < Map.GetLength(0) &&
						    y >= 0 && y < Map.GetLength(1) &&
						    Map[x, y].IsEmpty &&
						    Map[x, y].Bonus == '.' &&
						    (bf[x, y] == -1 || x == b.X && y == b.Y))
						{
							x += dx[d];
							y += dy[d];
						}
					}
					if (x >= 0 && x < Map.GetLength(0) &&
					    y >= 0 && y < Map.GetLength(1) &&
					    bf[x, y] != -1)
					{
						Unite(i, bf[x, y], p, r);
					}
				}
			}
			int[] detTime = new int[bombs.Count];
			for (int i = 0; i < bombs.Count; ++i)
			{
				detTime[i] = int.MaxValue;
			}
			for (int i = 0; i < bombs.Count; ++i)
			{
				detTime[GetP(i, p)] = Math.Min(detTime[GetP(i, p)], bombs[i].DetTime);
			}
			for (int i = 0; i < bombs.Count; ++i)
			{
				Bomb b = bombs[i];
				int dt = detTime[GetP(i, p)];
				Map[b.X, b.Y] = new MapCell(
			                            false,
			                            false,
			                            true,
			                            dt,
			                            dt + Commons.ExplosionDuration - 1,
			                            dt,
			                            Map[b.X, b.Y].Bonus);
				for (int d = 0; d < 4; ++d)
				{
					int x = b.X;
					int y = b.Y;
					for (int it = 0; it < b.DamagingRange; ++it)
					{
						if (x >= 0 && x < Map.GetLength(0) &&
						    y >= 0 && y < Map.GetLength(1) &&
						    Map[x, y].IsEmpty &&
						    Map[x, y].Bonus == '.' &&
						    (bf[x, y] == -1 || x == b.X && y == b.Y))
						{
							x += dx[d];
							y += dy[d];
							if (x >= 0 && x < Map.GetLength(0) &&
							    y >= 0 && y < Map.GetLength(1))
							{
								if ((Map[x, y].IsEmpty || Map[x, y].Bonus != '.') && bf[x, y] == -1)
								{
									Map[x, y] = new MapCell(
								                        false,
								                        false,
								                        true,
								                        Math.Min(dt, Map[x, y].DeadlySince),
								                        Math.Max(dt + Commons.ExplosionDuration - 1, 
									                       Map[x, y].DeadlyTill == int.MaxValue ? 0 : Map[x, y].DeadlyTill),
								                        int.MaxValue,
								                        Map[x, y].Bonus);
								}
								if (Map[x, y].IsBreakableWall)
								{
									Map[x, y] = new MapCell(
								                        false,
								                        true,
								                        false,
								                        int.MaxValue,
								                        int.MaxValue,
								                        dt + Commons.ExplosionDuration,
								                        Map[x, y].Bonus);
								}
							}
						}
					}
				}
			}
		}
		
		private int GetP(int i, int[] p)
		{
			if (p[i] != i)
			{
				p[i] = GetP(p[i], p);
			}
			return p[i];
		}
		
		private void Unite(int i, int j, int[] p, int[] r)
		{
			i = GetP(i, p);
			j = GetP(j, p);
			if (i != j)
			{
				if (r[i] < r[j])
				{
					p[i] = j;
				} else {
					p[j] = i;
				}
				if (r[i] == r[j])
				{
					++r[i];
				}
			}
		}
		
		private bool Expired(Bomb b)
		{
			return b.DetTime + Commons.ExplosionDuration - 1 < Time;
		}

		private void InitMapFrom(char[,] m)
		{
			Map = new MapCell[m.GetLength(0),m.GetLength(1)];
			for (int x = 0; x < m.GetLength(0); x++)
				for (int y = 0; y < m.GetLength(1); y++)
					Map[x, y] = new MapCell(m[x, y]);
		}
		
		public bool InvertedMe
		{
			get
			{
				if (lastInverted && MySapka.IsDead) return lastInverted;
				SapkaInfo me = Sapkas[Me];
				lastInverted = me.Infected &&
				          (me.BombsLeft > 0 || lastUsedBomb >= Time - Commons.BombTimeout) &&
				          me.BombsStrength > 0 && me.Speed > 1;
				return lastInverted;
			}
		}

		public int GetWaitForBombTime()
		{
			if(MySapka.BombsLeft > 0) return 0;
			// TODO Если сапка заражена безбомбием, то надо бы как-то это обрабатывать покузявее, чем Math.Min...
			return Math.Max(0, Commons.BombTimeout - (Time - lastUsedBomb)); 
		}

		public void UseBomb()
		{
			lastUsedBomb = Time;
		}
	}
}
