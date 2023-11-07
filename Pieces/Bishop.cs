using ChessBoard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    internal class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(board, color)
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

            // NW
            pos.DefineValues(Position.Line - 1, Position.Row - 1);
            while (Board.ValidatePosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
                if (Board.GetPieceByPosition(pos) != null && Board.GetPieceByPosition(pos).Color != Color)
                {
                    break;
                }
                pos.DefineValues(pos.Line - 1, pos.Row - 1);
            }

            // NE
            pos.DefineValues(Position.Line - 1, Position.Row + 1);
            while (Board.ValidatePosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
                if (Board.GetPieceByPosition(pos) != null && Board.GetPieceByPosition(pos).Color != Color)
                {
                    break;
                }
                pos.DefineValues(pos.Line - 1, pos.Row + 1);
            }

            // SE
            pos.DefineValues(Position.Line + 1, Position.Row + 1);
            while (Board.ValidatePosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
                if (Board.GetPieceByPosition(pos) != null && Board.GetPieceByPosition(pos).Color != Color)
                {
                    break;
                }
                pos.DefineValues(pos.Line + 1, pos.Row + 1);
            }

            // SW
            pos.DefineValues(Position.Line + 1, Position.Row - 1);
            while (Board.ValidatePosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
                if (Board.GetPieceByPosition(pos) != null && Board.GetPieceByPosition(pos).Color != Color)
                {
                    break;
                }
                pos.DefineValues(pos.Line + 1, pos.Row - 1);
            }


            return mat;

        }

        public override string ToString()
        {
            return "B";
        }

    }
}
