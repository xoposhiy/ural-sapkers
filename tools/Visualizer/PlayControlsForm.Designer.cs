namespace Visualizer
{
    partial class PlayControlsForm
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
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.buttonStep = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.labelMin = new System.Windows.Forms.Label();
            this.labelMax = new System.Windows.Forms.Label();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.labelParse = new System.Windows.Forms.Label();
            this.labelRound = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar
            // 
            this.trackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar.Location = new System.Drawing.Point(12, 12);
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(470, 42);
            this.trackBar.TabIndex = 1;
            this.trackBar.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // buttonStep
            // 
            this.buttonStep.Location = new System.Drawing.Point(15, 121);
            this.buttonStep.Name = "buttonStep";
            this.buttonStep.Size = new System.Drawing.Size(74, 23);
            this.buttonStep.TabIndex = 3;
            this.buttonStep.Text = "One Step >";
            this.buttonStep.UseVisualStyleBackColor = true;
            this.buttonStep.Click += new System.EventHandler(this.buttonStep_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Location = new System.Drawing.Point(15, 150);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(74, 23);
            this.buttonPlay.TabIndex = 4;
            this.buttonPlay.Text = "Auto >>";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // labelMin
            // 
            this.labelMin.AutoSize = true;
            this.labelMin.Location = new System.Drawing.Point(12, 41);
            this.labelMin.Name = "labelMin";
            this.labelMin.Size = new System.Drawing.Size(13, 13);
            this.labelMin.TabIndex = 5;
            this.labelMin.Text = "0";
            // 
            // labelMax
            // 
            this.labelMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMax.AutoSize = true;
            this.labelMax.Location = new System.Drawing.Point(447, 41);
            this.labelMax.Name = "labelMax";
            this.labelMax.Size = new System.Drawing.Size(35, 13);
            this.labelMax.TabIndex = 6;
            this.labelMax.Text = "label2";
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMessage.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxMessage.Location = new System.Drawing.Point(95, 99);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.ReadOnly = true;
            this.textBoxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMessage.Size = new System.Drawing.Size(387, 74);
            this.textBoxMessage.TabIndex = 7;
            // 
            // labelParse
            // 
            this.labelParse.AutoSize = true;
            this.labelParse.Location = new System.Drawing.Point(12, 99);
            this.labelParse.Name = "labelParse";
            this.labelParse.Size = new System.Drawing.Size(37, 13);
            this.labelParse.TabIndex = 8;
            this.labelParse.Text = "Parse:";
            // 
            // labelRound
            // 
            this.labelRound.AutoSize = true;
            this.labelRound.Location = new System.Drawing.Point(92, 73);
            this.labelRound.Name = "labelRound";
            this.labelRound.Size = new System.Drawing.Size(48, 13);
            this.labelRound.TabIndex = 9;
            this.labelRound.Text = "Round ?";
            // 
            // PlayControlsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 177);
            this.Controls.Add(this.labelRound);
            this.Controls.Add(this.labelParse);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.labelMax);
            this.Controls.Add(this.labelMin);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.buttonStep);
            this.Controls.Add(this.trackBar);
            this.Name = "PlayControlsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PlayControlsForm";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Button buttonStep;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Label labelMin;
        private System.Windows.Forms.Label labelMax;
        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.Label labelParse;
        private System.Windows.Forms.Label labelRound;
    }
}