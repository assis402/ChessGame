using ChessGame;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ChessBoard
{
    class Piece
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
    }
}
