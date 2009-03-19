using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections;
using System.Threading;
using System.Windows.Forms;
using Core;
using Core.AI;
using Core.Parsing;
using Core.PathFinding;
using Core.StateCalculations;
using TheSapka;
using Visualizer.UserControlledSapka;
using Path=System.IO.Path;
using Timer=System.Windows.Forms.Timer;

namespace Visualizer
{
	public partial class Visualizer : Form
	{
		private const int Gap = 30;
		private const int MaxSapkaCount = 4;
		private const string PicturesDirectory = @"Pictures";
		private const int SmallPictureSize = 6;

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

		public readonly VisualizerModel model = new VisualizerModel();
		private readonly IDictionary<char, Image> typeToPicture = new Dictionary<char, Image>();
		private readonly ModelUpdatersQueue updatersQueue;
		private int fieldPaddingX;
		private int fieldPaddingY;
		private volatile bool makeSnapshot;
		private TreeView tvInfo;
		private ISapkaMindView ai;

		public Visualizer(ModelUpdatersQueue updatersQueue)
		{
			this.updatersQueue = updatersQueue;

			InitializeComponent();
			InitTimer();
			InitStatsTreeView();
			InitPictures();
			fieldPaddingX = Gap;
			fieldPaddingY = 100;
		}

		private int PictureSize
		{
			get { return model.CellSize*SmallPictureSize; }
		}

		private void InitTimer()
		{
			var timer = new Timer {Interval = 200};
			timer.Tick += (sender, e) => Refresh();
			timer.Start();
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

		private void InitPictures()
		{
			for (int i = 0; i < MaxSapkaCount; i++)
			{
				AddPictureForSapka((char) i, sapkaBrushes[i]);
			}
			foreach (string filename in Directory.GetFiles(PicturesDirectory))
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
				else if (stripped.StartsWith("asterisk"))
				{
					key = '*';
				}
				else if (stripped.StartsWith("unknown"))
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
			using (Graphics gr = Graphics.FromImage(bmp))
			{
				painter(gr);
				using (var stream = new MemoryStream())
				{
					bmp.Save(stream, ImageFormat.Bmp);
					typeToPicture.Add(key, Image.FromStream(stream));
				}
			}
		}

		private void Visualizer_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 'q')
			{
				makeSnapshot = true;
			}
		}

        public void UpdateModel()
        {
            updatersQueue.ExecuteBatch(model);
        }

		private void Visualizer_Paint(object sender, PaintEventArgs e)
		{
            UpdateModel();
			DrawRoundString(e.Graphics);
			DrawMap(e.Graphics);
			DrawSapkaTargetPath(e.Graphics);
			UpdateStatsTreeView();
			if (makeSnapshot && model.CurrentMap != null)
			{
				var fieldImage = new Bitmap(model.WidthInCoords*SmallPictureSize, model.HeightInCoords*SmallPictureSize);
				using (Graphics gr = Graphics.FromImage(fieldImage))
				{
					int oldPaddingX = fieldPaddingX;
					int oldPaddingY = fieldPaddingY;
					fieldPaddingX = 0;
					fieldPaddingY = 0;
					DrawMap(gr);
					fieldPaddingX = oldPaddingX;
					fieldPaddingY = oldPaddingY;
				}
				new SnapshotForm(fieldImage, new GameStateSnapshot(model, PictureSize, SmallPictureSize)).Show();
				makeSnapshot = false;
			}
		}

		private void DrawSapkaTargetPath(Graphics g)
		{
			if (ai == null || model.State.Sapkas == null || model.State.MySapka.IsDead) return;
			var path = ai.LastDecisionPath;
			var curX = model.State.MySapka.Pos.X;
			var curY = model.State.MySapka.Pos.Y;
			foreach(var dir in path)
			{
                if (dir == 's' || dir == 'b')
				{
					continue;
				}
				var finder = new PathFinder();
				finder.SetMap(model.State.Map, model.State.CellSize);
				int endX = curX;
				int endY = curY;
				finder.Move(ref endX, ref endY, model.State.Time, model.State.MySapka.Speed, Commons.dirIndex[dir]);
				while(curX != endX || curY != endY)
				{
					curX += Commons.dx[dir];
					curY += Commons.dy[dir];
					Fill(g, curX, curY);
				}
			}
		}

		private void Fill(Graphics gr, int x, int y)
		{
			gr.FillRectangle(
				Brushes.Yellow, 
				fieldPaddingX + x * SmallPictureSize, 
				fieldPaddingY + y * SmallPictureSize, 
				SmallPictureSize, SmallPictureSize);
		}


