using System;

namespace Parsing
{
	public class Parser
	{
		private readonly IParserListener listener;

		public Parser(IParserListener listener)
		{
			if (listener == null) throw new ArgumentNullException("listener");
			this.listener = listener;
		}

		public void ParseMessage(string message)
		{
			Reader r = new Reader(message);
			if(message.StartsWith("PID")) 
				listener.OnGameStart(new GameInfo(r));
			else if (message.StartsWith("START"))
				listener.OnRoundStart(new StartRoundInfo(r));
			else if (message.StartsWith("T"))
				listener.OnMapChange(new MapChangeInfo(r));
			else if(message.StartsWith("REND"))
			{
				listener.OnFinishRound(ReadEndRound(r));
			}
			else if(message.StartsWith("GEND"))
			{
				listener.OnFinishGame(ReadEndGame(r));
			}
			
		}

		private PlayerResult[] ReadEndGame(Reader r)
		{
			throw new NotImplementedException();
		}


		private int ReadEndRound(Reader r)
		{
			throw new NotImplementedException();
		}
	}
}