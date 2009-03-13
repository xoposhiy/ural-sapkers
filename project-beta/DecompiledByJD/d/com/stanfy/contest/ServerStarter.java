package com.stanfy.contest;

import com.stanfy.contest.a.e;
import com.stanfy.contest.b.c;
import java.io.File;
import java.io.FileInputStream;
import java.util.Arrays;
import java.util.Properties;
import org.apache.log4j.Level;
import org.apache.log4j.Logger;

public final class ServerStarter
{
  private static final Logger a = Logger.getLogger(ServerStarter.class);
  private static int[] b;
  private static e[] c;
  private static f d;
  private static c e;

  private static boolean a(String paramString)
  {
    return (("yes".equalsIgnoreCase(paramString)) || ("true".equalsIgnoreCase(paramString)) || ("y".equalsIgnoreCase(paramString)));
  }

  public static void main(String[] paramArrayOfString)
  {
    File localFile1;
    if (paramArrayOfString.length < 1)
    {
      a.fatal("Incorrect parameter.\nUsage: java -cp '<path to jars>' com.stanfy.contest.ServerStarter <config-file-name>");
      System.exit(1);
    }
    Logger.getRootLogger().setLevel(Level.ERROR);
    if (!((localFile1 = new File(paramArrayOfString[0])).exists()))
    {
      a.fatal("Configuration file does not exist! (" + paramArrayOfString[0] + ")");
      System.exit(1);
    }
    a.info("Read configuration file.");
    paramArrayOfString = new Properties();
    try
    {
      paramArrayOfString.load(new FileInputStream(localFile1));
    }
    catch (Throwable localThrowable1)
    {
      a.fatal("Cannot read configuration file.", localThrowable1);
      System.exit(1);
    }
    a.info("Read map, create game model.");
    String str1 = paramArrayOfString.getProperty("map");
    Object localObject2 = new File(str1 + ".map");
    File localFile2 = new File(str1 + ".properties");
    if (!(((File)localObject2).exists()))
    {
      a.fatal("Incorrect path to the map: " + str1 + ".map");
      System.exit(1);
    }
    if (!(localFile2.exists()))
    {
      a.fatal("Incorrect path to the map configuration: " + str1 + ".properties");
      System.exit(1);
    }
    try
    {
      e = new c(new FileInputStream((File)localObject2), new FileInputStream(localFile2));
    }
    catch (Throwable localThrowable2)
    {
      a.fatal("Unable to create game model: " + localThrowable2.getMessage(), localThrowable2);
      System.exit(1);
    }
    a.info("Create client connectors.");
    String str2 = paramArrayOfString.getProperty("client.ports");
    try
    {
      localObject2 = str2.split("\\s*,\\s*");
    }
    catch (Exception localException)
    {
      a.fatal("Incorrect 'client.ports' parameter: " + str2);
      System.exit(1);
      return;
    }
    int i = localObject2.length;
    a.info("Clients count: " + i + " " + Arrays.toString(localObject2));
    c = new e[i];
    b = new int[i];
    for (int k = 0; k < i; ++k)
    {
      try
      {
        b[k] = Integer.parseInt(localObject2[k]);
      }
      catch (NumberFormatException localNumberFormatException1)
      {
        a.fatal("Incorrect port number " + localObject2[k]);
        if (d != null)
          d.e();
        System.exit(1);
      }
      a.debug("Start connector on port " + b[k]);
      c[k] = new e(b[k]);
    }
    a.info("Read misc parameters.");
    Object localObject3 = paramArrayOfString.getProperty("trace.out");
    Object localObject1 = paramArrayOfString.getProperty("result.out");
    localObject2 = paramArrayOfString.getProperty("visualizer.port");
    String str3 = paramArrayOfString.getProperty("client.wait");
    paramArrayOfString = paramArrayOfString.getProperty("visualizer.wait");
    localObject3 = new File((String)localObject3);
    localObject1 = new File((String)localObject1);
    int l = 0;
    if (localObject2 != null)
      try
      {
        l = Integer.parseInt((String)localObject2);
      }
      catch (NumberFormatException localNumberFormatException2)
      {
        a.warn("Incorrect visualizer port. Ignore it.");
      }
    localObject1 = new g((File)localObject3, (File)localObject1, l);
    boolean bool = false;
    if (paramArrayOfString != null)
      bool = a(paramArrayOfString);
    a.debug("Wait visualizer: " + bool);
    ((g)localObject1).b(bool);
    paramArrayOfString = 0;
    if (str3 != null)
      paramArrayOfString = a(str3);
    int j = paramArrayOfString;
    a.debug("waitAllConnectors = " + paramArrayOfString);
    a.debug("Start model controller");
    new d("Model controller starter", (g)localObject1, j).start();
  }
}