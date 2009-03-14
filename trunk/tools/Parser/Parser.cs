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

		//TODO To be continued...
	}
}