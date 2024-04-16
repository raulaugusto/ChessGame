namespace ChessLogic
{
    public enum Player
    {
        None,
        White,
        Black,
    }

    static class PlayerExtensions
    {
        public static Player Oponnent(this Player player)
        {
            return player switch
            {
                Player.White => Player.Black,
                Player.Black => Player.Black,
                _ => Player.None,
            };
        }
    }
}
