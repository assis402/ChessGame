using ChessGame.Board;
using ChessGame.Pieces;

namespace ChessGame;

internal class GameScreen : IGameScreen
{
    #region Public Methods

    public void PrintMatch(ChessMatch match)
    {
        PrintBoard(match.ChessBoard);
        WriteLine();
        
        PrintCapturedPieces(match);
        WriteLine();
        WriteLine("Turn: " + match.Turn + "º");
        
        if (!match.Finished)
        {
            WriteLine("Next move: " + match.CurrentPlayer);
            
            if (match.Check)
            {
                BackgroundColor = ConsoleColor.DarkYellow;
                WriteLine("CHECK!");
            }
        }
        else
        {
            BackgroundColor = ConsoleColor.DarkRed;;
            WriteLine("CHECKMATE!");
            WriteLine("Winner: " + match.CurrentPlayer);
        }
        
        SetOriginalBackgroundColor();
    }
    
    public void PrintBoard(ChessBoard chessBoard, bool[,] possiblePositions)
    {
        for (var i = 0; i < chessBoard.Rows; i++)
        {
            Write(" " + (8 - i) + " ");

            for (var j = 0; j < chessBoard.Columns; j++)
            {
                if (possiblePositions[i, j])
                    BackgroundColor = ConsoleColor.DarkBlue;
                else
                    SetOriginalBackgroundColor();

                PrintPiece(chessBoard.GetPieceByPosition(i, j));
            }

            SetOriginalBackgroundColor();
            WriteLine();
        }

        PrintBoardColumns();
    }

    public void PrintError(string errorMessage)
    {
        ForegroundColor = ConsoleColor.Red;
        
        WriteLine(errorMessage);
        ReadLine();
        
        SetOriginalForegroundColor();
    }

    public Position ReadOrigin()
    {
        WriteLine();
        WriteLine("Example: 1b (row and column).");
        Write("Enter origin: ");
        return ReadPlayerChessPositionEntry().ConvertToNumericPosition();
    }

    public Position ReadDestiny()
    {
        WriteLine();
        WriteLine("Example: 1b (row and column).");
        Write("Enter destiny: ");
        return ReadPlayerChessPositionEntry().ConvertToNumericPosition();
    }

    #endregion

    #region Private Methods

    private ChessPosition ReadPlayerChessPositionEntry()
    {
        var validRows = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        var validColumns = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
        
        try
        {
            var entry = ReadLine();
            var row = int.Parse(entry[0].ToString());
            var column = entry[1];
            
            if (!validRows.Contains(row) || !validColumns.Contains(column))
                throw new Exception();
            
            return new ChessPosition(column, row);
        }
        catch (Exception)
        {
            throw new BoardException("Enter a valid position.");
        }
    }

    private void PrintCapturedPieces(ChessMatch chessMatch)
    {
        WriteLine("Captured pieces: ");
        Write("White: ");
        ForegroundColor = ConsoleColor.White;
        PrintGroup(chessMatch.GetCapturedPieces(Color.White));
        SetOriginalForegroundColor();
        
        WriteLine();
        Write("Black: ");
        ForegroundColor = ConsoleColor.Yellow;
        PrintGroup(chessMatch.GetCapturedPieces(Color.Black));
        SetOriginalForegroundColor();
        WriteLine();
    }

    private void PrintGroup(HashSet<Piece> group)
    {
        Write("[");
        
        if (group.Count == 1)
        {
            Write(group.ElementAt(0));
        }
        else if (group.Count > 1)
        {
            Write(group.ElementAt(0));
            
            for (var i = 1; i < group.Count; i++) 
                Write(" " + group.ElementAt(i));
        }

        Write("]");
    }

    private void PrintBoard(ChessBoard chessBoard)
    {
        for (var i = 0; i < chessBoard.Rows; i++)
        {
            Write(" " + (8 - i) + " ");
            
            for (var j = 0; j < chessBoard.Columns; j++) 
                PrintPiece(chessBoard.GetPieceByPosition(i, j));

            WriteLine();
        }

        PrintBoardColumns();
    }

    private void PrintPiece(Piece piece)
    {
        if (piece == null)
        {
            Write("- ");
        }
        else
        {
            if (piece.Color == Color.White)
            {
                ForegroundColor = ConsoleColor.White;
                Write(piece);
            }
            else
            {
                ForegroundColor = ConsoleColor.Yellow;
                Write(piece);
            }

            SetOriginalForegroundColor();
            Write(" ");
        }
    }
    
    private void PrintBoardColumns()
        => WriteLine("   a b c d e f g h ");
    
    private void SetOriginalBackgroundColor() => BackgroundColor = ConsoleColor.Black;
    
    private void SetOriginalForegroundColor() => ForegroundColor = ConsoleColor.Gray;

    #endregion
}