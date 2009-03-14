namespace Parsing
{
	public class SapkaInfo
	{
		public readonly int BombsLeft;
		public readonly int BombsStrength;
		public readonly bool Infected;
		public readonly bool IsDead;
		public readonly Pos Pos;
		public readonly int SapkaNumber;
		public readonly int Speed;

		public SapkaInfo(int sapkaNumber, bool isDead, Pos pos, int bombsLeft, int bombsStrength, int speed, bool infected)
		{
			SapkaNumber = sapkaNumber;
			IsDead = isDead;
			Pos = pos;
			BombsLeft = bombsLeft;
			BombsStrength = bombsStrength;
			Speed = speed;
			Infected = infected;
		}
	}
}