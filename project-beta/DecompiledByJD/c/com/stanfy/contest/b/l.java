package com.stanfy.contest.b;

public final class l extends RuntimeException
{
  public l(String paramString)
  {
    super(paramString);
  }

  public l(Throwable paramThrowable)
  {
    super("Map configuration is not correct!", paramThrowable);
  }
}