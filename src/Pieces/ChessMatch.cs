using ChessGame.Board;

namespace ChessGame.Pieces;

internal class ChessMatch
{
    internal ChessMatch()
    {
        ChessBoard = new ChessBoard(8, 8);
        Turn = 1;
        CurrentPlayer = Color.White;
        Finished = false;
        Check = false;
        EnPassant = null;
        _pieces = new HashSet<Piece>();
        _capturedPieces = new HashSet<Piece>();
        
        PutPiecesOnTheBoard();
    }
    
    internal ChessBoard ChessBoard { get; private set; }
    
    internal int Turn { get; private set; }
    
    internal Color CurrentPlayer { get; private set; }
    
    internal bool Finished { get; private set; }
    
    private readonly HashSet<Piece> _pieces;
    
    private readonly HashSet<Piece> _capturedPieces;
    
    internal bool Check { get; private set; }
    
    internal Piece EnPassant { get; private set; }
    
    private Piece Move(Position origin, Position destiny)
    {
        var piece = ChessBoard.RemovePiece(origin);
        piece.IncreaseMove();
        
        var capturedPiece = ChessBoard.RemovePiece(destiny);
        ChessBoard.PutPiece(piece, destiny);
        
        if (capturedPiece != null) _capturedPieces.Add(capturedPiece);

        // EN PASSANT

        if (piece is Pawn)
        {
            if (origin.Column != destiny.Column && capturedPiece == null)
            {
                Position posP;
                if (piece.Color == Color.White)
                    posP = new Position(destiny.Row + 1, destiny.Column);

                else
                    posP = new Position(destiny.Row - 1, destiny.Column);

                capturedPiece = ChessBoard.RemovePiece(posP);
                _capturedPieces.Add(capturedPiece);
            }
        }

        // ROQUE

        if (piece is King && destiny.Column == origin.Column + 2)
        {
            var originR = new Position(origin.Row, origin.Column + 3);
            var destinyR = new Position(origin.Row, origin.Column + 1);
            var removePiece = ChessBoard.RemovePiece(originR);
            removePiece.IncreaseMove();
            ChessBoard.PutPiece(removePiece, destinyR);
        }

        if (piece is King && destiny.Column == origin.Column - 2)
        {
            var originR = new Position(origin.Row, origin.Column - 4);
            var destinyR = new Position(origin.Row, origin.Column - 1);
            var removePiece = ChessBoard.RemovePiece(originR);
            removePiece.IncreaseMove();
            ChessBoard.PutPiece(removePiece, destinyR);
        }

        return capturedPiece;
    }

    internal void TryMovePiece(Position origin, Position destiny)
    {
        var capturedPiece = Move(origin, destiny);

        if (IsInCheck(CurrentPlayer))
        {
            UndoMove(origin, destiny, capturedPiece);
            throw new BoardException("You can't put yourself in checkmate.");
        }

        var piece = ChessBoard.GetPieceByPosition(destiny);

        // PROMOTION SPECIAL MOVE

        if (piece is Pawn)
        {
            if (piece.Color == Color.White && destiny.Row == 0 || piece.Color == Color.Black && destiny.Row == 7)
            {
                piece = ChessBoard.RemovePiece(destiny);
                _pieces.Remove(piece);
                Piece queen = new Queen(ChessBoard, piece.Color);
                ChessBoard.PutPiece(queen, destiny);
                _pieces.Add(queen);
            }
        }

        if (IsInCheck(GetOpponentColor(CurrentPlayer)))
            Check = true;
        else
            Check = false;

        if (VerifyCheckMate(GetOpponentColor(CurrentPlayer)))
        {
            Finished = true;
        }
        else
        {
            Turn++;
            ChangePlayer();
        }

        // EN PASSANT

        if (piece is Pawn && (destiny.Row == origin.Row - 2 || destiny.Row == origin.Row + 2))
            EnPassant = piece;
        else
            EnPassant = null;
    }

    private void UndoMove(Position origin, Position destiny, Piece capturedPiece)
    {
        var piece = ChessBoard.RemovePiece(destiny);
        piece.DecreaseMove();
        if (capturedPiece != null)
        {
            ChessBoard.PutPiece(capturedPiece, destiny);
            _capturedPieces.Remove(capturedPiece);
        }

        ChessBoard.PutPiece(piece, origin);

        // EN PASSANT

        if (piece is Pawn)
        {
            if (origin.Column != destiny.Column && capturedPiece == EnPassant)
            {
                var pawn = ChessBoard.RemovePiece(destiny);
                Position posP;
                if (piece.Color == Color.White)
                {
                    posP = new Position(3, destiny.Column);
                }
                else
                {
                    posP = new Position(4, destiny.Column);
                }

                ChessBoard.PutPiece(pawn, posP);
            }
        }

        // ROQUE

        if (piece is King && destiny.Column == origin.Column + 2)
        {
            var originR = new Position(origin.Row, origin.Column + 3);
            var destinyR = new Position(origin.Row, origin.Column + 1);
            var removePiece = ChessBoard.RemovePiece(destinyR);
            removePiece.DecreaseMove();
            ChessBoard.PutPiece(removePiece, originR);
        }

        if (piece is King && destiny.Column == origin.Column - 2)
        {
            var originR = new Position(origin.Row, origin.Column - 4);
            var destinyR = new Position(origin.Row, origin.Column - 1);
            var r = ChessBoard.RemovePiece(destinyR);
            r.DecreaseMove();
            ChessBoard.PutPiece(r, originR);
        }
    }

    internal void ValidateOriginPosition(Position position)
    {
        if (ChessBoard.GetPieceByPosition(position) == null)
            throw new BoardException("There is no piece in the chosen position.");
        else if (CurrentPlayer != ChessBoard.GetPieceByPosition(position).Color)
            throw new BoardException("The piece chosen isn't yours.");
        else if (!ChessBoard.GetPieceByPosition(position).IsThereAnyPossibleMove())
            throw new BoardException("There are no possible moves for this piece.");
    }

