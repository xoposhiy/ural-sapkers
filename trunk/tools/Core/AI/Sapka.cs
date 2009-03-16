using System;

namespace Core.AI
{
	public class Sapka : AbstractSapka
	{
		private readonly Random r = new Random();

		public Sapka(string host, int port, string teamName) : base(host, port, teamName)
		{
		}

		public override string GetMove()
		{
			Decision decision = new Chief(GameState).MakeDecision();
			//Console.WriteLine("move = {0}, path len = {1}", decision.GetMove(), decision.Path == null ? -1 : decision.Path.Size());
			return decision.GetMove();
		}
	}
}