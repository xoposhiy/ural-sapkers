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
			socket.Blocking = true;
			socket.Connect(host, port);
		}

		//Возвращает все, что уже пришло в сокет на данный момент времени.
		public IEnumerable<string> ListenToServer()
		{
			var output = new byte[10000];
			//if (socket.Available <= 0) return;
			int bytesReceived = socket.Receive(output);
			if(bytesReceived == 0) yield break;
			string result = lastChunk + Encoding.ASCII.GetString(output, 0, bytesReceived);
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
