namespace ChessLogic
{
    public abstract class Move
    {
        public abstract MoveType Type { get; }
        public abstract Position fromPosition { get; }
        public abstract Position toPosition { get; }
        public abstract void Execute(Board board);

        public virtual bool IsLegal(Board board)
        {
            Player movingPlayer = board[fromPosition].Color;
            Board copyBoard = board.Copy();
            Execute(copyBoard);
            return !copyBoard.IsIncheck(movingPlayer);
        }
    }
}
