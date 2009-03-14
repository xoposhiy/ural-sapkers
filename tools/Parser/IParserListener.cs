namespace Parsing
{
	/// <summary>
	/// ������� ������� ������ ����������� ����� ���������. ������ ����� ��������� � ������������ ��������.
	/// </summary>
	public interface IParserListener
	{
		void OnGameStart(GameInfo gameInfo);
		void OnRoundStart(StartRoundInfo startRoundInfo);

		/// <summary>���-�� ���������� �� �����.
		/// ����� ���������� �������� ���, � ��������� � ��������� ������� OnAdd � OnRemove </summary>
		void OnMapChange(MapChangeInfo mapChangeInfo);

		/// <summary> �� ����� ��������� �����-�� ���������. </summary>
		void OnAdd(AddInfo addInfo);

		/// <summary> �����-�� ��������� ������� � ����� </summary>
		void OnRemove(RemoveInfo removeInfo);

		void OnFinishRound(int score);
		void OnFinishGame(PlayerResult[] playerResults);
	}
}