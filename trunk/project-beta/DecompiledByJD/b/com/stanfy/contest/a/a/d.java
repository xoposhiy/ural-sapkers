package com.stanfy.contest.a.a;

import com.stanfy.contest.a.a.d.a;
import java.util.HashMap;

public class d extends u
{
  private boolean c;
  private boolean d;
  private com.stanfy.contest.a.a.d.b e;
  private boolean f;
  private int g;
  private int h;
  private int i;
  private a[] j;

  public final void a(com.stanfy.contest.a.a.b.b paramb)
  {
    super.a(paramb);
    c("\r\n ====== Sattelite control =============\r\n");
    a(new String[] { "Sending command to sattelite", ".", ".", ".", "\r\n" });
    if (!(this.c))
    {
      this.e = new com.stanfy.contest.a.a.d.b();
      if ((paramb = this).f)
      {
        paramb.c("Finish the current game first!");
      }
      else
      {
        paramb.e.a();
        paramb.g = 1;
        paramb.j = paramb.e.a(1);
        paramb.h = -1;
        paramb.f = true;
      }
      a(new String[] { "Error!\r\n", "Cannot instantiate connection with sattelite.\r\n", "Check firewall or cont..!@#asdczko3SAT$D .. ch $@#@@@#$\r\n", "YEAH!!! OR PLAY WITH ME!!! I'M GOD!!! PLAY WITH ME, PLAY!!!!\r\n", "I THINK IT'S YOUR MOVE NOW!!!\r\n", g() });
      h();
      return;
    }
    c("Sattelite connection state :" + ((this.d) ? "CONNECTED" : "DISCONNECTED") + ".");
    c("Input 'sattelite on|off' to turn on/off sattelite.\r\n");
    c(g());
  }

