using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    struct Square
    {
        public static Square none = new Square(-1, -1);
        public int X { get; private set; }
        public int Y { get; private set; }

        public Square(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public Square(string pos)
        {
            if (pos == null) throw new ArgumentNullException(nameof(pos));
            if (pos.Length != 2  ) throw new ArgumentException();
            if (pos[0] >= 'a' && pos[0] <= 'h' && pos[1] >= '1' && pos[1] <= '8')
            {
                X = (int)(pos[0] - 'a');
                Y = (int)(pos[1] - '1');
            }
            else this = none; 
        }
        public static IEnumerable<Square> YieldSquares ()
        {
            for ( int y = 0; y < 8; y++ )
            {
                for ( int x = 0; x < 8; x++ )
                {
                    yield return new Square( x, y );
                }
            }
        }

        public string Name { get { return ((char)('a' + X)).ToString() + (Y + 1).ToString(); } }
        public bool OnBoard ()
        {
            return this.X >= 0 && this.Y >= 0 && this.X < 8 && this.Y < 8;
        }

        public static bool operator == (Square left, Square right)
        {
            return left.X == right.X && left.Y == right.Y;
        }
        public static bool operator != ( Square left, Square right )
        {
            return !(left == right);
        }
        
    }
}
