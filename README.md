# ChessGame

**ChessGame** é um jogo de xadrez funcional para dois jogadores, desenvolvido em C# utilizando a estrutura de classes para representar peças e lógicas do jogo.

**Documentação Completa:** [https://github.com/raulaugusto/ChessGame/wiki]
## Funcionalidades
- Suporte a partidas entre dois jogadores no mesmo dispositivo.
- Tabuleiro gráfico que mostra o movimento das peças.
- Regras básicas do xadrez implementadas, incluindo:
  - Movimento de todas as peças (torre, cavalo, bispo, rainha, rei e peões).
  - Detecção de cheque e xeque-mate.
  - Validação de movimentos válidos para cada peça.

## Estrutura do Projeto
- **ChessLogic**: Contém a lógica principal do jogo de xadrez, incluindo as regras de movimentação e captura de peças.
  - São implementadas classes para representar cada peça do jogo, definindo suas direções de movimento.
  - Ao clicar em uma peça no tabuleiro, gera todos os movimentos possíveis para a peça e exclui os ilegais depois imprime na tela.
  - Faz a verificação para xeque-mate após cada movimento que ameaça o rei calculando os movimentos possíveis que protejam o rei.
- **ChessUI**: Interface gráfica para o tabuleiro de xadrez, permitindo interação do usuário.
  - Usa Xaml para gerar uma representação gráfica do tabuleiro na tela.

## Requisitos
- .NET Framework.
- Sistema Windows.

## Como Rodar
1. Clone o repositório:
   ```bash
   git clone https://github.com/raulaugusto/ChessGame.git
   ```
2. Abra o projeto no Visual Studio.
3. Compile e execute a solução `ChessGame.sln`.

## Contribuição
Se desejar contribuir:
1. Faça um fork do repositório.
2. Crie uma branch para a sua feature:
   ```bash
   git checkout -b feature/nova-feature
   ```
3. Faça commit das suas alterações e envie um pull request.

---
