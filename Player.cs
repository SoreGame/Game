namespace Game
{
    public class Player
    {
		public Player(Vector location, Vector velocity, double direction, bool isMyTurn)
		{
			Location = location;
			Velocity = velocity;
			Direction = direction;
			IsMyTurn = isMyTurn;
		}

		public readonly Vector Location;
		public readonly Vector Velocity;
		public readonly double Direction;
		public readonly bool IsMyTurn;
	}
}
