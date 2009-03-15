using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Core.Parsing
{
	public delegate void ProcessMessageDelegate(string message);

	public class SapkaServer
	{
		private readonly Socket socket;
		private string lastChunk = "";
		
		public SapkaServer(string host, int port)
		{
			socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.ReceiveBufferSize = 10000000;
			socket.Blocking = true;
			socket.Connect(host, port);
			var output = new byte[10000];
			int bytesReceived = socket.Receive(output);
			string prefix = Encoding.ASCII.GetString(output, 0, bytesReceived);
			if (!prefix.Contains("Loading..."))
			{
				lastChunk = prefix;
				return;
			}
			while(!prefix.Contains("config>"))
			{
				bytesReceived = socket.Receive(output);
				prefix = prefix + Encoding.ASCII.GetString(output, 0, bytesReceived);
			}
			var startIndex = prefix.IndexOf("config>") + 7;
			lastChunk = prefix.Substring(startIndex, prefix.Length - startIndex);
		}

		//Возвращает все, что уже пришло в сокет на данный момент времени.
		public IEnumerable<string> ListenToServer()
		{
			var output = new byte[10000];
			string result = lastChunk;
			if(lastChunk.IndexOf(";") == -1)
			{
				int bytesReceived = socket.Receive(output);
				if (bytesReceived == 0) yield break;
				result = result + Encoding.ASCII.GetString(output, 0, bytesReceived);
			}
			
			string[] messages = result.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
			int len = messages.Length;
			if(!result.EndsWith(";"))
			{
				lastChunk = messages[messages.Length - 1];
				len--;
			}
			else
			{
				lastChunk = "";
			}
			for (int i = 0; i < len; i++)
				yield return messages[i] + ";";
		}

		public void Say(string text)
		{
			socket.Send(Encoding.ASCII.GetBytes(text));
		}
	}
}
