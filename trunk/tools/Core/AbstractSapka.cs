using System;
using System.Net.Sockets;
using Core.Parsing;

namespace Core
{
	public abstract class AbstractSapka
	{
		private readonly Parser parser;
		private readonly SapkaServer sapkaServer;
		private readonly string teamName;

		protected AbstractSapka(string host, int port, string teamName)
		{
			this.teamName = teamName;
			sapkaServer = new SapkaServer(host, port);
			parser = new Parser(new DummyParserListener());
		}

		public void Run()
		{
			sapkaServer.Say("launch " + teamName +
			                " CFGWyiasdaKrEatWPEVUO@k#CFGGmswKaMWPiweckyPCnUV#CFGaqFceUYwnYgkYRSJIjAu#CFGameMdKaERmacuSRGpqfw#CFGWyOwjOLobEuFOQOFiihE#CFGaqggxGbiZuyJcIPcPosl#CFGquhMFGDGoXGbMSCVmgmY#CFGgMRaIDiSXwgiaSmpWaOe#CFGaUMkpoeaTYWyKJsnMGuf#CFGqumEIGiARCWlkuAJyMKY#CFGgaagKGGqjigmoSEHaOaa#CFGqCUqHuQudokTGPocXoIu#CFGGoaGAhqgqbETCQTQgMaB;");
			try
			{
				ListenToServer();
				while (true)
				{
					ListenToServer();
					MakeMove();
				}
			}
			catch (SocketException e)
			{
				Console.WriteLine(e.Message);
				return;
			}
		}

		public abstract string GetMove();

		private void MakeMove()
		{
			sapkaServer.Say(GetMove() + ";");
		}

		private void ListenToServer()
		{
			foreach (string message in sapkaServer.ListenToServer())
				ProcessMessage(message);
		}

		private void ProcessMessage(string s)
		{
			parser.ParseMessage(s);
			Console.WriteLine(s);
		}
	}
}