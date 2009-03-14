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

		// ��-����� ��� ��� � �� ���� �� ������ � ����� ���������...
		//public readonly int DamagingRange;
	}
}