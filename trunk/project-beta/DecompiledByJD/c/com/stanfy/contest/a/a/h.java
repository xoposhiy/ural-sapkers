package com.stanfy.contest.a.a;

import com.stanfy.contest.a.a.b.b;
import java.util.HashMap;

public class h extends u
{
  private char[][][] c = new char[6][2][2];
  private char[] d = { 'P', 'C', 'N', 'O', 'H', 'B' };
  private String e = "ABABAGALAMAGA";
  private String f = "";
  private boolean g = false;
  private boolean h;
  private static final int[] i = { 1, 4, 3, 5 };
  private static final int[] j = { 5, 3, 4, 1 };

  public h()
  {
    for (int k = 0; k < 6; ++k)
      for (int l = 0; l < 2; ++l)
        for (int i1 = 0; i1 < 2; ++i1)
          this.c[k][l][i1] = this.d[k];
    for (k = 0; k < 200; ++k)
      if (k % 2 == 1)
        a((int)(Math.random() * 2.0D), Math.random() > 0.5D);
      else
        b((int)(Math.random() * 2.0D), Math.random() > 0.5D);
  }

  public final void a(b paramb)
  {
    super.a(paramb);
    c("\r\n======================== DNA LAB ====================\r\n");
    c("DNA LAB module initiated. Use (help|man) dna; to get help about the module\r\n");
    h();
  }

  public final void a(String paramString)
  {
    if (this.b)
      return;
    if (paramString.isEmpty())
      return;
    if (this.e.startsWith(this.f + paramString.toUpperCase()))
      this.f += paramString;
    else
      this.f = "";
    if ((this.f.equalsIgnoreCase(this.e)) && (!(this.g)))
    {
      c("\r\nDeeper DNA processing is enabled. Process DNA to get correct DMA token\r\n");
      this.g = true;
    }
    if ((paramString.equalsIgnoreCase("help dna")) || (paramString.equalsIgnoreCase("man dna")) || (paramString.equalsIgnoreCase("help")))
    {
      this = this;
      (paramString = new StringBuilder()).append("\r\n======================== DNA LAB ====================\r\n");
      paramString.append("DNA Lab is the tool with extremely high usabilty.\r\n");
      paramString.append("It uses twelve aminoacids for restructuring DNA parts.\r\n");
      paramString.append("Aminoacids that can be used in DNA LAB:\r\n");
      paramString.append(" - sigma dimension aminoacids :\r\n");
      paramString.append("     Lysine(LA), Proline(PR)\r\n");
      paramString.append(" - sigma dimension aminoacids antipodes:\r\n");
      paramString.append("     Tyrosine(TY), Methionine(MA)\r\n");
      paramString.append(" - theta dimension aminoacids :\r\n");
      paramString.append("     Alanine(A), Valin(BA)\r\n");
      paramString.append(" - theta dimension aminoacids antipodes:\r\n");
      paramString.append("     Serine(SE), Glycine(GA)\r\n");
      paramString.append("                                                        \r\n");
      paramString.append("      LA PR  <-----  sigma dimension                    \r\n");
      paramString.append("      ------   .------ theta dimension                  \r\n");
      paramString.append(" A  | CC NN | SE                                        \r\n");
      paramString.append(" BA | NN OO | GA                                        \r\n");
      paramString.append("      ------                                            \r\n");
      paramString.append("      TY MA <------- sigma dimension antipodes          \r\n");
      paramString.append("                                                        \r\n");
      paramString.append(" DNA reperesented as two intersected thoroidal dimensions\r\n");
      paramString.append("such as theta and sigma dimension. Using of aminoacid,\r\n");
      paramString.append("can be done by inputing it's short name i.e. 'A' for Alanine,\r\n");
      paramString.append("to process DNA, input 'process'. For correct DNA processing,\r\n");
      paramString.append("DNA must be in absolute form, when each projection of DNA on \r\n");
      paramString.append("each dimension are .equ.l. Do.not tr..us..D.A...B..for..t. \r\n");
      paramString.append("fr...I..a...v.....n...........................sdax........ \r\n");
      paramString.append("............sm,cia......F.................r....a.......... \r\n");
      paramString.append("n.................dpqf,a...k..vjikqoa..............o...... \r\n");
      paramString.append(">> Data corrupted. End of data " + g());
      c(paramString.toString());
      return;
    }
    switch (paramString.toUpperCase().charAt(0))
    {
    case 'A':
      if (paramString.equalsIgnoreCase("A"))
      {
        a(0, true);
        h();
        return;
      }
    case 'B':
      if (paramString.equalsIgnoreCase("BA"))
      {
        a(1, true);
        h();
        return;
      }
    case 'G':
      if (paramString.equalsIgnoreCase("GA"))
      {
        a(1, false);
        h();
        return;
      }
    case 'L':
      if (paramString.equalsIgnoreCase("LA"))
      {
        b(0, true);
        h();
        return;
      }
    case 'M':
      if (paramString.equalsIgnoreCase("MA"))
      {
        b(1, false);
        h();
        return;
      }
    case 'P':
      if (paramString.equalsIgnoreCase("PR"))
      {
        b(1, true);
        h();
        return;
      }
      if (paramString.equalsIgnoreCase("PROCESS"))
      {
        if (!(e()))
          c("\r\nCannot process DNA. DNA is not in absolute form" + g());
        return;
      }
    case 'S':
      if (paramString.equalsIgnoreCase("SE"))
      {
        a(0, false);
        h();
        return;
      }
    case 'T':
      if (paramString.equalsIgnoreCase("TY"))
      {
        b(0, false);
        h();
        return;
      }
    case 'Q':
      if (paramString.equalsIgnoreCase("QUIT"))
      {
        b("statusExit");
        return;
      }
    case 'C':
    case 'D':
    case 'E':
    case 'F':
    case 'H':
    case 'I':
    case 'J':
    case 'K':
    case 'N':
    case 'O':
    case 'R':
    }
    (this = this).c("" + g());
  }

