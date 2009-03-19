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
using Visualizer.LogParsing;

namespace Visualizer
{
    public partial class PlayControlsForm : Form
    {
        Visualizer visualizer;
        private List<String> sapkaLog;
        private List<ChiefLogLine> chiefLog = new List<ChiefLogLine>();
        private Parser parser;
        private LogSapka sapka;
        private Thread threadAutoPlay;

        public PlayControlsForm(Visualizer visualizer, Parser parser, LogSapka sapka)
        {
            ParseLogs();
            this.visualizer = visualizer;
        	visualizer.IsRunning = false;
            this.parser = parser;
            this.sapka = sapka;
            InitializeComponent();
   
            trackBar.Minimum = 0;
            trackBar.Maximum = sapkaLog.Count - 1;
            labelMax.Text = trackBar.Maximum.ToString();
            trackBar.Value = 0;

            trackBar.TickFrequency = 10;
        }

        private static void RemoveNotChosenDuplicates(List<ChiefLogLine> chiefLog)
        {
            List<int> toKill = new List<int>();
            for (int i = 0; i<chiefLog.Count; ++i)
                for (int j = i+1; j<chiefLog.Count; ++j)
                    if (ChiefLogLine.AreDuplicates(chiefLog[i], chiefLog[j]))
                    {
                        if (chiefLog[i].IsChosen)
                            toKill.Add(j);
                        else
                            toKill.Add(i);
                    }
            toKill.Sort();
            toKill.Reverse();
            foreach (int i in toKill)
                chiefLog.RemoveAt(i);
        }

        private static FileStream CreateStream(string fileName)
        {
            return new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }

        private void ParseLogs()
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
            while (!chiefReader.EndOfStream)
            {
                string s = chiefReader.ReadLine();
                try { chiefLog.Add(new ChiefLogLine(s)); } catch (Exception) {}
            }
            chiefReader.Close();

            //RemoveNotChosenDuplicates(chiefLog);
        }

        private void RefreshWayListBox()
        {
            int round = visualizer.model.State.RoundNumber;
            int time = visualizer.model.State.Time;
            Text = String.Format("Round {0}, time {1}", round, time);

            List<ChiefLogLine> currentTimeLines = new List<ChiefLogLine>();
            ChiefLogLine chosen = null;
            foreach (ChiefLogLine line in chiefLog)
                if (line.Round == round && line.Time == time)
                {
                    currentTimeLines.Add(line);
                    if (line.IsChosen)
                        chosen = line;
                }
            if (chosen != null)
            {
                sapka.LastDecisionPath = chosen.Target.path.ToCharArray();
                sapka.LastDecisionName = chosen.Target.adviser;
                // Удалим не-Chosen строчку
                for (int i = 0; i < currentTimeLines.Count; ++i)
                    if (chosen.Target.ToString() == currentTimeLines[i].Target.ToString() && !currentTimeLines[i].IsChosen)
                    {
                        currentTimeLines.RemoveAt(i);
                        break;
                    }
            }
            listBoxTargets.Items.Clear();
            foreach (ChiefLogLine line in currentTimeLines)
                listBoxTargets.Items.Add(line);
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
                visualizer.UpdateModel();

                RefreshWayListBox();
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
				visualizer.IsRunning = true;
				threadAutoPlay = new Thread(() => AutoPlay(this)) { IsBackground = true };
                threadAutoPlay.Start();
                buttonPlay.Text = "Pause ||";
            }
            else
            {
				visualizer.IsRunning = false;
				threadAutoPlay.Abort();
                threadAutoPlay = null;
                buttonPlay.Text = "Auto >>";
            }
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            if (trackBar.Value < oldTrackbarValue)
            {
                for (int i = 0; i < trackBar.Value; ++i)
                    parser.ParseMessage(sapkaLog[i]);
            }
            else if (trackBar.Value > oldTrackbarValue)
            {
                for (int i = oldTrackbarValue; i < trackBar.Value; ++i )
                    parser.ParseMessage(sapkaLog[i]);
            }
            RefreshWayListBox();
            visualizer.UpdateModel();

            textBoxMessage.Text = sapkaLog[trackBar.Value];
            oldTrackbarValue = trackBar.Value;
        }

        private static void AutoPlay(PlayControlsForm form)
        {
            while (true)
            {
                Thread.Sleep(50);
                form.NextStep();
            }
        }

        private int oldTrackbarValue = 0;
        private static Regex sapkaTickRegex = new Regex(@"^T\d+&");

        private void listBoxTargets_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChiefLogLine line = (ChiefLogLine) listBoxTargets.SelectedItem;
            sapka.LastDecisionPath = line.Target.path.ToCharArray();
            sapka.LastDecisionName = line.Target.adviser ;
        }

        private void PlayControlsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (threadAutoPlay!=null)
                threadAutoPlay.Abort();
        }
    }
}
