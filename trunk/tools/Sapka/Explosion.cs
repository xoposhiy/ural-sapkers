using System;
using Parsing;

namespace Sapka
{
	public class Explosion
	{
		public Pos Pos;
		public int Range, Until;
		
		public Explosion(Pos Pos, int Range, int Until)
		{
			this.Pos = Pos;
			this.Range = Range;
			this.Until = Until;
		}
	}
}
