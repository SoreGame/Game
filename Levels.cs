using System;
using System.Collections.Generic;

namespace Game
{
	public class LevelsTask
	{
		static readonly Physics standardPhysics = new Physics();

		public static IEnumerable<Level> CreateLevels()
		{
			yield return new Level("Zero",
				new Player(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
				standardPhysics);
		}
	}
}