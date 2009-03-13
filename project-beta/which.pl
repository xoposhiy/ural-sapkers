++$|;
$root = './DecompiledByJD';
@import = ();
@try = qw/a b c d/;

print "> ";
while (<>)
{
	s/^\s+//g;
	chomp;
	s|;$||;
	if (/^import/ || /^package/)
	{
		s/^import +//;
		s/^package +//;
		s|\.|/|g;
		push @import, $_;
		print "ok\n";
	}
	if (/^which/)
	{
		/ (.+)/;
		for $try (@try)
		{
			for $import (@import)
			{
				$full = "$root/$try/$import/$1.java";
				print "$full\n" if -f $full;
			}
		}
	}
	if (/^open/)
	{
		/ (.+)/;
		system("start notepad $1");
	}
	if (/^new/)
	{
		@import = ();
		print "ok\n";
	}
	print "> ";
}

__DATA__
package com.stanfy.contest.a.a;
import com.stanfy.contest.a.a.a.a;
import com.stanfy.contest.a.a.b.b;
import com.stanfy.contest.a.a.b.c;
import com.stanfy.contest.b.e;
import com.stanfy.contest.b.k;
import java.util.HashMap;
which u;
new;
