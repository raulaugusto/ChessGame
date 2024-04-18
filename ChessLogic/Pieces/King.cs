
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
            Copy().HasMoved = HasMoved;
            return copy;
        }


        public IEnumerable<Position> MovePositions(Position from, Board board)
        {
            foreach(Direction dir in dirs)
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
            foreach(Position pos in MovePositions(from, board))
            {
                yield return new NormalMove(from, pos);
            }
        }

    }
}
