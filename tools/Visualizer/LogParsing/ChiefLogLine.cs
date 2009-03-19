using System;
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
            Round = Int32.Parse(match.Groups[1].Captures[0].Value);
            Time = Int32.Parse(match.Groups[2].Captures[0].Value);
            Target = new TargetDescription(match.Groups[3].Captures[0].Value);
            IsChosen = s.Contains("chosen move:");
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

    	public TargetDescription Target { get; private set; }
    	public int Round { get; private set; }
    	public int Time { get; private set; }
    	public bool IsChosen { get; private set; }

    	private static readonly Regex parser = new Regex(@"^\S+\s+\S+\s+(\d+)\s+(\d+)\s+(.*)");
    }
}
