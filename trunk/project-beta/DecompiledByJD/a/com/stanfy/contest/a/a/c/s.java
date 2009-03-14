package com.stanfy.contest.a.a.c;

import java.util.Stack;

final class s extends o
{
  s(am paramam, String paramString)
  {
    super(paramString);
  }

  public final Object a(am paramam)
  {
    this = am.a(am.a(paramam).pop()).intValue();
    s locals = am.a(am.a(paramam).pop()).intValue();
    try
    {
      am.a(paramam).push(Integer.valueOf(locals / this));
      return Integer.valueOf(locals / this);
    }
    catch (ArithmeticException localArithmeticException)
    {
      throw y.b();
    }
  }

  public final String a()
  {
    return "Divides two operands at top of stack, returns result to top of stack\r\n Only Ints are allowed : <INT> <INT> / -> <INT>\r\n EXAMPLE: 3 2 / . OUTPUT:1\r\n          10 2 / . OUTPUT:5";
  }
}