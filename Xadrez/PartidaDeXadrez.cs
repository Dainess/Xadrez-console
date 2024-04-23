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
        T.ColocarPeca(peca, destino);
    }

    public void realizaJogada(Posicao origem, Posicao destino)
    {
        ExecutaMovimento(origem, destino);
        JogadorAtual = (Cor)((int)JogadorAtual * (-1));
        Turno++;
    }

    public void IniciarTabuleiro()
    {
        T.ColocarPeca(new Torre(Cor.Branca, T), new PosicaoXadrez('C', 1).ToPosition());
        T.ColocarPeca(new Torre(Cor.Branca, T), new PosicaoXadrez('C', 2).ToPosition());
        T.ColocarPeca(new Rei(Cor.Branca, T), new PosicaoXadrez('D', 1).ToPosition());
        T.ColocarPeca(new Torre(Cor.Branca, T), new PosicaoXadrez('D', 2).ToPosition());
        T.ColocarPeca(new Torre(Cor.Branca, T), new PosicaoXadrez('E', 1).ToPosition());
        T.ColocarPeca(new Torre(Cor.Branca, T), new PosicaoXadrez('E', 2).ToPosition());

        T.ColocarPeca(new Torre(Cor.Preta, T), new PosicaoXadrez('C', 7).ToPosition());
        T.ColocarPeca(new Torre(Cor.Preta, T), new PosicaoXadrez('C', 8).ToPosition());
        T.ColocarPeca(new Torre(Cor.Preta, T), new PosicaoXadrez('D', 7).ToPosition());
        T.ColocarPeca(new Rei(Cor.Preta, T), new PosicaoXadrez('D', 8).ToPosition());
        T.ColocarPeca(new Torre(Cor.Preta, T), new PosicaoXadrez('E',7).ToPosition());
        T.ColocarPeca(new Torre(Cor.Preta, T), new PosicaoXadrez('E',8).ToPosition());
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
                Console.WriteLine($"Turno: {Turno}");
                Console.WriteLine($"Aguardando a vez do jogador da cor: {JogadorAtual}");

                Console.Write("Origem: ");
                Posicao origem = Tela.LerProximaJogada().ToPosition();

                Peca proximaPeca = T.MandaPeca(origem);

                ValidarPosicaoDeOrigem(proximaPeca);

                Console.Clear();
                Tela.ImprimirTabuleiro(T, proximaPeca.MovimentosPossiveis());

                while (true)
                {
                    Console.Write("Destino: ");
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