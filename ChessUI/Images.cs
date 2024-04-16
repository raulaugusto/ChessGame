using System.Windows.Media;
using System.Windows.Media.Imaging;
using ChessLogic;

namespace ChessUI
{
    public class Images
    {
        private static ImageSource LoadImage(string filePath)
        {
            BitmapImage bitMap = new BitmapImage(new Uri(filePath, UriKind.Relative));
            bitMap.DecodePixelHeight = 50;
            bitMap.DecodePixelWidth = 50;
            return bitMap;
        }
        private static readonly Dictionary<PieceType, ImageSource> whiteSources = new()
        {
            {PieceType.Pawn, LoadImage("Assets/wp.png") },
            {PieceType.Bishop, LoadImage("Assets/wb.png")},
            {PieceType.Rook, LoadImage("Assets/wr.png")},
            {PieceType.Knight, LoadImage("Assets/wn.png")},
            {PieceType.King, LoadImage("Assets/wk.png")},
            {PieceType.Queen, LoadImage("Assets/wq.png")},

        };

        private static readonly Dictionary<PieceType, ImageSource> blackSources = new()
        {
            {PieceType.Pawn, LoadImage("Assets/bp.png") },
            {PieceType.Bishop, LoadImage("Assets/bb.png")},
            {PieceType.Rook, LoadImage("Assets/br.png")},
            {PieceType.Knight, LoadImage("Assets/bn.png")},
            {PieceType.King, LoadImage("Assets/bk.png")},
            {PieceType.Queen, LoadImage("Assets/bq.png")},

        };

        public static ImageSource GetImage(Player color, PieceType piece)
        {
            return color switch
            {
                Player.White => whiteSources[piece],
                Player.Black => blackSources[piece],
                _ => null,
            };

        }

        public static ImageSource GetImage(Piece piece)
        {
            if (piece == null) return null;
            return GetImage(piece.Color, piece.Type);   
        }
    }
}
