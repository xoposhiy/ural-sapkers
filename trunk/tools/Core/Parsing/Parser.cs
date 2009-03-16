using System;
using System.Collections.Generic;

namespace Core.Parsing
{
	public class Parser
	{
		public Parser(params IParserListener[] listeners)
		{
			if (listeners == null) throw new ArgumentNullException("listeners");
			this.listeners = listeners;
		}

		public void ParseMessage(string message)
		{
			var r = new Reader(message);
			if (message.StartsWith("PID"))
			{
				var gameInfo = new GameInfo(r);
				ForAllListeners(listener => listener.OnGameStart(gameInfo));
			}
			else if (message.StartsWith("START"))
			{
				var roundStartInfo = new StartRoundInfo(r);
				ForAllListeners(listener => listener.OnRoundStart(roundStartInfo));
			}
			else if (message.StartsWith("T"))
			{
				var mapChangeInfo = new MapChangeInfo(r);
				ForAllListeners(listener => listener.OnMapChange(mapChangeInfo));
			}
			else if (message.StartsWith("REND"))
			{
				var endRoundInfo = ReadEndRound(r);
				ForAllListeners(listener => listener.OnFinishRound(endRoundInfo));
			}
			else if (message.StartsWith("GEND"))
			{
				var endGameInfo = ReadEndGame(r);
				ForAllListeners(listener => listener.OnFinishGame(endGameInfo));
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

		private void ForAllListeners(Action<IParserListener> action)
		{
			Array.ForEach(listeners, action);
		}

		private readonly IParserListener[] listeners;
	}
}