namespace ChessLogic
{
    public class Result
    {
        public Player Winner { get; }
        public EndReason EndReason { get; }
        public Result(Player winner, EndReason endReason)
        {
            Winner = winner;
            EndReason = endReason;
        }

        public static Result Win(Player winner)
        {
            return new Result(winner, EndReason.Checkmate);
        }
        public static Result Draw(EndReason endReason)
        {
            return new Result(Player.None, endReason);
        }
    }
}
