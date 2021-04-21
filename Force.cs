using System;
using System.Drawing;
using System.Linq;

namespace Game
{
	public class ForcesTask
	{
		public static PlayerForce GetThrustForceY(double forceValue)
		{
			return rocket => new Vector(forceValue, 0).Rotate(rocket.Direction);
		}
		public static PlayerForce GetThrustForceX(double forceValue)
		{
			return rocket => new Vector(forceValue, 0).Rotate(rocket.Direction + 0.5*Math.PI);
		}

		public static PlayerForce Sum(params PlayerForce[] forces)
		{
			return r => forces.Select(force => force(r)).DefaultIfEmpty(Vector.Zero).Aggregate((c1, c2) => c1 + c2);
			//return forces[0];
		}
	}
}