using System.Collections.Generic;
using System;
using Core.Parsing;

namespace Core.PathFinding
{
	public struct Paths
	{
		public short[,,] Px, Py;
		public byte[,,] Pt;
		public char[,,] Pc;
		public int[,,] Dist;
		public int[,] MinTime;
		public readonly int N, M;
		
		public Paths(int n, int m, int maxTime)
		{
			Px = new short[n, m, maxTime + 1];
			Py = new short[n, m, maxTime + 1];
			Pt = new byte[n, m, maxTime + 1];
			Pc = new char[n, m, maxTime + 1];
			Dist = new int[n, m, maxTime + 1];
			MinTime = new int[n, m];
			N = n;
			M = m;
		}
		
		public Path GetPath(int x, int y, int time)
		{
			if (time == 0)
			{
				return new Path(null, 's');
			} else
			{
				return new Path(GetPath(Px[x, y, time], Py[x, y, time], Pt[x, y, time]), Pc[x, y, time]);
			}
		}
	}
	
	public class Path : IPath
	{
		private Path parent;
		private int size;
		private char firstMove;
		private char move;
		
		public Path(Path parent, char move)
		{
			this.parent = parent;
			this.move = move;
			this.size = (parent == null ? -1 : parent.size) + 1;
			this.firstMove = parent == null || parent.size == 0 ? move : parent.firstMove;
		}
		
		public int Size()
		{
			return size;
		}
		
		public char FirstMove()
		{
			return firstMove;
		}
		
		public List<char> FullPath()
		{
			List<char> r = parent == null ? new List<char>() : parent.FullPath();
			if (parent != null)
			{
				r.Add(move);
			}
			return r;
		}
		
		public int CompareTo(IPath other)
		{
			return Math.Sign(Size() - other.Size());
		}
	}
}