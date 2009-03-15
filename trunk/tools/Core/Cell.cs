namespace Parsing
{
	public static class Cell
	{
		//  bonus-type ::= 'b' | 'v' | 'f' | 'r' | 's' | 'u' | 'o' | '?'
		public const char Danger = '*';
		public const char Destructable = 'w';
		public const char Empty = '.';
		public const char Hell = '#';
		public const char Indestructable = 'X';
	}
}