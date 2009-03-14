package com.stanfy.contest.a.a.c;

import java.util.Stack;

final class af extends o
{
  af(am paramam, String paramString)
  {
    super(paramString);
  }

  public final Object a(am paramam)
  {
    this = am.b(am.a(paramam).pop());
    try
    {
      int i = Integer.valueOf(this).intValue();
      am.a(paramam).push(Integer.valueOf(i));
      return Integer.valueOf(i);
    }
    catch (NumberFormatException localNumberFormatException)
    {
      throw y.a(this);
    }
  }

  public final String a()
  {
    return "Converts String to int, ant puts result at top of stack\r\n Only String is alllowed : <STRING> STRTOINT -> <INT> \r\n EXAMPLE: '42' STRTOINT . OUTPUT:42";
  }
}