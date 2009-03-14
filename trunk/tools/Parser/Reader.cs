using System;

namespace Parsing
{
	public class Reader
	{
		private readonly string message;
		private int pos = 0;

		public Reader(string message)
		{
			this.message = message;
		}

		public void Ensure(string expectedText)
		{
			if(pos + expectedText.Length-1 >= message.Length)
				throw new Exception(string.Format("Unexpected end of message {0}. Expected {1}", message, expectedText));
			for(int i = 0; i < expectedText.Length; i++)
			{
				if(expectedText[i] != message[pos + i])
					throw new Exception(string.Format("Expected {0} at pos {1} in message {2}", expectedText, pos, message ));
			}
			pos += expectedText.Length;
		}

		public char ReadChar()
		{
			return message[pos++];
		}

		public string ReadLine()
		{
			int i = 0;
			while(pos+i < message.Length && message[pos + i] != '\r') i++;
			string line = message.Substring(pos, i);
			pos += i + 2;
			return line;
		}

		public int ReadNumber()
		{
			int i = 0;
			while (pos + i < message.Length && "0123456789".IndexOf(message[pos + i]) >= 0)
				i++;
			int num = int.Parse(message.Substring(pos, i));
			pos += i;
			return num;

		}
	}
}