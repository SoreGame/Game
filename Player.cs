using System.Drawing;

namespace Game
{
    public class Player
    {
		public Player(Vector location, Vector velocity)
		{
			Location = location;
			Velocity = velocity;
		}

		public readonly int Size = 30;
		public readonly Vector Location;
		public readonly Vector Velocity;
		public Image image;
	}
}
