using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessLogic;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Image[,] pieceImages = new Image[8, 8];
        private readonly Ellipse[,] highlights = new Ellipse[8, 8];

        private readonly Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();

        private GameState gameState;
        private Position selectedPosition = null;
        public Piece clickedPiece;
        public MainWindow()
        {
            InitializeComponent();
            InicializeBoard();
            gameState = new GameState(Player.White, Board.Initial());
            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentPlayer);
        }

        private void InicializeBoard()
        {
            double ellipseSize = 25; // Adjust the size of the highlight ellipses as needed

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Image image = new Image();
                    pieceImages[row, col] = image;
                    PieceGrid.Children.Add(image);

                    Ellipse highlight = new Ellipse();
                    highlights[row, col] = highlight;
                    // Set the size of the highlight ellipse
                    highlight.Width = ellipseSize;
                    highlight.Height = ellipseSize;
                    highlight.Opacity = 1;
                    HighlightGrid.Children.Add(highlight);
                }
            }
        }

        private void DrawBoard(Board board)
        {
            for(int row = 0; row < 8; row++)
            {
                for(int col = 0; col < 8;col++)
                {
                    Piece piece = board[row, col];
                    pieceImages[row, col].Source = Images.GetImage(piece);
                }
            }
        }

     
        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(BoardGrid);
            Position pos = ToSquarePosition(point);
            if (selectedPosition == null)
            {
                // If no piece is currently selected, handle selection of the clicked piece
                clickedPiece = gameState.Board[pos.Row, pos.Column];
                OnFromPositionSelected(pos);
            }
            else if (clickedPiece != null && clickedPiece.Color == gameState.CurrentPlayer)
            {
                // If a piece is already selected and the clicked position contains the player's own piece,
                // treat it as selecting a new piece and update the selection
                clickedPiece = gameState.Board[pos.Row, pos.Column];
                OnToPositionSelected(pos);
                OnFromPositionSelected(pos);
            }
            else
            {
                // If a piece is already selected and the clicked position does not contain a piece,
                // treat it as deselecting the current selection
                clickedPiece = gameState.Board[pos.Row, pos.Column];
                selectedPosition = null;
                HideHighlights();
            }
        }


        private void HandleMove(Move move)
        {
            gameState.MakeMove(move);
            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentPlayer);
        }

        private void OnFromPositionSelected(Position pos)
        {
            IEnumerable<Move> moves = gameState.LegalMovesForPiece(pos);

            if(moves.Any())
            {
                selectedPosition = pos;
                CacheMoves(moves);
                ShowHighlights();
            }
        }

        private void OnToPositionSelected(Position pos)
        {
            selectedPosition = null;
            HideHighlights();

            if(moveCache.TryGetValue(pos, out Move move))
            {
                HandleMove(move);
            }
        }

        private Position ToSquarePosition(Point point)
        {
            double squareSize = BoardGrid.ActualHeight / 8;
            int row = (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);
            return new Position(row, col);
        }
        private void CacheMoves(IEnumerable<Move> moves)
        {
            moveCache.Clear();
            foreach (Move move in moves) 
            {
                moveCache[move.toPosition] = move;
            }
        }

        private void ShowHighlights()
        {
            Color colorWhite = Color.FromRgb(202, 203, 179);
            Color colorBlack = Color.FromRgb(99, 128, 79);
            foreach (Position to in moveCache.Keys)
            {
         
               if (to.SquareColor() == Player.White)
                {
                    highlights[to.Row, to.Column].Fill = new SolidColorBrush(colorWhite);
                }
                else
                {
                    highlights[to.Row, to.Column].Fill = new SolidColorBrush(colorBlack);
                }
                if (gameState.Board[to] != null && gameState.Board[to].Color != gameState.CurrentPlayer)
                {
                    highlights[to.Row, to.Column].Width = 70;
                    highlights[to.Row, to.Column].Height = 70;

                    if (to.SquareColor() == Player.White)
                    {
                        highlights[to.Row, to.Column].Fill = new SolidColorBrush(colorWhite);
                    }
                    else
                    {
                        highlights[to.Row, to.Column].Fill = new SolidColorBrush(colorBlack);
                    }
                }
            }
        }

        private void HideHighlights()
        {
            foreach(Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = Brushes.Transparent;
            }
        }

        private void SetCursor(Player player)
        {
            if(player == Player.White)
            {
                Mouse.OverrideCursor = Cursors.whiteCursor;
            }
            else
            {
                Mouse.OverrideCursor = Cursors.blackCursor;
            }
        }
    }
}