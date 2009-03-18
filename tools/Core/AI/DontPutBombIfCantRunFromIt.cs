using System;
using Core.Parsing;
using Core.PathFinding;
using Core.StateCalculations;

namespace Core.AI
{
	internal class DontPutBombIfCantRunFromIt : IExpert
	{
		// TODO pe: Не учитывает собранные при отходе антибонусы.
		// TODO pe: Не учитывает взаимодетанирование бомб.
		public byte EstimateDecisionDanger(GameState state, Paths paths, Decision decision)
		{
			Pos me = state.MyCell;
			SapkaInfo sapka = state.Sapkas[state.Me];
			if (!decision.PutBomb) return 0;
			int safePositions = 0;
			for(int xp=0; xp<paths.N; xp++)
				for(int yp = 0; yp < paths.M; yp++)
				{
					int time = paths.MinTime[xp, yp];
					if (time != -1
					    && ((xp / state.CellSize != me.X && yp / state.CellSize != me.Y)
					        || Math.Abs(xp / state.CellSize - me.X) > sapka.BombsStrength
					        || Math.Abs(yp / state.CellSize - me.Y) > sapka.BombsStrength) 
					    && paths.Dist[xp,yp,time] < Constants.BombTimeout) safePositions++;
				}
			if (safePositions == 0) return 255;
			if(safePositions < 4) return 128;
			return 0;
		}

		public void OnNextMove()
		{
			
		}
	}
}