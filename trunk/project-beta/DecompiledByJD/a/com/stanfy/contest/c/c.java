package com.stanfy.contest.c;

import com.stanfy.contest.b.i;
import com.stanfy.contest.b.m;
import com.stanfy.contest.b.t;
import java.util.Iterator;
import java.util.List;

public final class c
  implements f
{
  private h a = null;

  public final g a(com.stanfy.contest.b.c paramc)
  {
    int i;
    i locali;
    if ((i = paramc.c()) == 1)
      return null;
    int k = (locali = paramc.a()).a();
    int j = locali.b();
    for (int l = i - 2; l <= k - i + 1; ++l)
      if (i - 2 <= j - i + 1)
      {
        a(paramc, i - 2, l);
        a(paramc, j - i + 1, l);
      }
    for (l = i - 2; l <= j - i + 1; ++l)
      if (i - 2 <= k - i + 1)
      {
        a(paramc, l, i - 2);
        a(paramc, l, k - i + 1);
      }
    return this.a;
  }

  private void a(com.stanfy.contest.b.c paramc, int paramInt1, int paramInt2)
  {
    if ((localObject = paramc.a().a(paramInt1, paramInt2)).c())
      return;
    Object localObject = new t(paramInt1, paramInt2, null);
    paramc.a().b((com.stanfy.contest.b.h)localObject);
    localObject = new e(k.a, (com.stanfy.contest.b.h)localObject);
    if (this.a == null)
      this.a = new h((g)localObject);
    else
      this.a = this.a.a((g)localObject);
    this = paramc.e().iterator();
    while (true)
    {
      do
        if (!(hasNext()))
          return;
      while (!((localObject = (com.stanfy.contest.b.k)next()).m()));
      if ((((com.stanfy.contest.b.k)localObject).e() == paramInt1) && (((com.stanfy.contest.b.k)localObject).d() == paramInt2))
        paramc.a((com.stanfy.contest.b.k)localObject);
    }
  }
}