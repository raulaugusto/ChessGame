// Arquivo: Bishop.cs
// Namespace: ChessLogic

/// <summary>
/// A classe Bishop representa a peça de bispo no jogo de xadrez.
/// O bispo se move ao longo das diagonais do tabuleiro, tanto para frente quanto para trás.
/// </summary>
public class Bishop : Piece
{
    /// <summary>
    /// Define o tipo da peça como bispo.
    /// </summary>
    public override PieceType Type => PieceType.Bishop;

    /// <summary>
    /// Cor do jogador (branco ou preto) que controla o bispo.
    /// </summary>
    public override Player Color { get; }

    /// <summary>
    /// Construtor da classe Bishop que inicializa a cor da peça.
    /// </summary>
    /// <param name="color">Cor do jogador que possui o bispo.</param>
    public Bishop(Player color)
    {
        Color = color;
    }

    /// <summary>
    /// Cria uma cópia do bispo, preservando a cor e o estado de movimento da peça.
    /// </summary>
    /// <returns>Uma nova instância de Bishop com as mesmas propriedades.</returns>
    public override Piece Copy()
    {
        Bishop copy = new Bishop(Color);
        copy.HasMoved = HasMoved; // Mantém o estado de movimento para regras como roque ou três repetições.
        return copy;
    }

    /// <summary>
    /// Definição das direções em que o bispo pode se mover, ou seja, as quatro diagonais do tabuleiro.
    /// </summary>
    private static readonly Direction[] dirs = new Direction[]
    {
        Direction.NorthWest,
        Direction.NorthEast,
        Direction.SouthWest,
        Direction.SouthEast,
    };

    /// <summary>
    /// Obtém todos os movimentos válidos para o bispo a partir de uma posição no tabuleiro.
    /// O bispo se move em qualquer direção diagonal até encontrar um limite ou uma peça.
    /// </summary>
    /// <param name="from">A posição atual do bispo.</param>
    /// <param name="board">O estado atual do tabuleiro.</param>
    /// <returns>Uma enumeração de movimentos válidos para o bispo.</returns>
    public override IEnumerable<Move> GetMoves(Position from, Board board)
    {
        // Para cada direção diagonal, calcula os movimentos possíveis.
        foreach (var dir in dirs)
        {
            foreach (var to in MovePositionsInDir(from, board, dir))
            {
                yield return new NormalMove(from, to);
            }
        }
    }
}
