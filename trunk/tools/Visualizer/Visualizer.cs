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
			DoubleBuffered = true;
			var timer = new Timer();
			timer.Tick += (sender,e) => Refresh();
			timer.Start();
		}

		public int PlayerId
		{
			get { return playerId; }
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
			totalWidth = widthInCoords*SmallPictureSize;
			totalHeight = heightInCoords*SmallPictureSize;
			currentMap = gameInfo.MapInfo.Map;
		}

		public void OnRoundStart(StartRoundInfo startRoundInfo)
		{
			currentMap = startRoundInfo.MapInfo.Map;
			damagingRanges = new int[widthInCells, heightInCells];
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
			fieldPaddingX = (Width - totalWidth)/2;
			fieldPaddingY = (Height - totalHeight)/2;
			if (currentMap == null) return;
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
				sapkaPositions[sapka.SapkaNumber] = sapka.Pos;
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
			DrawMap(e.Graphics);
		}

		private static bool GoodPos(int x, int y, int w, int h)
		{
			return x >= 0 && x < w && y >= 0 && y < h;
		}

		private void DrawMap(Graphics gr)
		{
			for (int x = 0; x < currentMap.GetLength(0); x++)
			{
				for (int y = 0; y < currentMap.GetLength(1); y++)
				{
					DrawCell(x, y, currentMap[x, y], gr, true);
				}
			}
			foreach (var pair in sapkaPositions)
			{
				DrawCoord(pair.Value.X, pair.Value.Y, (char) pair.Key, gr);
			}
		}

		private void DrawCell(int x, int y, char type, Graphics gr, bool shouldRecurse)
		{
			if (!GoodPos(x, y, widthInCells, heightInCells)) return;
			DrawPicture(type, x*PictureSize, y*PictureSize, PictureSize, PictureSize, gr);
			/*if (type == '#')
			{
				for (int d = 1; d <= damagingRanges[x, y]; d++)
				{
					DrawCell(x + d, y, type, gr, false);
					DrawCell(x - d, y, type, gr, false);
					DrawCell(x, y + d, type, gr, false);
					DrawCell(x, y - d, type, gr, false);
				}
			}*/
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
		private char[,] currentMap;
		private int[,] damagingRanges;

		private const int SmallPictureSize = 6;
		private int PictureSize;
		private int widthInCoords;
		private int heightInCoords;
		private int totalWidth;
		private int totalHeight;
		private int fieldPaddingX;
		private int fieldPaddingY;

		private const string PicturesDirectory = @"..\..\Pictures";
	}
}
