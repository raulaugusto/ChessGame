﻿namespace ChessLogic
{
    public class Knight : Piece
    {
        public override PieceType Type => PieceType.Knight;
        public override Player Color { get; }

        private static IEnumerable<Position> PotentialToPositions(Position from)
        {
            foreach(Direction vDir in new Direction[] {Direction.North, Direction.South})
            {
                foreach(Direction hDir in new Direction[] {Direction.East, Direction.West})
                {
                    yield return from + 2 * vDir + hDir;
                    yield return from + 2 * hDir + vDir;
                }
            }
        }

        public IEnumerable<Position> MovePositions(Position from, Board board)
        {
            return PotentialToPositions(from).Where(pos => Board.isInside(pos) && (board.isEmpty(pos) || board[pos].Color != Color));
        }

        public Knight(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Knight copy = new Knight(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }



        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositions(from, board).Select(to => new NormalMove(from, to));
        }
    }
}
