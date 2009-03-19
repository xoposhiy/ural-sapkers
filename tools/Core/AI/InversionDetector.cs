using System.Collections.Generic;
using Core.Parsing;
using Core.StateCalculations;
using log4net;

namespace Core.AI
{
	public interface IInversionDetector
	{
		void RegisterMove(GameState state, char c);
		bool Inverted { get; }
	}

	public class PossibleInversionDetector
	{
		public void RegisterMove(GameState state)
		{
			if (!state.MySapka.Infected) InversionIsPossible = false;
			else InversionIsPossible = true;
		}

		public bool InversionIsPossible { get; private set; }
	}
	
	class InversionDetector : IInversionDetector
	{
		private Pos lastPos;
		private char lastMove;
		private readonly PossibleInversionDetector possibleInversionDetector = new PossibleInversionDetector();

		public void RegisterMove(GameState state, char c)
		{
			possibleInversionDetector.RegisterMove(state);
			if (lastPos != null)
			{
				int dx = state.MySapka.Pos.X - lastPos.X;
				int dy = state.MySapka.Pos.Y - lastPos.Y;
				if (possibleInversionDetector.InversionIsPossible)
				{
					List<char> possibleRealMoves = GetPossibleRealMoves(state, dx, dy);
					log.Info(state.Time + " Inversion is possible lastMove:" + lastMove + " realMoves:" + new string(possibleRealMoves.ToArray()) + " dx:" + dx + " dy:" + dy);
					if (lastMove != 's')
					{
						if (possibleRealMoves.Contains(Inv(lastMove))) NoInverseDetected();
						else if (possibleRealMoves.Contains(Commons.InvertDir(Inv(lastMove)))) InverseDetected();
						else log.Warn("WTF?");
					}
					Inverted = invertCount > 2;
				}
				else
				{
					Inverted = false;
				}
			}
			lastPos = state.MySapka.Pos;
			lastMove = c;
		}

		private char Inv(char move)
		{
			if (Inverted) return Commons.InvertDir(move);
			return move;
		}

		private ILog log = LogManager.GetLogger(typeof (InversionDetector));

		private static List<char> GetPossibleRealMoves(GameState state, int dx, int dy)
		{
			var realMove = Commons.ToDirChar(dx, dy);
			var possibleRealMoves = new List<char> { realMove };
			if (realMove == 's') possibleRealMoves.AddRange(GetToWallDirections(state));
			return possibleRealMoves;
		}

		private void InverseDetected()
		{
			notinvertCount = 0;
			invertCount++;
			log.Info("Inversion? " + invertCount);
		}

		private int invertCount = 0;
		private int notinvertCount = 0;
		private void NoInverseDetected()
		{
			notinvertCount++;
			invertCount = 0;
			log.Info("Notinversion? " + notinvertCount);
		}

		private static IEnumerable<char> GetToWallDirections(GameState state)
		{
			int x = state.MySapka.Pos.X;
			int y = state.MySapka.Pos.Y;
			int cellSize = state.CellSize;
			if (x % cellSize == 0) yield return 'l';
			if (y % cellSize == 0) yield return 'u';
			if (x % cellSize == cellSize - 1) yield return 'r';
			if (y % cellSize == cellSize - 1) yield return 'd';
		}


		public bool Inverted {get; private set;}
	}
}
