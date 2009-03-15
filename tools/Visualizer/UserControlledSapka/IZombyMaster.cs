namespace Visualizer.UserControlledSapka
{
	internal interface IZombyMaster
	{
		char Command { get; }

		/// <summary>После вызова IsBombing аннулируется до тех пор, пока мастер снова не решит поставить бомбу</summary>
		bool IsBombing();
	}
}