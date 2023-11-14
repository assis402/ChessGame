using ChessGame.Board;
using ChessGame.Pieces;

namespace ChessGame;

internal interface IGameScreen
{
    public void PrintMatch(ChessMatch match);

    public void PrintBoard(ChessBoard chessBoard, bool[,] possiblePositions);

    public void PrintError(string errorMessage);
    
    public Position ReadOrigin();

    public Position ReadDestiny();
}