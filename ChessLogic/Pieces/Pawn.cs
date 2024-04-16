namespace ChessLogic
{
    public class Pawn : Piece
    {
        public override PieceType Type => PieceType.Pawn;
        public override Player Color { get; }

        public Pawn(Player color) 
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Pawn copy = new Pawn(Color);
            Copy().HasMoved = HasMoved;
            return copy;
        }

        public void Promote(PieceType type)
        {
            switch (type)
            {
                case PieceType.Bishop:
                    this.Equals(PieceType.Bishop);
                    break;
                case PieceType.Rook:
                    this.Equals(PieceType.Rook);
                    break;
                case PieceType.Knight:
                    this.Equals(PieceType.Knight);
                    break;
                case PieceType.Queen:
                    this.Equals(PieceType.Queen);
                    break;
                default:
                    break;
            }
        }
    }
}
