using ChessBoard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    internal class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color)
        {

        }

        private bool canMove(Position pos)
        {
            Piece p = Board.GetPieceByPosition(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Rows];
            Position pos = new Position(0, 0);

            pos.DefineValues(Position.Line - 1, Position.Row - 2);
            if (Board.ValidatePosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }
            pos.DefineValues(Position.Line - 2, Position.Row - 1);
            if (Board.ValidatePosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }
            pos.DefineValues(Position.Line - 2, Position.Row + 1);
            if (Board.ValidatePosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }
            pos.DefineValues(Position.Line - 1, Position.Row + 2);
            if (Board.ValidatePosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }
            pos.DefineValues(Position.Line + 1, Position.Row + 2);
            if (Board.ValidatePosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }
            pos.DefineValues(Position.Line + 2, Position.Row + 1);
            if (Board.ValidatePosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }
            pos.DefineValues(Position.Line + 2, Position.Row - 1);
            if (Board.ValidatePosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }
            pos.DefineValues(Position.Line + 1, Position.Row - 2);
            if (Board.ValidatePosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            return mat;

        }

        public override string ToString()
        {
            return "N";
        }

    }
}
