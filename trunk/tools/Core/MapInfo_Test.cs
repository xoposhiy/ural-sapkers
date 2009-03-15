using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Parsing
{
	[TestFixture]
	public class MapInfo_Test
	{
		[Test]
		public void TestRead()
		{
			var info = new MapInfo(new Reader("15\r\n...\r\n...\r\n;"));
			Assert.AreEqual(15, info.MapCellSize);
			Assert.AreEqual(3, info.Map.GetLength(0));
			Assert.AreEqual(2, info.Map.GetLength(1));
		}
	}
}
