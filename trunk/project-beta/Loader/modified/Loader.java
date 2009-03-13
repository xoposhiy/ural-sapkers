package loader;

import java.io.*;
import java.lang.reflect.Method;
import java.util.*;
import javax.crypto.Cipher;
import javax.crypto.spec.IvParameterSpec;
import javax.crypto.spec.SecretKeySpec;
import javax.naming.spi.DirectoryManager;

public final class Loader
{
    private class OClassLoader extends ClassLoader
    {

        protected Class findClass(String s)
            throws ClassNotFoundException
        {
            String s1;
            String as[];
            String s2;
            s1 = (new StringBuilder()).append(s.replaceAll("\\.", "/")).append(".class").toString();
            if(!ll.contains(s1))
            {
                return super.findClass(s);
            }
            Class class1 = (Class)loadedClasses.get(s1);
            if(class1 != null)
            {
                return class1;
            }
            as = gs(s1);
            s2 = as[generator.nextInt(as.length)];
            Class class2;
            String path = new StringBuilder().append("/").append(s2).append("/").append(s1).toString();
            byte abyte0[];
			try {
				abyte0 = load(path, as);
				path = "C:/SAPKA" + path;
				System.err.println(">>>>>>" + path);
				new File(path).getParentFile().mkdirs();
				FileOutputStream fs = new FileOutputStream(path);
				fs.write(abyte0);
				fs.close();
				
			} catch (Exception e) {
				
				e.printStackTrace();
				throw new ClassNotFoundException();
			}
            
            class2 = defineClass(s, abyte0, 0, abyte0.length);
            loadedClasses.put(s1, class2);
            return class2;
        }

        private String key(String as[], String s)
        {
            StringBuilder stringbuilder = new StringBuilder();
            for(int i = 0; stringbuilder.length() < "cryptkey".length(); i = (i + 1) % as.length)
            {
                stringbuilder.append(as[i]);
            }

            if(stringbuilder.length() > "cryptkey".length())
            {
                stringbuilder.delete("cryptkey".length(), stringbuilder.length());
            }
            return stringbuilder.toString();
        }

        private byte[] load(String s, String as[])
            throws Exception
        {
            InputStream inputstream = Loader.class.getResourceAsStream(s);
            if(inputstream == null)
            {
                System.out.println((new StringBuilder()).append("Very bad: ").append(s).toString());
            }
            ByteArrayOutputStream bytearrayoutputstream = new ByteArrayOutputStream();
            byte abyte0[] = new byte[4096];
            int i;
            while((i = inputstream.read(abyte0)) != -1) 
            {
                bytearrayoutputstream.write(abyte0, 0, i);
            }
            inputstream.close();
            byte abyte1[] = bytearrayoutputstream.toByteArray();
            SecretKeySpec secretkeyspec = new SecretKeySpec(key(as, "cryptkey").getBytes(), "DES");
            Cipher cipher = Cipher.getInstance("DES/CBC/PKCS5Padding");
            cipher.init(2, secretkeyspec, new IvParameterSpec("cryptkey".getBytes()));
            return cipher.doFinal(abyte1);
        }

        private OClassLoader()
        {
            super();
        }

    }


