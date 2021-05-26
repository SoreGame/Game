using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Block
    {
        public Block(Vector location, Size size)
        {
            Location = location;
            Size = size;
        }

        public double Left()
        {
            return Location.X;
        }

        public double Right()
        {
            return Location.X+Size.Width;
        }

        public double Up()
        {
            return Location.X;
        }

        public double Down()
        {
            return Location.X + Size.Height;
        }

        public readonly Vector Location;
        public readonly Size Size;
        public readonly Vector q;
    }
}
