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
            Copy().HasMoved = HasMoved;
            return copy;
        }
    }
}
