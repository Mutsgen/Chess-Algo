using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    enum Figure
    {
        None,
        whiteKing = 'K',
        whiteQueen = 'Q',
        whiteRook = 'R',
        whiteBishop = 'B',
        whiteKnight = 'N',
        whitePawn = 'P',

        blackKing = 'k',
        blackQueen = 'q',
        blackRook = 'r',
        blackBishop = 'b',
        blackKnight = 'n',
        blackPawn = 'p',
    }
    static class FigureMethods
    {
        public static Color GetColor ( this Figure figure )
        {
            if ( figure == Figure.None )
            {
                return Color.None;
            }
            return (figure == Figure.whiteRook ||
                figure == Figure.whiteKing ||
                figure == Figure.whiteKnight||
                figure == Figure.whiteQueen ||
                figure == Figure.whiteBishop||
                figure == Figure.whitePawn) ? Color.white : Color.black;
        }
    }
}
