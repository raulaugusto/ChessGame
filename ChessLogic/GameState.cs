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
            
    }
}
