namespace Parsing
{
	/// <summary>
	/// Клиенты парсера должны реализовать такой интерфейс. Парсер будет оповещать о происходящих событиях.
	/// </summary>
	public interface IParserListener
	{
		void OnGameStart(GameInfo gameInfo);
		void OnRoundStart(StartRoundInfo startRoundInfo);

		/// <summary>Что-то поменялось на карте.
		/// Общая информация приходит тут, а детальная в следующих вызовах OnAdd и OnRemove </summary>
		void OnMapChange(MapChangeInfo mapChangeInfo);

		/// <summary> На карте появилась какая-то хренотень. </summary>
		void OnAdd(AddInfo addInfo);

		/// <summary> Какая-то хренотень пропала с карты </summary>
		void OnRemove(RemoveInfo removeInfo);

		void OnFinishRound(int score);
		void OnFinishGame(PlayerResult[] playerResults);
	}
}