using Xadrez;

namespace AreaDeJogo
{
    class Posicao
    {
        public int Linha {get; set;}
        public int Coluna {get; set;}

        public Posicao(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public override string ToString()
        {
            return $"{Linha} , {Coluna}";
        }

        public PosicaoXadrez ToXadrez()
        {
            int converteLinha = Linha + (8 - (2 * Linha));
            char converteColuna = (char)(65 + Coluna);
            return new PosicaoXadrez(converteColuna, converteLinha);
        }
        public override bool Equals(object obj)
        {
            //
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //
            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            Posicao posicao = (Posicao)obj;
            // TODO: write your implementation of Equals() here
            if (posicao.Linha == Linha && posicao.Coluna == Coluna)
                return true;
            else
                return false;
        }
    }
}