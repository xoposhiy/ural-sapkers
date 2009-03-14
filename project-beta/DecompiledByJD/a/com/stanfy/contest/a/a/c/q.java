package com.stanfy.contest.a.a.c;

import java.util.Stack;

final class q extends o
{
  q(am paramam, String paramString)
  {
    super(paramString);
  }

  public final Object a(am paramam)
  {
    this = am.a(paramam).peek();
    am.a(paramam).push(this);
    return this;
  }

  public final String a()
  {
    return "Duplicates object at top of stack\r\n All types are allowed : <OBJ1> DUP -> <OBJ1> <OBJ1>\r\n EXAMPLE: 3 2 DUP DROP . OUTPUT:2";
  }
}