using AreaDeJogo;

namespace Xadrez
{
    class Torre : Peca
    {
        public Torre(Cor cor, Tabuleiro tab) : base(cor, tab)
        {

        }

        public override string ToString()
        {
            return "T";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] retorno = new bool[T.Linhas,T.Colunas];

            for (int i = PecaPosicao.Coluna - 1; i >= 0; i--)
            {
                Posicao proxima = new(PecaPosicao.Linha, i);
                if (T.ExistePeca(proxima))
                {
                    if (T.MandaPeca(proxima).PecaCor != PecaCor)
                    {
                        retorno[proxima.Linha, proxima.Coluna] = true;
                    }
                    break;     
                }
                retorno[proxima.Linha, proxima.Coluna] = true;
            }

            for (int i = PecaPosicao.Coluna + 1; i < T.Colunas; i++)
            {
                Posicao proxima = new(PecaPosicao.Linha, i);
                if (T.ExistePeca(proxima))
                {
                    if (T.MandaPeca(proxima).PecaCor != PecaCor)
                    {
                        retorno[proxima.Linha, proxima.Coluna] = true;
                    }
                    break;     
                }
                retorno[proxima.Linha, proxima.Coluna] = true;
            }

            for (int i = PecaPosicao.Linha - 1; i >= 0; i--)
            {
                Posicao proxima = new(i, PecaPosicao.Coluna);
                if (T.ExistePeca(proxima))
                {
                    if (T.MandaPeca(proxima).PecaCor != PecaCor)
                    {
                        retorno[proxima.Linha, proxima.Coluna] = true;
                    }
                    break;     
                }
                retorno[proxima.Linha, proxima.Coluna] = true;
            }

            for (int i = PecaPosicao.Linha + 1; i < T.Linhas; i++)
            {
                Posicao proxima = new(i, PecaPosicao.Coluna);
                if (T.ExistePeca(proxima))
                {
                    
                    if (T.MandaPeca(proxima).PecaCor != PecaCor)
                    {
                        retorno[proxima.Linha, proxima.Coluna] = true;
                    }
                    break;     
                }
                retorno[proxima.Linha, proxima.Coluna] = true;
            }

            return retorno;
        }
    }
}