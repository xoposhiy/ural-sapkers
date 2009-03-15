using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Core.Parsing;
using TheSapka;
using Visualizer.UserControlledSapka;
using Timer=System.Windows.Forms.Timer;

namespace Visualizer
{
	public partial class Visualizer : Form, IParserListener
	{
		private ISapkaServerLogger logger_;

		public Visualizer(ISapkaServerLogger logger)
		{
			logger_ = logger;

			InitializeComponent();
			InitTimer();
			DoubleBuffered = true;
			fieldPaddingX = Gap;
			fieldPaddingY = 100;
		}

		private void InitTimer()
		{
			var timer = new Timer();
			timer.Interval = 200;
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
					time = mapChangeInfo.Time;
					dangerLevel = mapChangeInfo.DangerLevel;
					
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
					logger_.Flush();
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
					logger_.Flush();
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
				while (actionQueue.Count > 0)
				{
					actionQueue.Dequeue()();
				}
			}
			var roundString = currentRound > 0 ? "Раунд " + currentRound : "Игра не идёт";
			var font = new Font(FontFamily.GenericSansSerif, 48);
			var areaNeeded = e.Graphics.MeasureString(roundString, font);
			e.Graphics.DrawString(roundString, font, Brushes.Black, (Width - areaNeeded.Width)/2, mainMenu.Height);
			DrawMap(e.Graphics);
			if (tvInfo != null)
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
				for (int y = 0; y < currentMap.GetLength(1); y++)
					DrawCell(x, y, currentMap[x, y], gr);

			//отрисовка взрыва
			for (int x = 0; x < currentMap.GetLength(0); x++)
				for (int y = 0; y < currentMap.GetLength(1); y++)
				{
					if (currentMap[x, y] != '#') continue;
					DrawDamageLine(gr, x, y, 1, 0);
					DrawDamageLine(gr, x, y, -1, 0);
					DrawDamageLine(gr, x, y, 0, 1);
					DrawDamageLine(gr, x, y, 0, -1);
				}

			foreach (var pair in sapkaInfos)
			{
				DrawCoord(pair.Value.Pos.X, pair.Value.Pos.Y, (char) pair.Key, gr);
			}
		}

		private void DrawDamageLine(Graphics gr, int x, int y, int dx, int dy)
		{
			for (int d = 1; d <= damagingRanges[x, y]; d++)
			{
				int nextX = x + d * dx;
				int nextY = y + d * dy;
                if (!GoodPos(nextX, nextY, widthInCells, heightInCells)) break;
				if (currentMap[nextX, nextY] == 'X') break;
				DrawCell(nextX, nextY, '#', gr);
				if (currentMap[nextX, nextY] != '.') break;
			}
		}

		private void InitStatsTreeView()
		{
			tvInfo = new TreeView
			         	{
			         		Visible = true,
							Width = 200,
							Dock = DockStyle.Right,
			         		Enabled = false,
				};
			
			Controls.Add(tvInfo);
		}

		private void UpdateStatsTreeView()
		{
			if (tvInfo.Handle == IntPtr.Zero) return;
			tvInfo.BeginUpdate();
			tvInfo.Nodes.Clear();
			var infoNode = new TreeNode("Игра");
			infoNode.Nodes.Add(string.Format("Время игры: {0}", time));
			infoNode.Nodes.Add(string.Format("Опасность коллапса: {0}", dangerLevel));
			tvInfo.Nodes.Add(infoNode);
			
			foreach(var sapkaIndex in sapkaInfos.Keys)
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

		private void DrawCell(int x, int y, char type, Graphics gr)
		{
			if (!GoodPos(x, y, widthInCells, heightInCells)) return;
			DrawPicture(type, x*PictureSize, y*PictureSize, PictureSize, PictureSize, gr);
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

		private int time;
		private int dangerLevel;


		private void StartDummy(int port)
		{
			var thread = new Thread(
				() => new DummySapka("localhost", port, "ural-sapkers").Run()
				);
			thread.IsBackground = true;
			thread.Start();
		}

		private void StartZombie(int port)
		{
			var thread = new Thread(
				() => new ZombieSapka("localhost", port, "ural-sapkers", new KeyboardZombieMaster(this)).Run()
				);
			thread.IsBackground = true;
			thread.Start();
		}

		private void наПорт20015ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StartZombie(20015);
		}

		private void наПорт20016ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StartZombie(20016);
		}

		private void чёЗаЗомбиЕщё0оToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show(this, "Зомби — это сапка, управляемая клавиатурой (курсоры + SPACE)\r\nEnjoy!", "Зомби?!?");
		}

		private void на20015ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StartDummy(20015);
		}

		private void какойТакойТормозToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show(this, "Тормоз — это сапка, которая всегда стоит на месте и ничего не делает\r\nEnjoy!", "Тормоз?!?");
		}

		private void на20016ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StartDummy(20016);
		}

	}

	internal class VisualizerSapkaInfo
	{
		public int Score;
		public int Rank;
	}
}
