package com.stanfy.contest.a;

import com.stanfy.contest.a.a.b.a;
import com.stanfy.contest.a.a.b.c;
import com.stanfy.contest.a.a.b.d;
import com.stanfy.contest.b.k;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.ServerSocket;
import java.net.Socket;
import org.apache.log4j.Logger;

public class e extends com.stanfy.contest.e
  implements com.stanfy.contest.a.a.b.b, d
{
  private static final Logger a = Logger.getLogger(e.class);
  private int b;
  private k c;
  private ServerSocket d;
  private Socket e;
  private OutputStream f;
  private StringBuilder g = new StringBuilder();
  private f h;
  private c i;

  public e(int paramInt)
  {
    this.b = paramInt;
    b("Client conector " + paramInt);
  }

  public final void a(f paramf)
  {
    this.h = paramf;
  }

  private void a(String paramString)
  {
    if ((((paramString == null) || (paramString.length() == 0))) && (this.g.length() == 0))
      return;
    if (this.f == null)
    {
      this.g.append(paramString);
      a.warn("Client " + this.c + " (port: " + this.b + ") is not ready.");
      return;
    }
    if (a.isDebugEnabled())
      a.debug("Sending data: " + paramString);
    try
    {
      if (this.g.length() > 0)
        this.f.write(this.g.toString().getBytes());
      this.g.delete(0, this.g.length());
      if (paramString != null)
        this.f.write(paramString.toString().getBytes());
      this.f.flush();
      return;
    }
    catch (IOException localIOException)
    {
      this.g.append(paramString);
      a.warn("Unable to send data to the client. PID: " + this.c + ", port: " + this.b);
    }
  }

  private void a(int paramInt)
  {
    j().delete(0, paramInt);
  }

  protected final void i()
  {
    int j;
    if (this.i == null)
    {
      a(j().length());
      return;
    }
    if ((j = j().lastIndexOf(";")) < 0)
      return;
    Object localObject = (localObject = j().substring(0, j)).split(";");
    a(j + 1);
    int k = (localObject = localObject).length;
    for (int l = 0; l < k; ++l)
    {
      for (String str = localObject[l]; str.contains("\b"); str = str.replaceFirst(".?\\x08", ""));
      if (this.i instanceof a)
        ((a)this.i).a(this.c, str);
      else
        this.i.a(str);
    }
    if (j < 1)
    {
      a.warn("Incorrect command => ignore it (port " + this.b + ")");
      a(j + 1);
      return;
    }
  }

  public final void e()
  {
    if (this.d != null)
      try
      {
        this.d.close();
      }
      catch (IOException localIOException)
      {
        a.error("Failed to close the server", localIOException);
      }
    a(false);
    super.e();
  }

  protected final InputStream h()
  {
    try
    {
      this.d = new ServerSocket(this.b);
    }
    catch (java.net.BindException this)
    {
      a.fatal("Cannot init client connector.", this);
      System.exit(1);
      return null;
    }
    this.e = this.d.accept();
    a.debug("Set output for port " + this.b);
    if (this.h != null)
      this.h.i();
    this.f = this.e.getOutputStream();
    a("");
    return this.e.getInputStream();
  }

  protected final void g()
  {
    a.info("Create server socket for port " + this.b);
    super.g();
    a.debug("Client connector work is finished.");
  }

  protected final void f()
  {
    a.info("Shutdown for port " + this.b);
    if (this.f != null)
      try
      {
        this.f.close();
      }
      catch (IOException localIOException1)
      {
        a.error("Cannot close output for port " + this.b, localIOException1);
      }
    if (this.e != null)
      try
      {
        this.e.close();
      }
      catch (IOException localIOException2)
      {
        a.error("Cannot close socket for port " + this.b, localIOException2);
      }
    if (this.d != null)
      try
      {
        this.d.close();
        return;
      }
      catch (IOException localIOException3)
      {
        a.error("Cannot close server for port " + this.b, localIOException3);
      }
  }

  public final void a(c paramc)
  {
    this.i = paramc;
    synchronized (this.i)
    {
      this.i.notifyAll();
    }
    if (paramc != null)
      paramc.a(this);
  }

  public final void b(c paramc, String paramString)
  {
    if (paramc == this.i)
      a(paramString);
  }

  public final k a()
  {
    return this.c;
  }

  public final void a(k paramk)
  {
    this.c = paramk;
  }

  public final void a(c paramc, String paramString)
  {
    if ((paramc = com.stanfy.contest.a.a.a.b.a().a(this, paramc, paramString)) != null)
      a(paramc);
  }

  public final c a(Class paramClass)
  {
    return com.stanfy.contest.a.a.a.b.a().a(this, paramClass);
  }

  public final c l()
  {
    return this.i;
  }
}