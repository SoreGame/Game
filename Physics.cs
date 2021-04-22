using System;
using System.Drawing;

namespace Game
{
    public class Physics
    {
        private readonly double maxVelocity;
        private readonly double maxTurnRate;

        public Physics() : this(300.0, 0.15)
        {
        }
        public Physics(double maxVelocity, double maxTurnRate)
        {
            this.maxVelocity = maxVelocity;
            this.maxTurnRate = maxTurnRate;
        }
        public Player MovePlayer(Player player, PlayerForce forceX, PlayerForce forceY, Turn turn, Size spaceSize, double dt)
        {
            var turnRate = turn == Turn.Left ? -maxTurnRate : turn == Turn.Right ? maxTurnRate : 0;
            var dir = player.Direction + turnRate * dt;
            var velocity =  (forceX(player) * dt  + forceY(player) * dt); 
            if (velocity.Length > maxVelocity) velocity = velocity.Normalize() * maxVelocity;
            var location = player.Location + velocity * dt;
            //if (location.X < 0) velocity = new Vector(Math.Max(0, velocity.X), velocity.Y);
            //if (location.X > spaceSize.Width) velocity = new Vector(Math.Min(0, velocity.X), velocity.Y);
            //if (location.Y < 0) velocity = new Vector(velocity.X, Math.Max(0, velocity.Y));
            //if (location.Y > spaceSize.Height) velocity = new Vector(velocity.X, Math.Min(0, velocity.Y));
            return new Player(location, velocity, dir, true);
        }
    }
}
