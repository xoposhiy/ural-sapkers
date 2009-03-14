package com.stanfy.contest.a.a;

import com.stanfy.contest.a.a.b.b;
import com.stanfy.contest.a.a.c.am;
import com.stanfy.contest.a.a.c.y;
import java.util.HashMap;
import org.apache.log4j.Logger;

public class j extends u
{
  private static final Logger c = Logger.getLogger(j.class);
  private boolean d;
  private boolean e;
  private am f = new am();

  public final void a(b paramb)
  {
    super.a(paramb);
    this.f.a(this);
    c("\r\n ====== 'FIFTH' INTERPRETER  =============\r\n");
    c("Use 'HELP;' for help, 'QUIT;' for quit;, 'TASKS' for unresolved tasks.\r\n");
    if (!(this.d))
    {
      c("Unresolved task found : MOVING OBJECT IDENTIFICATION problem (PROBLEM349)\r\n");
      (paramb = this).f.a(new r(paramb, "GET_DISTANCE_CHECK"));
    }
    if ((!(this.e)) && (((d)a(d.class)).d()))
    {
      c("Unresolved task found : DIFFERENT NUMERAL SYSTEMS problem (PROBLEM954)\r\n");
      (paramb = this).f.a(new q(paramb, "MAJEDOC_CHECK"));
    }
    c("" + g());
  }

  public final void a(String paramString)
  {
    c("\r\n");
    if (paramString.equals("PROBLEM349"))
    {
      (paramString = this).c(" ====== PROBLEM #349 DESCRIPTION  =============\r\n");
      paramString.c(" Objects 'move function' cannot be identified.\r\n");
      paramString.c(" Likely error : not correct coordinate system.\r\n");
      paramString.c(" Function is needed to determine the distance for deeper object analysis. \r\n");
      paramString.c(" Current GET_DISTANCE function must be replaced. input GET_DISTANCE to get more info. \r\n");
      if (paramString.f.c("GET_DISTANCE") == null)
        try
        {
          paramString.f.b(": GET_DISTANCE  'Current realisation of this function just describes input and output parameters\r\n' 'INPUT PARAMS(FROM STACK) : \r\n'  '  OBJX OBJY - object starting coordinates\r\n'  '  OBJVELX OBJVELY - object starting velocities (by each dimension)\r\n'  '  OBJACCELX OBJACCELY - object accelerations (by each dimension)\r\n'  '  OBJSTARTTIME - Time, when object began to move\r\n'  '  SELFX SELFY - SAPKA starting coordinates\r\n'  '  SELFVELX SELFVELY - SAPKA starting velocities (by each dimension)\r\n'  '  SELFACCELX SELFACCELY - SAPKA accelerations (by each dimension)\r\n'  '  SELFSTARTTIME - Time, when object began to move\r\n'  '  CURRTIME - current time\r\n'  'OUTPUT PARAMS(TO STACK)\r\n' '  CURR_DISTANCE - SQUARE of distance between OBJECT and SAPKA \r\n' + + + + + + + + + + + + . DROP  //");
        }
        catch (y localy)
        {
          paramString.c("Error when getting distance" + localy.getMessage());
        }
      paramString.c(" Input GET_DISTANCE_CHECK, to resolve problem with CURRENT GET_DISTANCE function \r\n");
      c("" + g());
      return;
    }
    if ((((d)a(d.class)).d()) && (paramString.equals("PROBLEM954")))
    {
      (paramString = this).c(" ====== PROBLEM #954 DESCRIPTION  =============\r\n");
      paramString.c(" Sattelites use different numeral systems(NS).\r\n");
      paramString.c(" Different sattelites data is needed to get SAPKA's coordinates with high precision.\r\n");
      paramString.c(" MAJEDOC Function that translates sattelites NS cannot be found. \r\n");
      paramString.c(" At now, only sattelites with '0123456789' NS can be used. \r\n");
      paramString.c(" All sattelites send data in format : 'all NS digits' 'number in specified NS'\r\n");
      paramString.c(" Example : \r\n");
      paramString.c(" 'a'  'aaaaa' ==> 4 (decimal NS) \r\n");
      paramString.c(" 'ab' 'baaaab' ==> 33 (decimal NS) \r\n");
      paramString.c(" '01234567890abcdef' 'ff' ==> 255 (decimal numeral system) \r\n");
      paramString.c(" MAJEDOC function is needed for translating one NS to another\r\n");
      paramString.c("Input ?MAJEDOC_CHECK to get more info about MAJEDOC input-output parameters \r\n");
      paramString.c("Input MAJEDOC_CHECK to check current MAJEDOC function implementation \r\n");
      c("" + g());
      return;
    }
    if (paramString.equals("TASKS"))
    {
      String[] arrayOfString = { "Searching for unresolved tasks", ".", ".", ".", ".", ".", ".\r\n" };
      long l = 1000L;
      (paramString = this).a(arrayOfString, l);
      if (!(this.d))
        c("Unresolved task found : MOVING OBJECT IDENTIFICATION problem (PROBLEM349)\r\n");
      if ((!(this.e)) && (((d)a(d.class)).d()))
        c("Unresolved task found : DIFFERENT NUMERAL SYSTEMS problem (PROBLEM954)\r\n");
    }
    else
    {
      try
      {
        this.f.b(paramString);
      }
      catch (y paramString)
      {
        c.warn("Error inside the Fifth interpreter", paramString);
        c(paramString.getMessage());
      }
    }
    c(g());
  }

  public final HashMap a_()
  {
    HashMap localHashMap;
    (localHashMap = super.a_()).put("mwyV8xyrWHPxjh5mEIbu", new t(this, j.class));
    localHashMap.put("NldhUVcw9g1H9wcDbZrA", new s(this, j.class));
    return localHashMap;
  }

  public final String c()
  {
    this = ((this.d) ? 0 : 1) + (((this.e) || (!(((d)a(d.class)).d()))) ? 0 : 1);
    return "FIFTH module " + ((this == 0) ? " has no unresolved problems." : new StringBuilder().append(" has ").append(this).append(" unresolved problems.").toString());
  }

  public final boolean d()
  {
    return this.d;
  }

  public final String b()
  {
    return "fifth" + ((this.f.c()) ? "|def" : "");
  }
}