using ChessGame;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ChessBoard
{
    abstract class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }

        public int qtsMoves { get; protected set; }
        public Board board { get; set; }

        public Piece(Board board, Color color)
        {
            this.position = null;
            this.color = color;
            this.qtsMoves = 0;
            this.board = board;
        }

        public void increaseQtsMoves()
        {
            qtsMoves++;
        }
        public void retireQtsMoves()
        {
            qtsMoves--;
        }

        public bool isTherePossibleMove()
        {
            bool[,] mat = possibleMoves();
            for (int i=0; i<board.Lines; i++)
            {
                for (int j = 0; j < board.Rows; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool canMoveTo(Position pos)
        {
            return possibleMoves()[pos.line, pos.row];
        }

        public abstract bool[,] possibleMoves();
    }
}
