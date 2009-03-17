﻿using System;
using Core.AI;
using Core.Parsing;
using System.Collections.Generic;
using System.Linq;

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
		
		private List<Bomb> bombs;

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
		}

		public void OnRoundStart(StartRoundInfo startRoundInfo)
		{
			DangerLevel = 0;
			RoundNumber = startRoundInfo.RoundNumber;
			CellSize = startRoundInfo.MapInfo.MapCellSize;
			InitMapFrom(startRoundInfo.MapInfo.Map);
			bombs = new List<Bomb>();
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
					AddBomb(new Bomb(p.X, p.Y, add.DamagingRange, Time + Constants.BombTimeout - 1));
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
					Map[p.X, p.Y].AddBonus(add.SubstanceType);
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

		private void RecalcDeadly()
		{
			var dx = new[] {1, -1, 0, 0};
			var dy = new[] {0, 0, 1, -1};
			bombs.RemoveAll(Expired);
			int[,] bf = new int[Map.GetLength(0), Map.GetLength(1)];
			for (int i = 0; i < Map.GetLength(0); ++i)
			{
				for (int j = 0; j < Map.GetLength(0); ++j)
				{
					if (Map[i, j].EmptySince < Time)
					{
						Map[i, j] = new MapCell(
					                        false,
					                        false,
					                        true,
					                        int.MaxValue,
					                        int.MaxValue,
					                        int.MaxValue,
					                        Map[i, j].Bonus);
					} else {
						Map[i, j] = new MapCell(
					                        Map[i, j].IsUnbreakableWall,
					                        Map[i, j].IsBreakableWall,
					                        Map[i, j].IsEmpty,
					                        int.MaxValue,
					                        int.MaxValue,
					                        int.MaxValue,
					                        Map[i, j].Bonus);
					}
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
						    bf[x, y] == -1)
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
				p[i] = i;
				bf[bombs[i].X, bombs[i].Y] = i;
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
			                            dt + Constants.ExplosionDuration - 1,
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
						    bf[x, y] == -1)
						{
							x += dx[d];
							y += dy[d];
							if (x >= 0 && x < Map.GetLength(0) &&
							    y >= 0 && y < Map.GetLength(1))
							{
								if (Map[x, y].IsEmpty && bf[x, y] == -1)
								{
									Map[x, y] = new MapCell(
								                        false,
								                        false,
								                        true,
								                        dt,
								                        dt + Constants.ExplosionDuration - 1,
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
								                        dt + Constants.ExplosionDuration,
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
			return b.DetTime + Constants.ExplosionDuration - 1 < Time;
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
