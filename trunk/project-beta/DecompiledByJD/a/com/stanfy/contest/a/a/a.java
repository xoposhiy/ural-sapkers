package com.stanfy.contest.a.a;

import com.stanfy.contest.a.a.b.b;
import com.stanfy.contest.b.e;
import com.stanfy.contest.b.k;

public class a extends u
{
  private String c = "Caesar";

  private static String a(String paramString1, String paramString2)
  {
    paramString2 = paramString2.toCharArray();
    StringBuilder localStringBuilder = new StringBuilder();
    int i = 0;
    for (int j = 0; i < paramString1.length(); j = (j + 1) % paramString2.length)
    {
      localStringBuilder.append("\\").append(paramString1.charAt(i) ^ paramString2[j]);
      ++i;
    }
    return localStringBuilder.toString();
  }

  private String c()
  {
    this = this.a.a().u();
    return "Internal clock: " + (((!(e())) && (a())) ? "eXtended clOck Routines - disabled" : ((a()) && (e())) ? "enabled" : "disabled") + ".";
  }

  public final void a(b paramb)
  {
    super.a(paramb);
    c("\r\n================== Internal clock ==============================================\r\n");
    c("Includes:\r\n");
    c(" - basic clock\r\n");
    c(" - eXtended clOck Routines\r\n");
    String[] arrayOfString = { "\r\nInstructions are available after configuring.\r\nInput 'info;' to get them.\r\n" };
    long l = 1000L;
    (localObject2 = this).a(arrayOfString, l);
    c("Input 'quit;' to return.\r\n\r\n");
    paramb = e("NTmx9KWzTuXSFybcgrBA");
    Object localObject1 = e("X1oj7Fs9xqvFOZugh1yE");
    arrayOfString = { "To switch on basic clock use configuration token: " };
    l = 1000L;
    (localObject2 = this).a(arrayOfString, l);
    Object localObject2 = localObject1;
    localObject1 = paramb;
    paramb = this;
    c(a(((String)localObject1) + "\r\nTo switch on eXtended clOck Routines use configuration token: " + a((String)localObject2, (String)localObject1), paramb.c));
    c("\r\n\"I came, I saw, I conquered.\"" + g());
  }

  public final void a(String paramString)
  {
    if (paramString.equals("info"))
    {
      paramString = (this = this).a.a().u();
      c("\r\n" + c() + "\r\n\r\n");
      if (paramString.a())
      {
        c("Internal clock is configured to recognize notifications about starting the new round ");
        c("(START) and finishing the current one (REND).\r\n");
        c("Format --> info ::= ( <game-info> | <start-round-info> | <map-change-info> | <finish-round-info> | <finish-game-info> ) ';'");
        c("           start-round-info ::= 'START' <number> '&' <map-info>\r\n");
        c("           finish-round-info ::= 'REND' <sep> <score>\r\n");
        c("           sep ::= ' '\r\n");
        c("           score ::= <number>\r\n");
        c("\r\n");
      }
      if (paramString.e())
      {
        c("Extended clock routines are synchronized with the remote server and can parse time of events.\r\n");
        c("Format --> map-change-info ::= <time-part> '&' <sapkas-part> '&' <changes-part> [ '&' <danger-part>]\r\n");
        c("           time-part ::= 'T' <number>\r\n");
        c("\r\nNumber after 'T' defines the internal server time (in ticks). One tick is about 50 milliseconds.\r\n");
        c("Server analyzes data sent from Sapka-clients every tick during the game. After analyzing it sends response to the Sapka-client that starts with 'T' <number> as a rule.\r\n");
      }
      c(g());
      return;
    }
    if (paramString.equals("quit"))
    {
      b("statusExit");
      return;
    }
    c("\r\nUnknown command" + g());
  }

  public final String a()
  {
    return c();
  }

  public final String b()
  {
    return "iclock";
  }
}