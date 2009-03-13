package com.stanfy.contest;

import java.io.InputStream;
import org.apache.log4j.Logger;

public abstract class e extends c
{
  private static final Logger a = Logger.getLogger(e.class);
  private byte[] b = new byte[512];
  private StringBuilder c = new StringBuilder();
  private boolean d = false;

  // ERROR //
  protected void g()
  {
    // Byte code:
    //   0: getstatic 16	com/stanfy/contest/e:a	Lorg/apache/log4j/Logger;
    //   3: new 11	java/lang/StringBuilder
    //   6: dup
    //   7: invokespecial 30	java/lang/StringBuilder:<init>	()V
    //   10: ldc 3
    //   12: invokevirtual 32	java/lang/StringBuilder:append	(Ljava/lang/String;)Ljava/lang/StringBuilder;
    //   15: aload_0
    //   16: invokevirtual 28	java/lang/Object:getClass	()Ljava/lang/Class;
    //   19: invokevirtual 31	java/lang/StringBuilder:append	(Ljava/lang/Object;)Ljava/lang/StringBuilder;
    //   22: invokevirtual 33	java/lang/StringBuilder:toString	()Ljava/lang/String;
    //   25: invokevirtual 37	org/apache/log4j/Logger:info	(Ljava/lang/Object;)V
    //   28: aload_0
    //   29: invokevirtual 23	com/stanfy/contest/e:h	()Ljava/io/InputStream;
    //   32: astore_1
    //   33: aload_0
    //   34: iconst_1
    //   35: putfield 19	com/stanfy/contest/e:d	Z
    //   38: aload_0
    //   39: invokevirtual 21	com/stanfy/contest/e:b_	()Z
    //   42: ifeq +88 -> 130
    //   45: aload_1
    //   46: invokevirtual 26	java/io/InputStream:available	()I
    //   49: dup
    //   50: istore_2
    //   51: ifle +57 -> 108
    //   54: iload_2
    //   55: sipush 512
    //   58: if_icmple +7 -> 65
    //   61: sipush 512
    //   64: istore_2
    //   65: aload_1
    //   66: aload_0
    //   67: getfield 17	com/stanfy/contest/e:b	[B
    //   70: iconst_0
    //   71: iload_2
    //   72: invokevirtual 27	java/io/InputStream:read	([BII)I
    //   75: dup
    //   76: istore_2
    //   77: ifle +28 -> 105
    //   80: aload_0
    //   81: getfield 18	com/stanfy/contest/e:c	Ljava/lang/StringBuilder;
    //   84: new 10	java/lang/String
    //   87: dup
    //   88: aload_0
    //   89: getfield 17	com/stanfy/contest/e:b	[B
    //   92: iconst_0
    //   93: iload_2
    //   94: invokespecial 29	java/lang/String:<init>	([BII)V
    //   97: invokevirtual 32	java/lang/StringBuilder:append	(Ljava/lang/String;)Ljava/lang/StringBuilder;
    //   100: pop
    //   101: aload_0
    //   102: invokevirtual 24	com/stanfy/contest/e:i	()V
    //   105: goto -67 -> 38
    //   108: ldc2_w 14
    //   111: invokestatic 34	java/lang/Thread:sleep	(J)V
    //   114: goto -76 -> 38
    //   117: astore_2
    //   118: getstatic 16	com/stanfy/contest/e:a	Lorg/apache/log4j/Logger;
    //   121: ldc 1
    //   123: aload_2
    //   124: invokevirtual 35	org/apache/log4j/Logger:error	(Ljava/lang/Object;Ljava/lang/Throwable;)V
    //   127: goto -89 -> 38
    //   130: goto +14 -> 144
    //   133: astore_1
    //   134: getstatic 16	com/stanfy/contest/e:a	Lorg/apache/log4j/Logger;
    //   137: aload_1
    //   138: invokevirtual 25	java/io/IOException:getMessage	()Ljava/lang/String;
    //   141: invokevirtual 38	org/apache/log4j/Logger:warn	(Ljava/lang/Object;)V
    //   144: aload_0
    //   145: invokevirtual 22	com/stanfy/contest/e:d	()V
    //   148: getstatic 16	com/stanfy/contest/e:a	Lorg/apache/log4j/Logger;
    //   151: new 11	java/lang/StringBuilder
    //   154: dup
    //   155: invokespecial 30	java/lang/StringBuilder:<init>	()V
    //   158: ldc 2
    //   160: invokevirtual 32	java/lang/StringBuilder:append	(Ljava/lang/String;)Ljava/lang/StringBuilder;
    //   163: aload_0
    //   164: invokevirtual 28	java/lang/Object:getClass	()Ljava/lang/Class;
    //   167: invokevirtual 31	java/lang/StringBuilder:append	(Ljava/lang/Object;)Ljava/lang/StringBuilder;
    //   170: invokevirtual 33	java/lang/StringBuilder:toString	()Ljava/lang/String;
    //   173: invokevirtual 37	org/apache/log4j/Logger:info	(Ljava/lang/Object;)V
    //   176: return
    //
    // Exception table:
    //   from	to	target	type
    //   108	114	117	java/lang/InterruptedException
    //   28	130	133	java/io/IOException
  }

  protected abstract InputStream h();

  protected abstract void i();

  protected final StringBuilder j()
  {
    return this.c;
  }

  public final boolean k()
  {
    return this.d;
  }
}