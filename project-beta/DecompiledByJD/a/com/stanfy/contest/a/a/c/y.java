package com.stanfy.contest.a.a.c;

public final class y extends Exception
{
  public y(String paramString)
  {
    super(paramString);
  }

  public static final y a()
  {
    return new y("Not enough data in stack.");
  }

  public static final y b()
  {
    return new y("Divizion by zero.");
  }

  public static final y a(String paramString)
  {
    return new y("Data in stack must be int(" + paramString + ").");
  }

  public static y b(String paramString)
  {
    return new y("Unknown variable(" + paramString + ")");
  }

  public static y c(String paramString)
  {
    return new y("Unexpected word (" + paramString + ")");
  }

  public static y a(String paramString, int paramInt1, int paramInt2)
  {
    return new y("Incorrect String index (" + paramInt1 + "" + ") at " + paramString);
  }
}