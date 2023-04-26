using Chess;

namespace Chess
{
    internal class ChessAscii
    {
        static void Main ( string[] args )
        {   
            Random random = new Random();
            Chess chess = new Chess( "rnbqkbnr/1p1111p1/8/8/8/8/1P1111P1/RNBQKBNR w KQkq - 0 1" );
            List<string> list;
            while ( true )
            {   
                list = chess.GetAllMoves();
                Console.WriteLine( chess.fen );
                Console.WriteLine( ChessToAscii( chess ) );
                foreach ( string moves in list )
                {
                    Console.Write( moves + "\t");
                }
                Console.WriteLine();
                string move = Console.ReadLine();
                if ( move == "q" ) break;
                if ( move == "" ) move = list[random.Next(list.Count)];
                chess = chess.Move( move );
            }
        }

        static string ChessToAscii ( Chess chess )
        {
            string text = "  +-----------------+ \n";
            for ( int i = 7; i >= 0; i-- )
            {
                text += i + 1;
                text += " | ";
                for ( int j = 0; j < 8; j++ )
                {
                    text += chess.GetFigureAt( j, i ) + " ";
                }
                text += "|\n";
            }
            text += "  +-----------------+ \n";
            text += "    a b c d e f g h\n";
            return text;
        }
    }
}