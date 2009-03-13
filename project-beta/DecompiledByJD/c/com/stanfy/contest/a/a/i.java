package com.stanfy.contest.a.a;

import com.stanfy.contest.a.a.b.b;
import com.stanfy.contest.b.e;
import com.stanfy.contest.b.k;
import com.stanfy.contest.d.a;
import java.util.HashMap;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class i extends u
{
  private boolean c;
  private String d;
  private static final Pattern e = Pattern.compile("([^&]+)&([^&]+)&([^&]*)(&(d\\s\\d+))?");
  private static final Pattern f = Pattern.compile("([\\+|\\-])(.)\\s(\\d+)\\s(\\d+)(\\s(\\d+)\\s(\\d+))?");
  private static Pattern g = Pattern.compile("\\?|((P\\d+)\\s(dead|\\x3F|((\\S+)\\s(\\S+)\\s(\\S+)\\s(\\S+)\\s([^\\s,]+)(\\si)?))).*");

  public final void a(b paramb)
  {
    super.a(paramb);
    c("\r\n ====== Lexical interpreter =============\r\n");
    c("Input 'help;' for help, 'quit;' for quit, 'dump;' for get current interpreting data\r\n");
    c("To set the data for interpretation input 'data <data-to-interpret>;'\r\n");
    c("To interpret data input 'interpret'" + g());
    paramb = this;
    String str1 = "star What do you know about Doom II GOD MODE? Try it, when typing password explanation mark";
    String str2 = "*To get other SAPKAs info at launch, use this config token " + paramb.e("nAtyB40emoaUDb1iBrKF") + ". Did you know, that not " + "only first character can be valid? By the way, this module can interpret all data, that you'll" + " get at SAPKA launch. But at first, turn this module on, by using this : " + paramb.d("jnonSXard7wSSPDLesnc") + " !";
    String str3 = "star each third exclamation mark";
    str1 = a(str1, 2, 0) + str3;
    str3 = "*each second!";
    str2 = a(str2, 1, 0) + str3;
    str3 = "*Each 2nd symbol is valid!";
    str1 = a(str2, str1, 0) + str3;
    str2 = "*Each 2nd character is valid. But apply shuffle before!";
    str1 = f(a(str1, 1, 0)) + str2;
    str2 = "*Each 2nd character is valid!";
    str1 = a(str1, 1, 0) + str2;
    str2 = "*For each five characters from start, apply shuffle : abcde = eadbc. Do nothing with tail (<5)!";
    str1 = f(str1) + str2;
    str2 = "*Each 3rd character in this string is valid. First character is always valid. This is one big string without CR or LF. Always remove information strings at the tail (from * to !). GO!";
    str1 = a(str1, 2, 0) + str2;
    paramb.d = str1;
    if (!(this.c))
      e();
  }

  public final void a(String paramString)
  {
    if (this.b)
      return;
    if (paramString.equals("quit"))
    {
      b("statusExit");
      return;
    }
    if (paramString.startsWith("data "))
    {
      int i = "data ".length();
      this.d = paramString.substring(i, paramString.length());
      c("\r\nInterpretation data was successfully changed" + g());
      return;
    }
    if (paramString.equals("help"))
    {
      (this = this).c("\r\nLexical Interpreter module can interpret data about changes, that SAPKA will receive.\r\n");
      c("Interpreter uses current configuration. If some parts is unknown, reconfigure SAPKA.\r\n");
      c("Each command that SAPKA will receive at launch phase delimited by ';' character.\r\n");
      c("Examples of commands received by SAPKA, that this interpreter can 'translate'\r\n");
      c(" ?&?& \r\n");
      c(" T34&P0 ?& \r\n");
      c(" T34&P0 3 4 ? 3 ? ?&\r\n");
      c("USAGE example:\r\n");
      c(" dump;      // get current data \r\n");
      c(" data ?&?&; // set data \r\n");
      c(" interpret; // interpret specified data\r\n");
      c("It's highly recommended to use this interpreter if you are uncertain about resolved data." + g());
      return;
    }
    if (paramString.equals("dump"))
    {
      c("\r\n Interpretation data : \r\n");
      c(this.d);
      c("" + g());
      return;
    }
    if (paramString.equals("interpret"))
    {
      e();
      return;
    }
    c("" + g());
  }

  private void e()
  {
    Object localObject1;
    c("Interpreter uses current configuration. If some parts is unknown, reconfigure SAPKA.\r\n");
    c("\r\nInterpreting " + this.d + "\r\n");
    e locale = this.a.a().u();
    if ((localObject1 = e.matcher(this.d)).matches())
    {
      Object localObject3;
      Object localObject4;
      if (!(this.c))
      {
        c("\r\nInterpreter must be turned on. Memory fix is needed." + g());
        return;
      }
      c("Matched for 'change-info'\r\n");
      String str1 = ((Matcher)localObject1).group(1);
      c(" " + str1 + ((locale.e()) ? " - Time info. Time, when change occurred." : " - Unknown message part") + "\r\n");
      str1 = ((Matcher)localObject1).group(2);
      Object localObject2 = g.matcher(str1);
      if (locale.f())
      {
        c("SAPKA's info found\r\n");
        if ((((Matcher)localObject2).matches()) && (!(str1.equals("?"))))
        {
          if (((Matcher)localObject2).group(2) != null)
            c(" " + ((Matcher)localObject2).group(2) + " - SAPKA identifier\r\n");
          if (((Matcher)localObject2).group(3).equals("dead"))
          {
            c(" " + ((Matcher)localObject2).group(3) + " - SAPKA status\r\n");
          }
          else
          {
            str1 = ((Matcher)localObject2).group(5);
            localObject3 = ((Matcher)localObject2).group(6);
            localObject4 = ((Matcher)localObject2).group(7);
            String str2 = ((Matcher)localObject2).group(8);
            String str3 = ((Matcher)localObject2).group(9);
            localObject2 = ((Matcher)localObject2).group(10);
            if (locale.g())
            {
              c(" " + str1 + " - SAPKA x-coordinate (in " + ((locale.h()) ? "points" : "cells") + " )\r\n");
              c(" " + ((String)localObject3) + " - SAPKA y-coordinate (in " + ((locale.h()) ? "points" : "cells") + " )\r\n");
            }
            else
            {
              c(" " + str1 + " - Unknown message part\r\n");
              c(" " + ((String)localObject3) + " - Unknown message part\r\n");
            }
            if (locale.t())
              c(" " + ((String)localObject4) + " - Annihilators left count\r\n");
            else
              c(" " + ((String)localObject4) + " - Unknown message part\r\n");
            if (locale.s())
              c(" " + str2 + " - Annihilators left count\r\n");
            else
              c(" " + str2 + " - Unknown message part\r\n");
            if (locale.r())
              c(" " + str3 + " - SAPKA speed\r\n");
            else
              c(" " + str3 + " - Unknown message part\r\n");
            if ((locale.q()) && (localObject2 != null))
              c(" " + ((String)localObject2) + " - SAPKA is infected\r\n");
            else
              c(" " + ((String)localObject2) + " - Unknown message part\r\n");
          }
        }
        else
        {
          c(" " + str1 + " - SAPKAs info part\r\n");
        }
      }
      else
      {
        c(" " + str1 + " - Unknown message part\r\n");
      }
      if (((str1 = ((Matcher)localObject1).group(3)) != null) && (!(str1.isEmpty())))
      {
        localObject3 = str1.split(",");
        c("MAP CHANGES(" + localObject3.length + ") info found\r\n");
        int i = (localObject4 = localObject3).length;
        for (int j = 0; j < i; ++j)
        {
          localObject2 = localObject4[j];
          if ((localObject1 = f.matcher((CharSequence)localObject2)).matches())
          {
            int k;
            str1 = ((Matcher)localObject1).group(1);
            localObject2 = (((Matcher)localObject1).group(1).equals("-")) ? "remove substance prefix" : "add substance prefix";
            localObject3 = ((Matcher)localObject1).group(2);
            String str4 = "unknown substance type";
            switch (k = ((String)localObject3).charAt(0))
            {
            case 'X':
              if (locale.l())
                str4 = "indestructable substance";
              break;
            case 'w':
              if (locale.k())
                str4 = "destructable substance";
              break;
            case '*':
              if (locale.m())
                str4 = "annihilator";
              break;
            case 'b':
              if (locale.o())
                str4 = "consumable substance, than can be processed into annihilator";
              break;
            case 'v':
              if (locale.o())
                str4 = "consumable substance, than affects engine speed";
              break;
            case 'f':
              if (locale.o())
                str4 = "consumable substance, than affects annihilator strength";
              break;
            case 'r':
              if (locale.o())
                str4 = "consumable substance, than temporarly moving controls";
              break;
            case 's':
              if (locale.o())
                str4 = "consumable substance, than temporarly decrease engine speed";
              break;
            case 'u':
              if (locale.o())
                str4 = "consumable substance, than temporarly blocks ability to place anihilators";
              break;
            case 'o':
              if (locale.o())
                str4 = "consumable substance, than temporarly significally decrease anihilator strength";
              break;
            case '#':
              if (locale.n())
                str4 = "extremely dangerous substance with hight temperature";
            }
            String str5 = ((Matcher)localObject1).group(3);
            String str6 = ((Matcher)localObject1).group(4);
            String str7 = ((Matcher)localObject1).group(6);
            localObject1 = ((Matcher)localObject1).group(7);
            c(" " + str1 + " - " + ((String)localObject2) + "\r\n");
            c(" " + ((String)localObject3) + " - " + str4 + "\r\n");
            c(" " + str5 + " - substance x-coordinate (in cells)\r\n");
            c(" " + str6 + " - substance y-coordinate (in cells)\r\n");
            if (str7 != null)
              c(" " + str7 + " - substance strength\r\n");
            if (localObject1 != null)
              c(" " + ((String)localObject1) + " - substance activation/deactivation time\r\n");
          }
          else
          {
            c(" " + ((String)localObject2) + " - Unknown message part\r\n");
          }
        }
      }
      c("\r\nDone." + g());
      return;
    }
    c("\r\nData cannot be interpreted! Please, make sure, that valid data was specified" + g());
  }

  private static String a(String paramString, int paramInt1, int paramInt2)
  {
    paramInt2 = new StringBuilder();
    int i = 0;
    for (int j = 0; j < paramString.length(); ++j)
    {
      while (i > 0)
      {
        paramInt2.append(a.a());
        --i;
      }
      paramInt2.append(paramString.charAt(j));
      i = paramInt1;
    }
    return paramInt2.toString();
  }

  private static String a(String paramString1, String paramString2, int paramInt)
  {
    paramInt = new StringBuilder();
    for (int i = 0; i < Math.max(paramString1.length(), paramString2.length()); ++i)
    {
      char c1 = (i < paramString1.length()) ? paramString1.charAt(i) : a.a();
      paramInt.append(c1);
      c1 = (i < paramString2.length()) ? paramString2.charAt(i) : a.a();
      paramInt.append(c1);
    }
    return paramInt.toString();
  }

  private static String f(String paramString)
  {
    StringBuilder localStringBuilder = new StringBuilder();
    for (int i = 0; i <= paramString.length() - 5; i += 5)
    {
      String str = paramString.substring(i, i + 5);
      localStringBuilder.append(str.charAt(1));
      localStringBuilder.append(str.charAt(3));
      localStringBuilder.append(str.charAt(4));
      localStringBuilder.append(str.charAt(2));
      localStringBuilder.append(str.charAt(0));
    }
    localStringBuilder.append(paramString.substring(i, paramString.length()));
    return localStringBuilder.toString();
  }

  public final String c()
  {
    return "LEXICAL INTERPRETER - " + ((this.c) ? " working." : " not configured.");
  }

  public final HashMap a_()
  {
    HashMap localHashMap;
    (localHashMap = super.a_()).put(d("jnonSXard7wSSPDLesnc"), new p(this, i.class));
    return localHashMap;
  }

  public final boolean d()
  {
    return this.c;
  }

  public final String b()
  {
    return "lex-interpr";
  }
}