using AreaDeJogo;

namespace Xadrez
{
    class Rei : Peca
    {
        public Rei(Cor cor, Tabuleiro tab) : base(cor, tab)
        {

        }

        public override string ToString()
        {
            return "R";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] retorno = new bool[T.Linhas,T.Colunas];

            foreach (var proxima in PosicoesPossiveis())
            {
                try
                {
                    if (T.ExistePeca(proxima))
                    {
                        if (T.MandaPeca(proxima).PecaCor != PecaCor)
                        {
                            retorno[proxima.Linha, proxima.Coluna] = true;
                        }
                        continue;
                    }
                    retorno[proxima.Linha, proxima.Coluna] = true;
                }
                catch (TabuleiroException e)
                {

                }
            }
            return retorno;
        }

        private Posicao[] PosicoesPossiveis()
        {
            return new Posicao[] {
                new Posicao(PecaPosicao.Linha, PecaPosicao.Coluna - 1), //oeste
                new Posicao(PecaPosicao.Linha - 1, PecaPosicao.Coluna - 1), //noroeste
                new Posicao(PecaPosicao.Linha - 1, PecaPosicao.Coluna), //norte
                new Posicao(PecaPosicao.Linha - 1, PecaPosicao.Coluna + 1), //nordeste
                new Posicao(PecaPosicao.Linha, PecaPosicao.Coluna + 1), //leste
                new Posicao(PecaPosicao.Linha + 1, PecaPosicao.Coluna + 1), //sudeste
                new Posicao(PecaPosicao.Linha + 1, PecaPosicao.Coluna), //sul
                new Posicao(PecaPosicao.Linha + 1, PecaPosicao.Coluna - 1), //sudoeste
            };
        }

        private bool PodeMover(Posicao posicao)
        {
            Peca p = T.MandaPeca(posicao);
            return p == null || p.PecaCor != PecaCor;
        }
    }
}

/* Mudar o tabuleiro para testar
Testar com outra peça no caminho
Tu esqueceu metade dos movimentos rs*/