// Arquivo: Pawn.cs
// Namespace: ChessLogic

/// <summary>
/// A classe Pawn representa a peça de peão no jogo de xadrez.
/// O peão se move para frente, mas captura peças na diagonal.
/// Possui movimentos especiais, como o avanço de duas casas em seu primeiro movimento.
/// </summary>
public class Pawn : Piece
{
    /// <summary>
    /// Define o tipo da peça como peão.
    /// </summary>
    public override PieceType Type => PieceType.Pawn;

    /// <summary>
    /// Cor do jogador (branco ou preto) que controla o peão.
    /// </summary>
    public override Player Color { get; }

    /// <summary>
    /// A direção na qual o peão se move, definida com base na cor da peça.
    /// Peões brancos se movem para o norte, enquanto peões pretos se movem para o sul.
    /// </summary>
    public Direction forward;

    /// <summary>
    /// Construtor da classe Pawn que inicializa a cor do peão e define a direção de movimento.
    /// </summary>
    /// <param name="color">Cor do jogador que controla o peão (branco ou preto).</param>
    public Pawn(Player color)
    {
        Color = color;
        if (Color == Player.White) forward = Direction.North;
        else forward = Direction.South;
    }

    /// <summary>
    /// Cria uma cópia do peão, preservando a cor e o estado de movimento da peça.
    /// </summary>
    /// <returns>Uma nova instância de Pawn com as mesmas propriedades.</returns>
    public override Piece Copy()
    {
        Pawn copy = new Pawn(Color);
        copy.HasMoved = HasMoved; // Mantém o estado de movimento.
        return copy;
    }

    /// <summary>
    /// Verifica se o peão pode se mover para uma determinada posição.
    /// </summary>
    /// <param name="pos">Posição alvo.</param>
    /// <param name="board">O estado atual do tabuleiro.</param>
    /// <returns>Verdadeiro se a posição é válida para o movimento do peão, falso caso contrário.</returns>
    private static bool CanMoveTo(Position pos, Board board)
    {
        return Board.isInside(pos) && board.isEmpty(pos);
    }

    /// <summary>
    /// Verifica se o peão pode capturar uma peça na posição especificada.
    /// </summary>
    /// <param name="pos">Posição alvo para a captura.</param>
    /// <param name="board">O estado atual do tabuleiro.</param>
    /// <returns>Verdadeiro se a captura for possível, falso caso contrário.</returns>
    private bool CanCaptureAt(Position pos, Board board)
    {
        if(!Board.isInside(pos) || board.isEmpty(pos))
        {
            return false;
        }
        return board[pos].Color != Color; // A peça deve ser de cor diferente.
    }

    /// <summary>
    /// Gera os movimentos em linha reta para o peão (avanço de uma ou duas casas, dependendo se ele já se moveu ou não).
    /// </summary>
    /// <param name="from">A posição atual do peão.</param>
    /// <param name="board">O estado atual do tabuleiro.</param>
    /// <returns>Uma lista de movimentos para frente que o peão pode realizar.</returns>
    private IEnumerable<Move> ForwardMoves(Position from, Board board)
    {
        Position oneMovePosition = from + forward;
        if (CanMoveTo(oneMovePosition, board))
        {
            yield return new NormalMove(from, oneMovePosition);
            Position twoMovePosition = oneMovePosition + forward;
            if (!board[from].HasMoved && CanMoveTo(twoMovePosition, board))
            {
                yield return new NormalMove(from, twoMovePosition); // Movimento de duas casas.
            }
        }
    }

    /// <summary>
    /// Gera os movimentos diagonais para o peão, que são usados para capturar peças inimigas.
    /// </summary>
    /// <param name="from">A posição atual do peão.</param>
    /// <param name="board">O estado atual do tabuleiro.</param>
    /// <returns>Uma lista de movimentos diagonais que o peão pode realizar para capturar peças inimigas.</returns>
    private IEnumerable<Move> DiagonalMoves(Position from, Board board)
    {
        foreach (Direction dir in new Direction[] { Direction.West, Direction.East })
        {
            Position to = from + dir + forward;
            if(CanCaptureAt(to, board))
            {
                yield return new NormalMove(from, to);
            }
        }
    }

    /// <summary>
    /// Retorna todos os movimentos possíveis do peão, combinando movimentos para frente e capturas diagonais.
    /// </summary>
    /// <param name="from">A posição atual do peão.</param>
    /// <param name="board">O estado atual do tabuleiro.</param>
    /// <returns>Uma lista de movimentos válidos para o peão.</returns>
    public override IEnumerable<Move> GetMoves(Position from, Board board)
    {
        return ForwardMoves(from, board).Concat(DiagonalMoves(from, board));
    }

    /// <summary>
    /// Verifica se o peão pode capturar o rei adversário.
    /// </summary>
    /// <param name="from">A posição atual do peão.</param>
    /// <param name="board">O estado atual do tabuleiro.</param>
    /// <returns>Verdadeiro se o peão pode capturar o rei adversário, falso caso contrário.</returns>
    public override bool CanCaptureOpponentKing(Position from, Board board)
    {
        return DiagonalMoves(from, board).Any(move =>
        {
            Piece piece = board[move.toPosition];
            return piece != null && piece.Type == PieceType.King; // Verifica se a peça capturada é o rei.
        });
    }
}
