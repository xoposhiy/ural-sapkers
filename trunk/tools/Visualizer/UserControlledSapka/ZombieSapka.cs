using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;

namespace Visualizer.UserControlledSapka
{
	class ZombieSapka : AbstractSapka
	{
		private readonly IZombieMaster zombieMaster;

		public ZombieSapka(string host, int port, string teamName, IZombieMaster zombieMaster) : base(host, port, teamName)
		{
			this.zombieMaster = zombieMaster;
		}

		public override string GetMove()
		{
			string s = zombieMaster.Command.ToString();
			if(zombieMaster.IsBombing()) s += 'b';
			return s;
		}
	}
}
