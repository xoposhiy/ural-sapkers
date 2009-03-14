package com.stanfy.contest.a.a.c;

import java.util.ArrayList;
import java.util.EmptyStackException;
import java.util.HashMap;
import java.util.Iterator;
import java.util.NoSuchElementException;
import java.util.Stack;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public final class am
{
  private Stack a = new Stack();
  private HashMap b = new HashMap();
  private HashMap c = new HashMap();
  private HashMap d = new HashMap();
  private com.stanfy.contest.a.a.u e;
  private boolean f = false;
  private ArrayList g;
  private int h = 0;
  private Pattern i = Pattern.compile("('.*?')", 32);

  public am()
  {
    (this = this).b.put("+", new x(this, "+"));
    this.b.put("-", new w(this, "-"));
    this.b.put("*", new t(this, "*"));
    this.b.put("/", new s(this, "/"));
    this.b.put("%", new v(this, "%"));
    this.b.put("SWAP", new u(this, "SWAP"));
    this.b.put("DUP", new q(this, "DUP"));
    this.b.put("DROP", new p(this, "DROP"));
    this.b.put("VAR", new r(this, "VAR"));
    this.b.put("<", new aa(this, "<"));
    this.b.put(">", new z(this, ">"));
    this.b.put("==", new ab(this, "=="));
    this.b.put("!=", new ac(this, "!="));
    this.b.put("=0", new ad(this, "=0"));
    this.b.put("LENGTH", new ae(this, "LENGTH"));
    this.b.put("STRTOINT", new af(this, "STRTOINT"));
    this.b.put("INTTOSTR", new ag(this, "INTTOSTR"));
    this.b.put("SUBSTR", new ah(this, "SUBSTR"));
    this.b.put("CHARAT", new ai(this, "CHARAT"));
    this.b.put("POS", new aj(this, "POS"));
    this.b.put("ORD", new al(this, "ORD"));
    this.b.put("CHR", new ak(this, "CHR"));
    this.b.put(".", new f(this, "."));
    this.b.put("WORDS", new g(this, "WORDS"));
    this.b.put("HELP", new d(this, "HELP"));
    this.b.put("?", new e(this, "?"));
    this.b.put(":", new b(this, ":"));
    this.b.put("//", new c(this, "//"));
    this.b.put("IFTHEN", new a(this, "IFTHEN"));
    this.b.put("ELSE", new k(this, "ELSE"));
    this.b.put("ENDIF", new j(this, "ENDIF"));
    this.b.put("->", new i(this, "->"));
    this.b.put("@", new h(this, "@"));
    this.b.put("CLEAR", new l(this, "CLEAR"));
    this.b.put("RESET", new m(this, "RESET"));
    this.b.put("QUIT", new n(this, "QUIT"));
  }

  public final void a(o paramo)
  {
    this.b.put(paramo.toString(), paramo);
  }

  public final void a(String paramString)
  {
    if (this.d.get(paramString) != null)
      return;
    this.d.put(paramString, Integer.valueOf(0));
  }

  public final Object b(String paramString)
  {
    // Byte code:
    //   0: new 99	java/util/ArrayList
    //   3: dup
    //   4: invokespecial 178	java/util/ArrayList:<init>	()V
    //   7: astore_2
    //   8: aload_1
    //   9: astore_3
    //   10: ldc 7
    //   12: bipush 32
    //   14: invokestatic 195	java/util/regex/Pattern:compile	(Ljava/lang/String;I)Ljava/util/regex/Pattern;
    //   17: dup
    //   18: astore 4
    //   20: aload_3
    //   21: invokevirtual 196	java/util/regex/Pattern:matcher	(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;
    //   24: astore 5
    //   26: aload 5
    //   28: invokevirtual 194	java/util/regex/Matcher:matches	()Z
    //   31: ifeq +55 -> 86
    //   34: aload 5
    //   36: iconst_1
    //   37: invokevirtual 193	java/util/regex/Matcher:group	(I)Ljava/lang/String;
    //   40: invokevirtual 169	java/lang/String:isEmpty	()Z
    //   43: ifne +14 -> 57
    //   46: aload_2
    //   47: aload 5
    //   49: iconst_1
    //   50: invokevirtual 193	java/util/regex/Matcher:group	(I)Ljava/lang/String;
    //   53: invokevirtual 179	java/util/ArrayList:add	(Ljava/lang/Object;)Z
    //   56: pop
    //   57: aload_2
    //   58: aload 5
    //   60: iconst_2
    //   61: invokevirtual 193	java/util/regex/Matcher:group	(I)Ljava/lang/String;
    //   64: invokevirtual 179	java/util/ArrayList:add	(Ljava/lang/Object;)Z
    //   67: pop
    //   68: aload 5
    //   70: iconst_3
    //   71: invokevirtual 193	java/util/regex/Matcher:group	(I)Ljava/lang/String;
    //   74: astore_3
    //   75: aload 4
    //   77: aload_3
    //   78: invokevirtual 196	java/util/regex/Pattern:matcher	(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;
    //   81: astore 5
    //   83: goto -57 -> 26
    //   86: aload_2
    //   87: aload_3
    //   88: invokevirtual 179	java/util/ArrayList:add	(Ljava/lang/Object;)Z
    //   91: pop
    //   92: aconst_null
    //   93: astore_3
    //   94: aload_2
    //   95: invokevirtual 182	java/util/ArrayList:iterator	()Ljava/util/Iterator;
    //   98: astore_2
    //   99: aload_2
    //   100: invokeinterface 197 1 0
    //   105: ifeq +162 -> 267
    //   108: aload_2
    //   109: invokeinterface 198 1 0
    //   114: checkcast 97	java/lang/String
    //   117: dup
    //   118: astore 4
    //   120: ldc 5
    //   122: invokevirtual 173	java/lang/String:startsWith	(Ljava/lang/String;)Z
    //   125: ifeq +15 -> 140
    //   128: iconst_1
    //   129: anewarray 97	java/lang/String
    //   132: dup
    //   133: iconst_0
    //   134: aload 4
    //   136: aastore
    //   137: goto +17 -> 154
    //   140: aload 4
    //   142: ldc 51
    //   144: ldc 1
    //   146: invokevirtual 171	java/lang/String:replaceAll	(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    //   149: ldc 1
    //   151: invokevirtual 172	java/lang/String:split	(Ljava/lang/String;)[Ljava/lang/String;
    //   154: dup
    //   155: astore 4
    //   157: dup
    //   158: astore 4
    //   160: arraylength
    //   161: istore 5
    //   163: iconst_0
    //   164: istore 6
    //   166: iload 6
    //   168: iload 5
    //   170: if_icmpge +94 -> 264
    //   173: aload 4
    //   175: iload 6
    //   177: aaload
    //   178: dup
    //   179: astore 7
    //   181: invokevirtual 169	java/lang/String:isEmpty	()Z
    //   184: ifne +74 -> 258
    //   187: aload_0
    //   188: dup
    //   189: getfield 114	com/stanfy/contest/a/a/c/am:h	I
    //   192: iconst_1
    //   193: iadd
    //   194: putfield 114	com/stanfy/contest/a/a/c/am:h	I
    //   197: aload_0
    //   198: getfield 114	com/stanfy/contest/a/a/c/am:h	I
    //   201: sipush 500
    //   204: if_icmple +37 -> 241
    //   207: aload_1
    //   208: astore_0
    //   209: new 91	com/stanfy/contest/a/a/c/y
    //   212: dup
    //   213: new 98	java/lang/StringBuilder
    //   216: dup
    //   217: invokespecial 175	java/lang/StringBuilder:<init>	()V
    //   220: ldc 46
    //   222: invokevirtual 176	java/lang/StringBuilder:append	(Ljava/lang/String;)Ljava/lang/StringBuilder;
    //   225: aload_0
    //   226: invokevirtual 176	java/lang/StringBuilder:append	(Ljava/lang/String;)Ljava/lang/StringBuilder;
    //   229: ldc 2
    //   231: invokevirtual 176	java/lang/StringBuilder:append	(Ljava/lang/String;)Ljava/lang/StringBuilder;
    //   234: invokevirtual 177	java/lang/StringBuilder:toString	()Ljava/lang/String;
    //   237: invokespecial 158	com/stanfy/contest/a/a/c/y:<init>	(Ljava/lang/String;)V
    //   240: athrow
    //   241: aload_0
    //   242: aload 7
    //   244: invokespecial 130	com/stanfy/contest/a/a/c/am:e	(Ljava/lang/String;)Ljava/lang/Object;
    //   247: astore_3
    //   248: aload_0
    //   249: dup
    //   250: getfield 114	com/stanfy/contest/a/a/c/am:h	I
    //   253: iconst_1
    //   254: isub
    //   255: putfield 114	com/stanfy/contest/a/a/c/am:h	I
    //   258: iinc 6 1
    //   261: goto -95 -> 166
    //   264: goto -165 -> 99
    //   267: aload_3
    //   268: areturn
  }

  private Object e(String paramString)
  {
    Object localObject1;
    Object localObject2;
    Matcher localMatcher;
    if (paramString.equals(":"))
    {
      if (this.f)
        throw new y("Definition is already defining.");
      this.f = true;
      this.g = new ArrayList();
      d("Entering to definition creation. To close definition, input //\r\n");
      return ":";
    }
    if (this.f)
    {
      if (paramString.equals("//"))
      {
        if (this.g.isEmpty())
          throw new y("Definition name is not specified.");
        localObject1 = (String)this.g.get(0);
        localObject2 = new an((String)localObject1);
        for (int j = 1; j < this.g.size(); ++j)
          ((an)localObject2).a((String)this.g.get(j));
        this.f = false;
        if (this.b.get(localObject1) != null)
        {
          this = (am)localObject1;
          throw new y("This word has been already declared (" + this + ")");
        }
        this.c.put(localObject1, localObject2);
        return localObject2;
      }
      this.g.add(paramString);
      return paramString;
    }
    if (paramString.startsWith("?"))
    {
      localObject1 = paramString.substring("?".length(), paramString.length());
      if ((localObject2 = (o)this.b.get(localObject1)) == null)
        localObject2 = (o)this.c.get(localObject1);
      if (localObject2 == null)
      {
        this = (am)localObject1;
        throw new y("Unknown definition name (" + this + ")");
      }
      d(((o)localObject2).a());
      return ((o)localObject2).a();
    }
    if (paramString.startsWith("->"))
    {
      localObject1 = paramString.substring("->".length(), paramString.length());
      if (this.d.get(localObject1) == null)
        throw y.b((String)localObject1);
      if (this.a.empty())
        throw y.a();
      localObject2 = this.a.pop();
      this.d.put(localObject1, localObject2);
      return localObject2;
    }
    if (paramString.startsWith("@"))
    {
      localObject1 = paramString.substring("@".length(), paramString.length());
      if ((localObject2 = this.d.get(localObject1)) == null)
        throw y.b((String)localObject1);
      this.a.push(localObject2);
      return localObject2;
    }
    if ((localObject1 = (o)this.c.get(paramString)) != null)
      try
      {
        return ((o)localObject1).a(this);
      }
      catch (EmptyStackException localEmptyStackException1)
      {
        throw y.a();
      }
      catch (NoSuchElementException localNoSuchElementException1)
      {
        throw y.a();
      }
    if ((localObject2 = (o)this.b.get(paramString)) != null)
      try
      {
        return ((o)localObject2).a(this);
      }
      catch (EmptyStackException localEmptyStackException2)
      {
        throw y.a();
      }
      catch (NoSuchElementException localNoSuchElementException2)
      {
        throw y.a();
      }
    if ((localMatcher = this.i.matcher(paramString)).matches())
    {
      localObject1 = paramString.substring(1, paramString.length() - 1);
      this.a.push(localObject1);
      return localObject1;
    }
    try
    {
      localObject1 = Integer.valueOf(Integer.parseInt(paramString));
      this.a.push(localObject1);
      return localObject1;
    }
    catch (NumberFormatException localNumberFormatException)
    {
      throw y.a(paramString);
    }
  }

  public final void a()
  {
    this.a.clear();
    this.c.clear();
    this.d.clear();
  }

  public static Integer a(Object paramObject)
  {
    if (!(paramObject instanceof Integer))
      throw y.a(paramObject.toString());
    return ((Integer)paramObject);
  }

  public static String b(Object paramObject)
  {
    if (!(paramObject instanceof String))
    {
      paramObject = paramObject.toString();
      throw new y("Data in stack must be string(" + paramObject + ").");
    }
    return ((String)paramObject);
  }

  public final o c(String paramString)
  {
    o localo;
    if ((localo = (o)this.b.get(paramString)) != null)
      return localo;
    return ((o)this.c.get(paramString));
  }

  public final void d(String paramString)
  {
    if (this.e != null)
      this.e.c(paramString);
  }

  public final void a(com.stanfy.contest.a.a.u paramu)
  {
    this.e = paramu;
  }

  public final Stack b()
  {
    return this.a;
  }

  public final boolean c()
  {
    return this.f;
  }
}