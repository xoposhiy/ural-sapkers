using Core.PathFinding;

namespace Core.AI
{
	public class Decision
	{
		public Decision(IPath path, bool putBomb, int duration, int potentialScore)
		{
			Path = path;
			PutBomb = putBomb;
			Duration = duration;
			PotentialScore = potentialScore;
		}

		public readonly IPath Path;
		public readonly bool PutBomb;
		public int Duration;
		public int PotentialScore;
		public static Decision DoNothing = new Decision(null, false, 1, 0);
	}
}