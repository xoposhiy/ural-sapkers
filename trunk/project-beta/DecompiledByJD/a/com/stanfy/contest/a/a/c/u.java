package com.stanfy.contest.a.a.c;

import java.util.Stack;

final class u extends o
{
  u(am paramam, String paramString)
  {
    super(paramString);
  }

  public final Object a(am paramam)
  {
    this = am.a(paramam).pop();
    Object localObject = am.a(paramam).pop();
    am.a(paramam).push(this);
    am.a(paramam).push(localObject);
    return localObject;
  }

  public final String a()
  {
    return "Swaps two objects at top of stack\r\n All types are allowed : <OBJ1> <OBJ2> SWAP -> <OBJ2> <OBJ1>\r\n EXAMPLE: 3 2 SWAP . OUTPUT:3\r\n          3 2 SWAP SWAP . OUTPUT:2";
  }
}