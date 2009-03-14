package com.stanfy.contest.a.a.c;

import java.util.Stack;

final class ai extends o
{
  ai(am paramam, String paramString)
  {
    super(paramString);
  }

  public final Object a(am paramam)
  {
    this = am.a(am.a(paramam).pop()).intValue();
    String str1 = am.b(am.a(paramam).pop());
    try
    {
      String str2 = str1.charAt(this) + "";
      am.a(paramam).push(str2);
      return str2;
    }
    catch (StringIndexOutOfBoundsException localStringIndexOutOfBoundsException)
    {
      throw y.a(str1, this, this);
    }
  }

  public final String a()
  {
    return "Gets character of String at specified position and puts it at top of stack\r\n FORMAT : <STRING> <INT(INDEX)>CHARAT -> <STRING> \r\n EXAMPLE: 'stake' 2 CHARAT . OUTPUT:a\r\n          'stake' 0 CHARAT . OUTPUT:s";
  }
}