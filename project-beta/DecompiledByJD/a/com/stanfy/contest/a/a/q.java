package com.stanfy.contest.a.a;

import com.stanfy.contest.a.a.a.a.h;
import com.stanfy.contest.a.a.b.b;
import com.stanfy.contest.a.a.c.am;
import com.stanfy.contest.a.a.c.o;
import com.stanfy.contest.a.a.c.y;
import com.stanfy.contest.b.k;
import com.stanfy.contest.d.a;
import java.util.HashMap;
import java.util.Stack;

final class q extends o
{
  q(j paramj, String paramString)
  {
    super(paramString);
  }

  public final Object a(am paramam)
  {
    if (paramam.c("MAJEDOC") == null)
    {
      this.a.c("\r\n MAJEDOC function is not found.\r\n");
      return "MAJEDOC ins't found";
    }
    for (int i = 0; i < 25; ++i)
    {
      Object localObject1 = new HashMap();
      int j = (int)(Math.random() * 15.0D) + 1;
      String str = "";
      for (int k = 0; k < j; ++k)
      {
        for (char c = a.a(); (str.contains(c + "")) || (c == ' '); c = a.a());
        str = str + c;
        ((HashMap)localObject1).put(Character.valueOf(c), Integer.valueOf(k));
      }
      int l = (int)(Math.random() * 5.0D) + 1;
      localObject1 = "";
      j = 0;
      k = 1;
      for (int i1 = 0; i1 <= l; ++i1)
      {
        int i2 = (int)(str.length() * Math.random());
        localObject1 = str.charAt(i2) + ((String)localObject1);
        j += i2 * k;
        k += str.length();
      }
      paramam.b().push(str);
      paramam.b().push(localObject1);
      try
      {
        Object localObject2;
        paramam.b("MAJEDOC");
        if (!((localObject2 = paramam.b("DROP")) instanceof Integer))
          throw y.a(localObject2.toString());
        if (j != ((Integer)localObject2).intValue())
        {
          this.a.c("\r\nMAJEDOC returned " + localObject2 + " on input " + str + " " + ((String)localObject1) + ", but correct result is " + j + "\r\n");
          return null;
        }
      }
      catch (Exception localException)
      {
        this.a.c("Cannot run MAJEDOC function - " + localException.getMessage());
        return null;
      }
    }
    this.a.c("MAGEDOC function works correctly!\r\n");
    String[] arrayOfString = { "Connecting to sattelites.", ".", ".", ".", ".", ".\r\n" };
    long l1 = 300L;
    (paramam = this.a).a(arrayOfString, l1);
    arrayOfString = { "Translating coordinates.", ".", ".", ".", ".", "Done.\r\n" };
    l1 = 300L;
    (paramam = this.a).a(arrayOfString, l1);
    (paramam = new h()).a(this.a.a.a().u());
    this.a.c("Updating sattelite data....\r\n");
    this.a.f();
    this.a.c("\r\n To activate this 'Numerical systems translate' module use configuration token : " + this.a.e("xkupLmctzs7BGKISFzE1") + "\r\n");
    this.a.c(" To save that this problem was solved, use this DMA token : " + this.a.d("NldhUVcw9g1H9wcDbZrA") + "\r\n");
    j.b(this.a, true);
    return "MAJEDOC OK";
  }

  public final String a()
  {
    return "Checks MAJEDOC function \r\n MAJEDOC function is function that translates one NS to another\r\n INPUT PARAMETERS(IN STACK)\r\n   <SOURCE_NS_DIGITS> - String that contains all source numeral system chars i.e. '01'\r\n   <SOURCE_NS_NUMBER> - String that contains Number that written in SOURCE_NS i.e. '10011' \r\n OUTPUT PARAMETERS(TO STACK)\r\n   <DECIMAL_NS_NUMBER> - Number that written in DECIMAL_NS i.e. 17 \r\n";
  }
}