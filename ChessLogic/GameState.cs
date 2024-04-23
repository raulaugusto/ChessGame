namespace ChessLogic
{
    public class GameState
    {
        public Board Board { get; }

        public Player CurrentPlayer { get; private set; }

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
            Console.WriteLine("test");
            return moveCandidates.Where(move => move.IsLegal(Board));
        }

        public void MakeMove(Move move)
        {
            move.Execute(Board);
            CurrentPlayer = CurrentPlayer.Oponnent();
        }
            
    }
}
