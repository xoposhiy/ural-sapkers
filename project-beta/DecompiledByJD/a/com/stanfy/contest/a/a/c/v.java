package com.stanfy.contest.a.a.c;

import java.util.Stack;

final class v extends o
{
  v(am paramam, String paramString)
  {
    super(paramString);
  }

  public final Object a(am paramam)
  {
    this = am.a(am.a(paramam).pop()).intValue();
    v localv = am.a(am.a(paramam).pop()).intValue();
    try
    {
      am.a(paramam).push(Integer.valueOf(localv % this));
      return Integer.valueOf(localv % this);
    }
    catch (ArithmeticException localArithmeticException)
    {
      throw y.b();
    }
  }

  public final String a()
  {
    return "Divides number1 by number2 and returns only the remainder as result to top of stack\r\n Only Ints are allowed : <INT> <INT> % -> <INT>\r\n EXAMPLE: 3 2 % . OUTPUT:1\r\n          10 2 % . OUTPUT:0";
  }
}