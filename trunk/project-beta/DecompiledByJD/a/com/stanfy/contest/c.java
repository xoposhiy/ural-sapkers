package com.stanfy.contest;

import org.apache.log4j.Logger;

public abstract class c
  implements Runnable
{
  private static final Logger a = Logger.getLogger(c.class);
  private boolean b;
  private boolean c;

  public final void b(String paramString)
  {
    new Thread(this, super.getClass().getName() + " " + paramString).start();
  }

  public void run()
  {
    this.c = true;
    this.b = true;
    g();
    h();
  }

  private synchronized void h()
  {
    a.info("Shutdown worker " + super.getClass().getName());
    f();
    a.info("Shutdown of " + super.getClass().getName() + " was performed.");
    this.c = false;
    super.notify();
  }

  protected final boolean b_()
  {
    return this.b;
  }

  protected final void a(boolean paramBoolean)
  {
    this.b = false;
  }

  protected final synchronized void b()
  {
    super.notify();
  }

  protected final synchronized void c()
  {
    try
    {
      super.wait();
      return;
    }
    catch (InterruptedException this)
    {
      a.error("Map controller failed to wait.", this);
    }
  }

  protected final synchronized void d()
  {
    if (this.b)
    {
      a.info(super.getClass().getName() + ": Worker cycle is finished by still active.");
      try
      {
        super.wait(1000L);
      }
      catch (InterruptedException localInterruptedException)
      {
        a.error("Failed wait while active.", localInterruptedException);
      }
    }
  }

  public void e()
  {
    a.info("Stopping worker " + super.getClass().getName());
    this.b = false;
    if (this.c)
      try
      {
        synchronized (this)
        {
          super.wait(1000L);
        }
      }
      catch (InterruptedException localInterruptedException)
      {
        a.error("Failed wait for shutdown.", localInterruptedException);
      }
  }

  protected abstract void f();

  protected abstract void g();
}