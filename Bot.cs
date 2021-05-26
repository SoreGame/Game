using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Bot
    {
        public Bot(Physics physics) 
        {
            Physics = physics;
        }

        private Queue<Comand> SavedFirstPlayerComands = new Queue<Comand>();
        private Queue<Comand> SavedSecondPlayerComands = new Queue<Comand>();

        private Queue<Comand> FirstBufferComands = new Queue<Comand>();
        private Queue<Comand> SecondBufferComands = new Queue<Comand>();

        private Physics Physics;
        private readonly Size spaceSize = new Size(900, 900);


        public void SaveTurn(bool isFirst, PlayerForce forceX, PlayerForce forceY, int HorizontalSpeed, int VerticalSpeed) 
        {
            if (isFirst)
            {
                SavedFirstPlayerComands.Enqueue(new Comand() { IsFirst = isFirst, forceX = forceX, forceY = forceY });
                FirstBufferComands.Enqueue(new Comand() { IsFirst = isFirst, forceX = forceX, forceY = forceY });
            }
            else
            {
                SavedSecondPlayerComands.Enqueue(new Comand() { IsFirst = isFirst, forceX = forceX, forceY = forceY });
                SecondBufferComands.Enqueue(new Comand() { IsFirst = isFirst, forceX = forceX, forceY = forceY });
            }
        }

        public Player BotMove(Player player, bool isFirtst, MapController map)
        {

            PlayerForce forceX = Force.GetThrustForceX(0);
            PlayerForce forceY = Force.GetThrustForceX(0);
            return !isFirtst 
                ? UseComandsFromQueue(player, forceX, forceY, SavedFirstPlayerComands, map) 
                : UseComandsFromQueue(player, forceX, forceY, SavedSecondPlayerComands, map);
        }

        private Player UseComandsFromQueue(Player player, PlayerForce forceX, PlayerForce forceY, Queue<Comand> queue, MapController map)
        {
            if (queue.Count > 0)
            {
                var comand = queue.Dequeue();
                forceX = comand.forceX;
                forceY = comand.forceY;
            }
            return Physics.MovePlayer(player, forceX, forceY, Turn.None, spaceSize, 0.3, map);
        }

        public void ClearBothQueue() 
        {
            SavedFirstPlayerComands.Clear();
            SavedSecondPlayerComands.Clear();
        }

        public void ClearFirstQueue() 
        {
            SavedFirstPlayerComands.Clear();
        }

        public void ClearSecondQueie() 
        {
            SavedSecondPlayerComands.Clear();
        }

        public void UseBuffer(bool IsFirst)
        {
            if (IsFirst)
                SavedSecondPlayerComands = SecondBufferComands;
            else
                SavedFirstPlayerComands = FirstBufferComands;
        }
    }
}
