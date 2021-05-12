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
        private static Level level = new Level("Test", player1, player2,new Vector(500,500) ,new Physics(), 3, 7);
        Size spaceSize = new Size(900, 900);
        Player expected1 = level.Player1;
        Player expected2 = level.Player2;

        [TestCase(Keys.W, true)]
        [TestCase(Keys.W, false)]
        [TestCase(Keys.A, true)]
        [TestCase(Keys.A, false)]
        [TestCase(Keys.S, true)]
        [TestCase(Keys.S, false)]
        [TestCase(Keys.D, true)]
        [TestCase(Keys.D, false)]

        public void TestCanOneOfPlayerMoveUp(Keys key, bool isFirst)
        {
            if(isFirst)
            expected1 = level.Move(spaceSize, isFirst, Turn.None, 0, 70);
            else
            expected2 = level.Move(spaceSize, isFirst, Turn.None, 0, 70);

            NUnit.Framework.Assert.AreEqual(player1.Location.X, expected1.Location.X, 50);
            NUnit.Framework.Assert.AreEqual(player1.Location.Y, expected1.Location.Y, 50);
            NUnit.Framework.Assert.AreEqual(player2.Location.X, expected2.Location.X, 50);
            NUnit.Framework.Assert.AreEqual(player2.Location.Y, expected2.Location.Y, 50);
        }

        [TestCase(Keys.W, true)]
        [TestCase(Keys.W, false)]
        [TestCase(Keys.A, true)]
        [TestCase(Keys.A, false)]
        [TestCase(Keys.S, true)]
        [TestCase(Keys.S, false)]
        [TestCase(Keys.D, true)]
        [TestCase(Keys.D, false)]
        public void TestCanOneOfPlayerMoveDown(Keys key, bool isFirst)
        {
            if (isFirst)
                expected1 = level.Move(spaceSize, isFirst, Turn.None, 0, -70);
            else
                expected2 = level.Move(spaceSize, isFirst, Turn.None, 0, -70);

            NUnit.Framework.Assert.AreEqual(player1.Location.X, expected1.Location.X, 50);
            NUnit.Framework.Assert.AreEqual(player1.Location.Y, expected1.Location.Y, 50);
            NUnit.Framework.Assert.AreEqual(player2.Location.X, expected2.Location.X, 50);
            NUnit.Framework.Assert.AreEqual(player2.Location.Y, expected2.Location.Y, 50);
        }

        [TestCase(Keys.W, true)]
        [TestCase(Keys.W, false)]
        [TestCase(Keys.A, true)]
        [TestCase(Keys.A, false)]
        [TestCase(Keys.S, true)]
        [TestCase(Keys.S, false)]
        [TestCase(Keys.D, true)]
        [TestCase(Keys.D, false)]
        public void TestCanOneOfPlayerMoveLeft(Keys key, bool isFirst)
        {
            if (isFirst)
                expected1 = level.Move(spaceSize, isFirst, Turn.None, -70, 0);
            else
                expected2 = level.Move(spaceSize, isFirst, Turn.None, -70, 0);

            NUnit.Framework.Assert.AreEqual(player1.Location.X, expected1.Location.X, 50);
            NUnit.Framework.Assert.AreEqual(player1.Location.Y, expected1.Location.Y, 50);
            NUnit.Framework.Assert.AreEqual(player2.Location.X, expected2.Location.X, 50);
            NUnit.Framework.Assert.AreEqual(player2.Location.Y, expected2.Location.Y, 50);
        }

        [TestCase(Keys.W, true)]
        [TestCase(Keys.W, false)]
        [TestCase(Keys.A, true)]
        [TestCase(Keys.A, false)]
        [TestCase(Keys.S, true)]
        [TestCase(Keys.S, false)]
        [TestCase(Keys.D, true)]
        [TestCase(Keys.D, false)]
        public void TestCanOneOfPlayerMoveRight(Keys key, bool isFirst)
        {
            if (isFirst)
                expected1 = level.Player1;
            else
                expected2 = level.Player2;
            Assert();
        }

        private void Assert()
        {
            NUnit.Framework.Assert.AreEqual(player1.Location.X, expected1.Location.X, 50);
            NUnit.Framework.Assert.AreEqual(player1.Location.Y, expected1.Location.Y, 50);
            NUnit.Framework.Assert.AreEqual(player2.Location.X, expected2.Location.X, 50);
            NUnit.Framework.Assert.AreEqual(player2.Location.Y, expected2.Location.Y, 50);
        }

        public Level MovePlayer(Keys key, bool isFirst) 
        {
            if (key == Keys.W) level.Move(spaceSize, isFirst, Turn.None, 0, 70);
            if (key == Keys.A) level.Move(spaceSize, isFirst, Turn.None, -70, 0);
            if (key == Keys.S) level.Move(spaceSize, isFirst, Turn.None, 0, -70);
            if (key == Keys.D) level.Move(spaceSize, isFirst, Turn.None, 70, 0);
            return level;
        }
    }
}
