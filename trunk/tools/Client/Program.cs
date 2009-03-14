using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace Client
{
	internal interface IServer : IDisposable
	{
		string Say(string text);
		string Connect();
		string Listen();
		void WaitFor(string substring);
	}

	internal class Program
	{
		private static void Main(string[] args)
		{
			Process start = Process.Start(
				new ProcessStartInfo
					{
						FileName = @"c:\work\sapka\project-beta-1.0\start.bat",
						WorkingDirectory = @"c:\work\sapka\project-beta-1.0\"
					});
			try
			{
				using(IServer server = new Server("localhost", 20015, Log))
				{
					server.Connect();
					server.Say("team ural-sapkers;");
					server.Say(
						"config CFGaqggxGbiZuyJcIPcPosl#CFGgMRaIDiSXwgiaSmpWaOe#CFGquhMFGDGoXGbMSCVmgmY#CFGWyiasdaKrEatWPEVUO@k#CFGGmswKaMWPiweckyPCnUV#CFGaqFceUYwnYgkYRSJIjAu#CFGameMdKaERmacuSRGpqfw#CFGgaagKGGqjigmoSEHaOaa;");
					server.Say("dma DMAWGgMvUryBWgKFECRmFuL#DMAqGgMCIgefAuNwcLMKNir#DMAGuySWOvaimYdOFqfIdEq;");
					DoWork(server);
				}
			}
			finally
			{
				start.Kill();
			}
		}

		private static void DoWork(IServer server)
		{
			server.Say("memconfig;");
			server.Say("password;");
			server.WaitFor("dnalab");
			server.Say("dnalab;");
			server.WaitFor("initiated");
			var words = new[] {"A", "SE", "BA", "GA", "LA", "PR", "TY", "MA"};
			var rnd = new Random();
			int prevLen = -1;
			while (true)
			{
				string word = words[rnd.Next(0, 8)];
				server.Say(word + ";");
				string answer = server.Say("PROCESS;");
				if(answer.Length != prevLen)
				{
					prevLen = answer.Length;
					if(prevLen != -1)
					{
						Console.WriteLine("!!!!!INTERESTING ANSWER!!!!!");
						Console.WriteLine(answer);
						Console.ReadKey();
					}
				}
			}
		}

		private static void Log(string text)
		{
			Console.WriteLine(text);
		}
	}

	internal delegate void LogDelegate(string text);

	internal class Server : IServer
	{
		private readonly string host;
		private readonly LogDelegate log;
		private readonly int port;
		private readonly Socket socket;

		public Server(string host, int port, LogDelegate log)
		{
			this.host = host;
			this.port = port;
			this.log = log;
			socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		}

		#region IServer Members

		public string Connect()
		{
			socket.Connect(host, port);
			return Listen();
		}

		public void Dispose()
		{
			socket.Disconnect(false);
		}

		public string Say(string text)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(text);
			socket.Send(bytes);
			log("ME>" + text);
			return Listen();
		}

		public string Listen()
		{
			var output = new byte[10000];
			int bytesReceived = socket.Receive(output);
			string result = Encoding.ASCII.GetString(output, 0, bytesReceived);
			log(result);
			return result;
		}

		public void WaitFor(string substring)
		{
			while(!Listen().Contains(substring)) ;
		}

		#endregion
	}
}