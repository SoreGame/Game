using System;
using System.Drawing;

namespace Game
{
	public class Level
	{
		public Level(string name, Player player1, Player player2, Vector exit, Physics physics, int id, int timer)
		{
			Name = name;
			Id = id;
			Player1 = player1;
			InitialPlayer1 = player1;
			Player2 = player2;
			InitialPlayer2 = player2;
			Exit = exit;
			this.physics = physics;
			Timer = timer;
			LevelBot = new Bot(physics);
		}

		private int Timer;
		public readonly string Name;
		public readonly Player InitialPlayer1;
		public readonly Player InitialPlayer2;
		public readonly int Id;
		public readonly Physics physics;
		public Player Player1;
		public Player Player2;
		public Vector Exit;
		private bool isFirst = true;
		public int IterationCount{ get; set; }
		public Bot LevelBot;


		//public void Move(Size spaceSize, Turn turn, int HorizontalSpeed, int VerticalSpeed)
		//{
		//	var forceX = ForcesTask.GetThrustForceX(HorizontalSpeed);
		//	var forceY = ForcesTask.GetThrustForceY(VerticalSpeed);
		//	Player = physics.MoveRocket(Player, forceX, forceY, turn, spaceSize, 0.3);
		//}

		public bool IsCompleted => (((Player1.Location - Exit).Length < 70)&&((Player2.Location - Exit).Length < 70));

		public Player Move(Size spaceSize,bool isFirst, Turn turn, int HorizontalSpeed, int VerticalSpeed)
		{
			var forceX = Force.GetThrustForceX(HorizontalSpeed);
			var forceY = Force.GetThrustForceY(VerticalSpeed);
			LevelBot.SaveTurn(isFirst, HorizontalSpeed, VerticalSpeed);
			if (isFirst)
			{
				Player1 = physics.MovePlayer(Player1, forceX, forceY, turn, spaceSize, 0.375);
				//if (IterationCount > 0) Player2 = LevelBot.BotMove(Player2, isFirst, forceX, forceY);
				return Player1;
			}
			else
			{
				Player2 = physics.MovePlayer(Player2, forceX, forceY, turn, spaceSize, 0.375);
				//if (IterationCount > 0) Player2 = LevelBot.BotMove(Player1, isFirst, forceX, forceY);
				return Player2;
			}
		}

		public void SwichIsFirst() => this.isFirst = !IsFirst;
        public void SetIsFirstTrue() => isFirst = true;
		public void SetIsFistFalse() => isFirst = false;
        public bool IsFirst => isFirst;

        public void TimerTick() => Timer--;
        public int TimerValue => Timer;

        public void Reset()
		{
			Player1 = InitialPlayer1;
			Player2 = InitialPlayer2;
			Timer = 7;
		}
	}
}