		private static bool GoodPos(int x, int y, int w, int h)
		{
			return x >= 0 && x < w && y >= 0 && y < h;
		}

		private void DrawRoundString(Graphics gr)
		{
			string roundString = model.CurrentRound > 0 ? ("Раунд " + model.CurrentRound) : "Игра не идёт";
			var font = new Font(FontFamily.GenericSansSerif, 48);
			SizeF areaNeeded = gr.MeasureString(roundString, font);
			gr.DrawString(roundString, font, Brushes.Black, (Width - areaNeeded.Width)/2, mainMenu.Height);
		}

		private void DrawMap(Graphics gr)
		{
			if (model.CurrentMap == null) return;
			for (int x = 0; x < model.CurrentMap.GetLength(0); x++)
				for (int y = 0; y < model.CurrentMap.GetLength(1); y++)
					DrawCell(x, y, model.CurrentMap[x, y], gr);

			//отрисовка взрыва
			for (int x = 0; x < model.CurrentMap.GetLength(0); x++)
				for (int y = 0; y < model.CurrentMap.GetLength(1); y++)
				{
					if (model.CurrentMap[x, y] != '#') continue;
					DrawDamageLine(gr, x, y, 1, 0);
					DrawDamageLine(gr, x, y, -1, 0);
					DrawDamageLine(gr, x, y, 0, 1);
					DrawDamageLine(gr, x, y, 0, -1);
				}

			foreach (var pair in model.SapkaInfos)
				DrawSapka(pair.Value.Pos.X, pair.Value.Pos.Y, gr, sapkaBrushes[pair.Key]);
		}

		private void DrawDamageLine(Graphics gr, int x, int y, int dx, int dy)
		{
			for (int d = 1; d <= model.DamagingRanges[x, y]; d++)
			{
				int nextX = x + d*dx;
				int nextY = y + d*dy;
				if (!GoodPos(nextX, nextY, model.WidthInCells, model.HeightInCells)) break;
				if (model.CurrentMap[nextX, nextY] == 'X') break;
				DrawCell(nextX, nextY, '#', gr);
				if (model.CurrentMap[nextX, nextY] != '.') break;
			}
		}

		private void DrawCell(int x, int y, char type, Graphics gr)
		{
			if (!GoodPos(x, y, model.WidthInCells, model.HeightInCells)) return;
			DrawPicture(type, x*PictureSize, y*PictureSize, PictureSize, PictureSize, gr);
			var cell = model.State.Map[x, y];
			if (cell.DeadlySince != int.MaxValue)
			{
				if (cell.DeadlyTill < model.State.Time)
				{
					throw new Exception(string.Format("Game state failed: {0} {1}", cell.DeadlyTill, model.State.Time));
				}
				gr.DrawString((cell.DeadlySince - model.State.Time).ToString(), new Font("Arial", 7), Brushes.Black,
				              fieldPaddingX + x*PictureSize, fieldPaddingY + y*PictureSize + PictureSize/2);
				gr.DrawString((cell.DeadlyTill - model.State.Time).ToString(), new Font("Arial", 7), Brushes.Black,
				              fieldPaddingX + x*PictureSize + PictureSize/2, fieldPaddingY + y*PictureSize + PictureSize/2);
			}
		}

		private void DrawSapka(int x, int y, Graphics gr, Brush brush)
		{
			if (!GoodPos(x, y, model.WidthInCoords, model.HeightInCoords)) return;
			int realX = (x/model.CellSize)*PictureSize + (x%model.CellSize)*SmallPictureSize;
			int realY = (y/model.CellSize)*PictureSize + (y%model.CellSize)*SmallPictureSize;
			gr.FillRectangle(brush, fieldPaddingX + realX, fieldPaddingY + realY, SmallPictureSize, SmallPictureSize);
		}

		private void DrawPicture(char type, int x, int y, int w, int h, Graphics gr)
		{
			gr.DrawImage(typeToPicture[type], fieldPaddingX + x, fieldPaddingY + y, w, h);
		}

