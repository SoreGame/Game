using System;
using System.Drawing;

namespace Game
{
	public class Level
	{
		public Level(string name, Player player1, Player player2, Block block, Physics physics, int id, int timer, MapController map)
		{
			Map = map;
			Name = name;
			Id = id;
			Player1 = new Player(map.InitialPositionP1, Vector.Zero);
			InitialPlayer1 = new Player(map.InitialPositionP1, Vector.Zero);
			Player2 = new Player(map.InitialPositionP2, Vector.Zero);
			InitialPlayer2 = new Player(map.InitialPositionP2, Vector.Zero);
			Exit = map.Exit;
			this.physics = physics;
			block_ = block;
			Timer = timer;
			LevelBot = new Bot(physics);
			Door = map.Door;
			Button = map.Button;
		}

		private bool isClose = true;
		public Vector Button;
		public Vector Door;
		public MapController Map;
		public Block block_;
		private int Timer;
		public readonly string Name;
		public Player InitialPlayer1; //#
		public Player InitialPlayer2; //#
		public readonly int Id;
		public readonly Physics physics;
		public Player Player1;
		public Player Player2;
		public Vector Exit;
		private bool isFirst = true;
		public int IterationCount { get; set; }
		public Bot LevelBot;


		//public void Move(Size spaceSize, Turn turn, int HorizontalSpeed, int VerticalSpeed)
		//{
		//	var forceX = ForcesTask.GetThrustForceX(HorizontalSpeed);
		//	var forceY = ForcesTask.GetThrustForceY(VerticalSpeed);
		//	Player = physics.MoveRocket(Player, forceX, forceY, turn, spaceSize, 0.3);
		//}

		public bool IsCompleted => (((Player1.Location - Exit).Length < 40) && ((Player2.Location - Exit).Length < 40));
		public bool IsOnButton => (((Player1.Location - Button).Length < 40) || ((Player2.Location - Button).Length < 40));

		public void Move(Size spaceSize,bool isFirst, Turn turn, int HorizontalSpeed, int VerticalSpeed)
		{
			var forceX = Force.GetThrustForceX(HorizontalSpeed);
			var forceY = Force.GetThrustForceY(VerticalSpeed);
			LevelBot.SaveTurn(isFirst, forceX, forceY, HorizontalSpeed, VerticalSpeed);
			if (isFirst)
			{
				Player1 = physics.MovePlayer(Player1, forceX, forceY, turn, spaceSize, 0.3, Map);
			}
			else
			{
				Player2 = physics.MovePlayer(Player2, forceX, forceY, turn, spaceSize, 0.3, Map);	
			}
		}

		public void MoveBot(Player player) 
		{
            if (isFirst && (IterationCount > 0))
			{
				Player2 = LevelBot.BotMove(Player2, isFirst, Map); //МАР
			}
			else
			{
				Player1 = LevelBot.BotMove(Player1, isFirst, Map);
			}
		}

		public void OpenDoor(Level level) 
		{
			if (isClose)
			{
				level.Door = Vector.Zero;
				level.Map.mapObjects.RemoveAt(Map.DoorIndex-1);
				isClose = false;
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