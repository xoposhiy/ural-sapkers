package com.stanfy.contest.a.a.c;

import java.util.Stack;

final class f extends o
{
  f(am paramam, String paramString)
  {
    super(paramString);
  }

  public final Object a(am paramam)
  {
    paramam = (am.a(paramam).isEmpty()) ? "Stack is empty" : am.a(paramam).peek();
    this.a.d(paramam.toString());
    return paramam;
  }

  public final String a()
  {
    return "Outputs value at top of stack. Doesn't changing stack.\r\n All types are allowed : <OBJ1> . -> <OBJ1> \r\n EXAMPLE: 115 . OUTPUT:115\r\n          'arkanzas' . OUTPUT:arkanzas\r\n          . OUTPUT:Stack is empty";
  }
}