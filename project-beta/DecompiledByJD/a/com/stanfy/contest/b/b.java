package com.stanfy.contest.b;

public final class b extends h
{
  private f a;
  private int b = 3;

  public b(f paramf, int paramInt)
  {
    super(paramf.e(), paramf.d(), paramf.g());
    this.a = paramf;
  }

  public final int a()
  {
    return this.b;
  }

  public final int b()
  {
    return this.a.b();
  }

  public final char a(k paramk)
  {
    if ((paramk != null) && (!(paramk.u().n())))
      return '?';
    return '#';
  }

  public final f c()
  {
    return this.a;
  }
}