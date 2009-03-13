package com.stanfy.contest;

import com.stanfy.contest.a.b.b;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import org.apache.log4j.Logger;

public final class g extends c
{
  private static final Logger a = Logger.getLogger(g.class);
  private List b;
  private OutputStream c = null;
  private OutputStream d = null;
  private ServerSocket e = null;
  private LinkedList f = new LinkedList();
  private boolean g = false;

  public g(File paramFile1, File paramFile2, int paramInt)
  {
    try
    {
      if (paramFile1 != null)
        this.c = new FileOutputStream(paramFile1);
      if (paramFile2 != null)
        this.d = new FileOutputStream(paramFile2);
      if (paramInt > 0)
      {
        a.info("Create visualizer server socket on " + paramInt);
        this.e = new ServerSocket(paramInt);
      }
    }
    catch (IOException paramFile1)
    {
      a.error("Cannot create some trace out.", paramFile1);
    }
    b("Tracer");
  }

  public final void a(a parama)
  {
    if (this.b == null)
      this.b = new ArrayList();
    this.b.add(parama);
  }

  public final void b(boolean paramBoolean)
  {
    this.g = paramBoolean;
  }

  public final void a(b paramb, String paramString)
  {
    if (paramString == null)
      return;
    if (this.c != null)
      try
      {
        this.c.write(paramString.getBytes());
      }
      catch (IOException localIOException1)
      {
        a.error("Cannot write to the trace.", localIOException1);
      }
    if ((this.d != null) && (paramb instanceof com.stanfy.contest.a.b.a))
      try
      {
        this.d.write(paramString.getBytes());
      }
      catch (IOException localIOException2)
      {
        a.error("Cannot write to the results.", localIOException2);
      }
    if (this.e != null)
    {
      LinkedList localLinkedList = new LinkedList();
      paramb = this.f.iterator();
      while (paramb.hasNext())
      {
        Socket localSocket = (Socket)paramb.next();
        a.debug("Send to visualizer...");
        if (!(a(localSocket, paramString)))
          localLinkedList.add(localSocket);
      }
      this.f.removeAll(localLinkedList);
    }
  }

  public static boolean a(Socket paramSocket, String paramString)
  {
    try
    {
      paramSocket.getOutputStream().write(paramString.getBytes());
    }
    catch (IOException localIOException)
    {
      a.error("Error while sendind data to the visualizer(socket)");
      return false;
    }
    return true;
  }

  public final void e()
  {
    if (this.e != null)
    {
      a(false);
      try
      {
        this.e.close();
      }
      catch (IOException localIOException)
      {
        a.error("Failed to close the server", localIOException);
      }
    }
    super.e();
  }

  protected final void f()
  {
    if (this.b != null)
      this.b.clear();
    try
    {
      if (this.c != null)
        this.c.close();
    }
    catch (IOException localIOException1)
    {
      a.error("Cannot close traceOut.", localIOException1);
    }
    try
    {
      if (this.c != null)
        this.d.close();
      return;
    }
    catch (IOException localIOException2)
    {
      a.error("Cannot close resultOut.", localIOException2);
    }
  }

  public final void h()
  {
    a.info("Wait for the first visualizer");
    while ((this.g) && (this.f.size() == 0))
    {
      a.info("Tracer is waiting...");
      c();
    }
  }

  protected final void g()
  {
    if (b_())
      try
      {
        Socket localSocket = this.e.accept();
        a.info("New visualizer!");
        Iterator localIterator = this.b.iterator();
        while (localIterator.hasNext())
        {
          a locala;
          (locala = (a)localIterator.next()).a(this, localSocket);
        }
        this.f.add(localSocket);
        b();
      }
      catch (IOException localIOException)
      {
        a.info(localIOException.getMessage());
      }
  }
}