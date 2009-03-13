package com.stanfy.contest;

import org.apache.log4j.Logger;

final class d extends Thread
{
  d(String paramString, g paramg, boolean paramBoolean)
  {
    super(paramString);
  }

  public final void run()
  {
    ServerStarter.a(new f(ServerStarter.a(), ServerStarter.b(), this.a, this.b));
    ServerStarter.c().info("Model controller started!");
  }
}