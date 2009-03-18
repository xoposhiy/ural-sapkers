using System.Collections.Generic;
using Core.PathFinding;
using Core.StateCalculations;

namespace Core.AI
{
	internal class SuicideAdviser : IAdviser
	{
		public IEnumerable<Decision> Advise(GameState state, Paths paths)
		{
			var decision = new Decision(null, state.MyCell, true, 1, -1000);
			decision.Name = "Suicide";
			yield return decision; //Убей себя! Выпей яду! Вгазенваген! Неудачнег! Лох — это судьба...
		}
	}
}