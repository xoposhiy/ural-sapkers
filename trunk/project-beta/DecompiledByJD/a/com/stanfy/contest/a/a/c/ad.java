package com.stanfy.contest.a.a.c;

import java.util.Stack;

final class ad extends o
{
  ad(am paramam, String paramString)
  {
    super(paramString);
  }

  public final Object a(am paramam)
  {
    this = ((this = am.a(am.a(paramam).pop()).intValue()) == 0) ? -1 : 0;
    am.a(paramam).push(Integer.valueOf(this));
    return Integer.valueOf(this);
  }

  public final String a()
  {
    return "Checks if variable at top of stack EQUAL to ZERO, returns result at top of stack (0=FALSE or -1=TRUE)\r\n Only Int are alllowed : <INT> =0 -> <INT(BOOLEAN)>  \r\n EXAMPLE: 2 2 - =0 . OUTPUT:-1";
  }
}