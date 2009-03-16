using System.Collections.Generic;
using Core.Parsing;
using Core.StateCalculations;

namespace Visualizer
{
	public class VisualizerModel
	{
		public char[,] CurrentMap { get; set; }
		public int[,] DamagingRanges { get; set; }
		public int CellSize { get; set; }
		public int PlayerId { get; set; }
		public int WidthInCells { get; set; }
		public int HeightInCells { get; set; }
		public int WidthInCoords { get; set; }
		public int HeightInCoords { get; set; }
		public int PlayerID { get; set; }
		public int CurrentRound { get; set; }
		public int Time { get; set; }
		public int DangerLevel { get; set; }
		public readonly GameState State = new GameState();
		public readonly IDictionary<int, SapkaInfo> SapkaInfos = new Dictionary<int, SapkaInfo>();
		public readonly IDictionary<int, VisualizerSapkaInfo> Scores = new Dictionary<int, VisualizerSapkaInfo>();
	}
}