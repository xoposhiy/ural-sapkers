using System.Collections.Generic;
using System.Drawing;
using Core;
using Core.Parsing;
using Core.PathFinding;
using Core.StateCalculations;

namespace Visualizer
{
	public class DecisionVisualizer
	{
		public string Draw(Graphics gr, GameState state, int pointSize, int cellSize, int paddingX, int paddingY)
		{
			pathFinder.SetMap(state.Map, state.CellSize);
			DrawSelectedSapka(gr, pointSize, paddingX, paddingY);
			return DrawSelectedPath(gr, state.Time, pointSize, cellSize, paddingX, paddingY, state.Map);
		}

		public void SelectStartPoint(int pointX, int pointY, int speed)
		{
			selectedStartPos = new Pos(pointX, pointY);
			selectedSpeed = speed;
		}

		public void SelectEndPoint(int pointX, int pointY)
		{
			selectedEndPos = new Pos(pointX, pointY);
		}

		private void DrawSelectedSapka(Graphics gr, int pointSize, int paddingX, int paddingY)
		{
			if (selectedStartPos == null) return;
			Fill(gr, selectedStartPos.X*pointSize, selectedStartPos.Y*pointSize, true, pointSize, paddingX, paddingY);
		}

		private string DrawSelectedPath(Graphics gr, int time, int pointSize, int cellSize, int paddingX, int paddingY, MapCell[,] cells)
		{
			if (selectedStartPos == null) return "Выберите начальную точку";
			if (selectedEndPos == null) return "Выберите конечную точку";
			var paths = pathFinder.FindPaths(selectedStartPos.X, selectedStartPos.Y, time, selectedSpeed, int.MaxValue);
			var alive = pathFinder.Live(selectedStartPos.X, selectedStartPos.Y, time, selectedSpeed);
			if (selectedEndPos.X >= paths.GetLength(0) || selectedEndPos.Y >= paths.GetLength(1)) return "Вылетели за пределы поля";
			var path = paths[selectedEndPos.X, selectedEndPos.Y];
			if (path == null) return "Уже таки прекратите тыкать куда попало!";
			var fullPath = new string(path.FullPath().ToArray());
			var curPixelX = selectedStartPos.X * pointSize;
			var curPixelY = selectedStartPos.Y * pointSize;
			foreach (var dir in fullPath)
			{
				if (dir == 's')
				{
					continue;
				}
				for (int i = 0; i < selectedSpeed; i++)
				{
					Fill(gr, curPixelX, curPixelY, alive, pointSize, paddingX, paddingY);
					int nextPixelX = curPixelX + Commons.dx[dir] * pointSize;
					int nextPixelY = curPixelY + Commons.dy[dir] * pointSize;
					var nextCell = cells[nextPixelX/cellSize, nextPixelY/cellSize];
					if (nextCell.IsBreakableWall || nextCell.IsUnbreakableWall) continue;
					curPixelX = nextPixelX;
					curPixelY = nextPixelY;
				}
			}
			Fill(gr, curPixelX, curPixelY, alive, pointSize, paddingX, paddingY);
			return fullPath;
		}

		private static void Fill(Graphics gr, int x, int y, bool alive, int pointSize, int paddingX, int paddingY)
		{
			gr.FillRectangle(alive ? Brushes.Yellow : Brushes.Cyan, x + paddingX, y + paddingY, pointSize, pointSize);
		}

		private Pos selectedStartPos;
		private Pos selectedEndPos;
		private int selectedSpeed;
		private readonly IPathFinder pathFinder = new PathFinder();
	}
}