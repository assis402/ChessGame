using ChessGame.Board;
using ChessGame.Pieces;

namespace ChessGame;

internal class Program
{
    private static void Main()
    {
        var chessMatch = new ChessMatch();
        IGameScreen gameScreen = new GameScreen();

        while (!chessMatch.Finished)
        {
            try
            {
                Clear();
                gameScreen.PrintMatch(chessMatch);

                var origin = gameScreen.ReadOrigin();
                chessMatch.ValidateOriginPosition(origin);

                var possiblePositions = chessMatch.ChessBoard.GetPieceByPosition(origin).PossibleMoves();

                Clear();
                gameScreen.PrintBoard(chessMatch.ChessBoard, possiblePositions);
                
                var destiny = gameScreen.ReadDestiny();
                chessMatch.ValidateDestinyPosition(origin, destiny);

                chessMatch.TryMovePiece(origin, destiny);
            }
            catch (BoardException e)
            {
                gameScreen.PrintError(e.Message);
            }
        }

        Clear();
        gameScreen.PrintMatch(chessMatch);
        ReadLine();
    }
}