		private void UpdateStatsTreeView()
		{
			tvInfo.BeginUpdate();
			tvInfo.Nodes.Clear();
			var infoNode = new TreeNode("Игра");
			infoNode.Nodes.Add(string.Format("Время игры: {0}", model.Time));
			infoNode.Nodes.Add(string.Format("Опасность коллапса: {0}", model.DangerLevel));
			tvInfo.Nodes.Add(infoNode);


            Dictionary<int, SapkaInfo> infos = new Dictionary<int,SapkaInfo>(model.SapkaInfos);
			foreach (int sapkaIndex in infos.Keys)
			{
				SapkaInfo sapkaInfo = infos[sapkaIndex];

				var sapkaNode = new TreeNode("Сапка " + sapkaIndex) {ForeColor = sapkaColors[sapkaIndex]};
				sapkaNode.Nodes.Add(string.Format("Осталось бомб: {0}", sapkaInfo.BombsLeft));
				sapkaNode.Nodes.Add(string.Format("Дальность бомбы: {0}", sapkaInfo.BombsStrength));
				sapkaNode.Nodes.Add(string.Format("Скорость: {0}", sapkaInfo.Speed));
				sapkaNode.Nodes.Add(sapkaInfo.IsDead ? "Сапка мертва" : "Сапка жива");
				sapkaNode.Nodes.Add(sapkaInfo.Infected ? "Сапка заражена" : "Сапка здорова");
				if (model.Scores.ContainsKey(sapkaIndex))
				{
					VisualizerSapkaInfo sapkaScore = model.Scores[sapkaIndex];
					sapkaNode.Nodes.Add(string.Format("Очки: {0}", sapkaScore.Score));
					sapkaNode.Nodes.Add(string.Format("Место: {0}", sapkaScore.Rank));
				}
				tvInfo.Nodes.Add(sapkaNode);
			}
			var aiNode = new TreeNode("AI");
			if(ai != null)
			{
				var decisionName = ai.LastDecisionName;
				aiNode.Nodes.Add("adviser: " + decisionName);
			}
			if (ai!=null)
				aiNode.Nodes.Add("inversed: " + ai.IsInverted);
			tvInfo.Nodes.Add(aiNode);

			tvInfo.ExpandAll();
			tvInfo.EndUpdate();
		}

		private void RunSapka(Func<AbstractSapka> create)
		{
			var thread = new Thread(
				() =>
					{
						try
						{
							create().Run();
						}
						catch (Exception e)
						{
							MessageBox.Show(e.ToString());
						}
					}
				);
			thread.IsBackground = true;
			thread.Start();
		}

		private void StartDummy(int port)
		{
			RunSapka(() => new DummySapka("localhost", port, "ural-sapkers"));
		}

		private void StartZombie(int port)
		{
			RunSapka(() => new ZombieSapka("localhost", port, "ural-sapkers", new KeyboardZombieMaster(this)));
		}

		private void наПорт20015ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StartZombie(20015);
		}

		private void наПорт20016ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StartZombie(20016);
		}

		private void наПорт20017ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StartZombie(20017);
		}

		private void наПорт20018ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StartZombie(20018);
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

		private void на20017ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StartDummy(20017);
		}

		private void на20018ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StartDummy(20018);
		}

		private void на20015ToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			StartAI(20015);
		}

        internal void StartSapkaMindView(ISapkaMindView sapka)
        {
            ai = sapka;
        }

		private void StartAI(int port)
		{
			RunSapka(
				() =>
					{
						var s = new Sapka("localhost", port, "ural-sapkers");
						ai = s;
						return s;
					}
				);
		}

		private void на20016ToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			StartAI(20016);
		}

		private void на20017ToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			StartAI(20017);
		}

		private void на20018ToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			StartAI(20018);
		}

		private void отладчекомЕгоToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Debugger.Launch();
		}

    }

	public class GameStateSnapshot
	{
		public readonly MapCell[,] Cells;
		public readonly int CellSize;
		public readonly int PictureSize;
		public readonly int SmallPictureSize;
		public readonly int Time;
		public IDictionary<Pos, SapkaSnapshotData> SapkasData = new Dictionary<Pos, SapkaSnapshotData>();

		public GameStateSnapshot(VisualizerModel model, int pictureSize, int smallPictureSize)
		{
			Cells = (MapCell[,]) model.State.Map.Clone();
			CellSize = model.CellSize;
			Time = model.Time;
			foreach (var sapka in model.SapkaInfos)
			{
				SapkaInfo sapkaInfo = sapka.Value;
				SapkasData[new Pos(sapkaInfo.Pos.X, sapkaInfo.Pos.Y)] = new SapkaSnapshotData(sapkaInfo.Speed);
			}
			PictureSize = pictureSize;
			SmallPictureSize = smallPictureSize;
		}
	}

	public class SapkaSnapshotData
	{
		public readonly int Speed;

		public SapkaSnapshotData(int speed)
		{
			Speed = speed;
		}
	}
}
