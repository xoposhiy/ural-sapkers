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
		private static int[] qx;
		private static int[] qy;
		private static int[] qt;
		private int[,] minVisitedTime;

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
			if (qx == null || qx.Length != n * m * (MAX_TIME + 1))
			{
				qx = new int[n * m * (MAX_TIME + 1)];
				qy = new int[n * m * (MAX_TIME + 1)];
				qt = new int[n * m * (MAX_TIME + 1)];
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

		public IPath[,,] FindPathsWithTime(int x0, int y0, int time0, int speed, int radius)
		{
			int qe = 1;
			var dist = new Path[n,m,MAX_TIME+1];
			minVisitedTime = new int[n, m];
			for (int i = 0; i < n; ++i)
			{
				for (int j = 0; j < m; ++j)
				{
					minVisitedTime[i, j] = -1;
				}
			}
			dist[x0, y0, 0] = new Path(null, 's');
			qx[0] = x0;
			qy[0] = y0;
			qt[0] = 0;
			for (int it = 0; it < qe; ++it)
			{
				int X = qx[it];
				int Y = qy[it];
				if (Math.Abs(X - x0) > radius || Math.Abs(Y - y0) > radius)
				{
					continue;
				}
				int time = qt[it];
				if (minVisitedTime[X, Y] == -1)
				{
					minVisitedTime[X, Y] = time;
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
					Add(x, y, Math.Min(time + 1, MAX_TIME), dist, qx, qy, qt, ref qe, 
					    new Path(dist[X, Y, time], Dir[d]));
				}
				if (time < MAX_TIME && !cell0.IsDeadlyAt(time + time0 + 1))
				{
					Add(X, Y, time + 1, dist, qx, qy, qt, ref qe, new Path(dist[X, Y, time], 's'));
				}
			}
			//Console.WriteLine("queue size: {0}", qe);
			return dist;
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

		public IPath[,] FindPaths(int x, int y, int time, int speed, int radius)
		{
			IPath[,,] dist = FindPathsWithTime(x, y, time, speed, radius);
			var r = new IPath[n,m];
			for (int i = 0; i < n; ++i)
			{
				for (int j = 0; j < m; ++j)
				{
					if (minVisitedTime[i, j] != -1)
					{
						r[i, j] = dist[i, j, minVisitedTime[i, j]];
					}
				}
			}
			return r;
		}

		#endregion

		public bool prohibited(MapCell cell, int time)
		{
			return cell.IsUnbreakableWall ||
				(cell.IsBreakableWall || cell.IsBomb) && time < cell.EmptySince ||
				cell.IsDeadlyAt(time);
		}

		private void Add(int x, int y, int time, Path[,,] dist, 
		                 int[] qx, int[] qy, int[] qt, ref int qe, Path path)
		{
			if (dist[x, y, time] == null)
			{
				dist[x, y, time] = path;
				qx[qe] = x;
				qy[qe] = y;
				qt[qe] = time;
				++qe;
			}
		}
	}

	internal class Node
	{
		public int Time;
		public int X, Y;

		public Node(int x, int y, int time)
		{
			X = x;
			Y = y;
			Time = time;
		}
	}
}