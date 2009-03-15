using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Parsing;
using Timer=System.Windows.Forms.Timer;

namespace Visualizer
{
	public partial class Visualizer : Form, IParserListener
	{
		public Visualizer()
		{
			InitializeComponent();
			InitTimer();
			DoubleBuffered = true;
			fieldPaddingX = Gap;
			fieldPaddingY = 100;
		}

		private void InitTimer()
		{
			var timer = new Timer();
			timer.Tick += (sender, e) => Refresh();
			timer.Start();
		}

		public int PlayerId
		{
			get { return playerId; }
		}

		public void OnGameStart(GameInfo gameInfo)
		{
			AddAction(() =>
				{
					playerId = gameInfo.PID;
					cellSize = gameInfo.MapInfo.MapCellSize;
					PictureSize = cellSize*SmallPictureSize;
					InitPictures();
					widthInCells = gameInfo.MapInfo.Map.GetLength(0);
					heightInCells = gameInfo.MapInfo.Map.GetLength(1);
					widthInCoords = widthInCells*cellSize;
					heightInCoords = heightInCells*cellSize;
					totalWidth = widthInCoords*SmallPictureSize;
					totalHeight = heightInCoords*SmallPictureSize;
					currentMap = gameInfo.MapInfo.Map;
					currentRound = 0;
					scores.Clear();
				});
		}

		public void OnRoundStart(StartRoundInfo startRoundInfo)
		{
			AddAction(() =>
				{
					currentMap = startRoundInfo.MapInfo.Map;
					currentRound++;
					damagingRanges = new int[widthInCells,heightInCells];
				});
		}

		public void OnMapChange(MapChangeInfo mapChangeInfo)
		{
			AddAction(() =>
				{
					fieldPaddingX = Gap;
					//fieldPaddingY = (Height - totalHeight)/2;

					if (tvInfo == null)
					{
						InitStatsTreeView();
					}

					foreach (var sapka in mapChangeInfo.Sapkas)
					{
						if (sapka.IsDead)
						{
							sapkaInfos[sapka.SapkaNumber].IsDead = true;
							continue;
						}
						sapkaInfos[sapka.SapkaNumber] = sapka;
					}
					foreach (var remove in mapChangeInfo.Removes)
					{
						currentMap[remove.Pos.X, remove.Pos.Y] = '.';
					}
					foreach (var add in mapChangeInfo.Adds)
					{
						currentMap[add.Pos.X, add.Pos.Y] = add.SubstanceType;
						if (add.SubstanceType == '#')
						{
							damagingRanges[add.Pos.X, add.Pos.Y] = add.DamagingRange;
						}
					}
				});
		}


		public void OnFinishRound(int score)
		{
			AddAction(() =>
				{
					if (!scores.ContainsKey(playerId))
					{
						scores[playerId] = new VisualizerSapkaInfo();
					}
					scores[playerId].Score = score;
				});
		}

		public void OnFinishGame(PlayerResult[] playerResults)
		{
			AddAction(() =>
				{
					foreach (var result in playerResults)
					{
						if (!scores.ContainsKey(result.PlayerNumber))
						{
							scores[result.PlayerNumber] = new VisualizerSapkaInfo();
						}
						scores[result.PlayerNumber].Score = result.Score;
						scores[result.PlayerNumber].Rank = result.Rank;
					}
					currentRound = 0;
				});
		}

		private void AddAction(Action action)
		{
			lock (actionQueue)
			{
				actionQueue.Enqueue(action);
			}
		}

		private void Visualizer_Paint(object sender, PaintEventArgs e)
		{
			lock (actionQueue)
			{
				if (actionQueue.Count > 0)
				{
					actionQueue.Dequeue()();
				}
			}
			var roundString = currentRound > 0 ? "Раунд " + currentRound : "Игра не идёт";
			var font = new Font(FontFamily.GenericSansSerif, 48);
			var areaNeeded = e.Graphics.MeasureString(roundString, font);
			e.Graphics.DrawString(roundString, font, Brushes.Black, (Width - areaNeeded.Width)/2, 0);
			DrawMap(e.Graphics);
			if (tvInfo != null && currentRound > 0)
			{
				UpdateStatsTreeView();
			}
		}

