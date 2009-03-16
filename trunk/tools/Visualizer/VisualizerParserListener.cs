using Core.Parsing;

namespace Visualizer
{
	public class VisualizerParserListener : IParserListener
	{
		public VisualizerParserListener(ModelUpdatersQueue updatersQueue)
		{
			this.updatersQueue = updatersQueue;
		}

		public void OnGameStart(GameInfo gameInfo)
		{
			updatersQueue.Add(m =>
				{
					m.PlayerID = gameInfo.PID;
					m.CellSize = gameInfo.MapInfo.MapCellSize;
					m.WidthInCells = gameInfo.MapInfo.Map.GetLength(0);
					m.HeightInCells = gameInfo.MapInfo.Map.GetLength(1);
					m.WidthInCoords = m.WidthInCells * m.CellSize;
					m.HeightInCoords = m.HeightInCells * m.CellSize;
					m.CurrentMap = gameInfo.MapInfo.Map;
					m.CurrentRound = 0;
					m.Scores.Clear();
					m.State.OnGameStart(gameInfo);
				});
		}

		public void OnRoundStart(StartRoundInfo startRoundInfo)
		{
			updatersQueue.Add(m =>
				{
					m.CurrentMap = startRoundInfo.MapInfo.Map;
					m.CurrentRound++;
					m.DamagingRanges = new int[m.WidthInCells, m.HeightInCells];
					m.State.OnRoundStart(startRoundInfo);
				});
		}

		public void OnMapChange(MapChangeInfo mapChangeInfo)
		{
			updatersQueue.Add(m =>
				{
					m.Time = mapChangeInfo.Time;
					m.DangerLevel = mapChangeInfo.DangerLevel;

					foreach (var sapka in mapChangeInfo.Sapkas)
					{
						if (sapka.IsDead)
						{
							m.SapkaInfos[sapka.SapkaNumber].IsDead = true;
							continue;
						}
						m.SapkaInfos[sapka.SapkaNumber] = sapka;
					}
					foreach (var remove in mapChangeInfo.Removes)
					{
						m.CurrentMap[remove.Pos.X, remove.Pos.Y] = '.';
					}
					foreach (var add in mapChangeInfo.Adds)
					{
						m.CurrentMap[add.Pos.X, add.Pos.Y] = add.SubstanceType;
						if (add.SubstanceType == '#')
						{
							m.DamagingRanges[add.Pos.X, add.Pos.Y] = add.DamagingRange;
						}
					}
					m.State.OnMapChange(mapChangeInfo);
				});
		}


		public void OnFinishRound(int score)
		{
			updatersQueue.Add(m =>
				{
					if (!m.Scores.ContainsKey(m.PlayerId))
					{
						m.Scores[m.PlayerId] = new VisualizerSapkaInfo();
					}
					m.Scores[m.PlayerId].Score = score;
					m.State.OnFinishRound(score);
				});
		}

		public void OnFinishGame(PlayerResult[] playerResults)
		{
			updatersQueue.Add(m =>
				{
					foreach (var result in playerResults)
					{
						if (!m.Scores.ContainsKey(result.PlayerNumber))
						{
							m.Scores[result.PlayerNumber] = new VisualizerSapkaInfo();
						}
						m.Scores[result.PlayerNumber].Score = result.Score;
						m.Scores[result.PlayerNumber].Rank = result.Rank;
					}
					m.CurrentRound = 0;
					m.State.OnFinishGame(playerResults);
				});
		}

		private readonly ModelUpdatersQueue updatersQueue;
	}
}