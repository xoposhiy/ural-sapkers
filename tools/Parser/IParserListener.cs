namespace Parsing
{
	/// <summary>
	/// Клиенты парсера должны реализовать такой интерфейс. Парсер будет оповещать о происходящих событиях.
	/// </summary>
	public interface IParserListener
	{
		void OnGameStart(GameInfo gameInfo);
		void OnRoundStart(StartRoundInfo startRoundInfo);

		/// <summary>Что-то поменялось на карте</summary>
		void OnMapChange(MapChangeInfo mapChangeInfo);

		void OnFinishRound(int score);
		void OnFinishGame(PlayerResult[] playerResults);
	}
}