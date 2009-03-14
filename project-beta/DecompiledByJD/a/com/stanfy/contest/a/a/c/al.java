package com.stanfy.contest.a.a.c;

import java.util.Stack;

final class al extends o
{
  al(am paramam, String paramString)
  {
    super(paramString);
  }

  public final Object a(am paramam)
  {
    this = am.b(am.a(paramam).pop());
    try
    {
      int i = charAt(0);
      am.a(paramam).push(Integer.valueOf(i));
      return Integer.valueOf(i);
    }
    catch (StringIndexOutOfBoundsException localStringIndexOutOfBoundsException)
    {
      throw y.a(this, 0, 0);
    }
  }

  public final String a()
  {
    return "Gets String's first character, converts it to ordinal value and puts it at top of stack\r\n Only Strings are allowed : <STRING> ORD -> <INT> \r\n EXAMPLE: 'stake' ORD . OUTPUT:115\r\n          'a' ORD . OUTPUT:97";
  }
}