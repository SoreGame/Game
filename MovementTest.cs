using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    class MovementTest
    {
        private static Player player1 = new Player(new Vector(100, 200), Vector.Zero);
        private static Player player2 = new Player(new Vector(100, 200), Vector.Zero);
        //private static Level level = new Level("Test", player1, player2,new Vector(500,500) ,new Physics(), 3, 7);
        Size spaceSize = new Size(900, 900);

        public void TestCanOneOfPlayerMoveUp(Keys key, bool isFirst)
        {
           
        }
    }
}
