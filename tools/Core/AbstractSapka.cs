using System;
using System.Net.Sockets;
using System.Threading;
using Core.Parsing;
using Core.StateCalculations;

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
			GameState = new GameState();
			parser = new Parser(GameState);
		}

		public GameState GameState { get; private set; }

		public void Run()
		{
			Thread.Sleep(500);
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
			if (GameState.Sapkas == null) return;
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
			//Console.WriteLine(this.GetHashCode() + " " + s);
		}
	}
}