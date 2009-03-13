package com.stanfy.contest.b;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

public final class i
{
  private List a = new ArrayList();
  private m[][] b;
  private m[][] c;
  private d d;

  public i(InputStream paramInputStream, d paramd)
  {
    this.d = paramd;
    paramInputStream = new BufferedReader(new InputStreamReader(paramInputStream));
    i locali = this;
    this.b = new m[locali.d.g()][(locali = this).d.f()];
    for (int i = 0; i < paramd.g(); ++i)
    {
      String str = paramInputStream.readLine();
      for (int j = 0; j < Math.min(str.length(), paramd.f()); ++j)
      {
        this.b[i][j] = new m();
        switch (str.charAt(j))
        {
        case 'w':
          this.b[i][j].add(new g(i, j, paramd));
          break;
        case 'X':
          this.b[i][j].add(new t(i, j, paramd));
          break;
        case 'o':
          this.a.add(new j(i, j, paramd));
        case '.':
        }
      }
    }
    this.c = a(this.b);
    paramInputStream.close();
    g();
  }

  public final int a()
  {
    return this.d.f();
  }

  public final int b()
  {
    return this.d.g();
  }

  public final int c()
  {
    return this.d.h();
  }

  public final m a(int paramInt1, int paramInt2)
  {
    if ((paramInt1 < 0) || (paramInt2 < 0) || (paramInt1 >= this.b.length) || (paramInt2 >= this.b[0].length))
      return null;
    return this.b[paramInt1][paramInt2];
  }

  private void g()
  {
    Iterator localIterator = this.a.iterator();
    while (localIterator.hasNext())
    {
      j localj1;
      (localj1 = (j)localIterator.next()).a(true);
    }
    for (int i = 0; i < this.a.size() << 1; ++i)
    {
      int j = (int)(Math.random() * this.a.size());
      int k = (int)(Math.random() * this.a.size());
      j localj2 = (j)this.a.get(j);
      j localj3 = (j)this.a.get(k);
      this.a.set(j, localj3);
      this.a.set(k, localj2);
    }
  }

  public final j d()
  {
    this = this.a.iterator();
    while (hasNext())
    {
      j localj;
      if ((localj = (j)next()).a())
      {
        localj.a(false);
        return localj;
      }
    }
    throw new Error("All starting points are filled");
  }

  public final List a(List paramList, b paramb)
  {
    ArrayList localArrayList = new ArrayList();
    int i = paramb.d();
    int j = paramb.e();
    int k = 0;
    int l = 0;
    int i1 = 0;
    int i2 = 0;
    for (int i3 = 0; i3 <= paramb.b(); ++i3)
    {
      Object localObject;
      k = ((k != 0) || (i - i3 < 0)) ? 1 : 0;
      if (l == 0);
      l = (i + i3 >= (localObject = this).d.f()) ? 1 : 0;
      i1 = ((i1 != 0) || (j - i3 < 0)) ? 1 : 0;
      if (i2 == 0);
      i2 = (j + i3 >= (localObject = this).d.g()) ? 1 : 0;
      if (k == 0)
      {
        localObject = a(j, i - i3);
        a(localArrayList, (m)localObject);
        a(localArrayList, paramList, j, i - i3);
        k = ((k != 0) || (!(((m)localObject).b()))) ? 1 : 0;
      }
      if (l == 0)
      {
        localObject = a(j, i + i3);
        a(localArrayList, (m)localObject);
        a(localArrayList, paramList, j, i + i3);
        l = ((l != 0) || (!(((m)localObject).b()))) ? 1 : 0;
      }
      if (i1 == 0)
      {
        localObject = a(j - i3, i);
        a(localArrayList, (m)localObject);
        a(localArrayList, paramList, j - i3, i);
        i1 = ((i1 != 0) || (!(((m)localObject).b()))) ? 1 : 0;
      }
      if (i2 == 0)
      {
        localObject = a(j + i3, i);
        a(localArrayList, (m)localObject);
        a(localArrayList, paramList, j + i3, i);
        i2 = ((i2 != 0) || (!(((m)localObject).b()))) ? 1 : 0;
      }
    }
    return ((List)localArrayList);
  }

  private static void a(List paramList, m paramm)
  {
    paramm = paramm.iterator();
    while (paramm.hasNext())
    {
      h localh;
      if (((((localh = (h)paramm.next()) instanceof g) || (localh instanceof o) || (localh instanceof f))) && (!(paramList.contains(localh))))
        paramList.add(localh);
    }
  }

  private static void a(List paramList1, List paramList2, int paramInt1, int paramInt2)
  {
    paramList2 = paramList2.iterator();
    while (true)
    {
      k localk;
      do
        if (!(paramList2.hasNext()))
          return;
      while (!((localk = (k)paramList2.next()).m()));
      if ((!(paramList1.contains(localk))) && (localk.e() == paramInt1) && (localk.d() == paramInt2))
        paramList1.add(localk);
    }
  }

  public final boolean a(h paramh)
  {
    return a(paramh.e(), paramh.d()).remove(paramh);
  }

  public final boolean b(h paramh)
  {
    return a(paramh.e(), paramh.d()).add(paramh);
  }

  public final void e()
  {
    this.b = a(this.c);
    g();
  }

  private m[][] a(m[][] paramArrayOfm)
  {
    int i = (localObject = this).d.f();
    this = (localObject = this).d.g();
    Object localObject = new m[i][this];
    for (i locali = 0; locali < this; ++locali)
      for (int j = 0; j < i; ++j)
        localObject[locali][j] = paramArrayOfm[locali][j].e();
    return ((m)localObject);
  }

  public final int f()
  {
    return this.a.size();
  }
}