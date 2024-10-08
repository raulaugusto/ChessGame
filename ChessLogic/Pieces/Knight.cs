// Arquivo: Knight.cs
// Namespace: ChessLogic

/// <summary>
/// A classe Knight representa a peça de cavalo no jogo de xadrez.
/// O cavalo se move em um padrão em "L", podendo saltar sobre outras peças.
/// </summary>
public class Knight : Piece
{
    /// <summary>
    /// Define o tipo da peça como cavalo.
    /// </summary>
    public override PieceType Type => PieceType.Knight;

    /// <summary>
    /// Cor do jogador (branco ou preto) que controla o cavalo.
    /// </summary>
    public override Player Color { get; }

    /// <summary>
    /// Gera posições potenciais de movimento para o cavalo a partir de uma posição inicial.
    /// O cavalo se move em um padrão em "L", ou seja, duas casas em uma direção e uma casa em uma direção perpendicular.
    /// </summary>
    /// <param name="from">A posição atual do cavalo.</param>
    /// <returns>Uma lista de posições potenciais para o movimento do cavalo.</returns>
    private static IEnumerable<Position> PotentialToPositions(Position from)
    {
        foreach(Direction vDir in new Direction[] {Direction.North, Direction.South})
        {
            foreach(Direction hDir in new Direction[] {Direction.East, Direction.West})
            {
                // Adiciona as posições em "L" (duas em uma direção e uma em perpendicular).
                yield return from + 2 * vDir + hDir;
                yield return from + 2 * hDir + vDir;
            }
        }
    }

    /// <summary>
    /// Obtém as posições válidas para o movimento do cavalo a partir de uma posição específica no tabuleiro.
    /// O cavalo pode se mover para uma posição vazia ou para uma ocupada por uma peça adversária.
    /// </summary>
    /// <param name="from">A posição atual do cavalo.</param>
    /// <param name="board">O estado atual do tabuleiro.</param>
    /// <returns>Uma lista de posições válidas para o movimento do cavalo.</returns>
    public IEnumerable<Position> MovePositions(Position from, Board board)
    {
        return PotentialToPositions(from)
            .Where(pos => Board.isInside(pos) && (board.isEmpty(pos) || board[pos].Color != Color));
    }

    /// <summary>
    /// Construtor da classe Knight que inicializa a cor da peça.
    /// </summary>
    /// <param name="color">Cor do jogador que possui o cavalo.</param>
    public Knight(Player color)
    {
        Color = color;
    }

    /// <summary>
    /// Cria uma cópia do cavalo, preservando a cor e o estado de movimento da peça.
    /// </summary>
    /// <returns>Uma nova instância de Knight com as mesmas propriedades.</returns>
    public override Piece Copy()
    {
        Knight copy = new Knight(Color);
        copy.HasMoved = HasMoved; // Mantém o estado de movimento.
        return copy;
    }

    /// <summary>
    /// Obtém todos os movimentos válidos para o cavalo a partir de uma posição no tabuleiro.
    /// </summary>
    /// <param name="from">A posição atual do cavalo.</param>
    /// <param name="board">O estado atual do tabuleiro.</param>
    /// <returns>Uma enumeração de movimentos válidos para o cavalo.</returns>
    public override IEnumerable<Move> GetMoves(Position from, Board board)
    {
        return MovePositions(from, board).Select(to => new NormalMove(from, to)); // Gera movimentos normais.
    }
}
