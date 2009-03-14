namespace TheSapka
{
	class Program
	{
		static void Main(string[] args)
		{
			string host = args.Length > 0 ? args[0] : "localhost";
			int port = args.Length > 1 ? int.Parse(args[1]) : 20015;
			string teamName = args.Length > 2 ? args[2] : "ural-sapkers";
			new Sapka(host, port, teamName).Run();
		}
	}
}
