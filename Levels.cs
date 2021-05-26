using System;
using System.Collections.Generic;
using System.Drawing;

namespace Game
{
	public class LevelsTask
	{
		static readonly Physics standardPhysics = new Physics(); //Переданы стандартные параметры физики (макс. скорость = 300, скорость разворота 0.15)

		public static IEnumerable<Level> CreateLevels()
		{
			yield return new Level("Zero", //Название уровня
				new Player(new Vector(500, 500), Vector.Zero/*, true*/), //Стартовая позиция, начальная скорость, поворот 
				new Player(new Vector(500, 250), Vector.Zero/*, true*/),
				//new Vector(500, 250),
				new Block(new Vector(200, 400), new Size(900, 900)),
				standardPhysics, 1, 7,
				MapController.FromText(Properties.Resources.Dungeon1));

			yield return new Level("second", //Название уровня
				new Player(new Vector(200, 200), Vector.Zero/*, true*/), //Стартовая позиция, начальная скорость, поворот 
				new Player(new Vector(200, 300), Vector.Zero/*, true*/),
				//new Vector(100, 00),
				new Block(new Vector(200, 400), new Size(900, 900)),
				standardPhysics, 2, 7,
				MapController.FromText(Properties.Resources.Dungeon2));

			yield return new Level("therd", //Название уровня
				new Player(new Vector(100, 200), Vector.Zero/*, true*/), //Стартовая позиция, начальная скорость, поворот 
				new Player(new Vector(200, 300), Vector.Zero/*, true*/),
				//new Vector(100, 00),
				new Block(new Vector(200, 400), new Size(900, 900)),
				standardPhysics, 3, 7,
				MapController.FromText(Properties.Resources.Dungeon3));
		}
	}
}