package com.stanfy.contest.a.b;

import com.stanfy.contest.b.c;
import com.stanfy.contest.b.e;
import com.stanfy.contest.b.k;
import java.io.Serializable;
import java.util.Map;

public final class d
  implements b
{
  private Map a;

  public d(c paramc, Map paramMap)
  {
    this.a = paramMap;
  }

  public final String a(k paramk)
  {
    e locale;
    int i = (paramk == null) ? 2147483647 : paramk.b();
    if (((locale = (paramk == null) ? null : paramk.u()) == null) || (locale.d()))
      return "REND " + ((paramk == null) ? "?" : (Serializable)this.a.get(Integer.valueOf(i))) + ";";
    return null;
  }
}