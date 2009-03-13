package com.stanfy.contest.b;

import com.stanfy.contest.c.f;
import java.util.LinkedList;
import org.apache.log4j.Logger;

public class v
{
  private static final Logger a = Logger.getLogger(v.class);
  private static final f b = new u();
  private int c = 0;
  private int d = -1;
  private LinkedList e = new LinkedList();

  public final int a()
  {
    return this.c;
  }

  public final void b()
  {
    this.c += 1;
  }

  public final f c()
  {
    if (this.d == this.c)
      throw new a("Server time was not changed!");
    this.d = this.c;
    if (this.e.size() > 0)
      return ((f)this.e.removeFirst());
    return b;
  }

  public final void a(int paramInt, f paramf)
  {
    f localf;
    if (paramInt < 0)
      throw new a("Incorrect delay.");
    paramf = (paramf == null) ? b : paramf;
    int i = this.e.size() - paramInt;
    if (a.isDebugEnabled())
      a.debug("delay: " + paramInt);
    if (i <= 0)
    {
      if (a.isDebugEnabled())
        a.debug("dif: " + i);
      for (int j = 0; j < -i; ++j)
        this.e.add(b);
      this.e.add(paramf);
      return;
    }
    if ((localf = (f)this.e.get(paramInt)) == b)
    {
      this.e.set(paramInt, paramf);
      return;
    }
    com.stanfy.contest.c.a locala = new com.stanfy.contest.c.a(localf);
    this.e.set(paramInt, locala.a(paramf));
  }

  public final void d()
  {
    this.c = 0;
    this.d = -1;
    this.e.clear();
  }
}