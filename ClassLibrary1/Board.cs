using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Board
    {
        public string fen { get; private set; }
        Figure[,] figures;
        public Color moveColor { get; private set; }

        public int moveNumber { get; private set; }

        public Board ( string fen )
        {
            this.fen = fen;
            figures = new Figure[8, 8];
            Init();
        }

        /*
         "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"
         */
        void Init ()
        {
            string[] partsFen = fen.Split();
            if ( partsFen.Length != 6 ) return;
            InitFigurs( partsFen[0] );
            moveNumber = int.Parse( partsFen[5] );
            this.moveColor = partsFen[1] == "b" ? Color.black : Color.white;
            /*  SetFigureAt( new Square( "a1" ), Figure.whiteKing );
                SetFigureAt( new Square( "h8" ), Figure.blackKing );
                this.moveColor = Color.white;
                this.moveNumber = 0;*/
        }
        void InitFigurs ( string data )
        {
            for ( int i = 8; i >= 2; i-- )
            {
                data = data.Replace( i.ToString(), (i - 1).ToString() + "1" );
            }
            data = data.Replace( "1", "." );
            string[] lines = data.Split( '/' );
            for ( int y = 7; y >= 0; y-- )
            {
                for ( int x = 0; x < 8; x++ )
                {
                    figures[x, y] = lines[7 - y][x] == '.' ? Figure.None : (Figure)lines[7 - y][x];
                }
            }

        }
        public Figure GetFigureAt ( Square square )
        {
            if ( square.OnBoard() ) return figures[square.X, square.Y];
            return Figure.None;
        }

        void SetFigureAt ( Square square, Figure figure )
        {

            if ( square.OnBoard() )
            {
                figures[square.X, square.Y] = figure;
            }
        }

        public Board Move ( FigureMoving figureMoving )
        {
            Board board = new Board( fen );
            board.SetFigureAt( figureMoving.from, Figure.None );
            board.SetFigureAt( figureMoving.to, figureMoving.promotion == Figure.None ? figureMoving.figure : figureMoving.promotion );
            if ( moveColor == Color.black )
            {
                board.moveNumber++;
            }

            board.moveColor = moveColor.FlipColor();
            board.GenerateFEN();
            return board;
        }

        private void GenerateFEN ()
        {
            try
            {
                this.fen = FenFigure() + " " + (moveColor == Color.white ? "w" : "b") + " - - 0 " + moveNumber.ToString();
            }
            catch ( Exception )
            {
                throw new NotImplementedException();
            }
        }

        private string FenFigure ()
        {
            StringBuilder sb = new StringBuilder();
            for ( int y = 7; y >= 0; y-- )
            {
                for ( int x = 0; x < 8; x++ )
                {
                    sb.Append( figures[x, y] == Figure.None ? '1' : (char)figures[x, y] );

                }
                if ( y > 0 )
                {
                    sb.Append( "/" );
                }
            }
            string eight = "11111111";
            for ( int i = 8; i >= 2; i-- )
            {
                sb.Replace( eight.Substring( 0, i ), i.ToString() );
            }
            return sb.ToString();
            throw new NotImplementedException();
        }

        public IEnumerable<FigureOnSquare> YieldFigures ()
        {
            foreach (Square square in Square.YieldSquares())
            {
                if(GetFigureAt(square).GetColor() == moveColor )
                {
                    yield return new FigureOnSquare(GetFigureAt(square), square);
                }
            }
        }
    }
}
