namespace Visualizer.UserControlledSapka
{
	internal interface IZombieMaster
	{
		char Command { get; }

		/// <summary>����� ������ IsBombing ������������ �� ��� ���, ���� ������ ����� �� ����� ��������� �����</summary>
		bool IsBombing();
	}
}