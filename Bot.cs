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

        Queue<Comand> SavedFirstPlayerTurns = new Queue<Comand>();
        Queue<Comand> SavedSecondPlayerTurns = new Queue<Comand>();

        private Physics Physics;
        private readonly Size spaceSize = new Size(900, 900);


        public void SaveTurn(bool isFirst, int HorizontalSpeed, int VerticalSpeed) 
        {
            if (isFirst)
                SavedFirstPlayerTurns.Enqueue(new Comand() {IsFirst = isFirst, HorizontalSpeed = HorizontalSpeed, VerticalSpeed = VerticalSpeed});
            else
                SavedSecondPlayerTurns.Enqueue(new Comand() {IsFirst = isFirst, HorizontalSpeed = HorizontalSpeed, VerticalSpeed = VerticalSpeed });
        }

        public Player BotMove(Player player, bool isFirtst, PlayerForce forceX, PlayerForce forceY)
        {
            if (isFirtst)
            {
                var turn = SavedFirstPlayerTurns.Dequeue();
                var horizontalSpeed = turn.HorizontalSpeed;
                var verticalSpeed = turn.VerticalSpeed;
                Console.WriteLine("FAKEMOVE1");
                return Physics.MovePlayer(player, forceX, forceY, Turn.None, spaceSize, 0.375);
            }
            else 
            {
                var turn = SavedSecondPlayerTurns.Dequeue();
                var horizontalSpeed = turn.HorizontalSpeed;
                var verticalSpeed = turn.VerticalSpeed;
                Console.WriteLine("FAKEMOVE2");
                return Physics.MovePlayer(player, forceX, forceY, Turn.None, spaceSize, 0.375);
            }
        }
    }

}
