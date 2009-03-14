package com.stanfy.contest.a.a.c;

import java.util.Stack;

final class z extends o
{
  z(am paramam, String paramString)
  {
    super(paramString);
  }

  public final Object a(am paramam)
  {
    int i;
    this = am.a(am.a(paramam).pop()).intValue();
    this = ((i = am.a(am.a(paramam).pop()).intValue()) > this) ? -1 : 0;
    am.a(paramam).push(Integer.valueOf(this));
    return Integer.valueOf(this);
  }

  public final String a()
  {
    return "Checks if variable2 GREATER than variable1, returns result at top of stack (0=FALSE or -1=TRUE)\r\n Only Ints are allowed : <INT2> <INT1>> -> <INT(BOOLEAN)>\r\n EXAMPLE: 2 3 > . OUTPUT:0";
  }
}