  public final void a(String paramString)
  {
    Object localObject1;
    if ((paramString.equals("state")) && (!(this.c)))
    {
      h();
      return;
    }
    if (paramString.equals("quit"))
    {
      if ((paramString = this).c)
      {
        c("\r\nSometimes viruses can turn the world head over heels.\r\n");
        c("Especially they like to replace '+' with '-' or '<' with '>'.\r\n");
      }
      b("statusExit");
      return;
    }
    if ((paramString.equals("help")) || (paramString.equals("hint")))
    {
      if (!(this.c))
      {
        localObject1 = { { "\r\nWHAT KIND OF HELP YOU WANT, MORTAL?\r\n", "THERE'S NO CHANCE THAT I'LL HELP YOU!" + g() }, { "\r\nSUCKER!!! DO YOUR BEST!!! WIN!!!" + g() }, { "\r\nTHERE'S NO CHANCE FOR YOU!!!" + g() }, { "\r\nYOU MUST BEG ME!!! YOU CAN TRY 'please help, the chosen one'" + g() } };
        a(localObject1[(int)(java.lang.Math.random() * localObject1.length)]);
        return;
      }
      localObject1 = { "\r\n Sattelite control module\r\n", "===========================\r\n", "This module controls connection with sattelite.\r\n", "Input 'sattelite on|off' to turn on/off sattelite.\r\n" };
      a(localObject1);
      c(" Initializing part of the data is recieved in next format :\r\n  info ::= ( <game-info> | <start-round-info> | <map-change-info> | <finish-round-info> | <finish-game-info> ) ';'  game-info ::= <sapka-init-info> '&' <map-info>\r\n  sapka-init-info ::= PID <digit>\r\n  map-info ::= <map-cell-size> <string-terminator> { { <map-symbol> } <string-terminator> }\r\n  map-cell-size ::= <number>\r\n  map-symbol ::= '.' | 'X' | 'w'\r\n\r\n  sep ::= ' '\r\n  string-terminator ::= '\r\n'\r\n  number ::= { <digit> }\r\n  digit ::= '0' | '1' | '2' | '3' | '4' | '5' | '6' | '7' | '8' | '9'\r\n");
      f();
    }
    if ((paramString.equals("please help, the chosen one")) && (!(this.c)))
    {
      localObject1 = { "\r\nLISTEN, MORTAL!!!\r\n", "WHAT DO YOU KNOW ABOUT GALACTIC WARS GAME?\r\n", "YOU KNOWS NOTHING!!!! WHA-HAHA!!!\r\n", "OBEY THE  G A M E  CREATOR. THE  G A M E  IS PERFECT. DON'T TRY TO UNDERSTAND THE RULES\r\n", "IT JUST WON'T WORK OUT. YOU'RE TOO HUMAN TO BE ABLE TO UNDERSTAND IT. I SAY.\r\n", "SO IF YOU WANT TO VIEW CURRENT STATE, INPUT 'state;'. DO YOU UNDERSTAND ME, SLUG?\r\n", "AND IF YOU WANT TO MAKE MOVE INPUT SOMETHING LIKE 'a1-b2;'.\r\n", "BUT THIS WON'T HELP YOU!!! WHA-HA-HA!!!" + g() };
      a(localObject1);
      return;
    }
    if ((!(this.c)) && (this.g == 1) && (this.f))
      if (((localObject1 = paramString.split("-")) != null) && (localObject1.length == 2))
      {
        Object localObject3 = localObject1[0];
        localObject1 = localObject1[1];
        if ((localObject3.length() == 2) && (((String)localObject1).length() == 2))
        {
          int i1 = localObject3.charAt(0) - 'a';
          int l = localObject3.charAt(1) - '1';
          int i2 = ((String)localObject1).charAt(0) - 'a';
          int k = ((String)localObject1).charAt(1) - '1';
          if (a(i1, l))
            a(i2, k);
          if (this.g == 1)
            c("\r\nMAKE YOUR MOVE, MORTAL!\r\n");
          else
            while ((this.g == 3) && (this.f))
            {
              c("\r\nMY MOVE IS:");
              Object localObject2 = (localObject2 = this.e.a(3))[(int)(java.lang.Math.random() * localObject2.length)];
              a(((a)localObject2).a(), ((a)localObject2).b());
              a(((a)localObject2).c(), ((a)localObject2).d());
              c((char)(((a)localObject2).a() + 97) + "" + (char)(((a)localObject2).b() + 49) + "" + "-" + (char)(((a)localObject2).c() + 97) + "" + (char)(((a)localObject2).d() + 49) + "\r\n");
            }
        }
      }
    if ((this.c) && (paramString.startsWith("sattelite")))
      if (paramString.equals("sattelite on"))
      {
        this.d = true;
        c("Sattelite connection state is CONNECTED\r\n");
        c("Configuration token for sattelite connection:" + e("99qzIHgvjcqNURGGDXI4") + g());
      }
      else if (paramString.equals("sattelite off"))
      {
        this.d = false;
        c("Sattelite connection state is DISCONNECTED" + g());
      }
    c(g());
  }

  private void h()
  {
    StringBuilder localStringBuilder;
    (localStringBuilder = new StringBuilder()).append("\r\n");
    for (int k = 0; k <= 8; ++k)
    {
      localStringBuilder.append(((k == 8) ? " " : Character.valueOf((char)(k + 97))) + "  ");
      for (int l = 0; l < 8; ++l)
      {
        if (k != 8);
        switch (this.e.a(k, l))
        {
        case 3:
          localStringBuilder.append("b");
          break;
        case 1:
          localStringBuilder.append("r");
          break;
        case 4:
          localStringBuilder.append("B");
          break;
        case 2:
          localStringBuilder.append("R");
          break;
        case 0:
          localStringBuilder.append(((l + k) % 2 == 1) ? "." : "*");
        default:
          break label196:
          label196: localStringBuilder.append(l + 1);
        }
      }
      localStringBuilder.append("\r\n");
    }
    localStringBuilder.append(">");
    c(localStringBuilder.toString());
  }

