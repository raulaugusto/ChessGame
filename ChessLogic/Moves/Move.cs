namespace ChessLogic
{
    public abstract class Move
    {
        public abstract MoveType Type { get; }
        public abstract Position fromPosition { get; }
        public abstract Position toPosition { get; }
        public abstract void Execute(Board board);

            
    }
}
