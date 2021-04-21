using System;
using System.Drawing;

namespace Game
{
	public class Level
	{
		public Level(string name, Player player, Physics physics)
		{
			Name = name;
			Player = player;
			InitialPlayer = player;
			this.physics = physics;
		}

		public readonly string Name;
		public readonly Player InitialPlayer;
		private readonly Physics physics;
		public Player Player;


		//public void Move(Size spaceSize, Turn turn, int HorizontalSpeed, int VerticalSpeed)
		//{
		//	var forceX = ForcesTask.GetThrustForceX(HorizontalSpeed);
		//	var forceY = ForcesTask.GetThrustForceY(VerticalSpeed);
		//	Player = physics.MoveRocket(Player, forceX, forceY, turn, spaceSize, 0.3);
		//}

		public void Move(Size spaceSize, Turn turn, int HorizontalSpeed, int VerticalSpeed)
		{
			var forceX = ForcesTask.GetThrustForceX(HorizontalSpeed);
			var forceY = ForcesTask.GetThrustForceY(VerticalSpeed);
			Player = physics.MoveRocket(Player, forceX, forceY, turn, spaceSize, 0.3);
		}

		public void Reset()
		{
			Player = InitialPlayer;
		}
	}
}