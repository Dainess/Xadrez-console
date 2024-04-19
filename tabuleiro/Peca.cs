namespace AreaDeJogo
{
    abstract class Peca
    {
        public Posicao PecaPosicao {get; protected set;}
        public Cor PecaCor {get; protected set;}
        public int Movimentos {get; protected set;}
        public Tabuleiro T {get; protected set;}

        public Peca (Cor cor, Tabuleiro tabuleiro)
        {
            PecaPosicao = null;
            PecaCor = cor;
            T = tabuleiro;
            Movimentos = 0;
        }

        public override string ToString()
        {
            return PecaPosicao.ToString();
        }

        public void SomarMovimento()
        {
            Movimentos++;
        }

        public void SetPosicao(Posicao posicao)
        {
            PecaPosicao = posicao;
        }

        public void SetCor(Cor cor)
        {
            PecaCor = cor;
        }

        public abstract bool[,] MovimentosPossiveis();

        public void MatrizDePossiveis()
        {
            int count = 0;
            foreach (var thing in this.MovimentosPossiveis())
            {
                Console.Write($"{thing}, ");
                count++;
                if (count >= 8)
                {
                    count = 0;
                    Console.WriteLine();
                }
            }
        }
    }
}