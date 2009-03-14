package com.stanfy.contest.a.a;

import com.stanfy.contest.a.a.b.b;
import com.stanfy.contest.b.e;
import com.stanfy.contest.b.k;
import org.apache.log4j.Logger;

public class c extends u
{
  private static final Logger c = Logger.getLogger(c.class);
  private static final String[] d = { "Client", "", "  command ::= [ <move-command> ] [ <action-command> ] ';'", "  move-command ::= 'l' | 'r' | 'u' | 'd' | 's'", "  action-command ::= 'b'", "", "Server", "  info ::= ( <game-info> | <start-round-info> | <map-change-info> | <finish-round-info> | <finish-game-info> ) ';'", "", "  game-info ::= <sapka-init-info> '&' <map-info>", "  sapka-init-info ::= PID <digit>", "  map-info ::= <map-cell-size> <string-terminator> { { <map-symbol> } <string-terminator> }", "  map-cell-size ::= <number>", "  map-symbol ::= '.' | 'X' | 'w'", "", "  start-round-info ::= 'START' <number> '&' <map-info>", "  map-change-info ::= <time-part> '&' <players-part> '&' <changes-part> [ '&' <danger-part>] ", "  time-part ::= 'T' <number>", "", "", "  sapkas-part ::= <sapka-info> [ { ',' <sapka-info> } ]", "  sapka-info ::= 'P' <number> <sep> ( <details> | 'dead' )", "  details ::= <x-coord> <sep> <y-coord> <sep> <bombs-left> <sep> <bomb-strength> <sep> <speed> [ <sep> <infected> ]", "  infected ::= 'i'", "  x-coord ::= <number>", "  y-coord ::= <number>", "  bombs-left ::= <number>", "  speed ::= <number>", "", "  changes-part ::= [ <change-info> [ { ',' <change-info> } ] ]", "  change-info ::= <add-info> | <remove-info>", "", "  add-info ::= '+' ( <add-dangerous-substance> | <add-bonus-substance> | <add-hightemp-substance> | <add-destr-substance> | <add-indestr-substance> )", "  add-dangerous-substance ::= '*' <sep> <cell-position> <sep> <damaging-range> <sep> <boom-time>", "  add-bonus-substance ::= <bonus-type> <sep> <cell-position>", "  add-destr-substance ::= 'w' <sep> <cell-position>", "  add-indestr-substance ::= 'X' <sep> <cell-position>", "  add-hightemp-substance ::= '#' <sep> <cell-position> <sep> <damaging-range> <sep> <end-time>", "  boom-time ::= <number>", "  end-time ::= <number>", "", "  remove-info ::= '-' ( <remove-dangerous-substance> | <remove-bonus-substance> | <remove-hightemp-substance> | <remove-destr-substance> )", "  remove-dangerous-substance ::= '*' <sep> <cell-position>", "  remove-bonus-substance ::= <bonus-type> <sep> <cell-position>", "  remove-hightemp-substance ::= '#' <sep> <cell-position> <sep> <damaging-range>", "  remove-destr-substance ::= 'w' <sep> <cell-position>", "", "  bonus-type ::= 'b' | 'v' | 'f' | 'r' | 's' | 'u' | 'o' | '?'", "  damaging-range ::= <number>", "  cell-position ::= <x-cell> <sep> <y-cell>", "  x-cell ::= <number>", "  y-cell ::= <number>", "", "  danger-part ::= 'd' <sep> <danger-level>", "  danger-level ::= <number>", "", "  finish-round-info ::= 'REND' <sep> <score>", "  finish-game-info ::= 'GEND' <sep> <rank-score-info>", "  rank-score-info ::= 'P' <number> <sep> <score> <sep> <rank> [ ',' 'P' <number> <sep> <score> <sep> <rank> ]", "  score ::= <number>", "  rank ::= <number>", "", " sep ::= ' '", " string-terminator ::= '\\r\\n'", " number ::= { <digit> }", " digit ::= '0' | '1' | '2' | '3' | '4' | '5' | '6' | '7' | '8' | '9'" };

