// Arquivo: NormalMove.cs
// Namespace: ChessLogic

/// <summary>
/// A classe NormalMove representa um movimento padrão no jogo de xadrez, onde uma peça
/// é movida de uma posição para outra sem captura especial, roque ou promoção.
/// </summary>
internal class NormalMove : Move
{
    /// <summary>
    /// Define o tipo de movimento como normal.
    /// </summary>
    public override MoveType Type => MoveType.Normal;

    /// <summary>
    /// Posição inicial de onde a peça será movida.
    /// </summary>
    public override Position fromPosition { get; }

    /// <summary>
    /// Posição de destino para onde a peça será movida.
    /// </summary>
    public override Position toPosition { get; }

    /// <summary>
    /// Construtor da classe NormalMove, que inicializa as posições de origem e destino.
    /// </summary>
    /// <param name="fromPosition">A posição inicial da peça.</param>
    /// <param name="toPosition">A posição de destino para a peça.</param>
    public NormalMove(Position fromPosition, Position toPosition)
    {
        this.fromPosition = fromPosition;
        this.toPosition = toPosition;
    }

    /// <summary>
    /// Executa o movimento de uma peça no tabuleiro. A peça é movida da posição inicial para a
    /// posição de destino, e a posição inicial é deixada vazia.
    /// </summary>
    /// <param name="board">O tabuleiro de xadrez onde o movimento será executado.</param>
    public override void Execute(Board board)
    {
        Piece piece = board[fromPosition];
        board[toPosition] = piece;
        board[fromPosition] = null;
        piece.HasMoved = true; // Marca que a peça foi movida, útil para roque ou outras regras.
    }
}
