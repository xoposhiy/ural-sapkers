using Core.PathFinding;
using Core.Parsing;

namespace Core.AI
{
	public class Decision
	{
		public Decision(IPath path, Pos target, bool putBomb, int duration, int potentialScore)
		{
			Path = path;
			PutBomb = putBomb;
			Duration = duration;
			PotentialScore = potentialScore;
			Target = target;
		}

		public readonly IPath Path;
		public readonly bool PutBomb;
		public Pos Target;
		public int Duration;
		public int PotentialScore;
		public static Decision DoNothing = new Decision(null, null, false, 1, 0);
	}
}