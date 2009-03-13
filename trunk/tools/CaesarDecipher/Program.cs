using System;
using System.IO;
using System.Text;

namespace CaesarDecipher
{
	class Program
	{
		private const int shift = 42;

		static void Main(string[] args)
		{
			DecodeFile("encoded.txt", "decoded.txt");
			//Console.WriteLine(DecodeString("sJ3:;0VJD:@J69:BVJB3,?J?30Jm,0>,=J.34;0=J4>"));
			//Console.ReadKey();
		}

        private static string DecodeString(string encoded)
		{
			var sb = new StringBuilder(encoded.Length);
			for(int i=0; i<encoded.Length; ++i)
			{
				int d = Char.ConvertToUtf32(encoded, i) - shift;
				if (d < 32)
					d += 126 - 32 + 1;
				sb.Append(Char.ConvertFromUtf32(d));
			}
			return sb.ToString();
		}

		private static void DecodeFile(string fin, string fout)
		{
			using (var sr = new StreamReader(fin))
			using (var sw = new StreamWriter(fout)) 
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					sw.WriteLine(DecodeString(line));
				}
			}
		}
	}
}
