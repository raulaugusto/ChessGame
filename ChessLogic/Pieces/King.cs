
namespace ChessLogic
{
    public class King : Piece
    {
        public override PieceType Type => PieceType.King;
        public override Player Color { get; }
        private static readonly Direction[] dirs =
            [
                Direction.North,
                Direction.South,
                Direction.East,
                Direction.West,
                Direction.NorthWest,
                Direction.NorthEast,
                Direction.SouthWest,
                Direction.SouthEast,
            ];
        public King(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            King copy = new King(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }


        public IEnumerable<Position> MovePositions(Position from, Board board)
        {
            foreach (Direction dir in dirs)
            {
                Position to = from + dir;
                if (!Board.isInside(to))
                {
                    continue;
                }

                if (board.isEmpty(to) || board[to].Color != Color)
                {
                    yield return to;
                }
            }
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            foreach (Position pos in MovePositions(from, board))
            {
                yield return new NormalMove(from, pos);
            }
        }

        public override bool CanCaptureOpponentKing(Position from, Board board)
        {
            return MovePositions(from, board).Any(move =>
            {
                Piece piece = board[move];
                return piece != null && piece.Type == PieceType.King;
            });
        }

        //public Position CanCastleQueenSide(Board board)
        //{
        //    Piece king = this;
        //    if (!king.HasMoved)
        //    {
        //        if (king.Color == Player.White)
        //        {
        //            int row = 0;
        //            for (int c = 4; c > 0; c++)
        //            {
        //                if (!board.isEmpty(new Position(row, c)))
        //                {
        //                    return null;
        //                }
        //            } 
        //            return new Position(row, 2);
        //        }
        //        else
        //        {
        //            int row = 7;
        //            for (int c = 4; c > 0; c++)
        //            {
        //                if (!board.isEmpty(new Position(row, c)))
        //                {
        //                    return null;
        //                }
        //            }
        //            return new Position(row, 2);
        //        }

        //    }
        //    else
        //    {
        //        return new Position(0, 0);
        //    }
        //}
    }
}
