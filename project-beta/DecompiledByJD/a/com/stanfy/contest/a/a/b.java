package com.stanfy.contest.a.a;

import com.stanfy.contest.b.e;
import com.stanfy.contest.b.k;

public class b extends u
{
  private String c;

  private static char[] a(char paramChar, int paramInt)
  {
    paramInt = new char[paramInt];
    for (int i = 0; i < paramInt.length; ++i)
      paramInt[i] = a(paramChar);
    return paramInt;
  }

  private static char a(char paramChar)
  {
    switch (paramChar)
    {
    case '+':
      return '-';
    case '-':
      return '+';
    case '<':
      return '>';
    case '>':
      return '<';
    case '[':
      return ']';
    case ']':
      return '[';
    case '.':
      return ',';
    case ',':
      return '.';
    }
    return paramChar;
  }

  private static String f(String paramString)
  {
    StringBuilder localStringBuilder;
    int j;
    (localStringBuilder = new StringBuilder()).append(a('+', 11)).append(a('['));
    for (int i = 0; i < paramString.length(); ++i)
    {
      j = paramString.charAt(i) / '\11';
      localStringBuilder.append(a('>')).append(a('+', j));
    }
    localStringBuilder.append(a('<', paramString.length()));
    localStringBuilder.append(a('-'));
    localStringBuilder.append(a(']'));
    for (i = 0; i < paramString.length(); ++i)
    {
      j = paramString.charAt(i) % '\11';
      localStringBuilder.append(a('>')).append(a('+', j)).append(a('.'));
    }
    return localStringBuilder.toString();
  }

  public final void a(com.stanfy.contest.a.a.b.b paramb)
  {
    super.a(paramb);
    this.c = e("zJDqE2CkZi1VsMbMBCIp");
    c("\r\n================== AAR subsytem ================================================\r\n");
    String[] arrayOfString = { "\r\nScanning associative memory", ".", ".", ".", ".\r\n" };
    long l = 1000L;
    (paramb = this).a(arrayOfString, l);
    c("ERROR!!! Information dedicated to process map resources is damaged!\r\n");
    arrayOfString = { ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".\r\n" };
    l = 100L;
    (paramb = this).a(arrayOfString, l);
    c("finish-game-info ::= 'GEND' <sep> <rank-score-info>\r\n  rank-score-info ::= 'P' <number> <sep> <score> <sep> <rank> [ ',' 'P' <number> <sep> <score> <sep> <rank> ]\r\n  score ::= <number>\r\n  rank ::= <number>\r\n");
    arrayOfString = { ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".\r\n" };
    l = 100L;
    (paramb = this).a(arrayOfString, l);
    arrayOfString = { ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".\r\n" };
    l = 100L;
    (paramb = this).a(arrayOfString, l);
    c("This part of the memory contains configuration token that activates the adoption of additional resources (bonuses).\r\n");
    c("Trying to recover.");
    arrayOfString = { ".", ".", ".", " hm...\r\n" };
    l = 1000L;
    (paramb = this).a(arrayOfString, l);
    arrayOfString = { "                      Unknown syntax detected. Loading possible description from\r\n" };
    l = 1000L;
    (paramb = this).a(arrayOfString, l);
    c("                                          http://uk.wikipedia.org/wiki/Brainfuck\r\n");
    c("                                                           Something went wrong.\r\n");
    c("                                                               Attempt failed :(\r\n");
    arrayOfString = { "                                                                 Memory content:\r\n", f(f(this.c)) };
    l = 1000L;
    (paramb = this).a(arrayOfString, l);
    b("statusExit");
  }

  public final void a(String paramString)
  {
  }

  public final String a()
  {
    if (this.a.a().u().x())
      return "AAR enabled. Token: " + e("zJDqE2CkZi1VsMbMBCIp");
    return "Adoption of Additional Resources (AAR) disabled.";
  }

  public final String b()
  {
    return "aar";
  }
}