using Core.PathFinding;
using Core.Parsing;

namespace Core.AI
{
	public class Decision
	{
		public bool Inverse = false;
		
		public Decision(IPath path, Pos target, bool putBomb, int duration, int potentialScore, string name)
		{
			Path = path;
			PutBomb = putBomb;
			Duration = duration;
			PotentialScore = potentialScore;
			Target = target;
			Name = name;
		}
		
		private char norm(char c)
		{
			const string s1 = "lrud";
			const string s2 = "rldu";
			return !Inverse || c == 's' ? c : s2[s1.IndexOf(c)];
		}
		
		public string GetMove()
		{
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
		public Pos Target;
		public int Duration;
		public int PotentialScore;
		public static Decision DoNothing = new Decision(null, null, false, 1, 0, "NOTHING");

		public string PathString()
		{
			if(Path == null) return "";
			return new string(Path.FullPath().ToArray());
		}
	}
}