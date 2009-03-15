namespace TheSapka
{
	class Program
	{
		private const string teamName = "ural-sapkers";

		static void Main(string[] args)
		{
			string host = args.Length > 0 ? args[0] : "localhost";
			int port = args.Length > 1 ? int.Parse(args[1]) : 20015;
			string strategy = args.Length > 2 ? args[2] : "";
			if(strategy == "")
			{

				new Sapka(host, port, Program.teamName).Run();
			}
			else
				new DummySapka(host, port, teamName).Run();
		}
	}

	internal class DummySapka : Sapka
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
