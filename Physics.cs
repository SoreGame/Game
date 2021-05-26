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

        public static bool IsCollide(Player player, Vector directionLocation, MapController map)
        {
            if (player.Location.X + directionLocation.X <= 0 
                || player.Location.X + directionLocation.X >= map.cellSize * (map.MapWidth - 1) 
                || player.Location.Y + directionLocation.Y <= 0 
                || player.Location.Y + directionLocation.Y >= map.cellSize * (map.MapHeight - 1))
                return true;

            for (int i = 0; i < map.mapObjects.Count; i++)
            {
                var currObject = map.mapObjects[i];
                Vector delta = new Vector(((player.Location.X + player.Size / 2) - (currObject.position.X + currObject.size.Width / 2)), 
                    ((player.Location.Y + player.Size / 2) - (currObject.position.Y + currObject.size.Height / 2)));
                if (Math.Abs(delta.X) <= player.Size / 2 + currObject.size.Width / 2)
                {
                    if (Math.Abs(delta.Y) <= player.Size / 2 + currObject.size.Height / 2)
                    {
                        if (delta.X < 0 && directionLocation.X > 0 && player.Location.Y + player.Size / 2 > currObject.position.Y && player.Location.Y + player.Size / 2 < currObject.position.Y + currObject.size.Height)
                        {
                            return true;
                        }
                        if (delta.X > 0 && directionLocation.X < 0 && player.Location.Y + player.Size / 2 > currObject.position.Y && player.Location.Y + player.Size / 2 < currObject.position.Y + currObject.size.Height)
                        {
                            return true;
                        }
                        if (delta.Y < 0 && directionLocation.Y > 0 && player.Location.X + player.Size / 2 > currObject.position.X && player.Location.X + player.Size / 2 < currObject.position.X + currObject.size.Width)
                        {
                            return true;
                        }
                        if (delta.Y > 0 && directionLocation.Y < 0 && player.Location.X + player.Size / 2 > currObject.position.X && player.Location.X + player.Size / 2 < currObject.position.X + currObject.size.Width)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public Physics(double maxVelocity, double maxTurnRate)
        {
            this.maxVelocity = maxVelocity;
            this.maxTurnRate = maxTurnRate;
        }
        public Player MovePlayer(Player player, PlayerForce forceX, PlayerForce forceY, Turn turn, Size spaceSize, double dt, MapController map)
        {
            var turnRate = turn == Turn.Left ? -maxTurnRate : turn == Turn.Right ? maxTurnRate : 0;
            var velocity =  (forceX(player) * dt  + forceY(player) * dt); 
            if (velocity.Length > maxVelocity) velocity = velocity.Normalize() * maxVelocity;
            Vector location = player.Location;
            if (!IsCollide(player, velocity, map))
                location = player.Location + velocity * dt;
            if (location.X < 0) velocity = new Vector(Math.Max(0, velocity.X), velocity.Y);
            if (location.X > spaceSize.Width) velocity = new Vector(Math.Min(0, velocity.X), velocity.Y);
            if (location.Y < 0) velocity = new Vector(velocity.X, Math.Max(0, velocity.Y));
            if (location.Y > spaceSize.Height) velocity = new Vector(velocity.X, Math.Min(0, velocity.Y));
            return new Player(location.BoundTo(spaceSize), velocity);

        }
    }
}
