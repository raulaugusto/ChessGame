// Arquivo: King.cs
// Namespace: ChessLogic

/// <summary>
/// A classe King representa a peça de rei no jogo de xadrez.
/// O rei é a peça central do jogo e pode se mover uma casa em qualquer direção.
/// O jogo termina quando o rei é capturado (xeque-mate).
/// </summary>
public class King : Piece
{
    /// <summary>
    /// Define o tipo da peça como rei.
    /// </summary>
    public override PieceType Type => PieceType.King;

    /// <summary>
    /// Cor do jogador (branco ou preto) que controla o rei.
    /// </summary>
    public override Player Color { get; }

    /// <summary>
    /// Define as direções em que o rei pode se mover: uma casa em qualquer direção (horizontal, vertical, diagonal).
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
    /// Construtor da classe King que inicializa a cor da peça.
    /// </summary>
    /// <param name="color">Cor do jogador que possui o rei.</param>
    public King(Player color)
    {
        Color = color;
    }

    /// <summary>
    /// Cria uma cópia do rei, preservando a cor e o estado de movimento da peça.
    /// </summary>
    /// <returns>Uma nova instância de King com as mesmas propriedades.</returns>
    public override Piece Copy()
    {
        King copy = new King(Color);
        copy.HasMoved = HasMoved; // Mantém o estado de movimento, importante para regras como o roque.
        return copy;
    }

    /// <summary>
    /// Calcula as posições válidas para o movimento do rei a partir de uma posição específica no tabuleiro.
    /// O rei pode se mover uma casa em qualquer direção, desde que a posição de destino esteja dentro do tabuleiro
    /// e não seja ocupada por uma peça do mesmo jogador.
    /// </summary>
    /// <param name="from">A posição atual do rei.</param>
    /// <param name="board">O estado atual do tabuleiro.</param>
    /// <returns>Uma enumeração das posições válidas para o movimento do rei.</returns>
    public IEnumerable<Position> MovePositions(Position from, Board board)
    {
        foreach (Direction dir in dirs)
        {
            Position to = from + dir;
            if (!Board.isInside(to)) // Verifica se a posição está dentro dos limites do tabuleiro.
            {
                continue;
            }

            // Verifica se a posição está vazia ou contém uma peça do oponente.
            if (board.isEmpty(to) || board[to].Color != Color)
            {
                yield return to;
            }
        }
    }

    /// <summary>
    /// Obtém todos os movimentos válidos para o rei a partir de uma posição no tabuleiro.
    /// </summary>
    /// <param name="from">A posição atual do rei.</param>
    /// <param name="board">O estado atual do tabuleiro.</param>
    /// <returns>Uma enumeração de movimentos válidos para o rei.</returns>
    public override IEnumerable<Move> GetMoves(Position from, Board board)
    {
        foreach (Position pos in MovePositions(from, board))
        {
            yield return new NormalMove(from, pos); // Gera um movimento normal para cada posição válida.
        }
    }

    /// <summary>
    /// Verifica se o rei pode capturar o rei adversário a partir de uma posição no tabuleiro.
    /// Essa verificação é usada para determinar se o rei está próximo o suficiente para realizar um xeque.
    /// </summary>
    /// <param name="from">A posição atual do rei.</param>
    /// <param name="board">O estado atual do tabuleiro.</param>
    /// <returns>Retorna verdadeiro se o rei pode capturar o rei adversário, falso caso contrário.</returns>
    public override bool CanCaptureOpponentKing(Position from, Board board)
    {
        return MovePositions(from, board).Any(move =>
        {
            Piece piece = board[move];
            return piece != null && piece.Type == PieceType.King; // Verifica se a peça na posição é o rei adversário.
        });
    }

    // Comentário: Este método comentado seria utilizado para verificar a possibilidade de realizar o roque pelo lado da rainha.
    // public Position CanCastleQueenSide(Board board)
    // {
    //     Piece king = this;
    //     if (!king.HasMoved)
    //     {
    //         if (king.Color == Player.White)
    //         {
    //             int row = 0;
    //             for (int c = 4; c > 0; c--)
    //             {
    //                 if (!board.isEmpty(new Position(row, c)))
    //                 {
    //                     return null; // Interrompe se houver qualquer peça no caminho.
    //                 }
    //             }
    //             return new Position(row, 2); // Posição de destino para o roque.
    //         }
    //         else
    //         {
    //             int row = 7;
    //             for (int c = 4; c > 0; c--)
    //             {
    //                 if (!board.isEmpty(new Position(row, c)))
    //                 {
    //                     return null;
    //                 }
    //             }
    //             return new Position(row, 2);
    //         }
    //     }
    //     else
    //     {
    //         return new Position(0, 0); // Valor de retorno caso o rei já tenha se movido.
    //     }
    // }
}
