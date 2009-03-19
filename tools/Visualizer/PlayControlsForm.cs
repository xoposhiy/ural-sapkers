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
using Core.AI;
using System.IO;

namespace Visualizer
{
    public partial class PlayControlsForm : Form
    {
        Visualizer visualizer;
        private List<String> sapkaLog;
        private List<String> chiefLog;
        private List<ChiefStateDescription> chiefStates = new List<ChiefStateDescription>();
        private Parser parser;
        private LogSapka sapka;
        private Thread threadAutoPlay;

        public PlayControlsForm(Visualizer visualizer, Parser parser, LogSapka sapka)
        {
            ReadLogs();
            this.visualizer = visualizer;
            this.parser = parser;
            this.sapka = sapka;
            InitializeComponent();
   
            trackBar.Minimum = 0;
            trackBar.Maximum = sapkaLog.Count - 1;
            labelMax.Text = trackBar.Maximum.ToString();
            trackBar.Value = 0;

            trackBar.TickFrequency = 10;
        }

        private static FileStream CreateStream(string fileName)
        {
            return new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }

        private void ReadLogs()
        {
            StreamReader sapkaReader = new StreamReader(CreateStream("sapka.log"));
            sapkaLog = new List<string>();
            while (!sapkaReader.EndOfStream)
            {
                StringBuilder sb = new StringBuilder();
                while (!sapkaReader.EndOfStream && !sb.ToString().Contains(";"))
                {
                    string line = sapkaReader.ReadLine();
                    sb.Append(line + Environment.NewLine);
                }
                sapkaLog.Add(sb.ToString());
            }
            sapkaReader.Close();

            StreamReader chiefReader = new StreamReader(CreateStream("chief.log"));
            chiefLog = new List<string>();
            while (!chiefReader.EndOfStream)
                chiefLog.Add(chiefReader.ReadLine());
            chiefReader.Close();
        }

        private void NextStep()
        {
			if(trackBar.InvokeRequired)
			{
				trackBar.Invoke(new ThreadStart(NextStep));
				return;
			}
			
			if (trackBar.Value < trackBar.Maximum)
            {
                string message = sapkaLog[trackBar.Value++];
                parser.ParseMessage(message);
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

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            textBoxMessage.Text = sapkaLog[trackBar.Value];
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
    }
}