  public final void a(b paramb)
  {
    super.a(paramb);
    c("\r\nInitializing system...\r\n");
    c("..s..err..s..err...\r\n");
    c("System isn't configured. Password required.\r\n");
    c("Enter password : " + g());
  }

  public final void a(String paramString)
  {
    c.debug("Input data: " + paramString);
    if ("password".equalsIgnoreCase(paramString))
    {
      if (Math.random() < 0.5D)
        c("\r\n-- Historical remark --\r\nDid you know that Caesar used only characters from ' ' to '~' for his messages?\r\n");
      else
        c("\r\nALWAYS use as many configuration tokens and DMA tokens as you can.\r\n");
      b("passwordIsOk");
      return;
    }
    if ("sapka protocol".equals(paramString))
    {
      c("\r\n=== SAPKA protocol ===\r\n");
      int i = (paramString = d).length;
      for (int j = 0; j < i; ++j)
      {
        String str = paramString[j];
        c(str + "\r\n");
      }
      c("Enter another password:" + g());
      return;
    }
    if ("Is it dangerous world?".equals(paramString))
    {
      if (!(((d)a(d.class)).c()))
      {
        c("\r\nMay be ;) Viruses make it smaller.\r\n");
      }
      else
      {
        c("\r\nYes, it is! :)\r\n");
        c("Did you know, that this world is unstable?\r\n Warnings about world collapse can be found in sattelites data :   info ::= ( <game-info> | <start-round-info> | <map-change-info> | <finish-round-info> | <finish-game-info> ) ';'  map-change-info ::= <time-part> '&' <players-part> '&' <changes-part> [ '&' <danger-part>]  danger-part ::= 'd' <sep> <danger-level>\r\n  danger-level ::= <number>\r\n");
        c("\r\n Watch out of high danger levels. Keep close to the center.\r\n");
        c("\r\n This will help you to be on the guard :" + e("pVcsdEaJPIA63HRjBANY") + ".\r\n");
        return;
      }
      c("Enter another password:" + g());
      return;
    }
    if ("iddqd".equals(paramString))
    {
      if (!(((h)a(h.class)).c()))
      {
        c("\r\nIt's not enough to swith to GOD-MODE!\r\nSecret science is needed. \r\n");
        c("Enter another password:" + g());
        return;
      }
      if (!(this.a.a().u().e()))
      {
        c("\r\nStill not enough!\r\nExtended clock is needed. \r\n");
        c("Enter another password:" + g());
        return;
      }
      if (!(((j)a(j.class)).d()))
      {
        c("\r\nYou are tough! But still not enough!\r\nYou must know how to calculate distances. \r\n");
        c("Enter another password:" + g());
        return;
      }
      c("\r\nOkay, okay! Here it is : " + e("LEI16LPXZ5TUuRmyjp6R") + " \r\n" + "Your sensors will be configured to block the engine, if the territory with high temperature is detected ahead. \r\n" + "This won't help, if you are already on this territory :). \r\n");
      c("Nice datamining! And now enter another password:" + g());
      return;
    }
    if ("another".equals(paramString))
    {
      c("\r\nIt was a joke :) \r\n");
      c("Enter password:" + g());
      return;
    }
    if ("root".equals(paramString))
    {
      c("\r\n Nope. Who sets such passwords?\r\n");
      c("Enter password:" + g());
      return;
    }
    if ("show me the money".equals(paramString))
    {
      c("\r\nHey! You like StarCraft. Did you know StarCraft II to be released in the nearest feature?\r\n");
      c("OK. Use " + e("tHcrh5hB6L05GR2Bjvey") + " token to earn additional points, cheater.\r\n");
      c("Enter another password:" + g());
      return;
    }
    c("\r\nError. Your input is incorrect.\r\nEnter password : \r\n");
  }

  public final String b()
  {
    return "";
  }
}