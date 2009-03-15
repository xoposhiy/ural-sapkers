using System;
using Core.StateCalculations;
using Wintellect.PowerCollections;

namespace Core.PathFinding
{
	public class PathFinder : IPathFinder
	{
		private const string dir = "udlr";
		private static readonly int[] dx = new[] {-1, 1, 0, 0};
		private static readonly int[] dy = new[] {0, 0, -1, 1};
		private int cellSize;
		private MapCell[,] map;

		#region IPathFinder Members

		public void SetMap(MapCell[,] newMap, int newCellSize)
		{
			map = newMap;
			cellSize = newCellSize;
		}

		public IPath[,] FindPaths(int x0, int y0, int time0, int speed)
		{
			var q = new OrderedSet<Node>();
			int n = map.GetLength(0)*cellSize;
			int m = map.GetLength(1)*cellSize;
			var dist = new Path[n,m,2];
			dist[x0, y0, 0] = new Path(null, ' ', 0);
			q.Add(new Node(x0, y0, 0, dist[x0, y0, 0]));
			while (q.Count > 0)
			{
				Node node = q.RemoveFirst();
				MapCell cell0 = map[node.X/cellSize, node.Y/cellSize];
				//Console.WriteLine("({0}, {1}) {2} {3}", node.X, node.Y, prohibited(cell0), node.Path.Size());
				for (int d = 0; d < 4; ++d)
				{
					int x = node.X + dx[d]*speed;
					int y = node.Y + dy[d]*speed;
					while ((x != node.X || y != node.Y) &&
					       (x < 0 || x >= n || y < 0 || y >= m || prohibited(map[x/cellSize, y/cellSize])))
					{
						x -= dx[d];
						y -= dy[d];
					}
					if (x == node.X && y == node.Y)
					{
						continue;
					}
					MapCell cell = map[x/cellSize, y/cellSize];
					int timeCur = time0 + node.Path.Size();
					int time = timeCur;
					if (cell.IsBreakableWall)
					{
						time = Math.Min(time, cell.EmptySince);
					}
					if (cell.EmptySince == cell.DeadlySince && cell0 != cell)
					{
						time = Math.Min(time, cell.EmptySince);
					}
					if (time + 1 >= cell.DeadlySince)
					{
						time = Math.Min(time, cell.DeadlyTill);
					}
					if (time0 <= cell0.DeadlyTill && cell0.DeadlySince <= time)
					{
						continue;
					}
					int after = time >= cell0.DeadlyTill ? 1 : 0;
					Add(x, y, after, dist, q, new Path(new Path(node.Path, 's', time - timeCur), dir[d], 1));
					if (after == 0 && cell.DeadlyTill < int.MaxValue)
					{
						after = 1;
						time = Math.Min(time, cell.DeadlyTill);
						if (time0 <= cell0.DeadlyTill && cell0.DeadlySince <= time)
						{
							continue;
						}
						Add(x, y, after, dist, q, new Path(new Path(node.Path, 's', time - timeCur), dir[d], 1));
					}
				}
			}
			var r = new IPath[n,m];
			for (int i = 0; i < n; ++i)
			{
				for (int j = 0; j < m; ++j)
				{
					r[i, j] = dist[i, j, 0] ?? dist[i, j, 1];
				}
			}
			return r;
		}

		#endregion

		private bool prohibited(MapCell cell)
		{
			return cell.IsUnbreakableWall || cell.IsBreakableWall && cell.EmptySince == int.MaxValue;
		}

		private void Add(int x, int y, int after, Path[,,] dist, OrderedSet<Node> q, Path path)
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

	internal class Node : IComparable<Node>
	{
		public int After;
		public Path Path;
		public int X, Y;

		public Node(int x, int y, int after, Path path)
		{
			X = x;
			Y = y;
			After = after;
			Path = path;
		}

		#region IComparable<Node> Members

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

		#endregion
	}
}