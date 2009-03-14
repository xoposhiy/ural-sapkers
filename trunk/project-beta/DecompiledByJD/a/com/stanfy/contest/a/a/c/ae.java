package com.stanfy.contest.a.a.c;

import java.util.Stack;

final class ae extends o
{
  ae(am paramam, String paramString)
  {
    super(paramString);
  }

  public final Object a(am paramam)
  {
    this = (this = am.b(am.a(paramam).pop())).length();
    am.a(paramam).push(Integer.valueOf(this));
    return Integer.valueOf(this);
  }

  public final String a()
  {
    return "Gets string from top of stack and returns its length as result at top of stack\r\n Only String is alllowed : <STRING> LENGTH -> <INT> \r\n EXAMPLE: 'ababagalamaga' LENGTH . OUTPUT:13";
  }
}