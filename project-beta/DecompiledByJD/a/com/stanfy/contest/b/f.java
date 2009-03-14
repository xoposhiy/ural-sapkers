package com.stanfy.contest.b;

public final class f extends h
{
  private k a;
  private long b;
  private int c;

  public f(k paramk, int paramInt1, int paramInt2, long paramLong)
  {
    super(paramInt1, paramInt2, paramk.g());
    this.b = paramLong;
    this.c = paramk.j();
    this.a = paramk;
  }

  public final long a()
  {
    return this.b;
  }

  public final int b()
  {
    return this.c;
  }

  public final char a(k paramk)
  {
    if ((paramk != null) && (!(paramk.u().m())))
      return '?';
    return '*';
  }

  public final k c()
  {
    return this.a;
  }
}