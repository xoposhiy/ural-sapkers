using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Parsing;
using System.Text.RegularExpressions;
using System.Threading;

namespace Visualizer
{
    public partial class PlayControlsForm : Form
    {
        private List<String> sapkaLog;
        private Parser parser;
        private Thread threadAutoPlay;

        public PlayControlsForm(List<String> sapkaLog, Parser parser)
        {
            this.sapkaLog = sapkaLog;
            this.parser = parser;
            InitializeComponent();
   
            trackBar.Minimum = 0;
            trackBar.Maximum = sapkaLog.Count - 1;
            labelMax.Text = trackBar.Maximum.ToString();
            trackBar.Value = 0;

            trackBar.TickFrequency = 10;
        }

        private void NextStep()
        {
            if (trackBar.Value < trackBar.Maximum)
            {
                string message = sapkaLog[trackBar.Value++];
                parser.ParseMessage(message);
                textBoxMessage.Text = message;
            }
        }

        private void buttonStep_Click(object sender, EventArgs e)
        {
            NextStep();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (threadAutoPlay == null)
            {
                threadAutoPlay = new Thread(() => AutoPlay(this));
                threadAutoPlay.Start();
                buttonPlay.Text = "Pause ||";
            }
            else
            {
                threadAutoPlay.Abort();
                threadAutoPlay = null;
                buttonPlay.Text = "Auto >>";
            }
        }

        private static void AutoPlay(PlayControlsForm form)
        {
            while (true)
            {
                Thread.Sleep(50);
                form.NextStep();
            }
        }

        private static Regex sapkaTickRegex = new Regex(@"^T\d+&");

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            string message = sapkaLog[trackBar.Value];
            textBoxMessage.Text = "Спозиционировались на:\r\n" + message;
        }
    }
}
