package com.stanfy.contest.b;

import java.io.InputStream;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;

public final class c
{
  private List a = new ArrayList();
  private i b;
  private d c;
  private v d;
  private int[] e;
  private boolean f = false;
  private int g = 0;

  public c(InputStream paramInputStream1, InputStream paramInputStream2)
  {
    this.c = new d(paramInputStream2);
    this.b = new i(paramInputStream1, this.c);
    this.d = new v();
    this.e = new int[this.b.f()];
  }

  public final i a()
  {
    return this.b;
  }

  public final v b()
  {
    return this.d;
  }

  public final int c()
  {
    return this.g;
  }

  public final void a(int paramInt)
  {
    this.g = paramInt;
  }

  public final String toString()
  {
    return a(true, null);
  }

  public final String a(boolean paramBoolean, k paramk)
  {
    StringBuilder localStringBuilder;
    e locale1 = (paramk == null) ? null : paramk.u();
    (localStringBuilder = new StringBuilder()).append(this.b.c()).append("\r\n");
    int i = localStringBuilder.length();
    int j = this.b.a();
    int k = this.b.b();
    LinkedList localLinkedList = new LinkedList();
    for (e locale3 = 0; locale3 < k; ++locale3)
    {
      for (int l = 0; l < j; ++l)
      {
        Object localObject;
        int i1 = 0;
        if ((paramBoolean) && (((paramk == null) || (paramk.u().g()))))
        {
          localObject = this.a.iterator();
          while (true)
          {
            k localk;
            do
            {
              if (!(((Iterator)localObject).hasNext()))
                break label213;
              localk = (k)((Iterator)localObject).next();
            }
            while ((i1 != 0) || (!(localk.m())));
            if ((localk.d() == l) && (localk.e() == locale3))
            {
              localStringBuilder.append((localk.s()) ? "I" : Integer.valueOf(localk.b()));
              i1 = 1;
            }
          }
        }
        if (i1 == 0)
        {
          if ((localObject = this.b.a(locale3, l).a()) instanceof b)
            label213: localLinkedList.add((b)localObject);
          localStringBuilder.append(((h)localObject).a(paramk));
        }
      }
      if ((locale1 == null) || (locale1.c()))
        localStringBuilder.append("\r\n");
    }
    if (paramBoolean)
    {
      locale3 = j + "\r\n".length();
      Iterator localIterator = localLinkedList.iterator();
      while (localIterator.hasNext())
      {
        b localb;
        boolean bool = (localb = (b)localIterator.next()).b();
        e locale4 = localb.e() * locale3 + localb.d() + i;
        paramBoolean = false;
        for (locale1 = locale4; (paramBoolean <= bool) && (localb.d() - paramBoolean >= 0) && (this.b.a(localb.e(), localb.d() - paramBoolean).d()); --locale1)
        {
          localStringBuilder.setCharAt(locale1, localb.a(paramk));
          if (!(this.b.a(localb.e(), localb.d() - paramBoolean).b()))
            break;
          ++paramBoolean;
        }
        paramBoolean = false;
        for (locale1 = locale4; (paramBoolean <= bool) && (localb.d() + paramBoolean < j) && (this.b.a(localb.e(), localb.d() + paramBoolean).d()); ++locale1)
        {
          localStringBuilder.setCharAt(locale1, localb.a(paramk));
          if (!(this.b.a(localb.e(), localb.d() + paramBoolean).b()))
            break;
          ++paramBoolean;
        }
        paramBoolean = false;
        locale1 = locale4;
        while ((paramBoolean <= bool) && (localb.e() - paramBoolean >= 0) && (this.b.a(localb.e() - paramBoolean, localb.d()).d()))
        {
          localStringBuilder.setCharAt(locale1, localb.a(paramk));
          if (!(this.b.a(localb.e() - paramBoolean, localb.d()).b()))
            break;
          ++paramBoolean;
          locale1 -= locale3;
        }
        paramBoolean = false;
        e locale2 = locale4;
        while ((paramBoolean <= bool) && (localb.e() + paramBoolean < k) && (this.b.a(localb.e() + paramBoolean, localb.d()).d()))
        {
          localStringBuilder.setCharAt(locale2, localb.a(paramk));
          if (!(this.b.a(localb.e() + paramBoolean, localb.d()).b()))
            break;
          ++paramBoolean;
          locale2 += locale3;
        }
      }
    }
    return ((String)localStringBuilder.toString());
  }

  public final k d()
  {
    int i = e().size();
    k localk = new k(i, this.c);
    e().add(localk);
    b(localk);
    return localk;
  }

  private void b(k paramk)
  {
    this = this.b.d();
    paramk.a(d());
    paramk.b(e());
  }

  public final List e()
  {
    if (this.a == null)
      this.a = new ArrayList();
    return this.a;
  }

  public final k b(int paramInt)
  {
    if ((this.a == null) || (this.a.size() <= paramInt))
      return null;
    return ((k)this.a.get(paramInt));
  }

  public final void f()
  {
    this.f = false;
    this.b.e();
    this.d.d();
    this.g = 0;
    Iterator localIterator = this.a.iterator();
    while (localIterator.hasNext())
    {
      k localk;
      (localk = (k)localIterator.next()).a();
      b(localk);
    }
  }

  public final int g()
  {
    return this.c.e();
  }

  public final void a(int paramInt1, int paramInt2)
  {
    this.e[paramInt1] += paramInt2;
  }

  public final int[] h()
  {
    return this.e;
  }

  public final void a(k paramk)
  {
    paramk.a(false);
    paramk = 0;
    Iterator localIterator = this.a.iterator();
    while (localIterator.hasNext())
    {
      k localk;
      if ((localk = (k)localIterator.next()).m())
        ++paramk;
    }
    this.f = (paramk < 2);
  }

  public final boolean i()
  {
    return ((this.f) || (this.d.a() >= 3600));
  }

  public final d j()
  {
    return this.c;
  }

  public final List b(int paramInt1, int paramInt2)
  {
    LinkedList localLinkedList = new LinkedList();
    this = this.a.iterator();
    while (hasNext())
    {
      k localk;
      if (((localk = (k)next()).e() == paramInt1) && (localk.d() == paramInt2))
        localLinkedList.add(localk);
    }
    return localLinkedList;
  }
}