using System;
using Core.StateCalculations;

namespace Core.PathFinding
{
	public class PathFinder : IPathFinder
	{
		public const string Dir = "lrud";
		private static readonly int[] dx = new[] {-1, 1, 0, 0};
		private static readonly int[] dy = new[] {0, 0, -1, 1};
		private const int MAX_TIME = 50;
		private int cellSize;
		private int n, m;
		private MapCell[,] map;
		private int[] cc;
		private bool[,,] alreadyVisited;
		private static Queues qs;
		private static int visited, maxVisited;

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
			if (qs == null || qs.X.Length != n * m * 16)
			{
				qs = new Queues(MAX_TIME + 1, n * m * 16);
			}
		}
		
		public bool Live(int x, int y, int time0, int speed)
		{
			Path[,] dist0, dist1;
			return FindPaths(x, y, time0, speed, Constants.Radius, out dist0, out dist1, true);
		}
		
		private bool FindPaths(int x0, int y0, int time0, int speed, int radius, out Path[,] dist0, out Path[,] dist1, bool exitOnOk)
		{
			qs.Clear();
			dist0 = new Path[n,m];
			dist1 = new Path[n,m];
			(time0 <= bound1(map[cc[x0], cc[y0]]) ? dist0 : dist1)[x0, y0] = new Path(null, 's');
			qs.Add(0, x0, y0);
			for (int time = 0; time <= MAX_TIME; ++time)
			{
				for (int it = qs.First[time]; it != -1; it = qs.Next[it])
				{
					int X = qs.X[it];
					int Y = qs.Y[it];
					Path p = (time + time0 <= bound1(map[cc[X], cc[Y]]) ? dist0 : dist1)[X, Y];
					if (time != MAX_TIME && p.Size() < time)
					{
						continue;
					}
					if (Math.Abs(X - x0) > radius || Math.Abs(Y - y0) > radius)
					{
						continue;
					}
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
						if (exitOnOk && (dist == dist1 || bound0(map[cc[x], cc[y]]) == int.MaxValue))
						{
							return true;
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
						if (exitOnOk)
						{
							return true;
						}
						dist1[x, y] = new Path(new Path(p, 's', time2 - time), Dir[d]);
						qs.Add(time2 + 1, x, y);
					}
				}
			}
			//Console.WriteLine("queue size: {0}", qe);
			return false;
		}
		
		public IPath[,] FindPaths(int x0, int y0, int time0, int speed, int radius)
		{
			Path[,] dist0, dist1;
			FindPaths(x0, y0, time0, speed, radius, out dist0, out dist1, false);
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
				if (InMap(x, y) && 
				    (map[cc[x], cc[y]].IsDeadlyAt(time) ||
				     map[cc[x], cc[y]].Bonus != '.' &&
				     map[cc[x], cc[y]].DeadlyTill != int.MaxValue &&
				     time <= map[cc[x], cc[y]].DeadlyTill))
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
			return cell.EmptySince == int.MaxValue || cell.IsBomb ? cell.DeadlySince : 0;
		}
		
		private int bound1(MapCell cell)
		{
			return cell.DeadlyTill;
		}
	}

	internal class Queues
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
			Clear();
		}
		
		public void Clear()
		{
			for (int i = 0; i < First.Length; ++i)
			{
				First[i] = Last[i] = -1;
			}
			used = 0;
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
