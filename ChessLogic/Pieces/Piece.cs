namespace ChessLogic
{
    public abstract class Piece
    {
        public abstract PieceType Type { get; }
        public abstract Player Color { get; }
        public bool HasMoved { get; set; } = false;
        public abstract Piece Copy();
        public abstract IEnumerable<Move> GetMoves(Position from, Board board);


        protected IEnumerable<Position> GetDirections(Position from, Board board, Direction[] dirs)
        {
            return dirs.SelectMany(dir => MovePositionsInDir(from, board, dir));
        }
        protected IEnumerable<Position> MovePositionsInDir(Position from, Board board, Direction dir)

        {
            for (Position pos = from + dir; Board.isInside(pos); pos += dir)
            {
                if (board.isEmpty(pos))
                {
                    yield return pos;
                    continue;
                }

                Piece piece = board[pos];

                if (piece.Color != Color)
                {
                    yield return pos;
                    break;
                }
                yield break;
            }
        }

        public virtual bool CanCaptureOpponentKing(Position from, Board board)
        {
            return GetMoves(from, board).Any(move =>
            {
                Piece piece = board[move.toPosition];
                return piece != null && piece.Type == PieceType.King;
            });
        }

    }
}
