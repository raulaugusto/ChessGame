


namespace ChessLogic
{
    public class Bishop : Piece
    {
        public override PieceType Type => PieceType.Bishop;
        public override Player Color { get; }

        public Bishop(Player color)
        {
            Color = color;
        }
        public override Piece Copy()
        {
            Bishop copy = new Bishop(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        private static readonly Direction[] dirs = new Direction[]
        {
            Direction.NorthWest,
            Direction.NorthEast,
            Direction.SouthWest,
            Direction.SouthEast,
        };


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
