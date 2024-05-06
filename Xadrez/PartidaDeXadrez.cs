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

    private Rei Rei(Cor cor)
    {
        Rei? reizinho = T.PecasEmJogo(cor).First(peca => peca is Rei) as Rei;
        if (reizinho == null)
        {
            throw new TabuleiroException("Por algum motivo não há um Rei entre as peças em jogo");
        }
        return reizinho;
    }

    private bool EmXeque(Cor cor)
    {
        Rei nossoRei = Rei(cor);
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

    private bool SimulaXeque(Posicao pos, Cor cor)
    {
        Rei nossoRei = Rei(cor);
        if (nossoRei.PecaPosicao == null)
            throw new TabuleiroException($"Por algum acaso o rei não tem uma posição no tabuleiro");
        Peca pecaDeslocada = ExecutaMovimento(nossoRei.PecaPosicao, pos);
        if (EmXeque(cor))
        {
            DesfazMovimento(nossoRei.PecaPosicao, pos, pecaDeslocada);
            return true;
        }
        else
        {
            DesfazMovimento(nossoRei.PecaPosicao, pos, pecaDeslocada);
            return false;
        }
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
                if (SimulaJogadaVerificandoXeque(original, posicao, pecaSimulada) == false)
                {
                    DesfazMovimento(original, posicao, pecaSimulada);
                    return true;
                }
                DesfazMovimento(original, posicao, pecaSimulada); // correção aleatória enquanto olhava outra coisa
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

    public bool SimulaJogadaVerificandoXeque(Posicao origem, Posicao destino, Peca pecaCapturada)
    {
        if (EmXeque(JogadorAtual))
        {
            DesfazMovimento(origem, destino, pecaCapturada);
            return true;
        }
        return false;
    }

    public bool Roque(Peca peca, Posicao destino)
    {
        // isso aqui é igual a: Rei reizinho = peca as Rei; if (reizinho != null) 
        if (peca is Rei reizinho)
        {
            if (reizinho.PecaPosicao == null)
                throw new TabuleiroException("De alguma forma o rei está sem posição durante a execução do Roque Pequeno");
            if (destino == new Posicao(reizinho.PecaPosicao.Linha, reizinho.PecaPosicao.Coluna + 2))
            {
                return true;
            }
        }
        return false;
    }

    public bool EnPassant(Peca peca, Posicao destino)
    {
        if (peca is Peao peao)
        {
            if (peao.PecaPosicao == null)
                throw new TabuleiroException("De alguma forma o peao está sem posição durante a execução do En Passant");

            Posicao esquerda = new(peao.PecaPosicao.Linha - 1 + ((int)peao.PecaCor * 2), peao.PecaPosicao.Coluna - 1);
            Posicao direita = new(peao.PecaPosicao.Linha - 1 + ((int)peao.PecaCor * 2), peao.PecaPosicao.Coluna + 1);
            
            if ((destino == esquerda || destino == direita) 
                && T.ExistePeca(destino) == false)
                return true;
        }
        return false;
    }

    public bool PodeRoque(Posicao origem, Posicao destino)
    {
        Posicao caminho = new Posicao(origem.Linha, origem.Coluna + 1);
        if (SimulaXeque(origem, JogadorAtual) == false && SimulaXeque(destino, JogadorAtual) == false && SimulaXeque(caminho, JogadorAtual) == false)
        {
            return true;
        }
        return false;
    }

    public void ColocarNovaPeca(Peca peca, char coluna, int linha)
    {
        T.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosition());
        T.PecasAtivas.Add(peca);
    }

    public void IniciarTabuleiro(bool test)
    {
        ColocarNovaPeca(new Peao(Cor.Branca, T), 'A', 2);
        ColocarNovaPeca(new Peao(Cor.Branca, T), 'B', 2);
        ColocarNovaPeca(new Peao(Cor.Branca, T), 'C', 2);
        ColocarNovaPeca(new Peao(Cor.Branca, T), 'D', 2);
        ColocarNovaPeca(new Peao(Cor.Branca, T), 'E', 2);
        ColocarNovaPeca(new Peao(Cor.Branca, T), 'F', 2);
        ColocarNovaPeca(new Peao(Cor.Branca, T), 'G', 2);
        ColocarNovaPeca(new Peao(Cor.Branca, T), 'H', 2);

        ColocarNovaPeca(new Torre(Cor.Branca,T), 'A', 1);
        ColocarNovaPeca(new Rei(Cor.Branca,T), 'E', 1);
        ColocarNovaPeca(new Torre(Cor.Branca,T), 'H', 1);

        ColocarNovaPeca(new Peao(Cor.Preta, T), 'A', 7);
        ColocarNovaPeca(new Peao(Cor.Preta, T), 'B', 7);
        ColocarNovaPeca(new Peao(Cor.Preta, T), 'C', 7);
        ColocarNovaPeca(new Peao(Cor.Preta, T), 'D', 7);
        ColocarNovaPeca(new Peao(Cor.Preta, T), 'E', 7);
        ColocarNovaPeca(new Peao(Cor.Preta, T), 'F', 7);
        ColocarNovaPeca(new Peao(Cor.Preta, T), 'G', 7);
        ColocarNovaPeca(new Peao(Cor.Preta, T), 'H', 7);

        ColocarNovaPeca(new Torre(Cor.Preta,T), 'A', 8);
        ColocarNovaPeca(new Rei(Cor.Preta,T), 'E', 8);
        ColocarNovaPeca(new Torre(Cor.Preta,T), 'H', 8);

        T.AtualizaMovimentosPossiveis();
    }

    public void IniciarTabuleiro()
    {
        var bots = new[] {
            new {peca = (Peca)(new Torre(Cor.Branca, T)), coluna = 'C', linha = 1},
        };

        ColocarNovaPeca(new Peao(Cor.Branca, T), 'A', 2);
        ColocarNovaPeca(new Peao(Cor.Branca, T), 'B', 2);
        ColocarNovaPeca(new Peao(Cor.Branca, T), 'C', 2);
        ColocarNovaPeca(new Peao(Cor.Branca, T), 'D', 2);
        ColocarNovaPeca(new Peao(Cor.Branca, T), 'E', 2);
        ColocarNovaPeca(new Peao(Cor.Branca, T), 'F', 2);
        ColocarNovaPeca(new Peao(Cor.Branca, T), 'G', 2);
        ColocarNovaPeca(new Peao(Cor.Branca, T), 'H', 2);

        ColocarNovaPeca(new Torre(Cor.Branca, T), 'A', 1);
        ColocarNovaPeca(new Cavalo(Cor.Branca, T), 'B', 1);
        ColocarNovaPeca(new Bispo(Cor.Branca, T), 'C', 1);
        ColocarNovaPeca(new Rainha(Cor.Branca, T), 'D', 1);
        ColocarNovaPeca(new Rei(Cor.Branca, T), 'E', 1);
        ColocarNovaPeca(new Bispo(Cor.Branca, T), 'F', 1);
        ColocarNovaPeca(new Cavalo(Cor.Branca, T), 'G', 1);
        ColocarNovaPeca(new Torre(Cor.Branca, T), 'H', 1);
        
        ColocarNovaPeca(new Peao(Cor.Preta, T), 'A', 7);
        ColocarNovaPeca(new Peao(Cor.Preta, T), 'B', 7);
        ColocarNovaPeca(new Peao(Cor.Preta, T), 'C', 7);
        ColocarNovaPeca(new Peao(Cor.Preta, T), 'D', 7);
        ColocarNovaPeca(new Peao(Cor.Preta, T), 'E', 7);
        ColocarNovaPeca(new Peao(Cor.Preta, T), 'F', 7);
        ColocarNovaPeca(new Peao(Cor.Preta, T), 'G', 7);
        ColocarNovaPeca(new Peao(Cor.Preta, T), 'H', 7);

        ColocarNovaPeca(new Torre(Cor.Preta, T), 'A', 8);
        ColocarNovaPeca(new Cavalo(Cor.Preta, T), 'B', 8);
        ColocarNovaPeca(new Bispo(Cor.Preta, T), 'C', 8);
        ColocarNovaPeca(new Rainha(Cor.Preta, T), 'D', 8);
        ColocarNovaPeca(new Rei(Cor.Preta, T), 'E', 8);
        ColocarNovaPeca(new Bispo(Cor.Preta, T), 'F', 8);
        ColocarNovaPeca(new Cavalo(Cor.Preta, T), 'G', 8);
        ColocarNovaPeca(new Torre(Cor.Preta, T), 'H', 8);
        
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
                        if (SimulaJogadaVerificandoXeque(origem, destino, pecaCapturada))
                        {
                            Console.WriteLine("Este movimento te deixa em xeque. Escolha outro.\n");
                        }
                        if (Roque(proximaPeca, destino))
                        {
                            if (PodeRoque(origem, destino))
                            {
                                ExecutaMovimento(new Posicao(destino.Linha, destino.Coluna + 1), new Posicao(destino.Linha, destino.Coluna - 1));
                                RealizaJogada(pecaCapturada);
                                break;
                            }                      
                            else
                            {
                                Console.WriteLine("O rei fica em xeque durante o movimento do Roque");
                                continue;
                            }
                        } 
                        else if (EnPassant(proximaPeca, destino))
                        {

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