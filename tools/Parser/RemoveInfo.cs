namespace Parsing
{
	public class RemoveInfo
	{
		public readonly Pos Pos;
		public readonly char SubstanceType;

		public RemoveInfo(char substanceType, Pos pos)
		{
			SubstanceType = substanceType;
			Pos = pos;
		}

		// По-моему это баг и ее быть не должно — нужно проверить...
		//public readonly int DamagingRange;
	}
}