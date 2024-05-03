using AreaDeJogo;

namespace Xadrez;

class Rainha : Peca
{
    public Rainha(Cor cor, Tabuleiro T) : base(cor, T)
    {
        
    }

    public override string ToString()
    {
        return "Q";
    }
    public override bool[,] MovimentosPossiveis()
    {
        bool[,] retorno = new bool[T.Linhas,T.Colunas];

        if (PecaPosicao == null)
            throw new TabuleiroException("A peça não pode criar matriz de movimentos pois sua posição é nula");

        for (int i = PecaPosicao.Coluna - 1; i >= 0; i--)
        {
            Posicao proxima = new(PecaPosicao.Linha, i);
            if (UmaDirecaoDoMovimentoPossivel(retorno, proxima))
            {
                break;
            }
        }

        for (int i = PecaPosicao.Coluna + 1; i < T.Colunas; i++)
        {
            Posicao proxima = new(PecaPosicao.Linha, i);
            if (UmaDirecaoDoMovimentoPossivel(retorno, proxima))
            {
                break;
            }
        }

        for (int i = PecaPosicao.Linha - 1; i >= 0; i--)
        {
            Posicao proxima = new(i, PecaPosicao.Coluna);
            if (UmaDirecaoDoMovimentoPossivel(retorno, proxima))
            {
                break;
            }
        }

        for (int i = PecaPosicao.Linha + 1; i < T.Linhas; i++)
        {
            Posicao proxima = new(i, PecaPosicao.Coluna);
            if (UmaDirecaoDoMovimentoPossivel(retorno, proxima))
            {
                break;
            }
        }

        Posicao testePos = new Posicao(PecaPosicao.Linha - 1, PecaPosicao.Coluna - 1);

        while (true)
        {
            if (UmaDirecaoDoMovimentoPossivel(testePos, retorno))
            {
                break;
            }
            testePos = new Posicao(testePos.Linha - 1, testePos.Coluna - 1);
        }

        testePos = new Posicao(PecaPosicao.Linha - 1, PecaPosicao.Coluna + 1);
        while (true)
        {
            if (UmaDirecaoDoMovimentoPossivel(testePos, retorno))
            {
                break;
            }
            testePos = new Posicao(testePos.Linha - 1, testePos.Coluna + 1);
        }

        testePos = new Posicao(PecaPosicao.Linha + 1, PecaPosicao.Coluna - 1);
        while (true)
        {
            if (UmaDirecaoDoMovimentoPossivel(testePos, retorno))
            {
                break;
            }
            testePos = new Posicao(testePos.Linha + 1, testePos.Coluna - 1);
        }

        testePos = new Posicao(PecaPosicao.Linha + 1, PecaPosicao.Coluna + 1);
        while (true)
        {
            if (UmaDirecaoDoMovimentoPossivel(testePos, retorno))
            {
                break;
            }
            testePos = new Posicao(testePos.Linha + 1, testePos.Coluna + 1);
        }

        return retorno;
    }

    private bool UmaDirecaoDoMovimentoPossivel(bool[,] retorno, Posicao proxima)
    {
        if (T.ExistePeca(proxima))
        {
            if (T.QualAPeca(proxima).PecaCor != PecaCor)
            {
                retorno[proxima.Linha, proxima.Coluna] = true;
            }     
            return true;
        }
        retorno[proxima.Linha, proxima.Coluna] = true;
        return false;
    }

    private bool UmaDirecaoDoMovimentoPossivel(Posicao testePos, bool[,] retorno)
    {
        if (T.ValidarPosicao(testePos) == false)
        {
            return true;   
        }
        else if (T.ExistePeca(testePos))
        {
            if (T.QualAPeca(testePos).PecaCor != PecaCor)
            {
                retorno[testePos.Linha, testePos.Coluna] = true;
            }
            return true;
        }
        retorno[testePos.Linha, testePos.Coluna] = true;
        return false;
    }
}