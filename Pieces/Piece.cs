using ChessGame;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ChessBoard
{
    public abstract class Piece
    {
        public Position Position { get; protected set; }
        
        public Color Color { get; }

        public int TotalMoves { get; private set; }

        protected Board Board { get; }

        protected Piece(Board board, Color color)
        {
            Position = null;
            Color = color;
            TotalMoves = 0;
            Board = board;
        }

        public void SetPosition(Position position) => Position = position;
        
        public void IncreaseMove() => TotalMoves++;
        
        public void DecreaseMove() => TotalMoves--;

        public bool IsTherePossibleMove()
        {
            var mat = PossibleMoves();
            for (var i = 0; i < Board.Lines; i++)
            {
                for (var j = 0; j < Board.Rows; j++)
                {
                    if (mat[i, j]) return true;
                }
            }

            return false;
        }

        public bool IsPossibleMoveToPosition(Position position) => PossibleMoves()[position.Line, position.Row];

        public abstract bool[,] PossibleMoves();
    }
}