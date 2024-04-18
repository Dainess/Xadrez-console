using AreaDeJogo;
using Xadrez;

namespace Aplicacao
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro Tab)
        {
            for (int i = 0; i < Tab.Linhas; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < Tab.Colunas; j++)
                {
                    Peca proxima = Tab.MandaPeca(i, j);
                    if (proxima == null)
                    {
                        Console.Write("_ ");
                    }
                    else
                    {
                        ImprimirPeca(proxima);
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static PosicaoXadrez LerProximaJogada()
        {
            string leitura = Console.ReadLine().ToUpper();
            int linha = int.Parse(leitura[1] + "");
            return new PosicaoXadrez(leitura[0],linha);
        }

        private static void ImprimirPeca(Peca peca)
        {
            if (peca.PecaCor == Cor.Branca)
                Console.Write(peca);
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }

        public static void ImprimirComFutura(Peca peca)
        {
            var movimento = peca.MovimentosPossiveis();

            for (int coluna = 0; coluna < peca.T.Linhas; coluna++)
            {
                for (int linha = 0; linha < peca.T.Colunas; linha++)
                {
                    if (movimento[linha, coluna])
                        peca.T.ColocarPeca(new Futura(peca.PecaCor, peca.T), new Posicao(linha, coluna));
                }
            }

            ImprimirTabuleiro(peca.T);

            for (int coluna = 0; coluna < peca.T.Linhas; coluna++)
            {
                for (int linha = 0; linha < peca.T.Colunas; linha++)
                {
                    if (movimento[linha, coluna])
                        peca.T.RetirarPeca(new Posicao(linha, coluna));
                }
            }
        } 
    }
}