    internal void ValidateDestinyPosition(Position origin, Position destiny)
    {
        if (!ChessBoard.GetPieceByPosition(origin).IsPossibleMoveToPosition(destiny))
            throw new BoardException("Invalid destination position.");
    }
    
    private void ChangePlayer() 
        => CurrentPlayer = CurrentPlayer == Color.White ? Color.Black : Color.White;

    internal HashSet<Piece> GetCapturedPieces(Color color)
    {
        var auxiliaryHashSet = new HashSet<Piece>();
        
        foreach (var x in _capturedPieces.Where(x => x.Color == color)) 
            auxiliaryHashSet.Add(x);
        
        return auxiliaryHashSet;
    }

    private HashSet<Piece> GetPiecesOnTheBoard(Color color)
    {
        var auxiliaryHashSet = new HashSet<Piece>();
        
        foreach (var x in _pieces.Where(x => x.Color == color)) 
            auxiliaryHashSet.Add(x);

        auxiliaryHashSet.ExceptWith(GetCapturedPieces(color));
        
        return auxiliaryHashSet;
    }

    private Color GetOpponentColor(Color color)
        => color == Color.White ? Color.Black : Color.White;

    private Piece GetKing(Color color) 
        => GetPiecesOnTheBoard(color).OfType<King>().FirstOrDefault();

    private bool IsInCheck(Color color)
    {
        var king = GetKing(color);
        
        if (king == null)
            throw new BoardException("There is no king of " + color + " color on the board.");
        
        foreach (var piece in GetPiecesOnTheBoard(GetOpponentColor(color)))
        {
            var possibleMoves = piece.PossibleMoves();
            
            if (possibleMoves[king.Position.Row, king.Position.Column])
                return true;
        }

        return false;
    }

    private bool VerifyCheckMate(Color color)
    {
        if (!IsInCheck(color))
            return false;

        foreach (var piece in GetPiecesOnTheBoard(color))
        {
            var possibleMoves = piece.PossibleMoves();
            for (var i = 0; i < ChessBoard.Rows; i++)
            {
                for (var j = 0; j < ChessBoard.Columns; j++)
                {
                    if (possibleMoves[i, j])
                    {
                        var origin = piece.Position;
                        var destiny = new Position(i, j);
                        var capturedPiece = Move(origin, destiny);
                        var testCheck = IsInCheck(color);
                            
                        UndoMove(origin, destiny, capturedPiece);
                        
                        if (!testCheck)
                            return false;
                    }
                }
            }
        }

        return true;
    }

    private void PutNewPiece(char column, int row, Piece piece)
    {
        ChessBoard.PutPiece(piece, new ChessPosition(column, row).ConvertToNumericPosition());
        _pieces.Add(piece);
    }

    private void PutPiecesOnTheBoard()
    {
        PutNewPiece('a', 1, new Rook(ChessBoard, Color.White));
        PutNewPiece('b', 1, new Knight(ChessBoard, Color.White));
        PutNewPiece('c', 1, new Bishop(ChessBoard, Color.White));
        PutNewPiece('d', 1, new Queen(ChessBoard, Color.White));
        PutNewPiece('e', 1, new King(ChessBoard, Color.White, this));
        PutNewPiece('f', 1, new Bishop(ChessBoard, Color.White));
        PutNewPiece('g', 1, new Knight(ChessBoard, Color.White));
        PutNewPiece('h', 1, new Rook(ChessBoard, Color.White));
        PutNewPiece('a', 2, new Pawn(ChessBoard, Color.White, this));
        PutNewPiece('b', 2, new Pawn(ChessBoard, Color.White, this));
        PutNewPiece('c', 2, new Pawn(ChessBoard, Color.White, this));
        PutNewPiece('d', 2, new Pawn(ChessBoard, Color.White, this));
        PutNewPiece('e', 2, new Pawn(ChessBoard, Color.White, this));
        PutNewPiece('f', 2, new Pawn(ChessBoard, Color.White, this));
        PutNewPiece('g', 2, new Pawn(ChessBoard, Color.White, this));
        PutNewPiece('h', 2, new Pawn(ChessBoard, Color.White, this));

        PutNewPiece('a', 8, new Rook(ChessBoard, Color.Black));
        PutNewPiece('b', 8, new Knight(ChessBoard, Color.Black));
        PutNewPiece('c', 8, new Bishop(ChessBoard, Color.Black));
        PutNewPiece('d', 8, new Queen(ChessBoard, Color.Black));
        PutNewPiece('e', 8, new King(ChessBoard, Color.Black, this));
        PutNewPiece('f', 8, new Bishop(ChessBoard, Color.Black));
        PutNewPiece('g', 8, new Knight(ChessBoard, Color.Black));
        PutNewPiece('h', 8, new Rook(ChessBoard, Color.Black));
        PutNewPiece('a', 7, new Pawn(ChessBoard, Color.Black, this));
        PutNewPiece('b', 7, new Pawn(ChessBoard, Color.Black, this));
        PutNewPiece('c', 7, new Pawn(ChessBoard, Color.Black, this));
        PutNewPiece('d', 7, new Pawn(ChessBoard, Color.Black, this));
        PutNewPiece('e', 7, new Pawn(ChessBoard, Color.Black, this));
        PutNewPiece('f', 7, new Pawn(ChessBoard, Color.Black, this));
        PutNewPiece('g', 7, new Pawn(ChessBoard, Color.Black, this));
        PutNewPiece('h', 7, new Pawn(ChessBoard, Color.Black, this));
    }
}