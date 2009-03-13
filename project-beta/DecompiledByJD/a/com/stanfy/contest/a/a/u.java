package com.stanfy.contest.a.a;

import com.stanfy.contest.a.a.a.a;
import com.stanfy.contest.a.a.b.b;
import com.stanfy.contest.a.a.b.c;
import com.stanfy.contest.b.e;
import com.stanfy.contest.b.k;
import java.util.HashMap;

public abstract class u
  implements c
{
  protected b a;
  protected boolean b;

  public HashMap a_()
  {
    return new HashMap();
  }

  public final void b(b paramb)
  {
    this.a = paramb;
  }

  public void a(b paramb)
  {
    this.a = paramb;
  }

  public final void b(String paramString)
  {
    if (this.a != null)
      this.a.a(this, paramString);
  }

  public final c a(Class paramClass)
  {
    if (this.a == null)
      return null;
    return this.a.a(paramClass);
  }

  public final void c(String paramString)
  {
    if (this.a != null)
      this.a.b(this, paramString);
  }

  public final String d(String paramString)
  {
    if (this.a != null)
      return a.a(false, paramString, this.a.a().v());
    return a.a(false, paramString, "");
  }

  public final String e(String paramString)
  {
    if (this.a != null)
      return a.a(true, paramString, this.a.a().v());
    return a.a(true, paramString, "");
  }

  public final void a(String[] paramArrayOfString)
  {
    a(paramArrayOfString, 500L);
  }

  public final void a(String[] paramArrayOfString, long paramLong)
  {
    this.b = true;
    int i = (paramArrayOfString = paramArrayOfString).length;
    for (int j = 0; j < i; ++j)
    {
      String str = paramArrayOfString[j];
      c(str);
      try
      {
        Thread.currentThread();
        Thread.sleep(()(Math.random() * paramLong));
      }
      catch (InterruptedException localInterruptedException)
      {
      }
    }
    this.b = false;
  }

  protected final void f()
  {
    e locale = this.a.a().u();
    c("Additional sattelites data, will be visible at Sapka launch in format:\r\n  sapkas-info ::= <sapka-info> [ { ',' <sapka-info> } ]\r\n  sapka-info  :: = 'P' <number> <sep> ( " + ((locale.g()) ? "<details>" : "'?'") + " | 'dead' )\r\n" + ((locale.g()) ? "<x-coord> <sep> <y-coord>" : "'?' <sep> '?'") + " <sep> " + ((locale.t()) ? "<annihilators-left>" : "'?'") + " <sep> " + ((locale.s()) ? "<annihilators-strength>" : "'?'") + " <sep> " + ((locale.r()) ? "<speed>" : "'?'") + ((locale.q()) ? " [ <sep> <infected>] " : "") + ((locale.h()) ? "\r\n <x-coord> ::= <number) // coordinate in POINTS \r\n <y-coord> ::= <number) // coordinate in POINTS " : "\r\n <x-coord> ::= <number) // coordinate in CELLS \r\n <y-coord> ::= <number) // coordinate in CELLS "));
  }

  public abstract String b();

  public final String g()
  {
    return "\r\n" + b() + ">";
  }
}