package com.stanfy.contest.a.b;

import com.stanfy.contest.b.k;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;

public final class a
  implements b
{
  private Map a;
  private Map b;

  public a(Map paramMap1, Map paramMap2)
  {
    this.a = paramMap1;
    this.b = paramMap2;
  }

  public final String a(k paramk)
  {
    (paramk = new StringBuilder()).append("GEND").append(" ");
    Iterator localIterator = this.b.keySet().iterator();
    while (localIterator.hasNext())
    {
      int i = ((Integer)localIterator.next()).intValue();
      paramk.append("P").append(i).append(" ").append(this.a.get(Integer.valueOf(i)));
      paramk.append(" ").append(this.b.get(Integer.valueOf(i))).append(",");
    }
    paramk.delete(paramk.length() - 1, paramk.length());
    paramk.append(";");
    return paramk.toString();
  }
}