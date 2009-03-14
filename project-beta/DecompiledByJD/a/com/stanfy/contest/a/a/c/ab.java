package com.stanfy.contest.a.a.c;

import java.util.Stack;

final class ab extends o
{
  ab(am paramam, String paramString)
  {
    super(paramString);
  }

  public final Object a(am paramam)
  {
    Object localObject;
    this = am.a(paramam).pop();
    this = ((localObject = am.a(paramam).pop()).equals(this)) ? -1 : 0;
    am.a(paramam).push(Integer.valueOf(this));
    return Integer.valueOf(this);
  }

  public final String a()
  {
    return "Checks if variable2 is EQUAL to variable1, returns result at top of stack (0=FALSE or -1=TRUE)\r\n All Types are allowed : <OBJ2> <OBJ1> == -> <INT(BOOLEAN)>  \r\n EXAMPLE: 2 3 == . OUTPUT:0";
  }
}