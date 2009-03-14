using System;
using System.Collections.Generic;

namespace Parsing
{
	public class MapChangeInfo
	{
		public readonly AddInfo[] Adds;
		public readonly int DangerLevel;
		public readonly bool HasDangerLevel;
		public readonly RemoveInfo[] Removes;
		public readonly SapkaInfo[] Sapkas;
		public readonly int Time;

		public MapChangeInfo(Reader r)
		{
			r.Ensure("T");
			Time = r.ReadNumber();
			r.Ensure("&");
			var sapkas = new List<SapkaInfo>();
			sapkas.Add(new SapkaInfo(r));
			while (r.Lookup() == ',')
			{
				r.Ensure(",");
				sapkas.Add(new SapkaInfo(r));
			}
			Sapkas = sapkas.ToArray();
			r.Ensure("&");
			var adds = new List<AddInfo>();
			var removes = new List<RemoveInfo>();
			if (r.Lookup() == '+' || r.Lookup() == '-')
			{
				if (r.Lookup() == '+') adds.Add(new AddInfo(r));
				else if (r.Lookup() == '-') removes.Add(new RemoveInfo(r));
				while (r.Lookup() == ',')
				{
					r.Ensure(",");
					if (r.Lookup() == '+') adds.Add(new AddInfo(r));
					else if (r.Lookup() == '-') removes.Add(new RemoveInfo(r));
					else throw new Exception("ќжидали + или -, а было " + r.Lookup());
				}
			}
			Adds = adds.ToArray();
			Removes = removes.ToArray();
			if (r.Lookup() == '&')
			{
				r.Ensure("&");
				r.Ensure("d ");
				DangerLevel = r.ReadNumber();
				HasDangerLevel = true;
			}
			else
				HasDangerLevel = false;
		}
	}
}