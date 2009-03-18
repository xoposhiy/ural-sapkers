using System;
using Core.Parsing;
using System.Collections.Generic;
using Core.StateCalculations;

namespace Core.PathFinding
{
	public interface IPath : IComparable<IPath>
	{
		int Size();
		char FirstMove();
		List<char> FullPath();
	}

	public interface IPathFinder
	{
		void SetMap(MapCell[,] newMap, int newCellSize);
		IPath[,] FindPaths(int x, int y, int time, int speed, int radius);
		bool Move(ref int x, ref int y, int time, int speed, int d);
		bool Live(int x, int y, int time, int speed);
	}
}