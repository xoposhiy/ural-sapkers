using Core.PathFinding;
using Core.Parsing;
using System;

namespace Core.AI
{
	public class Decision
	{
		public bool Inverse = false;

		public Decision(IPath path, Pos target, Pos targetPt, bool putBomb, int duration, double potentialScore, string name)
			: this(path, target, targetPt, putBomb, duration, potentialScore, name, false)
		{
		}

		public Decision(IPath path, Pos target, Pos targetPt, bool putBomb, int duration, double potentialScore, string name, bool willBomb)
		{
			Path = path;
			PutBomb = putBomb;
			Duration = duration;
			PotentialScore = potentialScore;
			Target = target;
			this.TargetPt = targetPt;
			Name = name;
			WillBomb = willBomb;
		}

		public bool WillBomb;

		private char norm(char c)
		{
			const string s1 = "lrud";
			const string s2 = "rldu";
			return !Inverse || c == 's' ? c : s2[s1.IndexOf(c)];
		}
		
		static Random rnd = new Random();
		
		public string GetMove()
		{
			if (Name == "NOTHING")
			{
				string r = "" + "lrud"[rnd.Next(4)];
				if (rnd.Next(2) == 0)
				{
					r += "b";
				}
				return r;
			}
			string res = (Path == null ? 's' : norm(Path.FirstMove())).ToString();
			if(PutBomb) res += "b";
			return res;
		}

		public override string ToString()
		{
			var s = PathString() + (PutBomb ? "b" : "") + "\t" + Name;
			if(Target != null)
				s += " target: (" + Target.X + "," + Target.Y + ")";
			return s;
		}

		public readonly string Name;
		public readonly IPath Path;
		public readonly bool PutBomb;
		public readonly Pos Target;
		public readonly Pos TargetPt;
		public int Duration;
		public double PotentialScore;
		public static Decision DoNothing = new Decision(null, null, null, false, 1, 0, "NOTHING");

		public string PathString()
		{
			if(Path == null) return "";
			return new string(Path.FullPath().ToArray());
		}
	}
}