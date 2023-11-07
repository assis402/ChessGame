using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBoard
{
    public class BoardException : Exception
    {
        public BoardException(string msg) : base(msg)
        {
        }
    }
}
