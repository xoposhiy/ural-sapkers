package com.stanfy.contest.a.a.a;

import org.apache.log4j.Logger;

public final class a
{
  private static final Logger a = Logger.getLogger(a.class);

  public static String a(boolean paramBoolean, String paramString1, String paramString2)
  {
    paramString2 = new StringBuilder(paramString2.substring(0, Math.min(80, paramString2.length())));
    int i = 1;
    while (true)
    {
      do
      {
        if (paramString2.length() >= 80)
          break label56;
        paramString2.append(i);
      }
      while ((i += 2) <= 8);
      i -= 8;
    }
    label56: paramString2 = paramString2.toString().getBytes();
    paramString1 = paramString1.toString().getBytes();
    char[] arrayOfChar = new char[20];
    int j = 8726;
    for (int k = 0; k < 20; ++k)
    {
      int l = paramString2[k] * paramString2[(k + 20)] * j + paramString2[(k + 40)] * paramString2[(k + 60)];
      j = (j | l) << 4 ^ (l | paramString1[k] + k);
      arrayOfChar[k] = (char)(64 + ((j < 0) ? -j : j) % 58);
      for (l = 0; ((arrayOfChar[k] >= '[') && (arrayOfChar[k] <= '`')) || (arrayOfChar[k] < '@') || (arrayOfChar[k] > 'z'); ++l)
      {
        int i1 = (char)(64 + (arrayOfChar[k] << 3 + k + 1 + paramString1[k] ^ ((j < 0) ? -j : j) + l) % 58);
        i1 = (char)(64 + (i1 + l) % 58);
        arrayOfChar[k] = i1;
        a.debug("Current resultChar is " + arrayOfChar[k]);
      }
    }
    return ((paramBoolean) ? "CFG" : "DMA") + String.valueOf(arrayOfChar);
  }
}