using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using Core.Parsing;

namespace Visualizer
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			var logger = new FileSapkaServerLogger("game.dump");
			var form = new Visualizer(logger) { WindowState = FormWindowState.Maximized };
			var parser = new Parser(form);
			var thread = new Thread(() => ListenToServer(parser, logger));
			thread.IsBackground = true;
			thread.Start();
			Application.Run(form);
		}

		private static void ListenToServer(Parser parser, ISapkaServerLogger logger)
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
							logger.Log(message);
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
