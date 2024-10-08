// Arquivo: Rook.cs
// Namespace: ChessLogic

/// <summary>
/// A classe Rook representa a peça de torre no jogo de xadrez.
/// A torre se move em linha reta, horizontal ou verticalmente, e pode se mover quantas casas quiser.
/// </summary>
public class Rook : Piece
{
    /// <summary>
    /// Define o tipo da peça como torre.
    /// </summary>
    public override PieceType Type => PieceType.Rook;

    /// <summary>
    /// Cor do jogador (branco ou preto) que controla a torre.
    /// </summary>
    public override Player Color { get; }

    /// <summary>
    /// Direções em que a torre pode se mover: norte, sul, leste e oeste.
    /// </summary>
    private static readonly Direction[] dirs =
    {
        Direction.North,
        Direction.South,
        Direction.East,
        Direction.West,
    };

    /// <summary>
    /// Construtor da classe Rook que inicializa a cor da torre.
    /// </summary>
    /// <param name="color">Cor do jogador que controla a torre (branco ou preto).</param>
    public Rook(Player color)
    {
        Color = color;
    }

    /// <summary>
    /// Cria uma cópia da torre, preservando a cor e o estado de movimento da peça.
    /// </summary>
    /// <returns>Uma nova instância de Rook com as mesmas propriedades.</returns>
    public override Piece Copy()
    {
        Rook copy = new Rook(Color);
        copy.HasMoved = HasMoved; // Mantém o estado de movimento.
        return copy;
    }

    /// <summary>
    /// Retorna todos os movimentos possíveis da torre a partir de uma posição dada.
    /// A torre pode se mover em linha reta, tanto vertical quanto horizontalmente.
    /// </summary>
    /// <param name="from">A posição atual da torre.</param>
    /// <param name="board">O estado atual do tabuleiro.</param>
    /// <returns>Uma lista de movimentos válidos para a torre.</returns>
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
