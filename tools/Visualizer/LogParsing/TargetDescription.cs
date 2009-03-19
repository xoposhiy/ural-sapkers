using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;

namespace Visualizer
{
    class TargetDescription
    {
        public TargetDescription(string s)
        {
            // "from (10, 0) (100, 0) b	WallBreaker target: (10,0) cost:20/1 = 20";
            Match match = parseRegex.Match(s);
            if (!match.Success)
                throw new FormatException(s);
            path = match.Groups[1].Captures[0].Value;
            adviser = match.Groups[2].Captures[0].Value;
            int x = Int32.Parse(match.Groups[3].Captures[0].Value);
            int y = Int32.Parse(match.Groups[4].Captures[0].Value);
            cost = Double.Parse(match.Groups[5].Captures[0].Value);
            target = new Point(x, y);
        }

        public override string ToString()
        {
            return String.Format("{1} target: {2}, cost: {3} - {0}", path, adviser, target, cost);
        }

        public readonly string adviser;
        public readonly Point target;
        public readonly string path;
        public readonly double cost;

        private static Regex parseRegex = new Regex(@"from\s+\(.*?\)\s+\(.*?\)\s+(\S+)\s+(\S+) target: \((\d+),(\d+)\).*? = (.*)");
    }
}
