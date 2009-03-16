using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Core.Parsing;
using Core.PathFinding;

namespace Visualizer
{
	public partial class SnapshotForm : Form
	{
		public SnapshotForm(Image backgroundImage, GameStateSnapshot gameStateInfo)
		{
			this.gameStateInfo = gameStateInfo;
			InitializeComponent();
			pbBackground.Image = backgroundImage;
			initialImage = backgroundImage;
			
			Size = pbBackground.PreferredSize;
			Width += Width - ClientRectangle.Width;
			Height += Height - ClientRectangle.Height;
			pathFinder.SetMap(gameStateInfo.Cells, gameStateInfo.CellSize);
		}
		
		private readonly IPathFinder pathFinder = new PathFinder();
		private readonly GameStateSnapshot gameStateInfo;
		private readonly Image initialImage;
		private Image selectedImage;
		private Pos selectedCoordPos;
		private int selectedSpeed;

		private void pbBackground_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				SetStartCell(e.X, e.Y);
			}
			else if (e.Button == MouseButtons.Right)
			{
				DrawPath(e.X, e.Y);
			}
		}

		private void SetStartCell(int x, int y)
		{
			var pos = new Pos(x/gameStateInfo.SmallPictureSize, y/gameStateInfo.SmallPictureSize);
			if (!gameStateInfo.SapkasData.ContainsKey(pos)) return;
			//selectedSpeed = gameStateInfo.SapkasData[pos].Speed;
			selectedSpeed = 1;
			pbBackground.Image = (Image) initialImage.Clone();
			using (var gr = Graphics.FromImage(pbBackground.Image))
			{
				Fill(gr, x - x%gameStateInfo.SmallPictureSize, y - y%gameStateInfo.SmallPictureSize);
				selectedCoordPos = new Pos(x/gameStateInfo.SmallPictureSize, y/gameStateInfo.SmallPictureSize);
			}
			selectedImage = (Image) pbBackground.Image.Clone();
		}

		private void DrawPath(int x, int y)
		{
			if (selectedCoordPos == null) return;
			pbBackground.Image = (Image) selectedImage.Clone();
			var paths = pathFinder.FindPaths(selectedCoordPos.X, selectedCoordPos.Y, gameStateInfo.Time, selectedSpeed);
			var path = paths[x/gameStateInfo.SmallPictureSize, y/gameStateInfo.SmallPictureSize];
			if (path == null) return;
			using (var gr = Graphics.FromImage(pbBackground.Image))
			{
				var curx = selectedCoordPos.X*gameStateInfo.SmallPictureSize;
				var cury = selectedCoordPos.Y*gameStateInfo.SmallPictureSize;
				foreach (var dir in path.FullPath())
				{
					if (dir == 's')
					{
						continue;
					}
					for (int i = 0; i < selectedSpeed; i++)
					{
						Fill(gr, curx, cury);
						curx += dx[dir] * gameStateInfo.SmallPictureSize;
						cury += dy[dir] * gameStateInfo.SmallPictureSize;
					}
				}
				Fill(gr, curx, cury);
			}
		}

		private void Fill(Graphics gr, int x, int y)
		{
			gr.FillRectangle(Brushes.Yellow, x, y, gameStateInfo.SmallPictureSize, gameStateInfo.SmallPictureSize);
		}

		private IDictionary<char, int> dx = new Dictionary<char, int>
			{
				{ 'u', 0 },
				{ 'd', 0 },
				{ 'l', -1 },
				{ 'r', 1 }
			};

		private IDictionary<char, int> dy = new Dictionary<char, int>
			{
				{ 'u', -1 },
				{ 'd', 1 },
				{ 'l', 0 },
				{ 'r', 0 }
			};
	}
}
