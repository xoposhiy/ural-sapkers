using NUnit.Framework;

namespace Core.Parsing
{
	[TestFixture]
	public class Reader_Test
	{
		[Test]
		public void TestReadLine()
		{
			Assert.AreEqual("a", new Reader("a").ReadLine());
			Assert.AreEqual("a", new Reader("a\r\n").ReadLine());
			var reader = new Reader("aa\r\nbb\r\ncc");
			Assert.AreEqual("aa", reader.ReadLine());
			Assert.AreEqual("bb", reader.ReadLine());
			Assert.AreEqual("cc", reader.ReadLine());
			reader = new Reader("aa\r\n\r\nbb");
			Assert.AreEqual("aa", reader.ReadLine());
			Assert.AreEqual("", reader.ReadLine());
			Assert.AreEqual("bb", reader.ReadLine());
		}

		[Test]
		public void TestReadNumber()
		{
			Assert.AreEqual(0, new Reader("0").ReadNumber());
			Assert.AreEqual(123, new Reader("123").ReadNumber());
			var reader = new Reader("123&456");
			Assert.AreEqual(123, reader.ReadNumber());
			reader.Ensure("&");
			Assert.AreEqual(456, reader.ReadNumber());
		}
	}
}

