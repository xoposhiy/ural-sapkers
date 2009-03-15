using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;

namespace Visualizer.UserControlledSapka
{
	class ZombySapka : AbstractSapka
	{
		private readonly IZombyMaster zombyMaster;

		public ZombySapka(string host, int port, string teamName, IZombyMaster zombyMaster) : base(host, port, teamName)
		{
			this.zombyMaster = zombyMaster;
		}

		public override string GetMove()
		{
			string s = zombyMaster.Command.ToString();
			if(zombyMaster.IsBombing()) s += 'b';
			return s;
		}
	}
}