		private static bool GoodPos(int x, int y, int w, int h)
		{
			return x >= 0 && x < w && y >= 0 && y < h;
		}

		private void DrawMap(Graphics gr)
		{
			if(currentMap == null) return;
			for (int x = 0; x < currentMap.GetLength(0); x++)
			{
				for (int y = 0; y < currentMap.GetLength(1); y++)
				{
					DrawCell(x, y, currentMap[x, y], gr, true);
				}
			}
			foreach (var pair in sapkaInfos)
			{
				DrawCoord(pair.Value.Pos.X, pair.Value.Pos.Y, (char) pair.Key, gr);
			}
		}
		
		private void InitStatsTreeView()
		{
			tvInfo = new TreeView
				{
					Visible = true,
					Left = fieldPaddingX + totalWidth + Gap,
					Top = fieldPaddingY,
					Height = totalHeight,
				};
			tvInfo.Width = Width - tvInfo.Left - Gap;
			Controls.Add(tvInfo);
		}

		private void UpdateStatsTreeView()
		{
			tvInfo.BeginUpdate();
			tvInfo.Nodes.Clear();
			foreach (var sapkaIndex in sapkaInfos.Keys)
			{
				var sapkaInfo = sapkaInfos[sapkaIndex];

				var sapkaNode = new TreeNode("Сапка " + sapkaIndex) { ForeColor = sapkaColors[sapkaIndex] };
				sapkaNode.Nodes.Add(string.Format("Осталось бомб: {0}", sapkaInfo.BombsLeft));
				sapkaNode.Nodes.Add(string.Format("Дальность бомбы: {0}", sapkaInfo.BombsStrength));
				sapkaNode.Nodes.Add(string.Format("Скорость: {0}", sapkaInfo.Speed));
				sapkaNode.Nodes.Add(sapkaInfo.IsDead ? "Сапка мертва" : "Сапка жива");
				sapkaNode.Nodes.Add(sapkaInfo.Infected ? "Сапка заражена" : "Сапка здорова");
				if (scores.ContainsKey(sapkaIndex))
				{
					var sapkaScore = scores[sapkaIndex];
					sapkaNode.Nodes.Add(string.Format("Очки: {0}", sapkaScore.Score));
					sapkaNode.Nodes.Add(string.Format("Место: {0}", sapkaScore.Rank));
				}
				tvInfo.Nodes.Add(sapkaNode);
			}
			tvInfo.ExpandAll();
			tvInfo.EndUpdate();
		}

		private void DrawCell(int x, int y, char type, Graphics gr, bool shouldRecurse)
		{
			if (!GoodPos(x, y, widthInCells, heightInCells)) return;
			DrawPicture(type, x*PictureSize, y*PictureSize, PictureSize, PictureSize, gr);
			if (type == '#' && shouldRecurse)
			{
				for (int d = 1; d <= damagingRanges[x, y]; d++)
				{
					DrawCell(x + d, y, type, gr, false);
					DrawCell(x - d, y, type, gr, false);
					DrawCell(x, y + d, type, gr, false);
					DrawCell(x, y - d, type, gr, false);
				}
			}
		}

		private void DrawCoord(int x, int y, char type, Graphics gr)
		{
			if (!GoodPos(x, y, widthInCoords, heightInCoords)) return;
			int realX = (x/cellSize)*PictureSize + (x%cellSize)*SmallPictureSize;
			int realY = (y/cellSize)*PictureSize + (y%cellSize)*SmallPictureSize;
			DrawPicture(type, realX, realY, SmallPictureSize, SmallPictureSize, gr);
		}

