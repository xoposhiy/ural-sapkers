using System;
using Core;
using Core.AI;

namespace TheSapka
{
	internal class Sapka : AbstractSapka
	{
		private readonly Random r = new Random();
		
		public Sapka(string host, int port, string teamName) : base(host, port, teamName)
		{
		}

		public override string GetMove()
		{
			Decision decision = new Chief(GameState).MakeDecision();
			string res = (decision.Path == null ? 's' : decision.Path.FirstMove()).ToString();
			if(decision.PutBomb) res += "b";
			return res;
		}
	}

}