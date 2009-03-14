package com.stanfy.contest.c;

import com.stanfy.contest.b.i;
import com.stanfy.contest.b.v;
import org.apache.log4j.Logger;

public class j
  implements f
{
  public final g a(com.stanfy.contest.b.c paramc)
  {
    v localv;
    j localj;
    this = Math.max(paramc.a().b(), paramc.a().a()) / 2 + 2;
    int i = paramc.c();
    if ((localj = (localv = paramc.b()).a() - 2400) < 0)
      return null;
    if ((this = localj * this / 1200) <= i)
      return null;
    paramc.a(this);
    return new c().a(paramc);
  }

  static
  {
    Logger.getLogger(j.class);
  }
}