		private void DrawPicture(char type, int x, int y, int w, int h, Graphics gr)
		{
			gr.DrawImage(typeToPicture[type], fieldPaddingX + x, fieldPaddingY + y, w, h);
		}

		private void InitPictures()
		{
			if(typeToPicture.Count > 0) return;
			foreach (var key in new[] {'b', 'v', 'f'})
			{
				AddPictureForBonus(key, true);
			}
			foreach (var key in new[] {'r', 's', 'u', 'o'})
			{
				AddPictureForBonus(key, false);
			}
			for (int i = 0; i < MaxSapkaCount; i++)
			{
				AddPictureForSapka((char) i, sapkaBrushes[i]);
			}
			foreach (var filename in Directory.GetFiles(PicturesDirectory))
			{
				if (Path.GetExtension(filename) != ".bmp") continue;
				Image picture = Image.FromFile(filename);
				char key;
				string stripped = Path.GetFileName(filename);
				if (stripped.StartsWith("dot"))
				{
					key = '.';
				}
				else if (stripped.StartsWith("sharp"))
				{
					key = '#';
				}
				else if(stripped.StartsWith("asterisk"))
				{
					key = '*';
				}
				else if(stripped.StartsWith("unknown"))
				{
					key = '?';
				}
				else
				{
					key = stripped[0];
				}
				typeToPicture.Add(key, picture);
			}
		}

		private void AddPictureForBonus(char key, bool good)
		{
			AddPicture(key, gr =>
				{
					gr.FillRectangle(Brushes.Green, new Rectangle(0, 0, PictureSize, PictureSize));
					gr.DrawString(key.ToString(), DefaultFont, good ? Brushes.Blue : Brushes.Red, PictureSize / 4.0f, PictureSize / 4.0f);
				}, PictureSize);
		}

		private void AddPictureForSapka(char key, Brush br)
		{
			AddPicture(key, gr => gr.FillRectangle(br, new Rectangle(0, 0, SmallPictureSize, SmallPictureSize)), SmallPictureSize);
		}

		private void AddPicture(char key, Action<Graphics> painter, int size)
		{
			using (var bmp = new Bitmap(size, size))
			using (var gr = Graphics.FromImage(bmp))
			{
				painter(gr);
				using (var stream = new MemoryStream())
				{
					bmp.Save(stream, ImageFormat.Bmp);
					typeToPicture.Add(key, Image.FromStream(stream));
				}
			}
		}

		private TreeView tvInfo;

		private readonly IDictionary<char, Image> typeToPicture = new Dictionary<char, Image>();

		private int cellSize;
		private int playerId;
		private int widthInCells;
		private int heightInCells;

		private char[,] currentMap;
		private int[,] damagingRanges;
		private readonly IDictionary<int, SapkaInfo> sapkaInfos = new Dictionary<int, SapkaInfo>();
		private readonly IDictionary<int, VisualizerSapkaInfo> scores = new Dictionary<int, VisualizerSapkaInfo>();
		private readonly Queue<Action> actionQueue = new Queue<Action>();

		private const int SmallPictureSize = 6;
		private int PictureSize;
		private int widthInCoords;
		private int heightInCoords;
		private int totalWidth;
		private int totalHeight;
		private int fieldPaddingX;
		private int fieldPaddingY;

		private const string PicturesDirectory = @"Pictures";
		private const int MaxSapkaCount = 4;
		private const int Gap = 30;
		private int currentRound;

		private static readonly Brush[] sapkaBrushes =
			{
				Brushes.Magenta,
				Brushes.Blue,
				Brushes.Brown,
				Brushes.Yellow
			};
		private static readonly Color[] sapkaColors =
			{
				Color.Magenta,
				Color.Blue,
				Color.Brown,
				Color.Yellow
			};
	}

	internal class VisualizerSapkaInfo
	{
		public int Score;
		public int Rank;
	}
}
