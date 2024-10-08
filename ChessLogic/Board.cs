// Arquivo: Board.cs
// Namespace: ChessLogic

/// <summary>
/// A classe Board representa o tabuleiro de xadrez com suas peças e operações.
/// Gerencia as posições das peças, seu estado e realiza cópias do tabuleiro.
/// </summary>
public class Board
{
    /// <summary>
    /// Matriz 8x8 de peças que representa o estado atual do tabuleiro.
    /// </summary>
    private readonly Piece[,] pieces = new Piece[8, 8];

    /// <summary>
    /// Indexador que permite acessar ou definir uma peça na posição especificada pelas coordenadas (linha, coluna).
    /// </summary>
    /// <param name="row">Linha no tabuleiro (0-7).</param>
    /// <param name="column">Coluna no tabuleiro (0-7).</param>
    /// <returns>A peça na posição especificada ou null se estiver vazia.</returns>
    public Piece this[int row, int column]
    {
        get { return pieces[row, column]; }
        set { pieces[row, column] = value; }
    }

    /// <summary>
    /// Indexador que permite acessar ou definir uma peça em uma posição do tipo <see cref="Position"/>.
    /// </summary>
    /// <param name="pos">A posição no tabuleiro.</param>
    /// <returns>A peça na posição especificada ou null se estiver vazia.</returns>
    public Piece this[Position pos]
    {
        get { return this[pos.Row, pos.Column]; }
        set { this[pos.Row, pos.Column] = value; }
    }

    /// <summary>
    /// Cria uma instância do tabuleiro inicial com todas as peças posicionadas nas suas posições padrão.
    /// </summary>
    /// <returns>Um tabuleiro configurado com a posição inicial das peças de xadrez.</returns>
    public static Board Initial()
    {
        Board board = new Board();
        board.AddStartPieces(board);
        return board;
    }

    /// <summary>
    /// Posiciona as peças iniciais no tabuleiro de acordo com as regras clássicas do xadrez.
    /// Peças pretas na primeira e segunda fileiras, peças brancas na sétima e oitava fileiras.
    /// </summary>
    private void AddStartPieces(Board board)
    {
        // Peças pretas
        this[0, 0] = new Rook(Player.Black);
        this[0, 1] = new Knight(Player.Black);
        this[0, 2] = new Bishop(Player.Black);
        this[0, 3] = new Queen(Player.Black);
        this[0, 4] = new King(Player.Black);
        this[0, 5] = new Bishop(Player.Black);
        this[0, 6] = new Knight(Player.Black);
        this[0, 7] = new Rook(Player.Black);

        for (int i = 0; i < 8; i++)
        {
            this[1, i] = new Pawn(Player.Black);
        }

        // Peças brancas
        this[7, 0] = new Rook(Player.White);
        this[7, 1] = new Knight(Player.White);
        this[7, 2] = new Bishop(Player.White);
        this[7, 3] = new Queen(Player.White);
        this[7, 4] = new King(Player.White);
        this[7, 5] = new Bishop(Player.White);
        this[7, 6] = new Knight(Player.White);
        this[7, 7] = new Rook(Player.White);

        for (int i = 0; i < 8; i++)
        {
            this[6, i] = new Pawn(Player.White);
        }
    }

    /// <summary>
    /// Verifica se a posição está dentro dos limites do tabuleiro.
    /// </summary>
    /// <param name="pos">A posição a ser verificada.</param>
    /// <returns>True se a posição estiver dentro do tabuleiro, caso contrário False.</returns>
    public static bool isInside(Position pos)
    {
        return pos.Row >= 0 && pos.Row < 8 && pos.Column >= 0 && pos.Column < 8;
    }

    /// <summary>
    /// Verifica se uma posição está vazia, ou seja, não contém nenhuma peça.
    /// </summary>
    /// <param name="pos">A posição a ser verificada.</param>
    /// <returns>True se a posição estiver vazia, caso contrário False.</returns>
    public bool isEmpty(Position pos)
    {
        return this[pos] == null;
    }

    /// <summary>
    /// Retorna todas as posições do tabuleiro que contêm peças.
    /// </summary>
    /// <returns>Uma coleção de posições que contêm peças no tabuleiro.</returns>
    public IEnumerable<Position> PiecePositions()
    {
        for (int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 8; c++)
            {
                Position pos = new Position(r, c);
                if (!isEmpty(pos))
                {
                    yield return pos;
                }
            }
        }
    }

    /// <summary>
    /// Retorna todas as posições no tabuleiro que contêm peças de um jogador específico.
    /// </summary>
    /// <param name="player">O jogador (branco ou preto).</param>
    /// <returns>Uma coleção de posições que contêm peças do jogador especificado.</returns>
    public IEnumerable<Position> PiecePositionsFor(Player player)
    {
        return PiecePositions().Where(pos => this[pos].Color == player);
    }

    /// <summary>
    /// Verifica se o jogador está em xeque, ou seja, se seu rei está sob ataque.
    /// </summary>
    /// <param name="player">O jogador a ser verificado.</param>
    /// <returns>True se o jogador estiver em xeque, caso contrário False.</returns>
    public bool IsInCheck(Player player)
    {
        return PiecePositionsFor(player.Oponnent()).Any(pos =>
        {
            Piece piece = this[pos];
            return piece.CanCaptureOpponentKing(pos, this);
        });
    }

    /// <summary>
    /// Cria uma cópia profunda do tabuleiro, copiando todas as peças e suas posições.
    /// </summary>
    /// <returns>Uma nova instância de Board que é uma cópia do tabuleiro atual.</returns>
    public Board Copy()
    { 
        Board copy = new Board();
        foreach(Position pos in PiecePositions())
        {
            copy[pos] = this[pos].Copy();
        }

        return copy;
    }
}
