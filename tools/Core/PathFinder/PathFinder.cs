using System;
using System.Collections.Generic;
using Core.StateCalculations;
using Wintellect.PowerCollections;

namespace Core.PathFinder
{	
	public class PathFinder : IPathFinder
	{
		private MapCell[,] map;
		private int cellSize;
		
		private static readonly int[] dx = new int[] {-1, 1, 0, 0};
		private static readonly int[] dy = new int[] {0, 0, -1, 1};
		private static readonly string dir = "lrud"; //TODO fix directions
		
		public PathFinder()
		{
		}
		
		public void SetMap(MapCell[,] map, int cellSize)
		{
			this.map = map;
			this.cellSize = cellSize;
		}
		
		private bool prohibited(MapCell cell)
		{
			return cell.IsUnbreakableWall || cell.IsBreakableWall && cell.EmptySince > 1000000;
			//TODO нормальное условие на EmptySince
		}
	
		public IPath[,] FindPaths(int x0, int y0, int time0, int speed)
		{
			OrderedSet<Node> q = new OrderedSet<Node>();
			int n = map.GetLength(0) * cellSize;
			int m = map.GetLength(1) * cellSize;
			Path[,,] dist = new Path[n, m, 2];
			dist[x0, y0, 0] = new Path(null, ' ', 0);
			q.Add(new Node(x0, y0, 0, dist[x0, y0, 0]));
			while (q.Count > 0)
			{
				Node node = q.RemoveFirst();
				MapCell cell0 = map[node.X / cellSize, node.Y / cellSize];
				for (int d = 0; d < 4; ++d)
				{
					int x = node.X + dx[d] * speed;
					int y = node.Y + dy[d] * speed;
					while ((x != node.X || y != node.Y) &&
					       (x < 0 || x >= n || y < 0 || y >= m || prohibited(map[x / cellSize, y / cellSize])))
					{
						x -= dx[d];
						y -= dy[d];
					}
					if (x == node.X && y == node.Y)
					{
						continue;
					}
					MapCell cell = map[x / cellSize, y / cellSize];
					int timeCur = time0 + node.Path.Size();
					int time = timeCur;
					if (cell.IsBreakableWall)
					{
						time = Math.Min(time, cell.EmptySince);
					}
					/*if (cell.Bomb && cell0 != cell)
					{
						time = Math.Min(time, cell.EmptySince);
					}*/
					//TODO
					if (time + 1 >= cell.DeadlySince)
					{
						time = Math.Min(time, cell.DeadlyTill);
					}
					if (time0 <= cell0.DeadlyTill && cell0.DeadlySince <= time)
					{
						continue;
					}
					int after = time >= cell0.DeadlyTill ? 1 : 0;
					Add(x, y, after, dist, q, new Path(new Path(node.Path, 's', time - time0), dir[d], 1));
					if (after == 0 && cell.DeadlyTill <= 1000000) //TODO
					{
						after = 1;
						time = Math.Min(time, cell.DeadlyTill);
						if (time0 <= cell0.DeadlyTill && cell0.DeadlySince <= time)
						{
							continue;
						}
						Add(x, y, after, dist, q, new Path(new Path(node.Path, 's', time - time0), dir[d], 1));
					}
				}
			}
			IPath[,] r = new IPath[n, m];
			for (int i = 0; i < n; ++i)
			{
				for (int j = 0; j < m; ++j)
				{
					r[i, j] = dist[i, j, 0] == null ? dist[i, j, 1] : dist[i, j, 0];
				}
			}
			return r;
		}
		
		void Add(int x, int y, int after, Path[,,] dist, OrderedSet<Node> q, Path path)
		{
			if (dist[x, y, after] == null || dist[x, y, after].CompareTo(path) > 0)
			{
				if (dist[x, y, after] != null)
				{
					q.Remove(new Node(x, y, after, dist[x, y, after]));
				}
				dist[x, y, after] = path;
				q.Add(new Node(x, y, after, path));
			}
		}
	}
	
	class Node : IComparable<Node>
	{
		public int X, Y;
		public int After;
		public Path Path;
		
		public Node(int x, int y, int after, Path path)
		{
			this.X = x;
			this.Y = y;
			this.After = after;
			this.Path = path;
		}
		
		public int CompareTo(Node other)
		{
			int cmp = Path.CompareTo(other.Path);
			if (cmp == 0)
			{
				cmp = Math.Sign(X - other.X);
			}
			if (cmp == 0)
			{
				cmp = Math.Sign(Y - other.Y);
			}
			if (cmp == 0)
			{
				cmp = Math.Sign(After - other.After);
			}
			return cmp;
		}
	}
}
