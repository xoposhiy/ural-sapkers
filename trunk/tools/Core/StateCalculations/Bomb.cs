using System;

namespace Core
{	
	public struct Bomb
	{
		public int X, Y, DamagingRange, DetTime;
		
		public Bomb(int X, int Y, int DamagingRange, int DetTime)
		{
			this.X = X;
			this.Y = Y;
			this.DamagingRange = DamagingRange;
			this.DetTime = DetTime;
		}
	}
}
