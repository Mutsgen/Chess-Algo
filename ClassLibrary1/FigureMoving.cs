using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class FigureMoving
    {
        public Figure figure { get; private set; }
        public Square from { get; private set; }
        public Square to { get; private set; }

        public Figure promotion { get; private set; }

        public FigureMoving ( FigureOnSquare fs, Square to, Figure promotion = Figure.None )
        {
            this.figure = fs.figure;
            this.from = fs.square;
            this.to = to;
            this.promotion = promotion;
        }
        public FigureMoving ( string move )
        {
            this.figure = (Figure)move[0];
            this.from = new Square( move.Substring( 1, 2 ) );
            this.to = new Square( move.Substring( 3, 2 ) );
            this.promotion = (move.Length == 6) ? (Figure)move[5] : Figure.None;
        }

        public int DeltaX { get { return to.X - from.X; } }
        public int DeltaY { get { return to.Y - from.Y; } }
        public int AbsDeltaX { get { return Math.Abs( DeltaX ); } }
        public int AbsDeltaY { get { return Math.Abs( DeltaY ); } }
        public int SignX { get { return Math.Sign( DeltaX ); } }
        public int SignY { get { return Math.Sign( DeltaY ); } }

        public override string ToString ()
        {
            string text = (char)figure + from.Name + to.Name;
            if ( promotion != Figure.None )
            {
                text += (char)promotion;
            }
            return text;
        }
    }
}
