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

        public abstract bool[,] MovimentosPossiveis();
    }
}