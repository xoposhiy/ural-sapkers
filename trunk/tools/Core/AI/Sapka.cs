using System.Collections.Generic;
using System.Diagnostics;
using log4net;

namespace Core.AI
{
	public class Sapka : AbstractSapka, ISapkaMindView
	{
		private static readonly ILog log = LogManager.GetLogger("performance");
		private Decision lastDecision;
		private int lastTime;

		public Sapka(string host, int port, string teamName) : base(host, port, teamName)
		{
		}

		#region ISapkaMindView Members

		public IList<char> LastDecisionPath
		{
			get
			{
				if (lastDecision == null || lastDecision.Path == null) return new char[0];
				return lastDecision.Path.FullPath();
			}
		}

		public string LastDecisionName
		{
			get
			{
				if (lastDecision == null) return "NULL";
				return lastDecision.Name;
			}
		}

		public bool IsInverted
		{
			get { return lastDecision == null ? false : lastDecision.Inverse; }
		}

		#endregion

		private readonly IInversionDetector inversionDetector = new InversionDetector();

		public override string GetMove()
		{
			if(GameState.Time == 0) return "s";
			Stopwatch sw = Stopwatch.StartNew();
			try
			{
				Decision decision = new Chief(GameState, inversionDetector).MakeDecision();

				int skipped = GameState.Time - lastTime;
				if (skipped > 1)
					log.WarnFormat("Пропуск {0} тиков", skipped);
				lastTime = GameState.Time;
				lastDecision = decision;
				return lastDecision.GetMove();
			}
			finally
			{
				sw.Stop();
				log.InfoFormat("{0}\t{1} ms", GameState.Time, sw.ElapsedMilliseconds);
			}
		}
	}
}