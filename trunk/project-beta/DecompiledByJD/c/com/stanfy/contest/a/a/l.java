package com.stanfy.contest.a.a;

public class l extends u
{
  public final void a(com.stanfy.contest.a.a.b.b paramb)
  {
    super.a(paramb);
    c();
  }

  public final void a(String paramString)
  {
    if (this.b)
      return;
    i locali = (i)a(i.class);
    if (paramString.equals("engine"))
    {
      b("enginecontrol");
      return;
    }
    if (paramString.equals("lexinterpr"))
    {
      b("lexicalinterpreter");
      return;
    }
    if (locali.d())
    {
      if (paramString.equals("dnalab"))
      {
        b("dnaLab");
        return;
      }
      if (paramString.equals("sattelite"))
      {
        b("sattelite");
        return;
      }
      if (paramString.equals("fifth"))
      {
        b("fifthModule");
        return;
      }
      if (paramString.equals("aar"))
      {
        b("bfModule");
        return;
      }
      if (paramString.equals("clock"))
      {
        b("xorModule");
        return;
      }
    }
    if (paramString.equals("quit"))
    {
      b("statusExit");
      return;
    }
    if (paramString.equals("status"))
    {
      c();
      return;
    }
    c(g());
  }

  private void c()
  {
    c("\r\n= System status = \r\n");
    a(new String[] { "Getting system status", ".", ".", "..", "...", ".", ".\r\n" });
    i locali = (i)a(i.class);
    c(" * <lexinterpr> " + locali.c() + "\r\n");
    c(" * <engine>     " + ((k)a(k.class)).c() + "\r\n");
    if (locali.d())
    {
      c(" * <dnalab>     " + ((h)a(h.class)).d() + "\r\n");
      c(" * <sattelite>  " + ((d)a(d.class)).e() + "\r\n");
      c(" * <fifth>      " + ((j)a(j.class)).c() + "\r\n");
      c(" * <aar>        " + ((b)a(b.class)).a() + "\r\n");
      c(" * <clock>      " + ((a)a(a.class)).a() + "\r\n");
    }
    c("System status check DONE." + g());
  }

  public final String b()
  {
    return "sys-stat";
  }
}