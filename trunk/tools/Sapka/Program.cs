using Core;
using Core.AI;

namespace TheSapka
{
	class Program
	{
		private const string teamName = "ural-sapkers";

		static void Main(string[] args)
		{
			log4net.Config.XmlConfigurator.Configure();
			string host = args.Length > 0 ? args[0] : "localhost";
			int port = args.Length > 1 ? int.Parse(args[1]) : 20015;
			string strategy = args.Length > 2 ? args[2] : "";
			System.Console.WriteLine("Launched: Sapka.exe {0} {1} {2}", host, port, strategy);
			GetSapka(host, port, strategy).Run();
		}

		public static AbstractSapka GetSapka(string host, int port, string strategy)
		{
			if(strategy == "")
				return new Sapka(host, port, teamName);
			return new DummySapka(host, port, teamName);
		}
	}
}
