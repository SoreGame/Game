using System;
using System.Collections.Generic;

namespace Game
{
	public class LevelsTask
	{
		static readonly Physics standardPhysics = new Physics(); //Переданы стандартные параметры физики (макс. скорость = 300, скорость разворота 0.15)

		public static IEnumerable<Level> CreateLevels()
		{
			yield return new Level("Zero", //Название уровня
				new Player(new Vector(100, 500), Vector.Zero, -0.5 * Math.PI, true), //Стартовая позиция, начальная скорость, поворот 
				new Player(new Vector(200, 250), Vector.Zero, -0.5 * Math.PI, true),
				standardPhysics); 
		}
	}
}