package com.stanfy.contest.c;

import com.stanfy.contest.b.b;
import com.stanfy.contest.b.f;
import com.stanfy.contest.b.h;
import com.stanfy.contest.b.o;
import com.stanfy.contest.b.t;

public final class e
  implements g
{
  private k a;
  private h b;

  public e(k paramk, h paramh)
  {
    this.a = paramk;
    this.b = paramh;
  }

  public final String a(com.stanfy.contest.b.k paramk)
  {
    Object localObject;
    String str = k.a(this.a);
    if (this.b instanceof f)
    {
      if ((paramk != null) && (!(paramk.u().m())))
        return "";
      localObject = (f)this.b;
      return str + this.b.a(paramk) + " " + a(this.b) + ((this.a == k.a) ? " " + ((f)localObject).b() + " " + ((f)localObject).a() : "");
    }
    if (this.b instanceof o)
    {
      if ((paramk != null) && (!(paramk.u().o())))
        return "";
      localObject = (o)this.b;
      return str + ((o)localObject).a(paramk) + " " + a(this.b);
    }
    if (this.b instanceof b)
    {
      if ((paramk != null) && (!(paramk.u().n())))
        return "";
      localObject = (b)this.b;
      return str + this.b.a(paramk) + " " + a(this.b) + ((this.a == k.a) ? " " + ((b)localObject).b() + " " + ((b)localObject).a() : "");
    }
    if (this.b instanceof com.stanfy.contest.b.g)
    {
      if ((paramk != null) && (!(paramk.u().k())))
        return "";
      return str + this.b.a(paramk) + " " + a(this.b);
    }
    if (this.b instanceof t)
    {
      if ((paramk != null) && (!(paramk.u().l())))
        return "";
      return str + this.b.a(paramk) + " " + a(this.b);
    }
    return ((String)"");
  }

  private static String a(h paramh)
  {
    return paramh.d() + " " + paramh.e();
  }
}