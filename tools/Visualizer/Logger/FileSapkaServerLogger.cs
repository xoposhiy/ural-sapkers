using System.IO;

namespace Visualizer
{
	internal class FileSapkaServerLogger : ISapkaServerLogger
	{
		private readonly StreamWriter streamWriter_;

		public FileSapkaServerLogger(string filename)
		{
			streamWriter_ = new StreamWriter(filename);
		}

		#region Implementation of ISapkaServerLogger

		public void Log(string message)
		{
			streamWriter_.WriteLine(message);
		}

		public void Flush()
		{
			streamWriter_.Flush();
		}

		#endregion
	}
}