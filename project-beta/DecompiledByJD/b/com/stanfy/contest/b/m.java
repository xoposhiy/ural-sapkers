package com.stanfy.contest.b;

import java.util.ArrayList;
import java.util.Iterator;
import org.apache.log4j.Logger;

public class m extends ArrayList
{
  private static final Logger a = Logger.getLogger(m.class);
  private static final h b = new n(-1, -1, null);

  public m()
  {
  }

  private m(int paramInt)
  {
    super(paramInt);
  }

  public final h a()
  {
    if (size() == 0)
      return b;
    return ((h)get(size() - 1));
  }

  public final boolean a(boolean paramBoolean)
  {
    this = iterator();
    while (hasNext())
    {
      h localh;
      if ((localh = (h)next()) instanceof f)
        return false;
      if (localh instanceof g)
        return false;
      if (localh instanceof t)
        return false;
      if ((!(paramBoolean)) && (localh instanceof b))
        return false;
    }
    return true;
  }

  public final boolean b()
  {
    this = iterator();
    while (hasNext())
    {
      h localh;
      if (((localh = (h)next()) instanceof t) || (localh instanceof f) || (localh instanceof g) || (localh instanceof o))
        return false;
    }
    return true;
  }

  public final boolean c()
  {
    this = iterator();
    while (hasNext())
    {
      h localh;
      if ((localh = (h)next()) instanceof t)
        return true;
    }
    return false;
  }

  public final boolean d()
  {
    return (!(a() instanceof t));
  }

  public final m e()
  {
    m localm = new m(size());
    for (int i = 0; i < size(); ++i)
    {
      h localh = (h)get(i);
      try
      {
        localm.add(localh.f());
      }
      catch (CloneNotSupportedException localCloneNotSupportedException)
      {
        a.error("Cannot clone bcel!!!!!!!!!!", localCloneNotSupportedException);
      }
    }
    return localm;
  }
}