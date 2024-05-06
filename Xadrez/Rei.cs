using AreaDeJogo;

namespace Xadrez
{
    class Rei : Peca
    {
        public Rei(Cor cor, Tabuleiro T) : base(cor, T)
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
                        if (T.QualAPeca(proxima).PecaCor != PecaCor)
                        {
                            retorno[proxima.Linha, proxima.Coluna] = true;
                        }
                        continue;
                    }
                    retorno[proxima.Linha, proxima.Coluna] = true;
                }
                catch (TabuleiroException)
                {

                }
            }
            return retorno;
        }

        private List<Posicao> PosicoesPossiveis()
        {
            if (PecaPosicao == null)
                throw new TabuleiroException("O Rei não pode criar matriz de movimentos pois sua posição é nula");;
            List<Posicao> doRei = new List<Posicao> {
                new Posicao(PecaPosicao.Linha, PecaPosicao.Coluna - 1), //oeste
                new Posicao(PecaPosicao.Linha - 1, PecaPosicao.Coluna - 1), //noroeste
                new Posicao(PecaPosicao.Linha - 1, PecaPosicao.Coluna), //norte
                new Posicao(PecaPosicao.Linha - 1, PecaPosicao.Coluna + 1), //nordeste
                new Posicao(PecaPosicao.Linha, PecaPosicao.Coluna + 1), //leste
                new Posicao(PecaPosicao.Linha + 1, PecaPosicao.Coluna + 1), //sudeste
                new Posicao(PecaPosicao.Linha + 1, PecaPosicao.Coluna), //sul
                new Posicao(PecaPosicao.Linha + 1, PecaPosicao.Coluna - 1), //sudoeste
            };
            if (RoquePequeno())
            {
                doRei.Add(new Posicao(PecaPosicao.Linha, PecaPosicao.Coluna + 2));
            }
            return doRei;
        }

        private bool RoquePequeno()
        {
            if (PecaPosicao == null)
                throw new TabuleiroException("De alguma forma o Rei não tem posição no tabuleiro");

            Torre queSejaTorre;

            try 
            {
                queSejaTorre = (Torre)T.QualAPeca(new Posicao (PecaPosicao.Linha, PecaPosicao.Coluna + 3));
            }
            catch
            {
                return false;
            }
                            
            if (Movimentos == 0 &&
            T.ExistePeca(new Posicao (PecaPosicao.Linha, PecaPosicao.Coluna + 1)) == false &&
            T.ExistePeca(new Posicao (PecaPosicao.Linha, PecaPosicao.Coluna + 2)) == false &&
            queSejaTorre.RoquePequeno())
            {
                return true;
            }
            return false;
        }

        private bool PodeMover(Posicao posicao)
        {
            Peca p = T.QualAPeca(posicao);
            return p == null || p.PecaCor != PecaCor;
        }
    }
}

/* Mudar o tabuleiro para testar
Testar com outra peça no caminho
Tu esqueceu metade dos movimentos rs*/