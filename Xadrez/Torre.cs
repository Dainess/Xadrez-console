using AreaDeJogo;

namespace Xadrez
{
    class Torre : Peca
    {
        public Torre(Cor cor, Tabuleiro T) : base(cor, T)
        {

        }

        public override string ToString()
        {
            return "T";
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

        internal bool RoquePequeno()
        {
            if (Movimentos == 0)
                return true;
            return false;
        }
    }
}