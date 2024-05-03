using AreaDeJogo;

namespace Xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Cor cor, Tabuleiro T) : base(cor, T)
        {

        }

        public override string ToString()
        {
            return "C";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] retorno = new bool[T.Linhas,T.Colunas];

            foreach (var thing in PosicoesPossiveis())
            {
                if (T.ExistePeca(thing))
                {
                    if (T.QualAPeca(thing).PecaCor != PecaCor)
                    {
                        retorno[thing.Linha, thing.Coluna] = true;
                    }
                    continue;
                }
                retorno[thing.Linha, thing.Coluna] = true;
            }

            return retorno;
        }

        private Posicao[] PosicoesPossiveis()
        {
            if (PecaPosicao == null)
                throw new TabuleiroException("O Rei não pode criar matriz de movimentos pois sua posição é nula");;
            return new Posicao[] {
                new Posicao(PecaPosicao.Linha - 1, PecaPosicao.Coluna - 2), //oeste
                new Posicao(PecaPosicao.Linha + 1, PecaPosicao.Coluna - 2),

                new Posicao(PecaPosicao.Linha - 2, PecaPosicao.Coluna - 1), //oeste2
                new Posicao(PecaPosicao.Linha + 2, PecaPosicao.Coluna - 1),

                //new Posicao(PecaPosicao.Linha - 2, PecaPosicao.Coluna - 1), == 03
                new Posicao(PecaPosicao.Linha - 2, PecaPosicao.Coluna + 1),

                //new Posicao(PecaPosicao.Linha - 1, PecaPosicao.Coluna - 2), == 01
                new Posicao(PecaPosicao.Linha - 1, PecaPosicao.Coluna + 2),

                //new Posicao(PecaPosicao.Linha - 1, PecaPosicao.Coluna + 2), == 08
                new Posicao(PecaPosicao.Linha + 1, PecaPosicao.Coluna + 2),

                //new Posicao(PecaPosicao.Linha - 2, PecaPosicao.Coluna + 1), == 06
                new Posicao(PecaPosicao.Linha + 2, PecaPosicao.Coluna + 1),

                //new Posicao(PecaPosicao.Linha + 2, PecaPosicao.Coluna - 1), == 04
                //new Posicao(PecaPosicao.Linha + 2, PecaPosicao.Coluna + 1), == 12

                //new Posicao(PecaPosicao.Linha + 1, PecaPosicao.Coluna - 2), == 02
                //new Posicao(PecaPosicao.Linha + 1, PecaPosicao.Coluna + 2) == 9,
            };
        }

        public void MostraPosicoesPossiveis()
        {
            Dictionary<Posicao, int> resultado = new Dictionary<Posicao, int>();
            Console.WriteLine($"Posicao do Cavalo: {PecaPosicao}");
            foreach (var thing in PosicoesPossiveis())
            {
                try
                {
                    resultado.Add(thing, 0);
                }
                catch (ArgumentException)
                {
                    resultado[thing]++;
                }
            }
            foreach (var thing in resultado)
            {
                Console.WriteLine(thing.ToString());
            }
        }
    }
}