using System;
using System.Collections.Generic;
using Core.StateCalculations;
using Core.Parsing;

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

		#region IPathFinder Members

		public void SetMap(MapCell[,] newMap, int newCellSize)
		{
			map = newMap;
			cellSize = newCellSize;
			n = map.GetLength(0)*cellSize;
			m = map.GetLength(1)*cellSize;
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
				MapCell cell0 = map[X/cellSize, Y/cellSize];
				if (time < MAX_TIME && prohibited(cell0, time + time0) == prohibited(cell0, time + time0 + 1))
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
					MapCell cell = map[x/cellSize, y/cellSize];
					if (prohibited(cell, time + time0 + 1) && 
					    (!prohibited(cell0, time + time0) || !prohibited(cell, time + time0)))
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
			       (x < 0 || x >= n || y < 0 || y >= m || 
			        (x0 / cellSize != x / cellSize || y0 / cellSize != y / cellSize) && 
			        	prohibited(map[x/cellSize, y/cellSize], time)))
			{
				x -= dx[d];
				y -= dy[d];
			}
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
				cell.IsBreakableWall && time < cell.EmptySince ||
				time >=cell.DeadlySince && time < cell.DeadlyTill;
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