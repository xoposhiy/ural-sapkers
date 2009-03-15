using System;
using System.Collections.Generic;
using Core.StateCalculations;

namespace Core
{
	public interface IPath : IComparable<IPath>
	{
		int Size();
		char FirstMove();
		List<char> FullPath();
	}
	
	public interface IPathFinder
	{
		void SetMap(MapCell[,] map, int cellSize);
		IPath[,] FindPaths(int x, int y, int time, int speed);
	}
}
