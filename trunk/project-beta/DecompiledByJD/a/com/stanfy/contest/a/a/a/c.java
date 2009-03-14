package com.stanfy.contest.a.a.a;

import com.stanfy.contest.a.a.a.a.e;
import com.stanfy.contest.a.a.a.a.f;
import com.stanfy.contest.a.a.a.a.g;
import com.stanfy.contest.a.a.a.a.m;
import com.stanfy.contest.a.a.a.a.n;
import com.stanfy.contest.a.a.u;
import java.util.HashMap;
import org.apache.log4j.Logger;

public final class c
{
  private static final Logger a = Logger.getLogger(c.class);
  private static HashMap b;
  private HashMap c;
  private HashMap d;

  public static c a(com.stanfy.contest.a.a.b.b paramb)
  {
    if (b == null)
      b = new HashMap();
    if ((localc = (c)b.get(paramb)) != null)
      return localc;
    c localc = new c();
    b.put(paramb, localc);
    localc.a(paramb, com.stanfy.contest.a.a.a.a.k.class);
    localc.a(paramb, com.stanfy.contest.a.a.a.a.d.class);
    localc.a(paramb, com.stanfy.contest.a.a.a.a.a.class);
    localc.a(paramb, com.stanfy.contest.a.a.a.a.c.class);
    localc.a(paramb, com.stanfy.contest.a.a.a.a.i.class);
    localc.a(paramb, g.class);
    localc.a(paramb, com.stanfy.contest.a.a.a.a.l.class);
    localc.a(paramb, m.class);
    localc.a(paramb, com.stanfy.contest.a.a.a.a.j.class);
    localc.a(paramb, com.stanfy.contest.a.a.a.a.h.class);
    localc.a(paramb, f.class);
    localc.a(paramb, e.class);
    localc.a(paramb, com.stanfy.contest.a.a.a.a.b.class);
    localc.b(paramb, com.stanfy.contest.a.a.l.class);
    localc.b(paramb, com.stanfy.contest.a.a.c.class);
    localc.b(paramb, com.stanfy.contest.a.a.h.class);
    localc.b(paramb, com.stanfy.contest.a.a.d.class);
    localc.b(paramb, com.stanfy.contest.a.a.k.class);
    localc.b(paramb, com.stanfy.contest.a.a.i.class);
    localc.b(paramb, com.stanfy.contest.a.a.j.class);
    return localc;
  }

  private void a(com.stanfy.contest.a.a.b.b paramb, Class paramClass)
  {
    if (this.c == null)
      this.c = new HashMap();
    try
    {
      paramClass = (n)paramClass.newInstance();
      this.c.put(a.a(true, paramClass.a(), paramb.a().v()), paramClass);
      return;
    }
    catch (Exception paramClass)
    {
      a.error("Cannot instantiate configuration part" + paramClass.getMessage());
    }
  }

  private void b(com.stanfy.contest.a.a.b.b paramb, Class paramClass)
  {
    paramb = (paramb = (u)b.a().a(paramb, paramClass)).a_();
    if (this.d == null)
      this.d = new HashMap();
    if (paramb == null)
      return;
    this.d.putAll(paramb);
  }

  public final n a(String paramString)
  {
    return ((n)this.c.get(paramString));
  }

  public final com.stanfy.contest.a.a.a.b.a b(String paramString)
  {
    return ((com.stanfy.contest.a.a.a.b.a)this.d.get(paramString));
  }
}