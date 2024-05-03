using AreaDeJogo;

namespace Xadrez
{
    class Bispo : Peca
    {
        public Bispo(Cor cor, Tabuleiro T) : base(cor, T)
        {

        }

        public override string ToString()
        {
            return "B";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] retorno = new bool[T.Linhas,T.Colunas];

            if (PecaPosicao == null)
                throw new TabuleiroException("A peça não pode criar matriz de movimentos pois sua posição é nula");
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
}