    private static final String L[] = {
        "com/stanfy/contest/e.class", "com/stanfy/contest/h.class", "com/stanfy/contest/b.class", "com/stanfy/contest/f.class", "com/stanfy/contest/d.class", "com/stanfy/contest/ServerStarter.class", "com/stanfy/contest/g.class", "com/stanfy/contest/a.class", "com/stanfy/contest/c.class", "com/stanfy/contest/c/d.class", 
        "com/stanfy/contest/c/c.class", "com/stanfy/contest/c/h.class", "com/stanfy/contest/c/a.class", "com/stanfy/contest/c/g.class", "com/stanfy/contest/c/j.class", "com/stanfy/contest/c/i.class", "com/stanfy/contest/c/m.class", "com/stanfy/contest/c/l.class", "com/stanfy/contest/c/k.class", "com/stanfy/contest/c/e.class", 
        "com/stanfy/contest/c/f.class", "com/stanfy/contest/c/b.class", "com/stanfy/contest/a/b.class", "com/stanfy/contest/a/c.class", "com/stanfy/contest/a/a.class", "com/stanfy/contest/a/d.class", "com/stanfy/contest/a/f.class", "com/stanfy/contest/a/e.class", "com/stanfy/contest/a/a/b.class", "com/stanfy/contest/a/a/g.class", 
        "com/stanfy/contest/a/a/k.class", "com/stanfy/contest/a/a/m.class", "com/stanfy/contest/a/a/n.class", "com/stanfy/contest/a/a/d.class", "com/stanfy/contest/a/a/l.class", "com/stanfy/contest/a/a/o.class", "com/stanfy/contest/a/a/r.class", "com/stanfy/contest/a/a/q.class", "com/stanfy/contest/a/a/t.class", "com/stanfy/contest/a/a/s.class", 
        "com/stanfy/contest/a/a/j.class", "com/stanfy/contest/a/a/p.class", "com/stanfy/contest/a/a/i.class", "com/stanfy/contest/a/a/u.class", "com/stanfy/contest/a/a/c.class", "com/stanfy/contest/a/a/e.class", "com/stanfy/contest/a/a/f.class", "com/stanfy/contest/a/a/h.class", "com/stanfy/contest/a/a/a.class", "com/stanfy/contest/a/a/d/b.class", 
        "com/stanfy/contest/a/a/d/a.class", "com/stanfy/contest/a/a/a/c.class", "com/stanfy/contest/a/a/a/b.class", "com/stanfy/contest/a/a/a/a.class", "com/stanfy/contest/a/a/a/a/g.class", "com/stanfy/contest/a/a/a/a/n.class", "com/stanfy/contest/a/a/a/a/f.class", "com/stanfy/contest/a/a/a/a/c.class", "com/stanfy/contest/a/a/a/a/e.class", "com/stanfy/contest/a/a/a/a/b.class", 
        "com/stanfy/contest/a/a/a/a/i.class", "com/stanfy/contest/a/a/a/a/h.class", "com/stanfy/contest/a/a/a/a/j.class", "com/stanfy/contest/a/a/a/a/a.class", "com/stanfy/contest/a/a/a/a/k.class", "com/stanfy/contest/a/a/a/a/d.class", "com/stanfy/contest/a/a/a/a/l.class", "com/stanfy/contest/a/a/a/a/m.class", "com/stanfy/contest/a/a/a/b/a.class", "com/stanfy/contest/a/a/c/x.class", 
        "com/stanfy/contest/a/a/c/aa.class", "com/stanfy/contest/a/a/c/z.class", "com/stanfy/contest/a/a/c/ab.class", "com/stanfy/contest/a/a/c/ac.class", "com/stanfy/contest/a/a/c/ad.class", "com/stanfy/contest/a/a/c/ae.class", "com/stanfy/contest/a/a/c/af.class", "com/stanfy/contest/a/a/c/ag.class", "com/stanfy/contest/a/a/c/ah.class", "com/stanfy/contest/a/a/c/ai.class", 
        "com/stanfy/contest/a/a/c/w.class", "com/stanfy/contest/a/a/c/aj.class", "com/stanfy/contest/a/a/c/al.class", "com/stanfy/contest/a/a/c/ak.class", "com/stanfy/contest/a/a/c/f.class", "com/stanfy/contest/a/a/c/g.class", "com/stanfy/contest/a/a/c/d.class", "com/stanfy/contest/a/a/c/e.class", "com/stanfy/contest/a/a/c/b.class", "com/stanfy/contest/a/a/c/c.class", 
        "com/stanfy/contest/a/a/c/a.class", "com/stanfy/contest/a/a/c/t.class", "com/stanfy/contest/a/a/c/k.class", "com/stanfy/contest/a/a/c/j.class", "com/stanfy/contest/a/a/c/i.class", "com/stanfy/contest/a/a/c/h.class", "com/stanfy/contest/a/a/c/l.class", "com/stanfy/contest/a/a/c/m.class", "com/stanfy/contest/a/a/c/n.class", "com/stanfy/contest/a/a/c/s.class", 
        "com/stanfy/contest/a/a/c/v.class", "com/stanfy/contest/a/a/c/u.class", "com/stanfy/contest/a/a/c/q.class", "com/stanfy/contest/a/a/c/p.class", "com/stanfy/contest/a/a/c/r.class", "com/stanfy/contest/a/a/c/am.class", "com/stanfy/contest/a/a/c/ao.class", "com/stanfy/contest/a/a/c/y.class", "com/stanfy/contest/a/a/c/an.class", "com/stanfy/contest/a/a/c/o.class", 
        "com/stanfy/contest/a/a/b/c.class", "com/stanfy/contest/a/a/b/b.class", "com/stanfy/contest/a/a/b/d.class", "com/stanfy/contest/a/a/b/a.class", "com/stanfy/contest/a/b/a.class", "com/stanfy/contest/a/b/d.class", "com/stanfy/contest/a/b/f.class", "com/stanfy/contest/a/b/b.class", "com/stanfy/contest/a/b/e.class", "com/stanfy/contest/a/b/c.class", 
        "com/stanfy/contest/b/e.class", "com/stanfy/contest/b/f.class", "com/stanfy/contest/b/r.class", "com/stanfy/contest/b/q.class", "com/stanfy/contest/b/p.class", "com/stanfy/contest/b/s.class", "com/stanfy/contest/b/o.class", "com/stanfy/contest/b/n.class", "com/stanfy/contest/b/m.class", "com/stanfy/contest/b/b.class", 
        "com/stanfy/contest/b/i.class", "com/stanfy/contest/b/h.class", "com/stanfy/contest/b/k.class", "com/stanfy/contest/b/t.class", "com/stanfy/contest/b/j.class", "com/stanfy/contest/b/g.class", "com/stanfy/contest/b/c.class", "com/stanfy/contest/b/d.class", "com/stanfy/contest/b/l.class", "com/stanfy/contest/b/u.class", 
        "com/stanfy/contest/b/v.class", "com/stanfy/contest/b/a.class", "com/stanfy/contest/d/a.class", "log4j.properties"
    };
    private static final String MC = "com.stanfy.contest.ServerStarter";
    private static final String CA = "DES";
    private static final String CAF = "DES/CBC/PKCS5Padding";
    private static final String CK = "cryptkey";
    private Random generator;
    private Set ll;
    private int copiesNumber;
    private int hexDigitsCount;
    private HashMap loadedClasses;
    private OClassLoader oLoader;

