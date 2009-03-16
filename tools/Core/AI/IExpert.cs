using Core.PathFinding;
using Core.StateCalculations;

namespace Core.AI
{
	internal interface IExpert
	{
		// ��� ������, ��� ���� �������. 0 � ������� �� ����� ������ ������ :)
		byte EstimateDecisionDanger(GameState state, IPath[,] paths, Decision decision);
	}
}