package com.stanfy.contest.a.a;

import com.stanfy.contest.a.a.a.a.n;
import com.stanfy.contest.a.a.a.b.a;
import com.stanfy.contest.b.k;
import com.stanfy.contest.f;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.Iterator;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import org.apache.log4j.Logger;

public class o extends u
{
  private static final Pattern c = Pattern.compile("^launch\\s+(.+?)\\s+(.+?)$");
  private static final Logger d = Logger.getLogger(o.class);
  private static com.stanfy.contest.a.a.a.c e;
  private String f = null;
  private static Pattern g = Pattern.compile("config\\s+(.*)");
  private static Pattern h = Pattern.compile("dma\\s+(.*)");
  private static Pattern i = Pattern.compile("team\\s+(.*)");

  public final void a(com.stanfy.contest.a.a.b.b paramb)
  {
    super.a(paramb);
    c("\r\nLoading... ");
    c("done.\r\n");
    c(" This is preconfiguration module for SAPKA.\r\n");
    if (this.f == null)
    {
      c(" ");
      (paramb = this).c("\r\nPlease, specify your team name in format 'team teamname;'\r\n");
    }
    c(" Use 'help;' command for information." + g());
  }

  private String f(String paramString)
  {
    n localn;
    paramString = paramString.split("#");
    HashSet localHashSet = new HashSet();
    int j = (paramString = paramString).length;
    for (int k = 0; k < j; ++k)
    {
      localObject = paramString[k];
      d.debug("Config token found " + ((String)localObject));
      if ((localn = e.a((String)localObject)) != null)
      {
        localHashSet.add(localn);
      }
      else
      {
        a(new String[] { "\r\n", "checking config tokens", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "." });
        c("\r\nIncorrect configuration token" + g());
        return null;
      }
    }
    paramString = new StringBuilder();
    com.stanfy.contest.b.c localc = ((f)a(f.class)).j();
    k localk = this.a.a();
    Object localObject = localHashSet.iterator();
    while (((Iterator)localObject).hasNext())
    {
      String str = (localn = (n)((Iterator)localObject).next()).a(this.a.a().u());
      if (localn instanceof com.stanfy.contest.a.a.a.a.b)
        localc.a(localk.b(), 20);
      paramString.append("\r\n").append(str);
    }
    localc.a(localk.b(), localHashSet.size() << 1);
    return ((String)paramString.toString());
  }

  private void c()
  {
    this.a.a().a(this.f);
    d.info("getting modulesConfiguration for player " + this.a.a());
    e = com.stanfy.contest.a.a.a.c.a(this.a);
  }

  public final void a(String paramString)
  {
    if (this.b)
      return;
    if ((localObject1 = i.matcher(paramString)).matches())
    {
      this.f = ((Matcher)localObject1).group(1);
      c("\r\nCurrent team name is '" + this.f + "'. Now you are able to configure SAPKA.'" + g());
      c();
      return;
    }
    Object localObject1 = c.matcher(paramString);
    if (d.isDebugEnabled())
      d.debug("input[" + paramString + "] via pattern " + c.pattern());
    if (((Matcher)localObject1).matches())
    {
      this.f = ((Matcher)localObject1).group(1);
      c();
      f(((Matcher)localObject1).group(2));
      b("launch");
      return;
    }
    if (this.f == null)
    {
      (paramString = this).c("\r\nPlease, specify your team name in format 'team teamname;'\r\n");
      c(g());
      return;
    }
    if (paramString.equals("help"))
    {
      c("\r\n ============ SAPKA PRECONFIGURATION HELP =============\r\n");
      c(" All commands are separated by ';' character.\r\n");
      c(" Some commands will need parameters, which can be specified by adding ' ' (space) character.\r\n");
      c(" \r\n");
      c(" To configure SAPKA, input 'config <config-token>[#<config-token>...];'.\r\n");
      c(" All configuration tokens can be found in your manual at page 42. It was reviewed by Caesar.\r\n");
      c(" \r\n");
      c(" To load DMA tokens, input 'dma <dma-token>[#<dma-token>...];'.\r\n");
      c(" \r\n");
      c(" To access memory, input 'memconfig;'\r\n");
      c(" To launch SAPKA, input 'launch;'\r\n");
      c(" You can use 'launch <team-name> <config-token>[#<config-token> ...];'\r\n");
      c(" to launch SAPKA instantly before defining the team name with the 'team' command.'\r\n");
      c(" \r\n");
      c(" NOTE: It's strongly recommended to configure SAPKA before launching,\r\n");
      c("       and always use as many configuration tokens and dma tokens as you can." + g());
      return;
    }
    if (paramString.equals("memconfig"))
    {
      c("\r\nAccessing memory...");
      b("memConfig");
      return;
    }
    if (paramString.equals("launch"))
    {
      c("\r\nLaunching SAPKA...");
      b("launch");
      return;
    }
    if ((localObject1 = g.matcher(paramString)).matches())
      c(f(((Matcher)localObject1).group(1)));
    if ((localObject1 = h.matcher(paramString)).matches())
    {
      Object localObject2;
      paramString = ((Matcher)localObject1).group(1).split("#");
      localObject1 = new ArrayList();
      int j = (paramString = paramString).length;
      for (int k = 0; k < j; ++k)
      {
        localObject2 = paramString[k];
        d.debug("DMA token found " + ((String)localObject2));
        if ((localObject2 = e.b((String)localObject2)) != null)
        {
          ((ArrayList)localObject1).add(localObject2);
        }
        else
        {
          a(new String[] { "\r\n", "checking DMA token", ".", ".", ".", ".", ".", ".", ".", "." });
          c("\r\nIncorrect DMA token" + g());
          return;
        }
      }
      paramString = ((ArrayList)localObject1).iterator();
      while (paramString.hasNext())
      {
        a locala;
        Class localClass = (locala = (a)paramString.next()).a();
        localObject2 = a(localClass);
        localObject2 = locala.a((com.stanfy.contest.a.a.b.c)localObject2);
        c("\r\n" + ((String)localObject2));
      }
    }
    c(g());
  }

  public final String b()
  {
    return "config";
  }
}