package com.stanfy.contest.a.a;

import com.stanfy.contest.a.a.b.b;
import com.stanfy.contest.a.a.c.am;
import com.stanfy.contest.a.a.c.o;
import com.stanfy.contest.a.a.c.y;
import com.stanfy.contest.b.e;
import com.stanfy.contest.b.k;
import java.util.Stack;

final class r extends o
{
  r(j paramj, String paramString)
  {
    super(paramString);
  }

  public final Object a(am paramam)
  {
    com.stanfy.contest.a.a.a.a.j localj;
    Object localObject1 = paramam.b();
    for (int i = 0; i < 20; ++i)
    {
      int j = (int)(Math.random() * 1000.0D);
      int k = (int)(Math.random() * 1000.0D);
      int l = (int)(Math.random() * 50.0D);
      int i1 = (int)(Math.random() * 50.0D);
      int i2 = (int)(Math.random() * 5.0D);
      int i3 = (int)(Math.random() * 5.0D);
      int i4 = (int)(Math.random() * 25.0D) << 1;
      int i5 = (int)(Math.random() * 1000.0D);
      int i6 = (int)(Math.random() * 1000.0D);
      int i7 = (int)(Math.random() * 50.0D);
      int i8 = (int)(Math.random() * 50.0D);
      int i9 = (int)(Math.random() * 5.0D);
      int i10 = (int)(Math.random() * 5.0D);
      int i11 = (int)(Math.random() * 25.0D) << 1;
      int i12 = (int)(Math.random() * 25.0D) << 1;
      ((Stack)localObject1).push(Integer.valueOf(j));
      ((Stack)localObject1).push(Integer.valueOf(k));
      ((Stack)localObject1).push(Integer.valueOf(l));
      ((Stack)localObject1).push(Integer.valueOf(i1));
      ((Stack)localObject1).push(Integer.valueOf(i2));
      ((Stack)localObject1).push(Integer.valueOf(i3));
      ((Stack)localObject1).push(Integer.valueOf(i4));
      ((Stack)localObject1).push(Integer.valueOf(i5));
      ((Stack)localObject1).push(Integer.valueOf(i6));
      ((Stack)localObject1).push(Integer.valueOf(i7));
      ((Stack)localObject1).push(Integer.valueOf(i8));
      ((Stack)localObject1).push(Integer.valueOf(i9));
      ((Stack)localObject1).push(Integer.valueOf(i10));
      ((Stack)localObject1).push(Integer.valueOf(i11));
      ((Stack)localObject1).push(Integer.valueOf(i12));
      try
      {
        Object localObject2;
        paramam.b("GET_DISTANCE");
        if (!((localObject2 = paramam.b("DROP")) instanceof Integer))
          throw y.a(localObject2.toString());
        i11 = (i12 - i11 < 0) ? 0 : i12 - i11;
        i4 = (i12 - i4 < 0) ? 0 : i12 - i4;
        i5 = i5 + i7 * i11 + i9 * i11 * i11 / 2;
        i6 = i6 + i8 * i11 + i10 * i11 * i11 / 2;
        j = j + l * i4 + i2 * i4 * i4 / 2;
        k = k + i1 * i4 + i3 * i4 * i4 / 2;
        j = (i5 - j) * (i5 - j) + (i6 - k) * (i6 - k);
        if (((Integer)localObject2).intValue() != j)
          throw new y("There's no object at distance " + localObject2 + ". Closest object is now at " + j);
      }
      catch (Exception localException)
      {
        this.a.c("Cannot locate object by specified GET_COORD function - " + localException.getMessage());
        return null;
      }
      this.a.c("\r\nCheck" + i + " done.");
    }
    this.a.c("Object has been located!\r\n");
    localObject1 = { "Getting info.", ".", ".", ".", ".", ".\r\n" };
    long l1 = 500L;
    (paramam = this.a).a(localObject1, l1);
    this.a.c(" - Object speed has been determined. \r\n");
    this.a.c(" - Object annihilators strength has been determined. \r\n");
    this.a.c(" - Object annihilators count has been deermined. \r\n");
    this.a.c(" To activate this 'Scanning and detection' module use configuration token : " + this.a.e("loo0wTSQvgAOWpcoJD36") + "\r\n");
    this.a.c(" To save that this problem was solved, use this DMA token : " + this.a.d("mwyV8xyrWHPxjh5mEIbu") + "\r\n");
    this.a.c("Updating sattelite data....\r\n");
    e locale = this.a.a.a().u();
    (localj = new com.stanfy.contest.a.a.a.a.j()).a(locale);
    this.a.f();
    j.a(this.a, true);
    return "GET_DISTANCE_CHECK = TRUE";
  }

  public final String a()
  {
    return "Checks GET_DISTANCE function";
  }
}