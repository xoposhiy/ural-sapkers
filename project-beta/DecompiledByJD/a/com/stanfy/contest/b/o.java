package com.stanfy.contest.b;

public final class o extends h
{
  private s a;
  private boolean b;

  public o(int paramInt1, int paramInt2, boolean paramBoolean, d paramd, s params)
  {
    super(paramInt1, paramInt2, paramd);
    this.a = params;
    this.b = paramBoolean;
  }

  public final char a(k paramk)
  {
    if ((paramk != null) && (((!(paramk.u().o())) || ((this.b) && (!(paramk.u().p()))))))
      return '?';
    if (this.b)
      return '?';
    return s.a(this.a);
  }

  public final s a()
  {
    return this.a;
  }

  public final void b(k paramk)
  {
    this.a.a(paramk);
  }

  public final void c(k paramk)
  {
    this.a.b(paramk);
  }
}