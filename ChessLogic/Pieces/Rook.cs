namespace ChessLogic
{
    public class Rook : Piece
    {
        public override PieceType Type => PieceType.Rook;
        public override Player Color { get; }

        private static readonly Direction[] dirs =
        [
            Direction.North,
            Direction.South,
            Direction.East,
            Direction.West,
        ];
        public Rook(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Rook copy = new Rook(Color);
            Copy().HasMoved = HasMoved;
            return copy;
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            foreach (var dir in dirs)
            {
                foreach (var to in MovePositionsInDir(from, board, dir))
                {
                    yield return new NormalMove(from, to);
                }
            }
        }
    }
}
