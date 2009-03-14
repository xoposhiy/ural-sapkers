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

	public class PlayerResult
	{
		public readonly int PlayerNumber;
		public readonly int Rank;
		public readonly int Score;

		public PlayerResult(int playerNumber, int score, int rank)
		{
			PlayerNumber = playerNumber;
			Score = score;
			Rank = rank;
		}
	}


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

	public class AddInfo
	{
		public readonly int BoomTime;
		public readonly int DamagingRange;
		public readonly Pos Pos;
		public readonly char SubstanceType;

		public AddInfo(char substanceType, Pos pos, int damagingRange, int boomTime)
		{
			SubstanceType = substanceType;
			Pos = pos;
			DamagingRange = damagingRange;
			BoomTime = boomTime;
		}
	}

	public class Pos
	{
		public readonly int X;
		public readonly int Y;

		public Pos(int x, int y)
		{
			X = x;
			Y = y;
		}
	}

	public class MapChangeInfo
	{
		public readonly int DangerLevel;
		public readonly Players Players;
		public readonly int Time;

		public MapChangeInfo(int time, Players players, int dangerLevel)
		{
			Time = time;
			Players = players;
			DangerLevel = dangerLevel;
		}
	}

	public class Players
	{
	}

	public class StartRoundInfo
	{
		public readonly MapInfo MapInfo;
		public readonly int RoundNumber;

		public StartRoundInfo(int roundNumber, MapInfo mapInfo)
		{
			RoundNumber = roundNumber;
			MapInfo = mapInfo;
		}
	}

	public class GameInfo
	{
		public readonly MapInfo MapInfo;
		public readonly int PID;

		public GameInfo(int pid, MapInfo mapInfo)
		{
			PID = pid;
			MapInfo = mapInfo;
		}
	}

	public static class Cell
	{
		//  bonus-type ::= 'b' | 'v' | 'f' | 'r' | 's' | 'u' | 'o' | '?'
		public const char Danger = '*';
		public const char Destructable = 'w';
		public const char Empty = '.';
		public const char Hell = '#';
		public const char Indestructable = 'X';
	}

	public class MapInfo
	{
		public readonly char[,] Map;
		public readonly int MapCellSize;

		public MapInfo(int mapCellSize, char[,] map)
		{
			MapCellSize = mapCellSize;
			Map = map;
		}
	}
}