namespace Parsing
{
	/// <summary>
	/// ������� ������� ������ ����������� ����� ���������. ������ ����� ��������� � ������������ ��������.
	/// </summary>
	public interface IParserListener
	{
		void OnGameStart(GameInfo gameInfo);
		void OnRoundStart(StartRoundInfo startRoundInfo);

		/// <summary>���-�� ���������� �� �����</summary>
		void OnMapChange(MapChangeInfo mapChangeInfo);

		void OnFinishRound(int score);
		void OnFinishGame(PlayerResult[] playerResults);
	}
}