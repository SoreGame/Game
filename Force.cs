using System;


namespace Game
{
	public class Force
	{
		public static PlayerForce GetThrustForceY(double forceValue)
		{
			return rocket => new Vector(forceValue, 0).Rotate(-0.5 * Math.PI);
		}
		public static PlayerForce GetThrustForceX(double forceValue)
		{
			return rocket => new Vector(forceValue, 0);
		}
	}
}