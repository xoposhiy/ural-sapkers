using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using Core;
using Core.Parsing;
using Core.StateCalculations;
using log4net.Config;
using System.IO;
using System.Text;
using Core.AI;

namespace Visualizer
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Control.CheckForIllegalCrossThreadCalls = true;

            ChooseDataSourceForm startupDialog = new ChooseDataSourceForm();
            startupDialog.ShowDialog();
            if (startupDialog.Cancel)
                Environment.Exit(1);
            DataSource dataSource = startupDialog.DataSource;
			if (dataSource == DataSource.Localhost)
			{
				var serverDir = ConfigurationManager.AppSettings["serverDir"];
				if (serverDir == null)
				{
					MessageBox.Show(@"Путь к папке с сервером должен быть указан в файлике settings.xml в папке, из которой запускается приложение.

Примерно так:
<?xml version=""1.0"" encoding=""utf-8"" ?>
<appSettings>
  <add key=""serverDir"" value=""D:\Code\Sapka\project-beta\project-beta-1.5\"" />
</appSettings>

В репозиторий этот файл не кладите!");
					Environment.Exit(0);
				}
				var serverProcess = Process.Start(new ProcessStartInfo
					{
						FileName = @"java.exe",
						Arguments = @"-cp lib/project-beta.jar;lib/log4j-1.2.15.jar loader.Loader server.properties",
						WorkingDirectory = serverDir
					});
				Application.ApplicationExit += (sender,e) =>
					{
						if (serverProcess != null)
						try
						{
							serverProcess.Kill();
						}
						catch
						{
						}
					};
			}
			Application.Run(CreateMainForm(dataSource));
		}

        private static Form CreateMainForm(DataSource dataSource)
		{
			var updatersQueue = new ModelUpdatersQueue();
			var parser = new Parser(new VisualizerParserListener(updatersQueue));

            Visualizer visualizer = new Visualizer(updatersQueue) { WindowState = FormWindowState.Maximized };

            if (dataSource == DataSource.Localhost)
            {
                new Thread(() => ListenToServer(parser)) { IsBackground = true }.Start();
                XmlConfigurator.Configure();
            }
            else if (dataSource == DataSource.Logs)
            {
                LogSapka sapka = new LogSapka();
                visualizer.StartSapkaMindView(sapka);
                new Thread(() => ReadLogs(parser,sapka)) { IsBackground = true }.Start();
            }
            else throw new IndexOutOfRangeException("Unknown dataSource");

            return visualizer;
		}

        private static void ReadLogs(Parser parser, LogSapka sapka)
        {
            try
            {
                (new PlayControlsForm(parser, sapka)).ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

		private static void ListenToServer(Parser parser)
		{
			while (true)
			{
				try
				{
					var sapkaServer = new SapkaServer("localhost", 20090);
					while(true)
					{
						foreach (var message in sapkaServer.ListenToServer())
						{
							parser.ParseMessage(message);
						}
					}
				}
				catch (SocketException)
				{
					Thread.Sleep(500);
				}
			}
		}
	}
}
