package com.stanfy.contest;

import java.util.Collection;
import java.util.Map;
import java.util.Set;
import java.util.TreeSet;

final class b
  implements Map
{
  private int[] a;

  private b(int[] paramArrayOfInt, byte paramByte)
  {
    this.a = paramArrayOfInt;
  }

  public final Set keySet()
  {
    TreeSet localTreeSet = new TreeSet();
    for (int i = 0; i < this.a.length; ++i)
      localTreeSet.add(Integer.valueOf(i));
    return localTreeSet;
  }

  public final void clear()
  {
  }

  public final boolean containsKey(Object paramObject)
  {
    return false;
  }

  public final boolean containsValue(Object paramObject)
  {
    return false;
  }

  public final Set entrySet()
  {
    return null;
  }

  public final boolean isEmpty()
  {
    return false;
  }

  public final void putAll(Map paramMap)
  {
  }

  public final int size()
  {
    return 0;
  }

  public final Collection values()
  {
    return null;
  }
}