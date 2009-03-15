using System;
using Parsing;

namespace Sapka
{
	public class Bomb
	{
		public Pos Pos;
		public int Range, Until;
		
		public Bomb(Pos Pos, int Range, int Until)
		{
			this.Pos = Pos;
			this.Range = Range;
			this.Until = Until;
		}
	}
}
