﻿namespace Chess
{
    public class Chess
    {
        public string fen { get; private set; }
        private Board board;
        private Moves moves;
        private List<FigureMoving> allMoves;
        public Chess ( string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1" )
        {
            this.fen = fen;
            board = new Board( fen );
            moves = new Moves( board );
        }

        Chess ( Board board )
        {
            moves = new Moves( board );
            this.board = board;
            fen = board.fen;

        }

        public Chess Move ( string move )
        {

            FigureMoving figureMoving = new FigureMoving( move );
            if ( !moves.CanMove( figureMoving ) )
            {
                return this;
            }
            Board nextBoard = board.Move( figureMoving );
            Chess nextChess = new Chess( nextBoard );
            return nextChess;
        }

        public char GetFigureAt ( int x, int y )
        {
            Square square = new Square( x, y );
            Figure figure = board.GetFigureAt( square );

            return figure == Figure.None ? '.' : (char)figure;
        }

        private void FindAllMoves ()
        {
            allMoves = new List<FigureMoving>();
            foreach ( FigureOnSquare fs in board.YieldFigures() )
            {
                foreach ( Square to in Square.YieldSquares() )
                {
                    FigureMoving fm = new FigureMoving( fs, to );
                    if ( moves.CanMove( fm ) )
                    {
                        allMoves.Add( fm );
                    }
                }
            }
        }
        public List<string> GetAllMoves ()
        {   
            FindAllMoves();
            List<string> list = new List<string>();
            foreach ( FigureMoving fm in allMoves )
            {
                list.Add( fm.ToString() );
            }
            return list;
        }
    }
}