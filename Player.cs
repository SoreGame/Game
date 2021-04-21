namespace Game
{
    public class Player
    {
		public Player(Vector location, Vector velocity, double direction)
		{
			Location = location;
			Velocity = velocity;
			Direction = direction;
		}

		public readonly Vector Location;
		public readonly Vector Velocity;
		public readonly double Direction;
	}
}