  private boolean a(int paramInt1, int paramInt2)
  {
    if ((paramInt1 < 0) || (paramInt1 >= 8) || (paramInt2 < 0) || (paramInt2 >= 8))
      return false;
    for (int k = 0; k < this.j.length; ++k)
      if ((this.j[k].a() == paramInt1) && (this.j[k].b() == paramInt2))
      {
        this.h = paramInt1;
        this.i = paramInt2;
        return true;
      }
    if (this.h < 0)
      return false;
    for (k = 0; k < this.j.length; ++l)
    {
      int l;
      if ((this.j[k].a() == this.h) && (this.j[k].b() == this.i) && (this.j[k].c() == paramInt1) && (this.j[k].d() == paramInt2))
      {
        paramInt1 = this.j[k];
        this = this;
        l = paramInt1;
        (paramInt2 = this.e).a(l.a(), l.b(), l.c(), l.d());
        if (((((paramInt2 = paramInt1).a() - paramInt2.c() == 2) || (paramInt2.c() - paramInt2.a() == 2)) ? 1 : 0) != 0)
        {
          this.j = this.e.a(this.g, paramInt1.c(), paramInt1.d());
          if (this.j != null)
          {
            this.h = paramInt1.c();
            this.i = paramInt1.d();
          }
        }
        else
        {
          paramInt1 = (this.g == 1) ? "RED" : "BLACK";
          paramInt2 = (this.g == 1) ? "BLACK" : "RED";
          this.g = ((this.g == 1) ? 3 : 1);
          this.j = this.e.a(this.g);
          if (this.j == null)
          {
            new StringBuilder().append(paramInt2).append(" has no moves.  ").append(paramInt1).append(" wins.");
            if ((paramInt2 = this).g == 1)
            {
              paramInt2.c("\r\nWHAT HAVE YOU DONE?! YOU WON!!. SO I'LL GIVE YOU A LITTLE SECRET:\r\n");
              paramInt2.c("I DON'T KNOW WHAT IT IS, BUT\r\n");
              paramInt2.c(paramInt2.d("U8sGOxzc1TaIj9NaOOfZ") + "\r\n");
              paramInt2.a(new String[] { "10", " 9", " 8", " 7", " 6", " 5", " 4", " 3", " 2", " 1", " 0", " .", ".", ".", ".", ".", ".", ".", ".", ".", " -1", " -2", " -3", " -4\r\n" }, 1000L);
              paramInt2.c("JUST JOKING. TURNING SELF OFF...\r\n");
              paramInt2.c = true;
              paramInt2.a(paramInt2.a);
            }
            else
            {
              paramInt2.c("HA-HA, YOU LOSED. WHAT DID I TELL YOU? AND YOU REALLY THINK THAT YOU KNOW THE RULES?");
            }
            paramInt2.f = false;
          }
          this.h = -1;
          if (this.j != null)
          {
            paramInt1 = 1;
            for (paramInt2 = 1; paramInt2 < this.j.length; ++paramInt2)
              if ((this.j[paramInt2].a() != this.j[0].a()) || (this.j[paramInt2].b() != this.j[0].b()))
              {
                paramInt1 = 0;
                break;
              }
            if (paramInt1 != 0)
            {
              this.h = this.j[0].a();
              this.i = this.j[0].b();
            }
          }
        }
        return true;
      }
    }
    return false;
  }

  public final HashMap a_()
  {
    HashMap localHashMap;
    (localHashMap = super.a_()).put(d("U8sGOxzc1TaIj9NaOOfZ"), new m(this, d.class));
    localHashMap.put(d("veLn0pnyXvPEIsXZw5N8"), new n(this, d.class));
    return localHashMap;
  }

  public final boolean c()
  {
    return this.c;
  }

  public final boolean d()
  {
    return this.d;
  }

  public final String e()
  {
    return "SATTELITE CONTROL - " + ((!(this.c)) ? "Unexpected behaviour. Possibly virus threat." : new StringBuilder().append(" sattelite connection state : ").append((this.d) ? "CONNECTED" : "DISCONNECTED").toString());
  }

  public final String b()
  {
    return "sat-control";
  }
}