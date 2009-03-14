using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
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
			var timer = new Timer();
			timer.Tick += (sender, e) => Refresh();
			timer.Start();
		}

		public void OnGameStart(GameInfo gameInfo)
		{
			playerId = gameInfo.PID;
			cellSize = gameInfo.MapInfo.MapCellSize;
			PictureSize = cellSize*SmallPictureSize;
			InitPictures();
			widthInCells = gameInfo.MapInfo.Map.GetLength(0);
			heightInCells = gameInfo.MapInfo.Map.GetLength(1);
			widthInCoords = widthInCells*cellSize;
			heightInCoords = heightInCells*cellSize;
			SetBackground(gameInfo.MapInfo.Map);
		}

		public void OnRoundStart(StartRoundInfo startRoundInfo)
		{
			SetBackground(startRoundInfo.MapInfo.Map);
		}

		public void OnMapChange(MapChangeInfo mapChangeInfo)
		{
			lock(mapChanges)
			{
				mapChanges.Enqueue(mapChangeInfo);
			}
		}

		public void OnFinishRound(int score)
		{
			// MessageBox.Show(string.Format("End of round. Score: {0}", score));
		}

		public void OnFinishGame(PlayerResult[] playerResults)
		{
			// MessageBox.Show(string.Format("End of game. Score: {0}", playerResults[playerId].Score));
		}

		private void Visualizer_Paint(object sender, PaintEventArgs e)
		{
			MapChangeInfo mapChangeInfo = null;
			lock (mapChanges)
			{
				if (mapChanges.Count > 0)
				{
					mapChangeInfo = mapChanges.Dequeue();
				}
			}
			if (mapChangeInfo == null) return;
			foreach (var sapka in mapChangeInfo.Sapkas)
			{
				if (sapka.IsDead) continue;
				if (sapkaPositions.ContainsKey(sapka.SapkaNumber))
				{
					var sapkaPos = sapkaPositions[sapka.SapkaNumber];
					DrawCell(sapkaPos.X / cellSize, sapkaPos.Y / cellSize, '.', e.Graphics);
				}
				DrawCoord(sapka.Pos.X, sapka.Pos.Y, (char)sapka.SapkaNumber, e.Graphics);
				sapkaPositions[sapka.SapkaNumber] = sapka.Pos;
			}
			foreach (var remove in mapChangeInfo.Removes)
			{
				DrawCell(remove.Pos.X, remove.Pos.Y, '.', e.Graphics);
			}
			foreach (var add in mapChangeInfo.Adds)
			{
				DrawCell(add.Pos.X, add.Pos.Y, add.SubstanceType, e.Graphics);
				if (add.SubstanceType == '#')
				{
					for (int d = 1; d <= add.DamagingRange; d++)
					{
						DrawCell(add.Pos.X + d, add.Pos.Y, add.SubstanceType, e.Graphics);
						DrawCell(add.Pos.X - d, add.Pos.Y, add.SubstanceType, e.Graphics);
						DrawCell(add.Pos.X, add.Pos.Y + d, add.SubstanceType, e.Graphics);
						DrawCell(add.Pos.X, add.Pos.Y - d, add.SubstanceType, e.Graphics);
					}
				}
			}
		}
		
		private static bool GoodPos(int x, int y, int w, int h)
		{
			return x >= 0 && x < w && y >= 0 && y < h;
		}

		private void SetBackground(char[,] map)
		{
			if (map == null) return;
			var background = new Bitmap(widthInCoords, heightInCoords);
			using (var gr = Graphics.FromImage(background))
			{
				for (int x = 0; x < map.GetLength(0); x++)
				{
					for (int y = 0; y < map.GetLength(1); y++)
					{
						DrawCell(x, y, map[x, y], gr);
					}
				}
			}
			BackgroundImage = background;
			BackgroundImageLayout = ImageLayout.None;
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
			gr.DrawImage(typeToPicture[type], x, y, w, h);
		}

		private void InitPictures()
		{
			foreach (var key in new[] { 'b', 'v', 'f' })
			{
				AddPictureForBonus(key, true);
			}
			foreach (var key in new[] { 'r', 's', 'u', 'o' })
			{
				AddPictureForBonus(key, false);
			}
			foreach (var filename in Directory.GetFiles(PicturesDirectory))
			{
				if (Path.GetExtension(filename) != ".bmp") continue;
				Image picture = Image.FromFile(filename);
				char key;
				string stripped = Path.GetFileName(filename);
				if (stripped.StartsWith("sapka"))
				{
					key = (char) (stripped.Substring("sapka".Length)[0] - '0');
				}
				else if (stripped.StartsWith("dot"))
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
				else
				{
					key = stripped[0];
				}
				typeToPicture.Add(key, picture);
			}
		}

		private void AddPictureForBonus(char key, bool good)
		{
			using (var bmp = new Bitmap(PictureSize, PictureSize))
			using (var gr = Graphics.FromImage(bmp))
			{
				gr.FillRectangle(Brushes.Green, new Rectangle(0, 0, PictureSize, PictureSize));
				gr.DrawString(key.ToString(), DefaultFont, good ? Brushes.Blue : Brushes.Red, PictureSize/4.0f, PictureSize/4.0f);
				using (var stream = new MemoryStream())
				{
					bmp.Save(stream, ImageFormat.Bmp);
					typeToPicture.Add(key, Image.FromStream(stream));
				}
			}
		}

		private int cellSize;
		private readonly IDictionary<char, Image> typeToPicture = new Dictionary<char, Image>();
		private readonly IDictionary<int, Pos> sapkaPositions = new Dictionary<int, Pos>();
		private int playerId;
		private readonly Queue<MapChangeInfo> mapChanges = new Queue<MapChangeInfo>();
		private int widthInCells;
		private int heightInCells;

		private const int SmallPictureSize = 6;
		private int PictureSize;
		private int widthInCoords;
		private int heightInCoords;

		private const string PicturesDirectory = @"..\..\Pictures";
	}
}
