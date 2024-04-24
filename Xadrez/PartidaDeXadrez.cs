using System.Reflection.Metadata;
using Aplicacao;
using AreaDeJogo;

namespace Xadrez;
class PartidaDeXadrez
{
    public Tabuleiro T { get; private set; }
    public int Turno { get; private set; }
    public Cor JogadorAtual { get; private set; }
    public bool Terminada { get; private set; }

    public PartidaDeXadrez()
    {
        T = new Tabuleiro(8,8);
        Turno = 1;
        JogadorAtual = Cor.Branca;
        Terminada = false;
    }

    public void ExecutaMovimento(Posicao origem, Posicao destino)
    {
        Peca peca = T.RetirarPeca(origem);
        peca.SomarMovimento();
        Peca pecaCapturada = T.RetirarPeca(destino);
        if (pecaCapturada != null)
        {
            T.PecasAtivas.Remove(pecaCapturada);
            T.PecasCapturadas.Add(pecaCapturada);
        }
        T.ColocarPeca(peca, destino);
    }

    public void realizaJogada(Posicao origem, Posicao destino)
    {
        ExecutaMovimento(origem, destino);
        JogadorAtual = (Cor)(1 - (int)JogadorAtual);
        Turno++;
    }

    public void ColocarNovaPeca(Peca peca, char coluna, int linha)
    {
        T.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosition());
        T.PecasAtivas.Add(peca);
    }

    public void IniciarTabuleiro()
    {
        var bots = new[] {
            new {peca = (Peca)(new Torre(Cor.Branca, T)), coluna = 'C', linha = 1},
        };

        ColocarNovaPeca(new Torre(Cor.Branca, T), 'C', 1);
        ColocarNovaPeca(new Torre(Cor.Branca, T), 'C', 2);
        ColocarNovaPeca(new Rei(Cor.Branca, T), 'D', 1);
        ColocarNovaPeca(new Torre(Cor.Branca, T), 'D', 2);
        ColocarNovaPeca(new Torre(Cor.Branca, T), 'E', 1);
        ColocarNovaPeca(new Torre(Cor.Branca, T), 'E', 2);

        ColocarNovaPeca(new Torre(Cor.Preta, T), 'C', 7);
        ColocarNovaPeca(new Torre(Cor.Preta, T), 'C', 8);
        ColocarNovaPeca(new Torre(Cor.Preta, T), 'D', 7);
        ColocarNovaPeca(new Rei(Cor.Preta, T), 'D', 8);
        ColocarNovaPeca(new Torre(Cor.Preta, T), 'E', 7);
        ColocarNovaPeca(new Torre(Cor.Preta, T), 'E', 8);
    }

    public void ValidarPosicaoDeOrigem(Peca proximaPeca)
    {
        if (proximaPeca == null)
        {
            throw new TabuleiroException("Não há uma peça aqui.");
        }
        if (proximaPeca.PecaCor != JogadorAtual)
        {
            throw new TabuleiroException("Você escolheu a peça da cor errada. Agora é o jogador: " + JogadorAtual);
        }
        if (proximaPeca.ExisteMovimentoPossivel() == false)
        {
            throw new TabuleiroException("Essa peça não tem movimentos legais disponíveis. Escolha novamente.");
        }
    }

    public void ValidarPosicaoDeDestino(Peca proximaPeca, Posicao destino)
    {
        if (proximaPeca.PodeMoverPara(destino) == false)
            Console.WriteLine("O destino escolhido não é um destino legal. Tente novamente.\n");
    }
    

    public void LoopDePartida()
    {
        while (!Terminada)
        {
            try
            {
                Console.Clear();
                Tela.ImprimirTabuleiro(T);
                Console.WriteLine();
                Tela.Capturadas(T, true);
                Console.WriteLine();
                Console.WriteLine($"Turno: {Turno}");
                Console.WriteLine($"Aguardando a vez do jogador da cor: {JogadorAtual}");
                Console.WriteLine();

                Console.Write("Origem: ");
                Posicao origem = Tela.LerProximaJogada().ToPosition();

                Peca proximaPeca = T.MandaPeca(origem);

                ValidarPosicaoDeOrigem(proximaPeca);

                Console.Clear();
                Tela.ImprimirTabuleiro(T, proximaPeca.MovimentosPossiveis());
                Tela.Capturadas(T, true);

                while (true)
                {
                    Console.Write("\nDestino: ");
                    Posicao destino = Tela.LerProximaJogada().ToPosition();
                    if (proximaPeca.DestinosLegais.Contains(destino))
                    {
                        realizaJogada(origem, destino);
                        break;
                    }
                    Console.WriteLine("O destino escolhido não é um destino legal. Tente novamente.\n");
                }
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}

/*             
    foreach (var thing in posicoesLegais)
    {
        Console.WriteLine(thing.ToXadrez());
    }
*/