  private boolean e()
  {
    for (int k = 0; k < 6; ++k)
    {
      int l = this.c[k][0][0];
      for (int i1 = 0; i1 < 2; ++i1)
        for (int i2 = 0; i2 < 2; ++i2)
          if (l != this.c[k][i1][i2])
            return false;
    }
    this.h = true;
    a(new String[] { "DNA has been successfully processed....                                                \r\n", "Checking environment", ".", ".", ".", "............                                \r\n", "5 substance types found.                                                           \r\n", "Configuring sensors ....                                                            \r\n", "Destructable substance found. Identifier - 'w'.                                      \r\n", "Indestructable substance found. Identifier - 'X'.                                     \r\n", "Unknown, possibly dangerous substance found. Identifier - '*'. Has constant life time. \r\n", "Extremely dangerous substance with high temperature. Identifier - '#'. Has cross shape. With constant ray length.\r\n", "Potentially positive substance found. Has several types. Identifiers - 'b', 'f', 'v', '?'. \r\n" + ((this.g) ? " '?'  - type analizyng... Can contain several undesirable subtype - 'r', 's', 'u', 'o'.  \r\n" : " '?' - unknown substance type. Possibly positive.  Need deeper analysis through Gryc's Science approach. \r\n"), "Sensor configuration token : ", ((this.g) ? e("dGmoZ8uKLC0K0Mox0D3T") : e("RhkyCmdgAemW9x8TWVSu")) + "\r\n", "DNA processed token : ", ((this.g) ? d("c4qKx5lSLufnzkrdeg7o") : d("KLJexj8DlAi4m1g5Xpf2")) + "\r\n" + "Information about substances format : \r\n" + "info ::= ( <game-info> | <start-round-info> | <map-change-info> | <finish-round-info> | <finish-game-info> ) ';'\r\n" + "map-change-info ::= <time-part> '&' <players-part> '&' <changes-part> [ '&' <danger-part>]\r\n" + "changes-part ::= [ <change-info> [ { ',' <change-info> } ] ]\r\n" + "change-info ::= <add-info> | <remove-info>\r\n" + "add-info ::= '+' ( <add-dangerous-substance> | <add-bonus-substance> | <add-hightemp-substance> | <add-destr-substance> | <add-indestr-substance> )\r\n" + "add-dangerous-substance ::= '*' <sep> <cell-position> <sep> <damaging-range> <sep> <boom-time>\r\n" + "add-bonus-substance ::= <bonus-type> <sep> <cell-position>\r\n" + "add-destr-substance ::= 'w' <sep> <cell-position>\r\n" + "add-indestr-substance ::= 'X' <sep> <cell-position>\r\n" + "add-hightemp-substance ::= '#' <sep> <cell-position> <sep> <damaging-range> <sep> <end-time>\r\n" + "boom-time ::= <number>\r\n" + "end-time ::= <number> \r\n" + "remove-info ::= '-' ( <remove-dangerous-substance> | <remove-bonus-substance> | <remove-hightemp-substance> | <remove-destr-substance> )\r\n" + "remove-dangerous-substance ::= '*' <sep> <cell-position>\r\n" + "remove-bonus-substance ::= <bonus-type> <sep> <cell-position>\r\n" + "remove-hightemp-substance ::= '#' <sep> <cell-position> <sep> <damaging-range>\r\n" + "remove-destr-substance ::= 'w' <sep> <cell-position>\r\n" + "bonus-type ::= 'b' | 'v' | 'f' | 'r' | 's' | 'u' | 'o' | '?'\r\n" + "damaging-range ::= <number>\r\n" + "cell-position ::= <x-cell> <sep> <y-cell>\r\n" + "x-cell ::= <number>\r\n" + "y-cell ::= <number>      \r\n" });
    c(g());
    return true;
  }

