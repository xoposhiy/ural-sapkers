using System.Collections.Generic;
using System;

namespace Core
{
	public class Path : IPath
	{
		private Path parent;
		private int size, repeat;
		private char firstMove;
		private char move;
		
		public Path(Path parent, char move, int repeat)
		{
			this.parent = parent;
			this.repeat = repeat;
			this.move = move;
			this.size = (parent == null ? 0 : parent.size) + repeat;
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
			for (int it = 0; it < repeat; ++it)
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
