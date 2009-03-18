using System;
using System.Diagnostics;
using log4net;

namespace Core.AI
{
	public class Sapka : AbstractSapka
	{
		public Sapka(string host, int port, string teamName) : base(host, port, teamName)
		{
		}

		private int lastTime;
		public override string GetMove()
		{
			Stopwatch sw = Stopwatch.StartNew();
			try
			{
				Decision decision = new Chief(GameState).MakeDecision();

				int skipped = GameState.Time - lastTime;
				if (skipped > 1)
					log.WarnFormat("Пропуск {0} тиков", skipped);
				lastTime = GameState.Time;
				return decision.GetMove();
			}
			finally
			{
				sw.Stop();
				log.InfoFormat("{0}\t{1} ms", GameState.Time, sw.ElapsedMilliseconds);
			}
		}

		private static readonly ILog log = LogManager.GetLogger("performance");
	}
}