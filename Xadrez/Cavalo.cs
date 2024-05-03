using AreaDeJogo;

namespace Xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Cor cor, Tabuleiro tab) : base(cor, tab)
        {

        }

        public override string ToString()
        {
            return "B";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] retorno = new bool[T.Linhas,T.Colunas];



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