package com.stanfy.contest.a.a.c;

import java.util.Stack;

final class w extends o
{
  w(am paramam, String paramString)
  {
    super(paramString);
  }

  public final Object a(am paramam)
  {
    this = am.a(am.a(paramam).pop()).intValue();
    w localw = am.a(am.a(paramam).pop()).intValue();
    am.a(paramam).push(Integer.valueOf(localw - this));
    return Integer.valueOf(localw - this);
  }

  public final String a()
  {
    return "Subtracts two operands at top of stack, returns result to top of stack\r\n Only Ints are allowed : <INT> <INT> - -> <INT>\r\n EXAMPLE: 3 2 - . OUTPUT:1\r\n          2 3 - . OUTPUT:-1";
  }
}