using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Parsing
{
	[TestFixture]
	public class GameInfo_Test
	{
		[Test]public void TestRead()
		{
			var info = new GameInfo(new Reader("PID0&10\r\n..\r\n;"));
			Assert.AreEqual(0, info.PID);
			Assert.AreEqual(10, info.MapInfo.MapCellSize);
		}
	}
}
