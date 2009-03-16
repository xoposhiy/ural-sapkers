using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using Core;
using Core.Parsing;
using Core.StateCalculations;
using log4net.Config;

namespace Visualizer
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			XmlConfigurator.Configure();
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(CreateMainForm());
		}

		private static Form CreateMainForm()
		{
			var updatersQueue = new ModelUpdatersQueue();
			var parser = new Parser(new VisualizerParserListener(updatersQueue));
			new Thread(() => ListenToServer(parser)) { IsBackground = true }.Start();
			return new Visualizer(updatersQueue) { WindowState = FormWindowState.Maximized };
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
