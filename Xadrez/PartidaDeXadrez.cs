using Aplicacao;
using AreaDeJogo;

namespace Xadrez;
class PartidaDeXadrez
{
    public Tabuleiro T { get; private set; }
    public int Turno { get; private set; }
    public Cor JogadorAtual { get; private set; }
    public bool Terminada { get; private set; }
    public bool Xeque {get; private set; }

    public PartidaDeXadrez()
    {
        T = new Tabuleiro(8,8);
        Turno = 1;
        JogadorAtual = Cor.Branca;
        Terminada = false;
        Xeque = false;
    }

    public Peca ExecutaMovimento(Posicao origem, Posicao destino)
    {
        Peca peca = T.RetirarPeca(origem);
        Peca pecaCapturada = T.RetirarPeca(destino);
        peca.SomarMovimento();
        T.ColocarPeca(peca, destino);
        T.AtualizaMovimentosPossiveis();
        return pecaCapturada;
    }
    
    public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
    {
        Peca pecaOriginal = T.RetirarPeca(destino);
        pecaOriginal.SubtraiMovimento();
        if (pecaCapturada != null)
            T.ColocarPeca(pecaCapturada, destino);
        T.ColocarPeca(pecaOriginal, origem);
        T.AtualizaMovimentosPossiveis();
    }

    private Cor Adversaria(Cor cor)
    {
        return (Cor)(1 - (int)cor);
    }

    private Peca Rei(Cor cor)
    {
        return T.PecasEmJogo(cor).First(peca => peca is Rei);
    }

    private bool EmXeque(Cor cor)
    {
        Rei nossoRei = (Rei)Rei(cor);
        if (nossoRei == null)
            throw new TabuleiroException($"Não há um rei da cor {JogadorAtual} no tabuleiro");
        else if (nossoRei.PecaPosicao == null)
            throw new TabuleiroException($"Por algum acaso o rei não tem uma posição no tabuleiro");
        foreach (var peca in T.PecasEmJogo(Adversaria(cor)))
        {
            if (peca.DestinosLegais.Contains(nossoRei.PecaPosicao))
            {
                return true;
            }
        }
        return false;
    }

    private bool SalvaOXeque(Cor cor)
    {
        Rei nossoRei = (Rei)Rei(cor);
        if (nossoRei == null)
            throw new TabuleiroException($"Não há um rei da cor {JogadorAtual} no tabuleiro");
        foreach(var peca in T.PecasEmJogo(cor))
        {
            if (peca.PecaPosicao == null)
                throw new TabuleiroException($"Por algum acaso essa peça não tem uma posição no tabuleiro");
            //T.PecasEmJogo(cor).AsParallel().ForAll(Console.WriteLine);
            var possiveis = new Posicao[peca.DestinosLegais.Count];
            peca.DestinosLegais.CopyTo(possiveis);

            foreach (var posicao in possiveis)
            {
                Posicao original = peca.PecaPosicao;
                Peca pecaSimulada = ExecutaMovimento(original, posicao);
                if (TestaJogada(original, posicao, pecaSimulada))
                {
                    DesfazMovimento(original, posicao, pecaSimulada);
                    return true;
                }
            }
        }
        return false;
    }

    public void RealizaJogada(Peca pecaCapturada)
    {
        if (pecaCapturada != null)
        {
            T.PecasAtivas.Remove(pecaCapturada);
            T.PecasCapturadas.Add(pecaCapturada);
        }
        JogadorAtual = Adversaria(JogadorAtual);
        if (EmXeque(JogadorAtual))
            Xeque = true;
        else
            Xeque = false;
        Turno++;
    }

    public bool TestaJogada(Posicao origem, Posicao destino, Peca pecaCapturada)
    {
        if (EmXeque(JogadorAtual))
        {
            DesfazMovimento(origem, destino, pecaCapturada);
            return false;
        }
        return true;
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

        ColocarNovaPeca(new Bispo(Cor.Branca, T), 'C', 1);
        ColocarNovaPeca(new Torre(Cor.Branca, T), 'C', 2);
        ColocarNovaPeca(new Rei(Cor.Branca, T), 'D', 1);
        ColocarNovaPeca(new Torre(Cor.Branca, T), 'D', 2);
        ColocarNovaPeca(new Bispo(Cor.Branca, T), 'E', 1);
        ColocarNovaPeca(new Torre(Cor.Branca, T), 'E', 2);

        ColocarNovaPeca(new Torre(Cor.Preta, T), 'C', 7);
        ColocarNovaPeca(new Bispo(Cor.Preta, T), 'C', 8);
        ColocarNovaPeca(new Torre(Cor.Preta, T), 'D', 7);
        ColocarNovaPeca(new Rei(Cor.Preta, T), 'D', 8);
        ColocarNovaPeca(new Torre(Cor.Preta, T), 'E', 7);
        ColocarNovaPeca(new Bispo(Cor.Preta, T), 'E', 8);

        T.AtualizaMovimentosPossiveis();
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
        if (proximaPeca.DestinosLegais.Count == 0)
        {
            throw new TabuleiroException("Essa peça não tem movimentos legais disponíveis. Escolha novamente.");
        }
    }

    public void ValidarPosicaoDeDestino(Peca proximaPeca, Posicao destino)
    {
        if (proximaPeca.PecaPodeIrPara(destino) == false)
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
                if (Xeque)
                {
                    Console.WriteLine("EM XEQUE!");
                    if (SalvaOXeque(JogadorAtual) == false)
                    {
                        Terminada = true;
                        throw new TabuleiroException("Acabou essa marmelada");
                    }
                }
                    

                Console.Write("Origem: ");
                Posicao origem = Tela.LerProximaJogada().ToPosition();

                Peca proximaPeca = T.QualAPeca(origem);

                ValidarPosicaoDeOrigem(proximaPeca);

                Console.Clear();
                Tela.ImprimirTabuleiro(T, proximaPeca.MovimentosPossiveis());
                Tela.Capturadas(T, true);

                //T.PecasEmJogo(cor).AsParallel().ForAll(Console.WriteLine);
                
                while (true)
                {
                    Console.Write("\nPara retornar, digite 'Origem'.\nDestino: ");
                    Posicao destino = Tela.LerProximaJogada().ToPosition();
                    if (proximaPeca.DestinosLegais.Contains(destino) == false)
                    {
                        Console.WriteLine("O destino escolhido não é um destino legal. Tente novamente.\n");
                    }
                    else
                    {   
                        Peca pecaCapturada = ExecutaMovimento(origem, destino);
                        if (TestaJogada(origem, destino, pecaCapturada) == false)
                        {
                            Console.WriteLine("Este movimento te deixa em xeque. Escolha outro.\n");
                        }
                        else
                        {
                            RealizaJogada(pecaCapturada);
                            break;
                        }    
                    }
                }
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
        Console.WriteLine($"Vencedor: {Adversaria(JogadorAtual)}");
    }
}

/*             
    foreach (var thing in posicoesLegais)
    {
        Console.WriteLine(thing.ToXadrez());
    }
*/