using ChessGame.Board;

namespace ChessGame.Pieces;

internal class Knight : Piece
{
    internal Knight(ChessBoard chessBoard, Color color) : base(chessBoard, color)
    {
    }
    
    public override string ToString() => "N";

    internal override bool[,] PossibleMoves()
    {
        var match = new bool[ChessBoard.Rows, ChessBoard.Columns];
        var position = new Position(0, 0);

        position.SetValues(Position.Row - 1, Position.Column - 2);
        if (ChessBoard.IsValidPosition(position) && CanMove(position)) 
            match[position.Row, position.Column] = true;
        
        position.SetValues(Position.Row - 2, Position.Column - 1);
        if (ChessBoard.IsValidPosition(position) && CanMove(position)) 
            match[position.Row, position.Column] = true;
        
        position.SetValues(Position.Row - 2, Position.Column + 1);
        if (ChessBoard.IsValidPosition(position) && CanMove(position)) 
            match[position.Row, position.Column] = true;
        
        position.SetValues(Position.Row - 1, Position.Column + 2);
        if (ChessBoard.IsValidPosition(position) && CanMove(position)) 
            match[position.Row, position.Column] = true;
        
        position.SetValues(Position.Row + 1, Position.Column + 2);
        if (ChessBoard.IsValidPosition(position) && CanMove(position)) 
            match[position.Row, position.Column] = true;
        
        position.SetValues(Position.Row + 2, Position.Column + 1);
        if (ChessBoard.IsValidPosition(position) && CanMove(position)) 
            match[position.Row, position.Column] = true;

        position.SetValues(Position.Row + 2, Position.Column - 1);
        if (ChessBoard.IsValidPosition(position) && CanMove(position)) 
            match[position.Row, position.Column] = true;
        
        position.SetValues(Position.Row + 1, Position.Column - 2);
        if (ChessBoard.IsValidPosition(position) && CanMove(position)) 
            match[position.Row, position.Column] = true;

        return match;
    }
}