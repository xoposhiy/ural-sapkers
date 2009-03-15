using NUnit.Framework;

namespace Core.Parsing
{
	[TestFixture]
	public class Parser_Test
	{
		public string TestInput =
			@"PID0&10
..wwwwwww..
.XwXwXwXwX.
wwwwwwwwwww
wXwXwXwXwXw
wwwwwwwwwww
wXwXwXwXwXw
wwwwwwwwwww
wXwXwXwXwXw
wwwwwwwwwww
.XwXwXwXwX.
..wwwwwww..
;START1&10
..wwwwwww..
.XwXwXwXwX.
wwwwwwwwwww
wXwXwXwXwXw
wwwwwwwwwww
wXwXwXwXwXw
wwwwwwwwwww
wXwXwXwXwXw
wwwwwwwwwww
.XwXwXwXwX.
..wwwwwww..
;T0&P0 100 102 0 2 2,P1 0 0 1 2 2&+* 10 10 2 40;T1&P0 102 102 0 2 2,P1 0 0 1 2 2&;T2&P0 102 100 0 2 2,P1 0 0 1 2 2&;T3&P0 102 98 0 2 2,P1 0 0 1 2 2&;T4&P0 100 98 0 2 2,P1 0 0 1 2 2&;T5&P0 100 99 0 2 2,P1 0 0 1 2 2&;T6&P0 100 99 0 2 2,P1 0 0 1 2 2&;T7&P0 100 99 0 2 2,P1 0 0 1 2 2&;T8&P0 100 99 0 2 2,P1 0 0 1 2 2&;T9&P0 102 99 0 2 2,P1 0 0 1 2 2&;T10&P0 104 99 0 2 2,P1 0 0 1 2 2&;T11&P0 104 97 0 2 2,P1 0 0 1 2 2&;T12&P0 104 99 0 2 2,P1 0 0 1 2 2&;T13&P0 102 99 0 2 2,P1 0 0 1 2 2&;T14&P0 100 99 0 2 2,P1 0 0 1 2 2&;T15&P0 102 99 0 2 2,P1 0 0 1 2 2&;T16&P0 104 99 0 2 2,P1 0 0 1 2 2&;T17&P0 102 99 0 2 2,P1 0 0 1 2 2&;T18&P0 102 97 0 2 2,P1 0 0 1 2 2&;T19&P0 102 95 0 2 2,P1 0 0 1 2 2&;T20&P0 102 93 0 2 2,P1 0 0 1 2 2&;T21&P0 100 93 0 2 2,P1 0 0 1 2 2&;T22&P0 102 93 0 2 2,P1 0 0 1 2 2&;T23&P0 104 93 0 2 2,P1 0 0 1 2 2&;T24&P0 106 93 0 2 2,P1 0 0 1 2 2&;T25&P0 106 95 0 2 2,P1 0 0 1 2 2&;T26&P0 108 95 0 2 2,P1 0 0 1 2 2&;T27&P0 108 97 0 2 2,P1 0 0 1 2 2&;T28&P0 109 97 0 2 2,P1 0 0 1 2 2&;T29&P0 109 97 0 2 2,P1 0 0 1 2 2&;T30&P0 109 97 0 2 2,P1 0 0 1 2 2&;T31&P0 109 95 0 2 2,P1 0 0 1 2 2&;T32&P0 107 95 0 2 2,P1 0 0 1 2 2&;T33&P0 105 95 0 2 2,P1 0 0 1 2 2&;T34&P0 105 93 0 2 2,P1 0 0 1 2 2&;T35&P0 105 95 0 2 2,P1 0 0 1 2 2&;T36&P0 105 93 0 2 2,P1 0 0 1 2 2&;T37&P0 105 95 0 2 2,P1 0 0 1 2 2&;T38&P0 103 95 0 2 2,P1 0 0 1 2 2&;T39&P0 103 97 0 2 2,P1 0 0 1 2 2&;T40&P0 dead,P1 0 0 1 2 2&-* 10 10,+# 10 10 2 3;REND -954;START2&10
..wwwwwww..
.XwXwXwXwX.
wwwwwwwwwww
wXwXwXwXwXw
wwwwwwwwwww
wXwXwXwXwXw
wwwwwwwwwww
wXwXwXwXwXw
wwwwwwwwwww
.XwXwXwXwX.
..wwwwwww..
;T0&P0 101 109 0 2 2,P1 0 0 1 2 2&+* 10 10 2 40;T1&P0 99 109 0 2 2,P1 0 0 1 2 2&;T2&P0 99 109 0 2 2,P1 0 0 1 2 2&;T3&P0 99 109 0 2 2,P1 0 0 1 2 2&;T4&P0 99 109 0 2 2,P1 0 0 1 2 2&;T5&P0 97 109 0 2 2,P1 0 0 1 2 2&;T6&P0 99 109 0 2 2,P1 0 0 1 2 2&;T7&P0 99 109 0 2 2,P1 0 0 1 2 2&;T8&P0 99 109 0 2 2,P1 0 0 1 2 2&;T9&P0 99 109 0 2 2,P1 0 0 1 2 2&;T10&P0 99 109 0 2 2,P1 0 0 1 2 2&;T11&P0 99 109 0 2 2,P1 0 0 1 2 2&;T12&P0 99 109 0 2 2,P1 0 0 1 2 2&;T13&P0 99 107 0 2 2,P1 0 0 1 2 2&;T14&P0 99 107 0 2 2,P1 0 0 1 2 2&;T15&P0 97 107 0 2 2,P1 0 0 1 2 2&;T16&P0 95 107 0 2 2,P1 0 0 1 2 2&;T17&P0 93 107 0 2 2,P1 0 0 1 2 2&;T18&P0 91 107 0 2 2,P1 0 0 1 2 2&;T19&P0 90 107 0 2 2,P1 0 0 1 2 2&;T20&P0 90 109 0 2 2,P1 0 0 1 2 2&;T21&P0 90 109 0 2 2,P1 0 0 1 2 2&;T22&P0 90 109 0 2 2,P1 0 0 1 2 2&;T23&P0 90 109 0 2 2,P1 0 0 1 2 2&;T24&P0 92 109 0 2 2,P1 0 0 1 2 2&;T25&P0 92 107 0 2 2,P1 0 0 1 2 2&;T26&P0 94 107 0 2 2,P1 0 0 1 2 2&;T27&P0 92 107 0 2 2,P1 0 0 1 2 2&;T28&P0 92 109 0 2 2,P1 0 0 1 2 2&;T29&P0 92 107 0 2 2,P1 0 0 1 2 2&;T30&P0 92 105 0 2 2,P1 0 0 1 2 2&;T31&P0 92 107 0 2 2,P1 0 0 1 2 2&;T32&P0 90 107 0 2 2,P1 0 0 1 2 2&;T33&P0 92 107 0 2 2,P1 0 0 1 2 2&;T34&P0 92 109 0 2 2,P1 0 0 1 2 2&;T35&P0 92 107 0 2 2,P1 0 0 1 2 2&;T36&P0 92 105 0 2 2,P1 0 0 1 2 2&;T37&P0 92 107 0 2 2,P1 0 0 1 2 2&;T38&P0 94 107 0 2 2,P1 0 0 1 2 2&;T39&P0 96 107 0 2 2,P1 0 0 1 2 2&;T40&P0 dead,P1 0 0 1 2 2&-* 10 10,+# 10 10 2 3;REND -1954;START3&10
..wwwwwww..
.XwXwXwXwX.
wwwwwwwwwww
wXwXwXwXwXw
wwwwwwwwwww
wXwXwXwXwXw
wwwwwwwwwww
wXwXwXwXwXw
wwwwwwwwwww
.XwXwXwXwX.
..wwwwwww..
;T0&P0 108 9 0 2 2,P1 0 0 1 2 2&+* 10 0 2 40;T1&P0 109 9 0 2 2,P1 0 0 1 2 2&;T2&P0 107 9 0 2 2,P1 0 0 1 2 2&;T3&P0 105 9 0 2 2,P1 0 0 1 2 2&;T4&P0 107 9 0 2 2,P1 0 0 1 2 2&;T5&P0 105 9 0 2 2,P1 0 0 1 2 2&;T6&P0 103 9 0 2 2,P1 0 0 1 2 2&;T7&P0 103 7 0 2 2,P1 0 0 1 2 2&;T8&P0 105 7 0 2 2,P1 0 0 1 2 2&;T9&P0 105 5 0 2 2,P1 0 0 1 2 2&;T10&P0 105 3 0 2 2,P1 0 0 1 2 2&;T11&P0 107 3 0 2 2,P1 0 0 1 2 2&;T12&P0 107 5 0 2 2,P1 0 0 1 2 2&;T13&P0 107 3 0 2 2,P1 0 0 1 2 2&;T14&P0 109 3 0 2 2,P1 0 0 1 2 2&;T15&P0 107 3 0 2 2,P1 0 0 1 2 2&;T16&P0 109 3 0 2 2,P1 0 0 1 2 2&;T17&P0 109 3 0 2 2,P1 0 0 1 2 2&;T18&P0 109 1 0 2 2,P1 0 0 1 2 2&;T19&P0 109 3 0 2 2,P1 0 0 1 2 2&;T20&P0 109 3 0 2 2,P1 0 0 1 2 2&;T21&P0 107 3 0 2 2,P1 0 0 1 2 2&;T22&P0 107 5 0 2 2,P1 0 0 1 2 2&;T23&P0 105 5 0 2 2,P1 0 0 1 2 2&;T24&P0 105 7 0 2 2,P1 0 0 1 2 2&;T25&P0 103 7 0 2 2,P1 0 0 1 2 2&;T26&P0 105 7 0 2 2,P1 0 0 1 2 2&;T27&P0 103 7 0 2 2,P1 0 0 1 2 2&;T28&P0 105 7 0 2 2,P1 0 0 1 2 2&;T29&P0 107 7 0 2 2,P1 0 0 1 2 2&;T30&P0 107 9 0 2 2,P1 0 0 1 2 2&;T31&P0 105 9 0 2 2,P1 0 0 1 2 2&;T32&P0 105 11 0 2 2,P1 0 0 1 2 2&;T33&P0 105 10 0 2 2,P1 0 0 1 2 2&;T34&P0 105 12 0 2 2,P1 0 0 1 2 2&;T35&P0 105 10 0 2 2,P1 0 0 1 2 2&;T36&P0 105 10 0 2 2,P1 0 0 1 2 2&;T37&P0 105 12 0 2 2,P1 0 0 1 2 2&;T38&P0 105 10 0 2 2,P1 0 0 1 2 2&;T39&P0 105 10 0 2 2,P1 0 0 1 2 2&;T40&P0 dead,P1 0 0 1 2 2&-* 10 0,+# 10 0 2 3;REND -2954;GEND P0 -2954 2,P1 0 1;";

		[Test]
		public void TestParse()
		{
			string[] messages = TestInput.Split(';');
			var parser = new Parser(new DummyParserListener());
			foreach (string message in messages)
			{
				parser.ParseMessage(message + ';');
			}
		}
	}

	public class DummyParserListener : IParserListener
	{
		#region IParserListener Members

		public void OnGameStart(GameInfo gameInfo)
		{
		}

		public void OnRoundStart(StartRoundInfo startRoundInfo)
		{
		}

		public void OnMapChange(MapChangeInfo mapChangeInfo)
		{
		}

		public void OnFinishRound(int score)
		{
		}

		public void OnFinishGame(PlayerResult[] playerResults)
		{
		}

		#endregion
	}
}

