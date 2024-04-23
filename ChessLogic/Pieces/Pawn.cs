
using System.Drawing;

namespace ChessLogic
{
    public class Pawn : Piece
    {
        public override PieceType Type => PieceType.Pawn;
        public override Player Color { get; }

        public Direction forward;

        public Pawn(Player color)
        {
            Color = color;
            if (Color == Player.White) forward = Direction.North;
            else forward = Direction.South;
        }

        public override Piece Copy()
        {
            Pawn copy = new Pawn(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        private static bool CanMoveTo(Position pos, Board board)
        {
            return Board.isInside(pos) && board.isEmpty(pos);
        }

        private bool CanCaptureAt(Position pos, Board board)
        {
            if(!Board.isInside(pos) || board.isEmpty(pos))
            {
                return false;
            }
            return board[pos].Color != Color;
        }

        private IEnumerable<Move> ForwardMoves(Position from, Board board)
        {
            Position oneMovePosition = from + forward;
            if (CanMoveTo(oneMovePosition, board))
            {
                yield return new NormalMove(from, oneMovePosition);
                Position twoMovePosition = oneMovePosition + forward;

                if (!HasMoved && CanMoveTo(oneMovePosition, board))
                {
                    yield return new NormalMove(from, twoMovePosition);
                }
            }
        }

        private IEnumerable<Move> DiagonalMoves(Position from, Board board)
        {
            foreach (Direction dir in new Direction[] { Direction.West, Direction.East })
            {
                Position to = from + dir + forward;

                if(CanCaptureAt(to, board))
                {
                    yield return new NormalMove(from, to);
                }
            }
        }
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return ForwardMoves(from, board).Concat(DiagonalMoves(from, board));
        }

        public override bool CanCaptureOponnentKing(Position from, Board board)
        {
            return DiagonalMoves(from, board).Any(move =>
            {
                Piece piece = board[move.toPosition];
                return piece != null && piece.Type == PieceType.King;
            });
        }
    }
}
