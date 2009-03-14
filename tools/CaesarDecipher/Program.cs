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
			string inputFile = args.Length > 0 ? args[0] : "encoded.txt";
			string outputFile = args.Length > 1 ? args[1] : "decoded.txt";
			DecodeFile(inputFile, outputFile);
			//Console.WriteLine(DecodeString("sJ3:;0VJD:@J69:BVJB3,?J?30Jm,0>,=J.34;0=J4>"));
			//Console.WriteLine(DecodeString("Js9J.=D;?:2=,;3DVJ,Jm,0>,=J.4;30=VJ,7>:J69:B9J,>J,Jm,0>,=Q>J.4;30=V"));
			Console.ReadKey();
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
