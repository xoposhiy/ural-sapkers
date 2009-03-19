using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Visualizer.LogParsing
{
    class ChiefLogLine
    {
        public ChiefLogLine(string s)
        {
            // 19:00:20.438	DEBUG	1 1 from (10, 10) (100, 100) l	RunAway target: (10,10) cost:-1/1 = -1
            Match match = parser.Match(s);
            if (!match.Success)
                throw new FormatException(s);
            round = Int32.Parse(match.Groups[1].Captures[0].Value);
            time = Int32.Parse(match.Groups[2].Captures[0].Value);
            target = new TargetDescription(match.Groups[3].Captures[0].Value);
            isChosen = s.Contains("chosen move:");
        }

        public static bool IsGoodLine(string s)
        {
            return parser.IsMatch(s);
        }

        public static bool AreDuplicates(ChiefLogLine a, ChiefLogLine b)
        {
            return a.Round == b.Round && a.Time == b.Time && a.Target.ToString() == b.Target.ToString();
        }

        public override string ToString()
        {
            if (IsChosen)
                return "->" + Target;
            return "  " + Target;
        }

        public TargetDescription Target { get { return target; } }
        public int Round { get { return round; } }
        public int Time { get { return time; } }
        public bool IsChosen { get { return isChosen; } }

        private TargetDescription target;
        private int round;
        private int time;
        private bool isChosen;

        private static string s;
        private static Regex parser = new Regex(@"^\S+\s+\S+\s+(\d+)\s+(\d+)\s+(.*)");
    }
}
