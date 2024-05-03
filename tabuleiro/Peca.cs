namespace AreaDeJogo
{
    abstract class Peca
    {
        public Posicao? PecaPosicao {get; protected set;}
        public Cor PecaCor {get; protected set;}
        public int Movimentos {get; protected set;}
        public Tabuleiro T {get; protected set;}
        public List<Posicao> DestinosLegais {get; protected set;} = new List<Posicao>();

        public Peca (Cor cor, Tabuleiro tabuleiro)
        {
            PecaPosicao = null;
            PecaCor = cor;
            T = tabuleiro;
            Movimentos = 0;
        }

        public override string ToString()
        {
            if (PecaPosicao == null)
                return "Null";
            return PecaPosicao.ToString();
        }

        public void SomarMovimento()
        {
            Movimentos++;
        }

        public void SubtraiMovimento()
        {
            Movimentos--;
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

        public bool ExisteMovimentoPossivel()
        {
            DestinosLegais.Clear();
            bool[,] possiveis = MovimentosPossiveis();
            for (int i = 0; i < possiveis.GetLength(0); i++)
            {
                for (int j = 0; j < possiveis.GetLength(1); j++)
                {
                    if (possiveis[i, j] == true)
                    {
                        DestinosLegais.Add(new Posicao(i, j));}
                }
            }
            if (DestinosLegais.Count == 0)
                return false;
            else
                return true;
        }

        public void MatrizDePossiveis()
        {
            int count = 0;
            foreach (var thing in MovimentosPossiveis())
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

        public bool PecaPodeIrPara(Posicao posicao)
        {
            return MovimentosPossiveis()[posicao.Linha, posicao.Coluna];
        }
    }
}