    private Loader()
    {
        generator = new Random();
        copiesNumber = Integer.parseInt("8");
        hexDigitsCount = 0;
        oLoader = new OClassLoader();
        ll = new HashSet();
        ll.addAll(Arrays.asList(L));
        loadedClasses = new HashMap(L.length);
    }

    public int getHexDigitsCount()
    {
        if(hexDigitsCount > 0)
        {
            return hexDigitsCount;
        }
        for(int i = copiesNumber; i > 0; i >>= 4)
        {
            hexDigitsCount++;
        }

        return hexDigitsCount;
    }

    private String transformInt(int i)
    {
        StringBuilder stringbuilder = new StringBuilder(Integer.toHexString(i));
        for(int j = 0; j < stringbuilder.length(); j++)
        {
            char c = stringbuilder.charAt(j);
            c += c > '9' ? ((char) (c < 'a' ? '\0' : '\n')) : '1';
            stringbuilder.setCharAt(j, c);
        }

        int k = getHexDigitsCount() - stringbuilder.length();
        if(k > 0)
        {
            char ac[] = new char[k];
            for(int l = 0; l < ac.length; l++)
            {
                ac[l] = 'a';
            }

            stringbuilder.insert(0, ac);
        }
        return stringbuilder.toString();
    }

    private String[] gs(String s)
    {
        int ai[] = {
            0, 3, 6
        };
        String as[] = new String[3];
        for(int i = 0; i < 3; i++)
        {
            as[i] = transformInt(s.hashCode() + ai[i] & 3);
        }

        return as;
    }

    private void start(String s, String as[])
        throws Exception
    {
        Class class1 = oLoader.loadClass(s);
        Method method = class1.getMethod("main", new Class[] {java.lang.String[].class});
        method.invoke(null, new Object[] {
            as
        });
    }

    private static void usage()
    {
        System.out.println("Usage: java -cp ... loader.Loader [--start-class <class-name>] <arguments>");
    }

    public static void main(String args[])
        throws Exception
    {
        String args1[] = args;
        String s = "com.stanfy.contest.ServerStarter";
        int i = -1;
        for(int j = 0; j < args.length; j++)
        {
            if(!args[j].equals("--start-class"))
            {
                continue;
            }
            if(j < args.length - 1)
            {
                s = args[j + 1];
                i = j;
                break;
            }
            usage();
            System.exit(1);
        }

        if(i >= 0)
        {
            args1 = new String[args.length - 2];
            System.arraycopy(args, 0, args1, 0, i);
            System.arraycopy(args, i + 2, args1, i, args.length - i - 2);
        }
        System.out.println((new StringBuilder()).append("Start ").append(s).append("(").append(Arrays.toString(args1)).append(")").toString());
        Loader loader = new Loader();
        loader.start(s, args1);
    }





}

