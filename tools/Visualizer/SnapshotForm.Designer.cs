namespace Visualizer
{
	partial class SnapshotForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pbBackground = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pbBackground)).BeginInit();
			this.SuspendLayout();
			// 
			// pbBackground
			// 
			this.pbBackground.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbBackground.Location = new System.Drawing.Point(0, 0);
			this.pbBackground.Name = "pbBackground";
			this.pbBackground.Size = new System.Drawing.Size(284, 264);
			this.pbBackground.TabIndex = 0;
			this.pbBackground.TabStop = false;
			this.pbBackground.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbBackground_MouseClick);
			// 
			// SnapshotForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 264);
			this.Controls.Add(this.pbBackground);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "SnapshotForm";
			this.Text = "Мгновенное фото!";
			((System.ComponentModel.ISupportInitialize)(this.pbBackground)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pbBackground;
	}
}