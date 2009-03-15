namespace Core.Parsing
{
	public class PlayerResult
	{
		public readonly int PlayerNumber;
		public readonly int Rank;
		public readonly int Score;

		public PlayerResult(int playerNumber, int score, int rank)
		{
			PlayerNumber = playerNumber;
			Score = score;
			Rank = rank;
		}

		public PlayerResult(Reader r)
		{
			r.Ensure("P");
			PlayerNumber = r.ReadNumber();
			r.Ensure(" ");
			Score = r.ReadNumber();
			r.Ensure(" ");
			Rank = r.ReadNumber();
		}
	}
}