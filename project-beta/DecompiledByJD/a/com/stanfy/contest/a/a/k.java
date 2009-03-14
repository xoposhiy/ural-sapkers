package com.stanfy.contest.a.a;

import com.stanfy.contest.a.a.b.b;
import java.util.HashMap;

public class k extends u
{
  private boolean c;
  private static int d = 95;
  private int e;

  public final void a(b paramb)
  {
    super.a(paramb);
    super.c("\r\n");
    super.c("=============== ENGINE CONTROL =======================\r\n");
    super.c("Current engine version : Caesar-1-3ci.5p5-h.23e.r ' ' - '~' \r\n");
    super.c("To get configuration info, input 'info;'. To get help, input 'help;' To quit, input 'quit;'." + g());
  }

  public final void a(String paramString)
  {
    if (paramString.equals("quit"))
    {
      b("statusExit");
      return;
    }
    if (paramString.equals("info"))
    {
      a("\r\nI hope, you know, what the Caesar chiper is\r\n In cryptography, a Caesar cipher, also known as a Caesar's cipher,\r\nthe shift cipher, Caesar's code or Caesar shift, is one of the sim-\r\nplest and most widely known encryption techniques. It is a type of \r\nsubstitution cipher in which each letter in the plaintext is repla-\r\nced by a letter some fixed number of positions down the alphabet.  \r\n  For example, with a shift of 3, 'A' would be replaced by 'D', 'B'\r\nwould become E, and so on. Password, that Caesar used often was :\r\n'Is it dangerous world?'. The method is named after Julius Caesar,\r\nwho used it to communicate with his generals. \r\nSo, if you are reading this, you can simply use this config token : \r\n", (this.c) ? 0 : 42);
      a(e("7sZGeaIvxWh8iByi0nmr") + "\r\n", (this.c) ? 0 : 42);
      a("And DMA token:\r\n", (this.c) ? 0 : 42);
      a(d("GLEUL427jybh8HoafY1a") + "\r\n", (this.c) ? 0 : 42);
      a("If this configuration is done, you can send commands.\r\nFormat -->    command ::= [ <move-command> ] [ <action-command> ] ';'\r\n              move-command ::= 'l' | 'r' | 'u' | 'd' | 's'\r\n              action-command ::= 'b'\r\nWhere\r\n l, r, u, d - commands to move left, right, up, and down correspondently.\r\n     s      - command to stop an engine.\r\n     b      - command to set up annihilator.\r\nExamples : r; lb; b; sb;\r\nDO NOT TRY IT AT HOME! TRY IT AT THE LAUNCH PHASE!\r\n\r\nThe more you destroy, the more scores you earn.\r\nFor each unique configuration token you'll get 2 points.\r\nDestroy object to get 10 points.\r\nKill enemy to get 1000 points.\r\nKill yourself to get -1000 points.\r\nEarn more to win.\r\n", (this.c) ? 0 : 42);
      c(g());
      return;
    }
    if (paramString.equals("help"))
    {
      this.e += 1;
      if (this.e < 6)
      {
        paramString = new StringBuilder();
        for (int i = 0; i < this.e - 1; ++i)
          paramString.append("!");
        c("\r\nThink" + paramString.toString() + "! Soon you'll understand. If you won't, try to get help again!." + g());
        return;
      }
      if (this.e < 10)
      {
        c("\r\nLook closely to the engine version. Your encoding is correct!" + g());
        return;
      }
      if (this.e < 20)
      {
        c("\r\nOkay! It's a Caesar cipher!" + g());
        return;
      }
      if (this.e < 30)
      {
        c("\r\nSymbols from ' ' to '~'." + g());
        return;
      }
      if (this.e < 42)
      {
        c("\r\nShift is \"The Answer to the Ultimate Question of Life, the Universe, and Everything\"" + g());
        return;
      }
      if (this.e == 42)
      {
        c("\r\n\"The Answer to the Ultimate Question of Life, the Universe, and Everything\" = 42" + g());
        this.e = 0;
        return;
      }
    }
    c(g());
  }

  private void a(String paramString, int paramInt)
  {
    String str;
    int j;
    paramString = paramInt;
    this = paramString;
    paramInt = new StringBuilder();
    i = 0;
    if (i >= length())
      break label78;
    if (((str = charAt(i)) >= ' ') && (str <= 126))
      if ((j = (char)(str + paramString)) > '~')
        j = (char)(j - d);
    label78: paramInt.append(j);
  }

  public final String c()
  {
    return "ENGINE CONTROL - " + ((this.c) ? " access granted." : " access denied.");
  }

  public final HashMap a_()
  {
    HashMap localHashMap;
    (localHashMap = super.a_()).put(d("GLEUL427jybh8HoafY1a"), new g(this, k.class));
    return localHashMap;
  }

  public final String b()
  {
    return "engine-control";
  }
}