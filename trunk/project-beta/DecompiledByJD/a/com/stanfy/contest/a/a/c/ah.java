package com.stanfy.contest.a.a.c;

import java.util.Stack;

final class ah extends o
{
  ah(am paramam, String paramString)
  {
    super(paramString);
  }

  public final Object a(am paramam)
  {
    this = am.a(am.a(paramam).pop()).intValue();
    int i = am.a(am.a(paramam).pop()).intValue();
    String str1 = am.b(am.a(paramam).pop());
    try
    {
      String str2 = str1.substring(i, this);
      am.a(paramam).push(str2);
      return str2;
    }
    catch (StringIndexOutOfBoundsException localStringIndexOutOfBoundsException)
    {
      throw y.a(str1, i, this);
    }
  }

  public final String a()
  {
    return "Gets substring of source String, and puts result at top of stack\r\n FORMAT : <STRING> <INT(FROM)> <INT(TO)> SUBSTR -> <STRING> \r\n EXAMPLE: 'cooler' 0 4 SUBSTR . OUTPUT:cool";
  }
}