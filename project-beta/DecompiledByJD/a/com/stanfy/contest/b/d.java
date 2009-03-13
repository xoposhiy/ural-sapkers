package com.stanfy.contest.b;

import java.io.InputStream;
import java.util.Properties;
import org.apache.log4j.Logger;

public final class d
{
  private static final Logger a = Logger.getLogger(d.class);
  private int b;
  private double[] c;
  private double d;
  private double e;
  private int f;
  private int g;
  private int h;
  private int i;
  private int j;

  public d(InputStream paramInputStream)
  {
    try
    {
      Properties localProperties;
      (localProperties = new Properties()).load(paramInputStream);
      this.b = Integer.parseInt(localProperties.getProperty("rounds.count"));
      a.debug("roundsCount = " + this.b);
      this.f = Integer.parseInt(localProperties.getProperty("width"));
      a.debug("width = " + this.f);
      this.g = Integer.parseInt(localProperties.getProperty("height"));
      a.debug("height = " + this.g);
      this.h = Integer.parseInt(localProperties.getProperty("cell.size"));
      a.debug("cellSize = " + this.h);
      this.c = new double[s.values().length];
      l = (paramInputStream = s.values()).length;
      for (int i1 = 0; i1 < l; ++i1)
      {
        Object localObject = paramInputStream[i1];
        String str = localProperties.getProperty("bonus." + localObject.toString());
        this.c[localObject.ordinal()] = ((str != null) ? Double.parseDouble(str) : localObject.c());
        a.debug(localObject.toString() + " = " + this.c[localObject.ordinal()]);
      }
      paramInputStream = localProperties.getProperty("bonus.unknown");
      this.e = ((paramInputStream != null) ? Double.parseDouble(paramInputStream) : 0.37D);
      a.debug("unknownBonusProbability = " + this.e);
    }
    catch (Exception localException)
    {
      throw new l(localException);
    }
    for (int k = 1; k < this.c.length; ++k)
      this.c[k] += this.c[(k - 1)];
    this.d = this.c[(this.c.length - 1)];
    if (this.d > 0.90000000000000002D)
      throw new l("Probability of bonus appearence is to big: " + this.d);
    this.i = (this.h >> 2);
    if ((l = this.h / this.i) == 0)
      l = 1;
    l <<= 1;
    this.j = (l << 2);
  }

  public final double a()
  {
    return this.e;
  }

  public final double b()
  {
    return this.d;
  }

  public final int c()
  {
    return this.j;
  }

  public final int d()
  {
    return this.i;
  }

  public final int e()
  {
    return this.b;
  }

  public final int f()
  {
    return this.f;
  }

  public final int g()
  {
    return this.g;
  }

  public final int h()
  {
    return this.h;
  }

  public final int i()
  {
    return this.h;
  }

  public final s a(double paramDouble)
  {
    for (int k = 0; k < this.c.length; ++k)
      if (paramDouble < this.c[k])
        return s.values()[k];
    return null;
  }
}