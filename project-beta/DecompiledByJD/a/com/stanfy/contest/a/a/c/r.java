package com.stanfy.contest.a.a.c;

import java.util.HashMap;
import java.util.Stack;

final class r extends o
{
  r(am paramam, String paramString)
  {
    super(paramString);
  }

  public final Object a(am paramam)
  {
    paramam = am.b(am.a(paramam).pop());
    this.a.a(paramam);
    return am.b(this.a).get(paramam);
  }

  public final String a()
  {
    return "Creates variable with name, than is in String at top of stack\r\n Only Strings are allowed : <STRING> VAR -> <>\r\n Variables redeclaring is allowed, but only one instance of variable can be at same time\r\n Use ->A to store data to variable, and @A to get data from it\r\n EXAMPLE: 'A' VAR 3 ->A  @A . OUTPUT:3";
  }
}