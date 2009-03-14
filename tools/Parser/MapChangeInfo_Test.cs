using NUnit.Framework;

namespace Parsing
{
	[TestFixture]
	public class MapChangeInfo_Test
	{
		[Test]
		public void TestRead()
		{
			var info = new MapChangeInfo(new Reader("T1&P0 100 102 0 2 2,P1 0 0 1 2 2&+w 10 10;"));
			Assert.AreEqual(1, info.Time);
			Assert.AreEqual(false, info.HasDangerLevel);
			Assert.AreEqual(1, info.Adds.Length);
			Assert.AreEqual(false, info.Adds[0].HasDamageInfo);
			Assert.AreEqual('w', info.Adds[0].SubstanceType);
			Assert.AreEqual(10, info.Adds[0].Pos.X);
			Assert.AreEqual(0, info.Removes.Length);
			Assert.AreEqual(2, info.Sapkas.Length);

			info = new MapChangeInfo(new Reader("T40&P0 dead,P1 0 0 1 2 2&-* 10 10,+# 10 10 2 3;"));
			Assert.AreEqual(40, info.Time);
			Assert.AreEqual(false, info.HasDangerLevel);
			Assert.AreEqual(1, info.Adds.Length);
			Assert.AreEqual(1, info.Removes.Length);
			Assert.AreEqual(2, info.Sapkas.Length);
			Assert.AreEqual(true, info.Sapkas[0].IsDead);
		}

		[Test]
		public void TestReadInfectedSapka()
		{
			var info = new MapChangeInfo(new Reader("T40&P0 0 0 1 2 2 i&;"));
			Assert.AreEqual(40, info.Time);
			Assert.AreEqual(false, info.HasDangerLevel);
			Assert.AreEqual(0, info.Adds.Length);
			Assert.AreEqual(0, info.Removes.Length);
			Assert.AreEqual(1, info.Sapkas.Length);
			Assert.AreEqual(true, info.Sapkas[0].Infected);
			Assert.AreEqual(false, info.Sapkas[0].IsDead);
		}

		[Test]
		public void TestReadWithDanger()
		{
			var info = new MapChangeInfo(new Reader("T40&P0 dead&&d 10;"));
			Assert.AreEqual(40, info.Time);
			Assert.AreEqual(true, info.HasDangerLevel);
			Assert.AreEqual(10, info.DangerLevel);
			Assert.AreEqual(0, info.Adds.Length);
			Assert.AreEqual(0, info.Removes.Length);
			Assert.AreEqual(1, info.Sapkas.Length);
			Assert.AreEqual(true, info.Sapkas[0].IsDead);
			Assert.AreEqual(false, info.Sapkas[0].Infected);
		}
	}
}