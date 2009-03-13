package com.stanfy.contest;

import org.apache.log4j.Logger;

final class h extends c
{
  protected final void f()
  {
  }

  protected final synchronized void g()
  {
    f.k().info("Starting awaker for map controller.");
    if (b_())
      this.a.b();
    try
    {
      Thread.sleep(50L);
    }
    catch (InterruptedException localInterruptedException)
    {
      while (true)
        f.k().error("Awake thread failed to sleep.", localInterruptedException);
      f.k().info("Awaker finished.");
    }
  }
}