  private void a(int paramInt, boolean paramBoolean)
  {
    char[] arrayOfChar = this.c[0][paramInt];
    for (int k = 0; k < 3; ++k)
      if (paramBoolean)
        this.c[k][paramInt] = this.c[(k + 1)][paramInt];
      else
        this.c[((4 - k) % 4)][paramInt] = this.c[((3 - k) % 4)][paramInt];
    if (paramBoolean)
    {
      this.c[3][paramInt] = arrayOfChar;
      return;
    }
    this.c[1][paramInt] = arrayOfChar;
  }

  private void b(int paramInt, boolean paramBoolean)
  {
    paramBoolean = (paramBoolean) ? i : j;
    int[] arrayOfInt = new int[4];
    for (int l = 0; l < paramBoolean.length; ++l)
      arrayOfInt[l] = ((paramBoolean[l] == 3) ? 3 - paramInt : paramInt);
    char[] arrayOfChar = new char[2];
    for (int k = 0; k < 2; ++k)
      arrayOfChar[k] = this.c[paramBoolean[0]][k][paramInt];
    for (k = 0; k < paramBoolean.length - 1; ++k)
    {
      int i1 = (3 == paramBoolean[k]) ? 1 : 0;
      int i2 = (3 == paramBoolean[(k + 1)]) ? 1 : 0;
      for (int i3 = 0; i3 < 2; ++i3)
        this.c[paramBoolean[k]][i3][((i1 != 0) ? 1 - paramInt : paramInt)] = this.c[paramBoolean[(k + 1)]][i3][paramInt];
    }
    for (k = 0; k < 2; ++k)
      this.c[paramBoolean[3]][k][paramInt] = arrayOfChar[k];
  }

  private void h()
  {
    StringBuilder localStringBuilder;
    (localStringBuilder = new StringBuilder()).append("\r\n");
    for (int k = 0; k < 2; ++k)
      for (int l = 0; l < 2; ++l)
      {
        for (int i1 = 0; i1 < 2; ++i1)
        {
          localStringBuilder.append(" ");
          localStringBuilder.append(this.c[1][k][i1]);
          localStringBuilder.append(this.c[1][k][i1]);
        }
        localStringBuilder.append("\r\n");
      }
    c(localStringBuilder.toString() + ">");
  }

  public final HashMap a_()
  {
    HashMap localHashMap;
    (localHashMap = super.a_()).put(d("c4qKx5lSLufnzkrdeg7o"), new e(this, h.class));
    localHashMap.put(d("KLJexj8DlAi4m1g5Xpf2"), new f(this, h.class));
    return localHashMap;
  }

  public final boolean c()
  {
    return this.g;
  }

  public final String d()
  {
    return "DNA LAB - DNA is " + ((!(this.h)) ? "NOT" : "") + " processed. " + ((this.g) ? "Deeper research done." : "");
  }

  public final String b()
  {
    return "dnalab";
  }
}