using System;
using System.Net.Sockets;
using System.Text;

namespace TheSapka
{
	internal class Sapka
	{
		private readonly Socket socket;
		private readonly string teamName;
		private string lastChunk = "";

		public Sapka(string host, int port, string teamName)
		{
			this.teamName = teamName;
			socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.Blocking = true;
			socket.Connect(host, port);
		}

		public void Say(string text)
		{
			Console.WriteLine("me>" + text);
			byte[] bytes = Encoding.ASCII.GetBytes(text);
			socket.Send(bytes);
		}

		public void Run()
		{
			Say("launch " + teamName +
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
		private Random r = new Random();

		private void MakeMove()
		{
			const string s = "urdlb";
			Say((s[r.Next(5)]) + ";");
		}

		private void ListenToServer()
		{
			var output = new byte[10000];
			//if (socket.Available <= 0) return;
			int bytesReceived = socket.Receive(output);
			if (bytesReceived == 0) return;
			string result = lastChunk + Encoding.ASCII.GetString(output, 0, bytesReceived);
			string[] messages = result.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries);
			if (!result.EndsWith(";"))
			{
				lastChunk = messages[messages.Length - 1];
				string[] temp = messages;
				messages = new string[messages.Length - 1];
				Array.Copy(temp, messages, messages.Length);
			}
			else
			{
				lastChunk = "";
			}
			foreach (string message in messages)
				ProcessMessage(message + ";");
		}

		private void ProcessMessage(string s)
		{
			//TODO Parser will come here soon...
			Console.WriteLine(s);
		}
	}
}