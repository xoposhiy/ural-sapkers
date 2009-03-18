using System;

namespace Core.AI
{
	public class Sapka : AbstractSapka
	{
		public Sapka(string host, int port, string teamName) : base(host, port, teamName)
		{
		}

		public override string GetMove()
		{
			Decision decision = new Chief(GameState).MakeDecision();
			return decision.GetMove();
		}
	}
}