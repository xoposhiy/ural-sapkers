package com.stanfy.contest.a.a.c;

import java.util.Arrays;
import java.util.Collection;
import java.util.HashMap;

final class g extends o
{
  g(am paramam, String paramString)
  {
    super(paramString);
  }

  public final Object a(am paramam)
  {
    o[] arrayOfo1;
    o[] arrayOfo2;
    this.a.d("\r\n");
    this.a.d(" == REGISTERED WORDS == \r\n");
    this.a.d("  :System words \r\n");
    Arrays.sort(arrayOfo1 = (o[])am.c(paramam).values().toArray(new o[0]));
    int i = (arrayOfo1 = arrayOfo1).length;
    for (int j = 0; j < i; ++j)
    {
      o localo = arrayOfo1[j];
      this.a.d("    " + localo.b() + "\r\n");
    }
    this.a.d("  :User defined words \r\n");
    Arrays.sort(arrayOfo1 = (o[])am.d(paramam).values().toArray(new o[0]));
    j = (arrayOfo2 = arrayOfo1).length;
    for (int k = 0; k < j; ++k)
    {
      paramam = arrayOfo2[k];
      this.a.d("    " + paramam.b() + "\r\n");
    }
    this.a.d(" == REGISTERED WORDS END== \r\n");
    return "REGISTERED WORDS";
  }

  public final String a()
  {
    return "Outputs ALL Registered Words. Doesn't changing stack.\r\n All types are allowed : <> WORDS -> <>";
  }
}