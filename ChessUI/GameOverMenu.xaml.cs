using ChessLogic;
using System.Windows;
using System.Windows.Controls;

namespace ChessUI
{
    public partial class GameOverMenu : UserControl
    {
        public event Action<Option> OptionSelected;
        public GameOverMenu(GameState gameState)
        {
            InitializeComponent();
            Result result = gameState.Result;
            WinnerText.Text = GetWinnerText(result.Winner);
            ReasonText.Text = GetReasonText(result.EndReason, gameState.CurrentPlayer);
        }

        public static string GetWinnerText(Player winner)
        {
            return winner switch
            {
                Player.White => "White Wins!",
                Player.Black => "Black Wins!",
                _ => "It's a Draw",
            };
        }

        public static string PlayerString(Player player)
        {
            return player switch
            {
                Player.White => "White",
                Player.Black => "Black",
                _ => ""
            };
        }

        public static string GetReasonText(EndReason endReason, Player player)
        {
            return endReason switch
            {
                EndReason.Stalemate => $"Stalemate",
                EndReason.Checkmate => $"By Checkmate",
                EndReason.InsufficientMaterial => "Insufficient Material",
                EndReason.FiftyMoveRule => "Fifty Move Rule",
                EndReason.ThreeRepeatedMoves => "Threefold Repetition",
                _ => ""
            };
        }
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Restart);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Exit);
        }
    }
}
