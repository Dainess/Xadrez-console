
namespace AreaDeJogo
{
    class Peca
    {
        public Posicao PecaPosicao {get; set;}
        public Cor PecaCor {get; protected set;}
        public int Movimentos {get; protected set;}
        public Tabuleiro Tab {get; protected set;}

        public Peca (Cor cor, Tabuleiro tabuleiro)
        {
            PecaPosicao = null;
            PecaCor = cor;
            Tab = tabuleiro;
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
    }
}