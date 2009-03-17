using System;
using Core.StateCalculations;

namespace Core.PathFinding
{
	public class PathFinder : IPathFinder
	{
		private const string dir = "lrud";
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
			MapCell cell0 = map[cc[X], c[Y]];
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

		public IPath[,,] FindPathsWithTime(int x0, int y0, int time0, int speed)
		{
			int[] qx = new int[n * m * (MAX_TIME + 1)];
			int[] qy = new int[n * m * (MAX_TIME + 1)];
			int[] qt = new int[n * m * (MAX_TIME + 1)];
			int qe = 1;
			var dist = new Path[n,m,MAX_TIME+1];
			dist[x0, y0, 0] = new Path(null, 's');
			qx[0] = x0;
			qy[0] = y0;
			qt[0] = 0;
			for (int it = 0; it < qe; ++it)
			{
				int X = qx[it];
				int Y = qy[it];
				int time = qt[it];
				MapCell cell0 = map[cc[X], cc[Y]];
				if (time < MAX_TIME && !cell0.IsDeadlyAt(dt + time0 + 1))
				{
					Add(X, Y, time + 1, dist, qx, qy, qt, ref qe, new Path(dist[X, Y, time], 's'));
				}
				for (int d = 0; d < 4; ++d)
				{
					int x = X;
					int y = Y;
					Move(ref x, ref y, time + time0, speed, d);
					if (x == X && y == Y)
					{
						continue;
					}
					MapCell cell = map[cc[x], cc[y]];
					if (cell.IsDeadlyAt(time0 + dt + 1))
					{
						continue;
					}
					Add(x, y, Math.Min(time + 1, MAX_TIME), dist, qx, qy, qt, ref qe, 
					    new Path(dist[X, Y, time], dir[d]));
				}
			}
			Console.WriteLine("queue size: {0}", qe);
			return dist;
		}
		
		public void Move(ref int x, ref int y, int time, int speed, int d)
		{
			int x0 = x;
			int y0 = y;
			x += dx[d]*speed;
			y += dy[d]*speed;
			while ((x != x0 || y != y0) &&
			       (!InMap(x, y) || 
			        (cc[x0] != cc[x] || cc[y0] != cc[y]) && 
			        	prohibited(map[cc[x], cc[y]], time)))
			{
				x -= dx[d];
				y -= dy[d];
			}
		}

		private bool InMap(int x, int y)
		{
			return x >= 0 && x < n && y >= 0 && y < m;
		}

		public IPath[,] FindPaths(int x, int y, int time, int speed)
		{
			IPath[,,] dist = FindPathsWithTime(x, y, time, speed);
			var r = new IPath[n,m];
			for (int i = 0; i < n; ++i)
			{
				for (int j = 0; j < m; ++j)
				{
					for (int it = 0; it <= MAX_TIME; ++it)
					{
						r[i, j] = r[i, j] ?? dist[i, j, it];
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