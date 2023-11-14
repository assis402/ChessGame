using System;

namespace ChessGame.Board;

public class BoardException : Exception
{
    public BoardException(string msg) : base(msg)
    {
    }
}