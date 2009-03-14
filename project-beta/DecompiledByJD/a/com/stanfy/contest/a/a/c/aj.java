package com.stanfy.contest.a.a.c;

import java.util.Stack;

final class aj extends o
{
  aj(am paramam, String paramString)
  {
    super(paramString);
  }

  public final Object a(am paramam)
  {
    String str;
    this = am.b(am.a(paramam).pop());
    this = (str = am.b(am.a(paramam).pop())).indexOf(this);
    am.a(paramam).push(Integer.valueOf(this));
    return Integer.valueOf(this);
  }

  public final String a()
  {
    return "Gets character of String at specified position and puts it at top of stack\r\n FORMAT : <STRING> <INT(INDEX)>CHARAT -> <STRING> \r\n EXAMPLE: 'stake' 2 CHARAT . OUTPUT:a\r\n          'stake' 0 CHARAT . OUTPUT:s";
  }
}