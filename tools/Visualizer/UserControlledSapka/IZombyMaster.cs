namespace Visualizer.UserControlledSapka
{
	internal interface IZombyMaster
	{
		char Command { get; }

		/// <summary>����� ������ IsBombing ������������ �� ��� ���, ���� ������ ����� �� ����� ��������� �����</summary>
		bool IsBombing();
	}
}