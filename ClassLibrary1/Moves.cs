using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Moves
    {
        FigureMoving fm;
        Board board;

        public Moves ( Board board )
        {
            this.board = board;
        }

        public bool CanMove ( FigureMoving fm )
        {
            this.fm = fm;
            return CanMoveFrom() && CanMoveTo() && CanFigureMove();
        }

        private bool CanFigureMove ()
        {
            switch ( fm.figure )
            {
                case Figure.whiteKing:
                case Figure.blackKing:
                    return CanKingMove();
                case Figure.whiteQueen:
                case Figure.blackQueen:
                    return CanStraightMove();
                case Figure.whiteRook:
                case Figure.blackRook:
                    return (fm.SignX == 0 || fm.SignY == 0) && CanStraightMove();
                case Figure.whiteBishop:
                case Figure.blackBishop:
                    return (fm.SignX != 0 && fm.SignY != 0) && CanStraightMove();
                case Figure.whiteKnight:
                case Figure.blackKnight:
                    return CanKnightMove();
                case Figure.whitePawn:
                case Figure.blackPawn:
                    return false;
                default: return false;
            }
        }

        private bool CanStraightMove ()
        {
            Square at = fm.from;
            do
            {
                at = new Square( at.X + fm.SignX, at.Y + fm.SignY );
                if ( at == fm.to )
                {
                    return true;
                }
            } while ( at.OnBoard() && board.GetFigureAt( at ) == Figure.None );
            return false;
        }

        private bool CanKingMove ()
        {
            if ( fm.AbsDeltaX <= 1 && fm.AbsDeltaY <= 1 )
            {
                return true;
            }
            return false;
        }

        private bool CanKnightMove ()
        {
            if ( fm.AbsDeltaX == 1 && fm.AbsDeltaY == 2 || fm.AbsDeltaX == 2 && fm.AbsDeltaY == 1 )
            {
                return true;
            }
            return false;
        }

        private bool CanMoveTo ()
        {
            return fm.to.OnBoard() && fm.to != fm.from && board.GetFigureAt( fm.to ).GetColor() != board.moveColor;
        }

        private bool CanMoveFrom ()
        {
            return fm.from.OnBoard() && fm.figure.GetColor() == board.moveColor;
        }
    }
}
