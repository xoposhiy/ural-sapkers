using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Visualizer
{
    class TargetDescription
    {
        public static TargetDescription FromString(string s)
        {
            // "from (10, 0) (100, 0) b	WallBreaker target: (10,0) cost:20/1 = 20";
            Match match = parseRegex.Match(s);
        }

        public string adviser;
        public string target;
        public string path;

        private static Regex parseRegex = new Regex(@"from \S+\s+\S+\s+(\S+)\s+(\S+) target: (\S+)");
    }
}
