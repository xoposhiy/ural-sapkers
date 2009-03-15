using System;
using System.Collections.Generic;

namespace Core.Parsing
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
			var r = new Reader(message);
			if (message.StartsWith("PID"))
				listener.OnGameStart(new GameInfo(r));
			else if (message.StartsWith("START"))
				listener.OnRoundStart(new StartRoundInfo(r));
			else if (message.StartsWith("T"))
				listener.OnMapChange(new MapChangeInfo(r));
			else if (message.StartsWith("REND"))
			{
				listener.OnFinishRound(ReadEndRound(r));
			}
			else if (message.StartsWith("GEND"))
			{
				listener.OnFinishGame(ReadEndGame(r));
			}
		}

		private PlayerResult[] ReadEndGame(Reader r)
		{
			r.Ensure("GEND ");
			var res = new List<PlayerResult>();
			res.Add(new PlayerResult(r));
			if (r.Lookup() == ',')
			{
				r.Ensure(",");
				res.Add(new PlayerResult(r));
			}
			return res.ToArray();
		}


		private int ReadEndRound(Reader r)
		{
			r.Ensure("REND ");
			return r.ReadNumber();
		}
	}
}