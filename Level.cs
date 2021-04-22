using System;
using System.Drawing;

namespace Game
{
	public class Level
	{
		public Level(string name, Player player1, Player player2, Physics physics)
		{
			Name = name;
			Player1 = player1;
			InitialPlayer1 = player1;
			Player2 = player2;
			InitialPlayer2 = player2;
			this.physics = physics;
		}

		public readonly string Name;
		public readonly Player InitialPlayer1;
		public readonly Player InitialPlayer2;
		private readonly Physics physics;
		public Player Player1;
		public Player Player2;


		//public void Move(Size spaceSize, Turn turn, int HorizontalSpeed, int VerticalSpeed)
		//{
		//	var forceX = ForcesTask.GetThrustForceX(HorizontalSpeed);
		//	var forceY = ForcesTask.GetThrustForceY(VerticalSpeed);
		//	Player = physics.MoveRocket(Player, forceX, forceY, turn, spaceSize, 0.3);
		//}

		public void Move(Size spaceSize,bool isFirst, Turn turn, int HorizontalSpeed, int VerticalSpeed)
		{
			var forceX = ForcesTask.GetThrustForceX(HorizontalSpeed);
			var forceY = ForcesTask.GetThrustForceY(VerticalSpeed);
			if (isFirst)
			Player1 = physics.MovePlayer(Player1, forceX, forceY, turn, spaceSize, 0.3);

		}

		public void Reset()
		{
			Player1 = InitialPlayer1;
			Player2 = InitialPlayer2;
		}
	}
}