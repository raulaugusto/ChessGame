// Arquivo: Move.cs
// Namespace: ChessLogic

/// <summary>
/// A classe Move é uma classe abstrata que define a estrutura básica de um movimento no jogo de xadrez.
/// Cada movimento contém informações sobre as posições de origem e destino,
/// além de métodos para execução e verificação de legalidade do movimento.
/// </summary>
public abstract class Move
{
    /// <summary>
    /// Tipo do movimento (normal, roque, promoção, etc.).
    /// Cada classe derivada de Move deve definir o tipo específico de movimento.
    /// </summary>
    public abstract MoveType Type { get; }

    /// <summary>
    /// Posição inicial de onde a peça será movida.
    /// </summary>
    public abstract Position fromPosition { get; }

    /// <summary>
    /// Posição de destino para onde a peça será movida.
    /// </summary>
    public abstract Position toPosition { get; }

    /// <summary>
    /// Executa o movimento no tabuleiro passado como parâmetro.
    /// Este método será implementado nas classes derivadas.
    /// </summary>
    /// <param name="board">Tabuleiro de xadrez onde o movimento será executado.</param>
    public abstract void Execute(Board board);

    /// <summary>
    /// Verifica se o movimento é legal.
    /// O movimento é considerado legal se, após ser executado, o jogador que está movendo a peça
    /// não estiver em cheque.
    /// </summary>
    /// <param name="board">O tabuleiro onde a verificação será feita.</param>
    /// <returns>Retorna verdadeiro se o movimento for legal, falso caso contrário.</returns>
    public virtual bool IsLegal(Board board)
    {
        Player movingPlayer = board[fromPosition].Color;
        Board copyBoard = board.Copy();
        Execute(copyBoard);
        return !copyBoard.IsInCheck(movingPlayer);
    }
}
