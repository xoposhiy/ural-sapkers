using System;
using Core.StateCalculations;

namespace Core.PathFinding
{
	public class PathFinder : IPathFinder
	{
		public const string Dir = "lrud";
		private static readonly int[] dx = new[] {-1, 1, 0, 0};
		private static readonly int[] dy = new[] {0, 0, -1, 1};
		private static readonly int MAX_TIME = 50;
		private int cellSize;
		private int n, m;
		private MapCell[,] map;
		private bool[,,] alreadyVisited;
		private int[] cc;

		#region IPathFinder Members

		public void SetMap(MapCell[,] newMap, int newCellSize)
		{
			map = newMap;
			cellSize = newCellSize;
			n = map.GetLength(0)*cellSize;
			m = map.GetLength(1)*cellSize;
			cc = new int[Math.Max(n, m)];
			for (int i = 0; i < cc.Length; ++i)
			{
				cc[i] = i / cellSize;
			}
		}
		
		public bool Live(int x, int y, int time0, int speed)
		{
			alreadyVisited = new bool[n, m, MAX_TIME + 1];
			return dfs(x, y, 0, speed, time0);
		}
		
		bool dfs(int X, int Y, int dt, int speed, int time0)
		{
			if (dt >= MAX_TIME)
			{
				return true;
			}
			if (alreadyVisited[X, Y, dt])
			{
				return false; //там уже искали, ничего не нашли...
			}
			alreadyVisited[X, Y, dt] = true;
			MapCell cell0 = map[cc[X], cc[Y]];
			if (!cell0.IsDeadlyAt(dt + time0 + 1) && dfs(X, Y, dt + 1, speed, time0))
			{
				return true;
			}
			for (int d = 0; d < 4; ++d)
			{
				int x = X;
				int y = Y;
				Move(ref x, ref y, dt + time0, speed, d);
				if (x == X && y == Y)
				{
					continue;
				}
				MapCell cell = map[cc[x], cc[y]];
				if (!cell.IsDeadlyAt(time0 + dt + 1) && dfs(x, y, dt + 1, speed, time0)) return true;
			}
			return false;
		}
		
		private static bool IsFire(MapCell cell, int time)
		{
			return time >= cell.DeadlySince && time <= cell.DeadlyTill;
		}

		public IPath[,] FindPaths(int x0, int y0, int time0, int speed, int radius)
		{
			//TODO extract qs in static
			Queues qs = new Queues(MAX_TIME + 1, n * m * 16);
			var dist0 = new Path[n,m];
			var dist1 = new Path[n,m];
			dist0[x0, y0] = new Path(null, 's');
			qs.Add(0, x0, y0);
			for (int time = 0; time <= MAX_TIME; ++time)
			{
				for (int it = qs.First[time]; it != -1; it = qs.Next[it])
				{
					int X = qs.X[it];
					int Y = qs.Y[it];
					Path p = (time + time0 <= bound1(map[cc[X], cc[Y]]) ? dist0 : dist1)[X, Y];
					//Console.WriteLine("{0}: ({1}, {2}) - {3}", time, X, Y, p.Size());
					if (time != MAX_TIME && p.Size() < time)
					{
						continue;
					}
					if (Math.Abs(X - x0) > radius || Math.Abs(Y - y0) > radius)
					{
						continue;
					}
					MapCell cell0 = map[cc[X], cc[Y]];
					for (int d = 0; d < 4; ++d)
					{
						int x = X;
						int y = Y;
						if (!Move(ref x, ref y, time + time0, speed, d) || x == X && y == Y)
						{
							continue;
						}
						MapCell cell = map[cc[x], cc[y]];
						if (cell.IsDeadlyAt(time0 + time + 1))
						{
							continue;
						}
						var dist = time + time0 + 1 <= bound1(map[cc[x], cc[y]]) ? dist0 : dist1;
						if (dist[x, y] != null && (time == MAX_TIME || dist[x, y].Size() <= time + 1))
						{
							continue;
						}
						dist[x, y] = new Path(p, Dir[d]);
						qs.Add(Math.Min(time + 1, MAX_TIME), x, y);
					}
					for (int d = 0; d < 4; ++d)
					{
						int x = X;
						int y = Y;
						if (!InMap(x + cellSize * dx[d], y + cellSize * dy[d]))
						{
							continue;
						}
						int time2 = bound1(map[cc[x] + dx[d], cc[y] + dy[d]]) + 1 - time0;
						if (time2 - 1 + time0 == int.MaxValue || time2 <= time)
						{
							continue;
						}
						if (!Move(ref x, ref y, time2 + time0, speed, d) || cc[x] == cc[X] && cc[y] == cc[Y])
						{
							continue;
						}
						int b0 = bound0(map[cc[X], cc[Y]]);
						if (time + time0 < b0 && time2 + time0 >= b0)
						{
							continue;
						}
						if (dist1[x, y] != null && dist1[x, y].Size() <= time2 + 1)
						{
							continue;
						}
						dist1[x, y] = new Path(new Path(p, 's', time2 - time), Dir[d]);
						qs.Add(time2 + 1, x, y);
					}
				}
			}
			//Console.WriteLine("queue size: {0}", qe);
			for (int i = 0; i < n; ++i)
			{
				for (int j = 0; j < m; ++j)
				{
					dist0[i, j] = dist0[i, j] ?? dist1[i, j];
				}
			}
			return dist0;
		}
		
		public bool Move(ref int x, ref int y, int time, int speed, int d)
		{
			//TODO bug with large speed
			int x0 = x;
			int y0 = y;
			x += dx[d]*speed;
			y += dy[d]*speed;
			while ((x != x0 || y != y0) &&
			       (!InMap(x, y) || 
			        (cc[x0] != cc[x] || cc[y0] != cc[y]) && 
			        	prohibited(map[cc[x], cc[y]], time)))
			{
				if (InMap(x, y) && map[cc[x], cc[y]].IsDeadlyAt(time))
				{
					return false;
				}
				x -= dx[d];
				y -= dy[d];
			}
			return true;
		}

		private bool InMap(int x, int y)
		{
			return x >= 0 && x < n && y >= 0 && y < m;
		}

		#endregion

		public bool prohibited(MapCell cell, int time)
		{
			return cell.IsUnbreakableWall ||
				(cell.IsBreakableWall || cell.IsBomb) && time < cell.EmptySince ||
				cell.IsDeadlyAt(time);
		}
		
		private int bound0(MapCell cell)
		{
			return cell.EmptySince == int.MaxValue ? cell.DeadlySince : 0;
		}
		
		private int bound1(MapCell cell)
		{
			return cell.DeadlyTill;
		}
	}

	internal struct Queues
	{
		public int[] First, Last, Next, X, Y;
		private int used;
		
		public Queues(int queues, int maxCount)
		{
			First = new int[queues];
			Last = new int[queues];
			Next = new int[maxCount];
			X = new int[maxCount];
			Y = new int[maxCount];
			used = 0;
			Clear();
		}
		
		public void Clear()
		{
			for (int i = 0; i < First.Length; ++i)
			{
				First[i] = Last[i] = -1;
			}
		}
		
		public void Add(int queue, int x, int y)
		{
			X[used] = x;
			Y[used] = y;
			if (Last[queue] != -1)
			{
				Next[Last[queue]] = used;
			}
			Next[used] = -1;
			Last[queue] = used;
			if (First[queue] == -1)
			{
				First[queue] = used;
			}
			used++;
		}
	}
}