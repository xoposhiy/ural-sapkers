package com.stanfy.contest.a.a.a;

import com.stanfy.contest.a.a.a;
import com.stanfy.contest.a.a.d;
import com.stanfy.contest.a.a.h;
import com.stanfy.contest.a.a.i;
import com.stanfy.contest.a.a.j;
import com.stanfy.contest.a.a.k;
import com.stanfy.contest.a.a.l;
import com.stanfy.contest.a.a.o;
import com.stanfy.contest.f;
import java.util.HashMap;
import org.apache.log4j.Logger;

public final class b
{
  private static final Logger a = Logger.getLogger(b.class);
  private static b b;
  private HashMap c = new HashMap();
  private HashMap d = new HashMap();

  public static b a()
  {
    if (b == null)
    {
      b localb;
      (localb = b.b = new b()).a(o.class, "memConfig", com.stanfy.contest.a.a.c.class);
      localb.a(o.class, "launch", f.class);
      localb.a(com.stanfy.contest.a.a.c.class, "passwordIsOk", l.class);
      localb.a(l.class, "dnaLab", h.class);
      localb.a(l.class, "sattelite", d.class);
      localb.a(l.class, "enginecontrol", k.class);
      localb.a(l.class, "lexicalinterpreter", i.class);
      localb.a(l.class, "fifthModule", j.class);
      localb.a(l.class, "bfModule", com.stanfy.contest.a.a.b.class);
      localb.a(l.class, "xorModule", a.class);
      localb.a(j.class, "statusExit", l.class);
      localb.a(k.class, "statusExit", l.class);
      localb.a(h.class, "statusExit", l.class);
      localb.a(d.class, "statusExit", l.class);
      localb.a(i.class, "statusExit", l.class);
      localb.a(com.stanfy.contest.a.a.b.class, "statusExit", l.class);
      localb.a(a.class, "statusExit", l.class);
      localb.a(l.class, "statusExit", o.class);
    }
    return b;
  }

  public final com.stanfy.contest.a.a.b.c a(com.stanfy.contest.a.a.b.b paramb, com.stanfy.contest.a.a.b.c paramc, String paramString)
  {
    Object localObject;
    a.debug("Getting next module for " + paramc.getClass().getSimpleName() + " and exit result " + paramString);
    if ((localObject = (HashMap)this.c.get(paramc.getClass())) == null)
    {
      a.error("Cannot find any exit results for " + paramc.getClass().getSimpleName());
      return null;
    }
    if ((localObject = (Class)((HashMap)localObject).get(paramString)) == null)
    {
      a.error("Cannot find any next module class for " + paramc.getClass().getSimpleName() + " and exit result " + paramString);
      return null;
    }
    return ((com.stanfy.contest.a.a.b.c)a(paramb, (Class)localObject));
  }

  public final com.stanfy.contest.a.a.b.c a(com.stanfy.contest.a.a.b.b paramb, Class paramClass)
  {
    HashMap localHashMap;
    a.debug("Getting " + paramClass.getSimpleName() + " module instance for player " + paramb);
    if ((localHashMap = (HashMap)this.d.get(paramb)) == null)
    {
      a.debug("Such player is not registered yet, adding instances map for him");
      localHashMap = new HashMap();
      this.d.put(paramb, localHashMap);
    }
    if ((this = (com.stanfy.contest.a.a.b.c)localHashMap.get(paramClass)) == null)
    {
      a.debug("Module instance is not instantiated. Creating new " + paramClass.getSimpleName() + " module instance");
      if (paramClass == f.class)
      {
        this = f.h();
        a.debug("Received modelController : " + this);
      }
      else
      {
        try
        {
          (this = (com.stanfy.contest.a.a.b.c)paramClass.newInstance()).b(paramb);
        }
        catch (Exception this)
        {
          a.error("Cannot instantiate module : " + getMessage());
          return null;
        }
      }
      localHashMap.put(paramClass, this);
    }
    return this;
  }

  private void a(Class paramClass1, String paramString, Class paramClass2)
  {
    HashMap localHashMap;
    if ((localHashMap = (HashMap)this.c.get(paramClass1)) == null)
    {
      a.debug("Currently there's no forwarding rules for " + paramClass1);
      localHashMap = new HashMap();
      this.c.put(paramClass1, localHashMap);
    }
    localHashMap.put(paramString, paramClass2);
    a.debug("Rule added : " + paramClass1.getSimpleName() + "(" + paramString + ") -> " + paramClass2.getSimpleName());
  }
}