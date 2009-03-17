using System;
using System.Collections.Generic;
using Core;
using Core.Parsing;
using Core.PathFinding;
using Core.StateCalculations;

namespace Core.AI
{
	internal interface IAdviser
	{
		IEnumerable<Decision> Advise(GameState state, IPath[,] paths);
	}
}