namespace Visualizer
{
	public interface ISapkaServerLogger
	{
		void Log(string message);
		
		//Потенциально тормозной метод, поэтому он не должен вызываться синхронно в слушателе сервера
		void Flush();
	}
}
