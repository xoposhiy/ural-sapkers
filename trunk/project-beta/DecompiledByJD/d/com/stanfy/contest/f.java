package com.stanfy.contest;

import com.stanfy.contest.a.a.o;
import com.stanfy.contest.b.k;
import com.stanfy.contest.b.v;
import com.stanfy.contest.c.j;
import java.util.HashMap;
import org.apache.log4j.Logger;

public class f extends c
  implements com.stanfy.contest.a.a.b.a, com.stanfy.contest.a.f
{
  private static final Logger a = Logger.getLogger(f.class);
  private com.stanfy.contest.a.e[] b;
  private com.stanfy.contest.b.c c;
  private c d;
  private boolean e = false;
  private int f = 0;
  private int g = 1;
  private b h;
  private g i = null;
  private HashMap j;
  private static f k;

  public static f h()
  {
    return k;
  }

  public f(com.stanfy.contest.b.c paramc, com.stanfy.contest.a.e[] paramArrayOfe, g paramg, boolean paramBoolean)
  {
    this.b = paramArrayOfe;
    for (paramArrayOfe = 0; paramArrayOfe < this.b.length; ++paramArrayOfe)
    {
      this.b[paramArrayOfe].a(paramc.d());
      this.b[paramArrayOfe].a(this);
      this.b[paramArrayOfe].a(com.stanfy.contest.a.a.a.b.a().a(this.b[paramArrayOfe], o.class));
    }
    this.i = paramg;
    this.h = new b(paramc.h());
    this.c = paramc;
    this.j = new HashMap();
    k = this;
    if (paramg != null)
      paramg.a(new a(this, paramc));
    this.d = new h(this);
    paramg.h();
    a.debug("Tracer out");
    if (!(paramBoolean))
    {
      a.info("Init delay: 5000");
      try
      {
        Thread.sleep(5000L);
      }
      catch (InterruptedException localInterruptedException)
      {
        a.error("Failed to sleep.");
      }
    }
    else
    {
      a.info("Waitnig for all connectors.");
      this.e = true;
      paramc = (paramArrayOfe = this.b).length;
      for (paramg = 0; paramg < paramc; ++paramg)
        if (!((paramBoolean = paramArrayOfe[paramg]).k()))
          c();
      this.e = false;
    }
    a.info("========== Start main thread. ==========");
    b("Model controller");
    this.d.b("Awaker for model controller");
  }

  protected final void f()
  {
    com.stanfy.contest.a.e[] arrayOfe;
    a.info("Stopping awaker");
    this.d.e();
    a.info("Awaker was stopped");
    this.i.e();
    int l = (arrayOfe = this.b).length;
    for (int i1 = 0; i1 < l; ++i1)
    {
      com.stanfy.contest.a.e locale = arrayOfe[i1];
      while ((locale.l() != this) && (locale.k()))
      {
        a.info("Waiting for switching connector " + locale.a().b() + " to model controller.");
        c();
      }
      locale.e();
    }
  }

  public final void i()
  {
    l();
  }

  private synchronized void l()
  {
    if (!(this.e))
      return;
    this.f += 1;
    if (this.f >= this.b.length)
      b();
  }

  private void a(com.stanfy.contest.a.b.b paramb)
  {
    Object localObject1;
    int l = (localObject1 = this.b).length;
    for (int i1 = 0; i1 < l; ++i1)
    {
      Object localObject2 = localObject1[i1];
      String str2 = (paramb == null) ? null : paramb.a(localObject2.a());
      localObject2.b(this, str2);
    }
    if (this.i != null)
      if ((localObject1 = this.b[0].a()) != null)
      {
        String str1 = (paramb == null) ? null : paramb.a((k)localObject1);
        this.i.a(paramb, str1);
      }
  }

  private void m()
  {
    if (a.isDebugEnabled())
      a.debug("Round duration: 3600");
    while ((b_()) && (!(this.c.i())))
    {
      Object localObject1;
      com.stanfy.contest.c.g localg1;
      com.stanfy.contest.c.g localg2;
      c();
      com.stanfy.contest.c.h localh = new com.stanfy.contest.c.h(null);
      int l = (localObject1 = this.b).length;
      for (int i1 = 0; i1 < l; ++i1)
      {
        localObject2 = localObject1[i1];
        if ((localObject2 = (com.stanfy.contest.c.f)this.j.get(((com.stanfy.contest.a.e)localObject2).a())) != null)
          if ((localObject2 = ((com.stanfy.contest.c.f)localObject2).a(this.c)) != null)
            localh = localh.a((com.stanfy.contest.c.g)localObject2);
      }
      if ((localg1 = (localObject1 = this.c.b().c()).a(this.c)) != null)
        localh = localh.a(localg1);
      if ((localg2 = new j().a(this.c)) != null)
        localh = localh.a(localg2);
      Object localObject2 = new com.stanfy.contest.a.b.e(this.c, localh);
      a((com.stanfy.contest.a.b.b)localObject2);
      this.c.b().b();
    }
  }

  private static int[] a(int[] paramArrayOfInt, int paramInt)
  {
    int[][] arrayOfInt = new int[2][paramInt];
    for (int l = 0; l < paramInt; ++l)
    {
      arrayOfInt[0][l] = paramArrayOfInt[l];
      arrayOfInt[1][l] = l;
    }
    for (int i1 = 0; i1 < paramInt; ++i1)
      for (paramArrayOfInt = i1; (paramArrayOfInt > 0) && (arrayOfInt[0][(paramArrayOfInt - 1)] < arrayOfInt[0][paramArrayOfInt]); --paramArrayOfInt)
      {
        int i2 = arrayOfInt[0][paramArrayOfInt];
        arrayOfInt[0][paramArrayOfInt] = arrayOfInt[0][(paramArrayOfInt - 1)];
        arrayOfInt[0][(paramArrayOfInt - 1)] = i2;
        i2 = arrayOfInt[1][paramArrayOfInt];
        arrayOfInt[1][paramArrayOfInt] = arrayOfInt[1][(paramArrayOfInt - 1)];
        arrayOfInt[1][(paramArrayOfInt - 1)] = i2;
      }
    int[] arrayOfInt1 = new int[paramInt];
    for (paramArrayOfInt = 0; paramArrayOfInt < paramInt; ++paramArrayOfInt)
      arrayOfInt1[arrayOfInt[1][paramArrayOfInt]] = (paramArrayOfInt + 1);
    return arrayOfInt1;
  }

  protected final void g()
  {
    a(new com.stanfy.contest.a.b.f(this.c));
    while ((b_()) && (this.g <= this.c.g()))
    {
      try
      {
        Thread.sleep(2000L);
      }
      catch (InterruptedException localInterruptedException)
      {
        a.error("Failed to sleep.");
      }
      a.info("Start new round " + this.g);
      a(new com.stanfy.contest.a.b.c(this.g, this.c));
      m();
      a(new com.stanfy.contest.a.b.d(this.c, this.h));
      a.info("Reset");
      this.c.f();
      this.g += 1;
    }
    a(new com.stanfy.contest.a.b.a(this.h, new b(a(b.a(this.h), this.b.length))));
    a.info("Finished main cycle.");
  }

  public final void a(k paramk, String paramString)
  {
    if ((paramString == null) || (paramString.isEmpty()))
      return;
    int l = (paramk == null) ? 2147483647 : paramk.b();
    if (paramString.length() == 1)
    {
      this.j.put(paramk, new com.stanfy.contest.a.d(l, Character.valueOf(paramString.charAt(0)), null));
      return;
    }
    this.j.put(paramk, new com.stanfy.contest.a.d(l, Character.valueOf(paramString.charAt(0)), Character.valueOf(paramString.charAt(1))));
  }

  public final com.stanfy.contest.b.c j()
  {
    return this.c;
  }

  public final void a(String paramString)
  {
    a.warn("Someone calls model controller parse without player. This can be BAD");
  }

  public final void a(com.stanfy.contest.a.a.b.b paramb)
  {
    a.debug("This module cannot be initalized or switched");
  }

  public final void b(com.stanfy.contest.a.a.b.b paramb)
  {
    a.debug("This module cannot be initalized or switched");
  }
}