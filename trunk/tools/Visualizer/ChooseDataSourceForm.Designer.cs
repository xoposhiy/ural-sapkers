namespace Visualizer
{
    partial class ChooseDataSourceForm
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
            this.buttonLocalhost = new System.Windows.Forms.Button();
            this.buttonLogs = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonLocalhost
            // 
            this.buttonLocalhost.Location = new System.Drawing.Point(65, 12);
            this.buttonLocalhost.Name = "buttonLocalhost";
            this.buttonLocalhost.Size = new System.Drawing.Size(124, 23);
            this.buttonLocalhost.TabIndex = 0;
            this.buttonLocalhost.Text = "localhost:20090";
            this.buttonLocalhost.UseVisualStyleBackColor = true;
            this.buttonLocalhost.Click += new System.EventHandler(this.buttonLocalhost_Click);
            // 
            // buttonLogs
            // 
            this.buttonLogs.Location = new System.Drawing.Point(65, 41);
            this.buttonLogs.Name = "buttonLogs";
            this.buttonLogs.Size = new System.Drawing.Size(124, 23);
            this.buttonLogs.TabIndex = 1;
            this.buttonLogs.Text = "Отрыть логи";
            this.buttonLogs.UseVisualStyleBackColor = true;
            this.buttonLogs.Click += new System.EventHandler(this.buttonLogs_Click);
            // 
            // ChooseDataSourceForm
            // 
            this.AcceptButton = this.buttonLocalhost;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 78);
            this.Controls.Add(this.buttonLogs);
            this.Controls.Add(this.buttonLocalhost);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseDataSourceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Источник данных";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonLocalhost;
        private System.Windows.Forms.Button buttonLogs;
    }
}