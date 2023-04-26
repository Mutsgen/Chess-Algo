using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Moves
    {
        FigureMoving figureMoving;
        Board board;

        public Moves ( Board board )
        {
            this.board = board;
        }

        public bool CanMove ( FigureMoving figureMoving )
        {
            this.figureMoving = figureMoving;
            return CanMoveFrom() && CanMoveTo() && CanFigureMove();
        }

        private bool CanFigureMove ()
        {
            switch ( figureMoving.figure )
            {
                case Figure.whiteKing:
                case Figure.blackKing:
                    return CanKingMove();
                case Figure.whiteQueen:
                case Figure.blackQueen:
                    return CanStraightMove();
                case Figure.whiteRook:
                case Figure.blackRook:
                    return (figureMoving.SignX == 0 || figureMoving.SignY == 0) && CanStraightMove();
                case Figure.whiteBishop:
                case Figure.blackBishop:
                    return (figureMoving.SignX != 0 && figureMoving.SignY != 0) && CanStraightMove();
                case Figure.whiteKnight:
                case Figure.blackKnight:
                    return CanKnightMove();
                case Figure.whitePawn:
                case Figure.blackPawn:
                    return CanPawnMove();
                default: return false;
            }
        }

        private bool CanPawnMove ()
        {
            if ( figureMoving.from.Y < 1 || figureMoving.from.Y > 6 )
            {
                return false;
            }
            int directionY = figureMoving.figure.GetColor() == Color.white ? 1 : -1;
            return CanPawnGo( directionY ) || CanPawnJump( directionY ) || CanPawnEat( directionY );

        }

        private bool CanPawnEat ( int directionY )
        {
            if ( board.GetFigureAt( figureMoving.to ) != Figure.None &&
                figureMoving.DeltaX == 1 &&
                figureMoving.DeltaY == directionY)
            {
                return true;
            }
            return false;
        }

        private bool CanPawnJump ( int directionY )
        {
            if ( board.GetFigureAt( figureMoving.to ) == Figure.None &&
                figureMoving.DeltaX == 0 &&
                figureMoving.DeltaY == 2 * directionY &&
                (figureMoving.from.Y == 1 || figureMoving.from.Y == 6) &&
                board.GetFigureAt(new Square(figureMoving.from.X, figureMoving.from.Y + directionY)) == Figure.None)
            {
                return true;
            }
            return false;
        }


        private bool CanPawnGo ( int directionY )
        {
            if ( board.GetFigureAt( figureMoving.to ) == Figure.None && figureMoving.DeltaX == 0 && figureMoving.DeltaY == directionY )
            {
                return true;
            }
            return false;
        }

        private bool CanStraightMove ()
        {
            Square at = figureMoving.from;
            do
            {
                at = new Square( at.X + figureMoving.SignX, at.Y + figureMoving.SignY );
                if ( at == figureMoving.to )
                {
                    return true;
                }
            } while ( at.OnBoard() && board.GetFigureAt( at ) == Figure.None );
            return false;
        }

        private bool CanKingMove ()
        {
            if ( figureMoving.AbsDeltaX <= 1 && figureMoving.AbsDeltaY <= 1 )
            {
                return true;
            }
            return false;
        }

        private bool CanKnightMove ()
        {
            if ( figureMoving.AbsDeltaX == 1 && figureMoving.AbsDeltaY == 2 || figureMoving.AbsDeltaX == 2 && figureMoving.AbsDeltaY == 1 )
            {
                return true;
            }
            return false;
        }

        private bool CanMoveTo ()
        {
            return figureMoving.to.OnBoard() && figureMoving.to != figureMoving.from && board.GetFigureAt( figureMoving.to ).GetColor() != board.moveColor;
        }

        private bool CanMoveFrom ()
        {
            return figureMoving.from.OnBoard() && figureMoving.figure.GetColor() == board.moveColor;
        }
    }
}
