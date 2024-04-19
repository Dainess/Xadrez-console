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

    public void LoopDePartida()
    {
        Console.Clear();
        Tela.ImprimirTabuleiro(T);

        Console.Write("Origem: ");
        Posicao origem = Tela.LerProximaJogada().ToPosition();

        Console.Clear();
        Tela.ImprimirTabuleiro(T, T.MandaPeca(origem).MovimentosPossiveis());

        Console.Write("Destino: ");
        Posicao destino = Tela.LerProximaJogada().ToPosition();

        ExecutaMovimento(origem, destino);
    }
}