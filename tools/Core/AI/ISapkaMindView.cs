using System.Collections.Generic;

namespace Core.AI
{
	public interface ISapkaMindView
	{
		IList<char> LastDecisionPath { get; }
		string LastDecisionName { get; }
		bool IsInverted { get; }
	}
}