using System;
using Core;

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
			const string s = "urdlb";
			return (s[r.Next(5)]) + ";";
		}
	}
}