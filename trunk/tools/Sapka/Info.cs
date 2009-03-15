using System;
using Parsing;
using System.Collections.Generic;

namespace TheSapka
{
	public class Info : IParserListener
	{
		public static int[] DX = new int[] {-1, 1, 0, 0};
		public static int[] DY = new int[] {0, 0, -1, 1};
		
		private MapInfo map;
		private char[,] bonus;
		private SapkaInfo[] sapkas;
		private int dangerLevel;
		private int time;
		private Bomb[,] bombs;
		private Explosion[,] explosions;
		
		private Explosion[,] explosionAt;
		
		public char[,] Map; 
		
		public SapkaInfo[] GetSapkas()
		{
			return sapkas;
		}
		
		public int GetDangerLevel()
		{
			return dangerLevel;
		}
		
		public int GetTime()
		{
			return time;
		}
		
		private void init()
		{
			bonus = new char[map.Map.GetLength(0), map.Map.GetLength(1)];
			bombs = new Bomb[map.Map.GetLength(0), map.Map.GetLength(1)];
			explosions = new Explosion[map.Map.GetLength(0), map.Map.GetLength(1)];
			explosionAt = new Explosion[map.Map.GetLength(0), map.Map.GetLength(1)];
			for (int i = 0; i < bonus.GetLength(0); ++i)
			{
				for (int j = 0; j < bonus.GetLength(1); ++j)
				{
					bonus[i, j] = '.';
				}
			}
			refill();
		}
		
		private void refill()
		{
			Map = (char[,])map.Map.Clone();
			for (int i = 0; i < Map.GetLength(0); ++i)
			{
				for (int j = 0; j < Map.GetLength(1); ++j)
				{
					if (bombs[i, j] != null)
					{
						Map[i, j] = '*';
					}
					if (bonus[i, j] != '.')
					{
						Map[i, j] = bonus[i, j];
					}
				}
			}
			for (int i = 0; i < Map.GetLength(0); ++i)
			{
				for (int j = 0; j < Map.GetLength(1); ++j)
				{
					if (explosions[i, j] != null)
					{
						Explosion exp = explosions[i, j];
						for (int d = 0; d < 4; ++d)
						{
							int x = i;
							int y = j;
							for (int it = 0; it <= exp.Range; ++it)
							{
								if (x < 0 || x >= Map.GetLength(0) || 
								    y < 0 || y >= Map.GetLength(1) || 
								    Map[i, j] != '.' && Map[i, j] != '#')
								{
									break;
								}
								explosionAt[i, j] = exp;
								Map[i, j] = '#';
								x += DX[d];
								y += DY[d];
							}
						}
					}
				}
			}
		}
		
		#region IParserListener members
		
		public void OnGameStart(GameInfo gameInfo)
		{
			this.map = gameInfo.MapInfo;
			init();
		}
		
		public void OnRoundStart(StartRoundInfo startRoundInfo)
		{
			this.map = startRoundInfo.MapInfo;
			this.time = 0;
			init();
		}

		public void OnMapChange(MapChangeInfo mapChangeInfo)
		{
			this.sapkas = mapChangeInfo.Sapkas;
			this.dangerLevel = mapChangeInfo.HasDangerLevel ? mapChangeInfo.DangerLevel : -1;
			this.time = mapChangeInfo.Time;
			foreach (AddInfo i in mapChangeInfo.Adds)
			{
				if (i.SubstanceType == '*')
				{
					bombs[i.Pos.X, i.Pos.Y] = new Bomb(i.Pos, i.DamagingRange, time + i.Time);
				} else if (i.SubstanceType == '#')
				{
					explosions[i.Pos.X, i.Pos.Y] = new Explosion(i.Pos, i.DamagingRange, time + i.Time);
				} else
				{
					bonus[i.Pos.X, i.Pos.Y] = i.SubstanceType;
				}
			}
			foreach (RemoveInfo r in mapChangeInfo.Removes)
			{
				if (r.SubstanceType == '*')
				{
					bombs[r.Pos.X, r.Pos.Y] = null;
				} else if (r.SubstanceType == '#')
				{
					explosions[r.Pos.X, r.Pos.Y] = null;
				} else if (r.SubstanceType == 'w')
				{
					map.Map[r.Pos.X, r.Pos.Y] = '.';
				} else
				{
					bonus[r.Pos.X, r.Pos.Y] = '.';
				}
			}
			refill();
		}

		public void OnFinishRound(int score)
		{
		}
		
		public void OnFinishGame(PlayerResult[] playerResults)
		{
		}
		
		#endregion
	}
}