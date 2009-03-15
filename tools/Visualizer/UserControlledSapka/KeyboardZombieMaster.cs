using System.Windows.Forms;

namespace Visualizer.UserControlledSapka
{
	class KeyboardZombieMaster : IZombieMaster
	{
		private readonly Control control;
		private bool isBombing;

		public KeyboardZombieMaster(Control control)
		{
			this.control = control;
			control.KeyDown += Form_OnKeyDown;
			control.KeyUp += Form_OnKeyUp;
			control.KeyPress += Form_OnKeyPress;
		}

		private void Form_OnKeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == ' ')
			{
				isBombing = true;
				e.Handled = true;
				UpdateCaption();
			}
		}

		private void UpdateCaption()
		{
			control.Text = Command + (isBombing ? "b" : "");
		}

		private void Form_OnKeyUp(object sender, KeyEventArgs e)
		{
			e.Handled = true;
			if(e.KeyCode == Keys.A || e.KeyCode == Keys.Left) Reset('l');
			else if(e.KeyCode == Keys.D || e.KeyCode == Keys.Right) Reset('r');
			else if(e.KeyCode == Keys.W || e.KeyCode == Keys.Up) Reset('u');
			else if(e.KeyCode == Keys.S || e.KeyCode == Keys.Down) Reset('d');
			else e.Handled = false;
			UpdateCaption();
		}

		private void Reset(char c)
		{
			if(Command == c) Command = 's';
		}

		private void Form_OnKeyDown(object sender, KeyEventArgs e)
		{
			e.Handled = true;
			if(e.KeyCode == Keys.A || e.KeyCode == Keys.Left) Command = 'l';
			else if(e.KeyCode == Keys.D || e.KeyCode == Keys.Right) Command = 'r';
			else if(e.KeyCode == Keys.W || e.KeyCode == Keys.Up) Command = 'u';
			else if(e.KeyCode == Keys.S || e.KeyCode == Keys.Down) Command = 'd';
			else e.Handled = false;
			UpdateCaption();
		}

		public char Command
		{
			get; private set;
		}

		public bool IsBombing()
		{
			bool temp = isBombing;
			isBombing = false;
			return temp;
		}
	}
}