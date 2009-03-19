using System.Collections.Generic;

namespace Core.AI
{
	public class LogSapka : ISapkaMindView
	{
		public LogSapka()
		{
			LastDecisionPath = new char[0];
			LastDecisionName = "?";
		}

		#region ISapkaMindView Members

		public IList<char> LastDecisionPath { get; set; }

		public string LastDecisionName { get; set; }

		public bool IsInverted { get; set; }

		#endregion
	}
}