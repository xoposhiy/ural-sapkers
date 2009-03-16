using System.Collections.Generic;
using System;
using Core.Parsing;

namespace Core.PathFinding
{
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