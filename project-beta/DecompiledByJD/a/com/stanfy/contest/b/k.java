package com.stanfy.contest.b;

import java.util.Arrays;
import java.util.LinkedList;
import java.util.List;

public class k extends h
{
  private int a;
  private String b;
  private int c;
  private int d;
  private int e;
  private int f;
  private int g;
  private int h;
  private boolean i;
  private int[] j;
  private int[] k;
  private e l;

  public k(int paramInt, d paramd)
  {
    super(0, 0, paramd);
    this.a = paramInt;
    this.j = new int[s.values().length];
    this.k = new int[s.values().length];
    this.l = new e();
    a();
  }

  public final void a()
  {
    this.f = 2;
    this.g = 1;
    this.e = g().d();
    this.h = 0;
    this.i = true;
    for (int i1 = 0; i1 < this.j.length; ++i1)
    {
      this.j[i1] = 0;
      this.k[i1] = 0;
    }
  }

  public final int e()
  {
    return (this.d / g().h());
  }

  public final int d()
  {
    return (this.c / g().h());
  }

  public final void b(int paramInt)
  {
    super.b(paramInt);
    int i1 = g().h();
    this.d = (paramInt * i1 + this.d % i1);
  }

  public final void a(int paramInt)
  {
    super.a(paramInt);
    int i1 = g().h();
    this.c = (paramInt * i1 + this.c % i1);
  }

  public final int b()
  {
    return this.a;
  }

  public final int c()
  {
    return this.c;
  }

  public final void c(int paramInt)
  {
    this.c = paramInt;
  }

  public final int h()
  {
    return this.d;
  }

  public final void d(int paramInt)
  {
    this.d = paramInt;
  }

  public final int i()
  {
    if (!(a(s.b)))
      return this.e;
    if ((this = g().d() >> 1) > 0)
      return this;
    return 1;
  }

  public final void e(int paramInt)
  {
    if (!(this.l.y()))
      return;
    if (paramInt < g().i())
      this.e = paramInt;
  }

  public final int j()
  {
    if (a(s.d))
      return 0;
    return this.f;
  }

  public final void f(int paramInt)
  {
    if (!(this.l.w()))
      return;
    this.f = paramInt;
  }

  public final int k()
  {
    return this.g;
  }

  public final void g(int paramInt)
  {
    if (!(this.l.x()))
      return;
    this.g = paramInt;
  }

  public final int l()
  {
    return this.h;
  }

  public final boolean m()
  {
    return this.i;
  }

  public final void a(boolean paramBoolean)
  {
    this.i = false;
  }

  public final boolean n()
  {
    return ((!(a(s.c))) && (this.g - this.h > 0));
  }

  public final void o()
  {
    this.h += 1;
  }

  public final void p()
  {
    this.h -= 1;
  }

  public final boolean q()
  {
    return (!(a(s.a)));
  }

  private boolean a(s params)
  {
    return (this.j[params.ordinal()] > 0);
  }

  public final void a(s params, boolean paramBoolean)
  {
    params = params.ordinal();
    if (paramBoolean)
      this.j[params] += 1;
    else
      this.j[params] -= 1;
    if ((!(m)) && (this.j[params] < 0))
      throw new AssertionError();
  }

  public final void a(s params, int paramInt)
  {
    this.k[params.ordinal()] = paramInt;
  }

  public final int[] r()
  {
    return this.k;
  }

  public final List b(k paramk)
  {
    s[] arrayOfs;
    LinkedList localLinkedList = new LinkedList();
    int i1 = (arrayOfs = s.e).length;
    for (int i2 = 0; i2 < i1; ++i2)
    {
      s locals = arrayOfs[i2];
      if ((a(locals)) && (!(paramk.a(locals))))
      {
        paramk.j[locals.ordinal()] += 1;
        paramk.k[locals.ordinal()] = this.k[locals.ordinal()];
        localLinkedList.add(locals);
      }
    }
    return localLinkedList;
  }

  public final boolean s()
  {
    s[] arrayOfs;
    int i1 = (arrayOfs = s.e).length;
    for (int i2 = 0; i2 < i1; ++i2)
    {
      s locals = arrayOfs[i2];
      if (a(locals))
        return true;
    }
    return false;
  }

  public final String t()
  {
    s[] arrayOfs;
    StringBuilder localStringBuilder = new StringBuilder();
    int i1 = (arrayOfs = s.e).length;
    for (int i2 = 0; i2 < i1; ++i2)
    {
      s locals = arrayOfs[i2];
      if (a(locals))
        localStringBuilder.append(locals.a());
    }
    return localStringBuilder.toString();
  }

  public final String toString()
  {
    return "BPlayer(" + this.b + "){posX=" + this.c + ", posY=" + this.d + ", bonusStatus=" + Arrays.toString(this.j) + ", healTicks=" + Arrays.toString(this.k) + "}";
  }

  public final e u()
  {
    return this.l;
  }

  public final String v()
  {
    return this.b;
  }

  public final void a(String paramString)
  {
    this.b = paramString;
  }
}