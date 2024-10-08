// Arquivo: Queen.cs
// Namespace: ChessLogic

/// <summary>
/// A classe Queen representa a peça de rainha no jogo de xadrez.
/// A rainha se move em linha reta em qualquer direção (horizontal, vertical ou diagonal) e pode se mover quantas casas quiser.
/// </summary>
public class Queen : Piece
{
    /// <summary>
    /// Define o tipo da peça como rainha.
    /// </summary>
    public override PieceType Type => PieceType.Queen;

    /// <summary>
    /// Cor do jogador (branco ou preto) que controla a rainha.
    /// </summary>
    public override Player Color { get; }

    /// <summary>
    /// Direções em que a rainha pode se mover: norte, sul, leste, oeste e diagonais.
    /// </summary>
    private static readonly Direction[] dirs =
    {
        Direction.North,
        Direction.South,
        Direction.East,
        Direction.West,
        Direction.NorthWest,
        Direction.NorthEast,
        Direction.SouthWest,
        Direction.SouthEast,
    };

    /// <summary>
    /// Construtor da classe Queen que inicializa a cor da rainha.
    /// </summary>
    /// <param name="color">Cor do jogador que controla a rainha (branco ou preto).</param>
    public Queen(Player color)
    {
        Color = color;
    }

    /// <summary>
    /// Cria uma cópia da rainha, preservando a cor e o estado de movimento da peça.
    /// </summary>
    /// <returns>Uma nova instância de Queen com as mesmas propriedades.</returns>
    public override Piece Copy()
    {
        Queen copy = new Queen(Color);
        copy.HasMoved = HasMoved; // Mantém o estado de movimento.
        return copy;
    }

    /// <summary>
    /// Retorna todos os movimentos possíveis da rainha a partir de uma posição dada.
    /// A rainha pode se mover em linha reta em qualquer direção, tanto vertical quanto horizontal e diagonalmente.
    /// </summary>
    /// <param name="from">A posição atual da rainha.</param>
    /// <param name="board">O estado atual do tabuleiro.</param>
    /// <returns>Uma lista de movimentos válidos para a rainha.</returns>
    public override IEnumerable<Move> GetMoves(Position from, Board board)
    {
        foreach (var dir in dirs)
        {
            foreach (var to in MovePositionsInDir(from, board, dir))
            {
                yield return new NormalMove(from, to); // Gera movimentos válidos em cada direção.
            }
        }
    }
}
