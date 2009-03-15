using Core;

namespace TheSapka
{
	public class DummySapka : AbstractSapka
	{
		public DummySapka(string host, int port, string teamName) : base(host, port, teamName)
		{
		}

		public override string GetMove()
		{
			return "s;";
		}
	}
}