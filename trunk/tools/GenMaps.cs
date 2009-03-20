using System;
using System.IO;
using System.Globalization;

class Program
{
  private static void SetChar(char[,] map, char ch, int i, int j, int width, int height)
  {
    map[i, j] = ch;
    map[i, width - j - 1] = ch;
    map[height - i - 1, j] = ch;
    map[height - i - 1, width - j - 1] = ch;
  }

  private static void GenMap(int width, int height, int cellSize)
  {
    if (width % 2 != 0 || height % 2 != 0)
    {
      Console.Error.WriteLine("Can't make a symmetric map out of this");
      Environment.Exit(1);
    }
    string mapName = width + "x" + height + "x" + cellSize;
    using (var props = new StreamWriter(mapName + ".properties"))
    {
      props.WriteLine("rounds.count = " + roundCount);
      props.WriteLine("width = " + width);
      props.WriteLine("height = " + height);
      props.WriteLine("cell.size = " + cellSize);
    }
    int w = width / 2;
    int h = height / 2;
    char[,] map = new char[height, width];
    var random = new Random();
    for (int i = 0; i < h; i++)
    {
      for (int j = 0; j < w; j++)
      {
          double d = random.NextDouble();
          char ch =
            i == 0 && j == 0 ? 'o' :
            i + j == 1 ? '.' :
            d <= wallProb ? 'w' :
            d <= wallProb + uwallProb ? 'X' :
            '.';
          SetChar(map, ch, i, j, width, height);
      }
    }
    using (var mapWriter = new StreamWriter(mapName + ".map"))
    {
      for (int row = 0; row < height; row++)
      {
        string r = "";
        for (int col = 0; col < width; col++)
        {
          r += map[row, col];
        }
        mapWriter.WriteLine(r);
      }
    }
  }
  
  private static int roundCount;
  private static double wallProb;
  private static double uwallProb;
  
  private static void GenMaps(int width, int height)
  {
    GenMap(width, height, 10);
    GenMap(width, height, 12);
    GenMap(width, height, 18);
    GenMap(width, height, 23);
    GenMap(width, height, 30);
  }
  
  private static void Main(string[] args)
  {
    if (args.Length != 3)
    {
      Console.Error.WriteLine("Usage: round-count wall-prob unbreakable-wall-prob");
      Environment.Exit(1);
    }
    roundCount = int.Parse(args[0]);
    wallProb = double.Parse(args[1], CultureInfo.InvariantCulture);
    uwallProb = double.Parse(args[2], CultureInfo.InvariantCulture);
    if (wallProb + uwallProb > 1.0)
    {
      Console.Error.WriteLine("I wonder how you managed to pass your probability theory exam");
      Environment.Exit(1);
    }
    GenMaps(6, 8);
    GenMaps(10, 16);
    GenMaps(14, 14);
    GenMaps(24, 20);
    GenMaps(30, 28);
  }
}