using System.Diagnostics.CodeAnalysis;

namespace ChessLogic
{
    public class GameState
    {
        public Board Board { get; }
        public Player CurrentPlayer { get; private set; }
        public Result Result { get; private set; } = null;

        public GameState(Player player, Board board)
        {
            Board = board;
            CurrentPlayer = player;
        }

        public IEnumerable<Move> LegalMovesForPiece(Position pos)
        {
            if(Board.isEmpty(pos) || Board[pos].Color != CurrentPlayer)
            {
                return Enumerable.Empty<Move>();
            }
            Piece piece = Board[pos];

            IEnumerable<Move> moveCandidates = piece.GetMoves(pos, Board);
            return moveCandidates.Where(move => move.IsLegal(Board));
        }

        public void MakeMove(Move move)
        {
            move.Execute(Board);
            CurrentPlayer = CurrentPlayer.Oponnent();
            CheckForGameOver();
        }
            
        public IEnumerable<Move> AllLegamMovesForPlayer(Player player)
        {
            IEnumerable<Move> moveCandidates = Board.PiecePositionsFor(player).SelectMany(pos =>
            {
                Piece piece = Board[pos];
                return piece.GetMoves(pos, Board);
            });

            return moveCandidates.Where(move => move.IsLegal(Board));
        }
        public void CheckForGameOver()
        {
            if (!AllLegamMovesForPlayer(CurrentPlayer).Any())
            {
                if(Board.IsInCheck(CurrentPlayer))
                {
                    Result = Result.Win(CurrentPlayer.Oponnent());
                }
                else
                {
                    Result = Result.Draw(EndReason.Stalemate);
                }
            }
        }
        public bool IsGameOver()
        {
            return Result != null;
        }
    }
}
