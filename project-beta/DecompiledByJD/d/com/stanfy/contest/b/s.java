package com.stanfy.contest.b;

public enum s
{
  f, g, h, a, b, c, d;

  public static final s[] e;
  private boolean i;
  private char j;
  private double k;

  protected void a(k paramk)
  {
    paramk.a(this, true);
  }

  protected final void b(k paramk)
  {
    paramk.a(this, false);
  }

  public final char a()
  {
    return this.j;
  }

  public final boolean b()
  {
    return this.i;
  }

  public final double c()
  {
    return this.k;
  }

  static
  {
    e = { a, b, c, d };
  }
}