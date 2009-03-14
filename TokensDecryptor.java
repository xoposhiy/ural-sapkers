package sapkers;
public class TokensDecryptor {

	/**
	 * @param args
	 */
	  public static String a(boolean paramBoolean, String paramString1, String paramString2)
	  {
		//paramString2 = new StringBuilder(paramString2.substring(0, Math.min(80, paramString2.length())));
		StringBuilder sb = new StringBuilder(paramString2.substring(0, Math.min(80, paramString2.length())));
	    int i = 1;
	    
	    while (true)
	    {
	    	Boolean exit = false;
	      do
	      {
	        if (sb.length() >= 80)
	          {
	        	exit = true;
	        	break;
	          }
	        //paramString2.append(i);
	        sb.append(i);
		  }
	      while ((i += 2) <= 8);
	      if (exit) break;
	      i -= 8;
	    }
	    //label56: paramString2 = paramString2.toString().getBytes();
	    byte[] bytes2 = sb.toString().getBytes();
	    //paramString1 = paramString1.toString().getBytes();
	    byte[] bytes1 = paramString1.toString().getBytes();
	    char[] arrayOfChar = new char[20];
	    int j = 8726;
	    for (int k = 0; k < 20; ++k)
	    {
//	      int l = paramString2[k] * paramString2[(k + 20)] * j + paramString2[(k + 40)] * paramString2[(k + 60)];
	    	int l = bytes2[k] * bytes2[(k + 20)] * j + bytes2[(k + 40)] * bytes2[(k + 60)];
	      //j = (j | l) << 4 ^ (l | paramString1[k] + k);
	    	j = (j | l) << 4 ^ (l | bytes1[k] + k);
	      arrayOfChar[k] = (char)(64 + ((j < 0) ? -j : j) % 58);
	      for (l = 0; ((arrayOfChar[k] >= '[') && (arrayOfChar[k] <= '`')) || (arrayOfChar[k] < '@') || (arrayOfChar[k] > 'z'); ++l)
	      {
	        //int i1 = (char)(64 + (arrayOfChar[k] << 3 + k + 1 + paramString1[k] ^ ((j < 0) ? -j : j) + l) % 58);
	    	char i1 = (char)(64 + (arrayOfChar[k] << 3 + k + 1 + bytes1[k] ^ ((j < 0) ? -j : j) + l) % 58);
	        i1 = (char)(64 + (i1 + l) % 58);
	        arrayOfChar[k] = i1;
	        //a.debug("Current resultChar is " + arrayOfChar[k]);
	      }
	    }
	    return ((paramBoolean) ? "CFG" : "DMA") + String.valueOf(arrayOfChar);
	  }

	  public static void cfg(String s){
		  System.out.println("config " + a(true, s, "ural-sapkers") + ";");
	  }

	  public static void dma(String s){
		  System.out.println("dma " + a(false, s, "ural-sapkers") + ";");
	  }
	  public static void main(String[] args) {
			cfg("NTmx9KWzTuXSFybcgrBA");
			cfg("X1oj7Fs9xqvFOZugh1yE");
			cfg("zJDqE2CkZi1VsMbMBCIp");
			cfg("pVcsdEaJPIA63HRjBANY");
			cfg("LEI16LPXZ5TUuRmyjp6R");
			cfg("tHcrh5hB6L05GR2Bjvey");
			cfg("nAtyB40emoaUDb1iBrKF");
			cfg("7sZGeaIvxWh8iByi0nmr");
			cfg("xkupLmctzs7BGKISFzE1");
			cfg("loo0wTSQvgAOWpcoJD36");
			cfg("99qzIHgvjcqNURGGDXI4");
			cfg("dGmoZ8uKLC0K0Mox0D3T");
			cfg("RhkyCmdgAemW9x8TWVSu");
			
			dma("jnonSXard7wSSPDLesnc");
			dma("NldhUVcw9g1H9wcDbZrA");
			dma("mwyV8xyrWHPxjh5mEIbu");
			dma("GLEUL427jybh8HoafY1a");
			dma("U8sGOxzc1TaIj9NaOOfZ");
			dma("veLn0pnyXvPEIsXZw5N8");
			

	}

}
