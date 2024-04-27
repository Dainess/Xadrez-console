using AreaDeJogo;
using Xadrez;

namespace Aplicacao
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro T)
        {
            for (int i = 0; i < T.Linhas; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < T.Colunas; j++)
                {
                    ImprimirPeca(T.QualAPeca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
            //Capturadas(T);
        }

        public static void ImprimirTabuleiro(Tabuleiro T, bool[,] movimentos)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoPossiveis = ConsoleColor.DarkGray;

            for (int i = 0; i < T.Linhas; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < T.Colunas; j++)
                {
                    if (movimentos[i, j])
                    {
                        Console.BackgroundColor = fundoPossiveis;
                        
                    }
                    ImprimirPeca(T.QualAPeca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H\n");
        }

        public static PosicaoXadrez LerProximaJogada()
        {
            string leitura = Console.ReadLine().ToUpper();
            if (leitura[0] == 'Q' || leitura[0] == 'q')
                Environment.Exit(0);
            else if (leitura == "ORIGEM")
                throw new TabuleiroException("Retornando para escolha das peças.");
            int linha = int.Parse(leitura[1] + "");
            return new PosicaoXadrez(leitura[0],linha);
        }

        private static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("_ ");
            }
            else
            {
                DecideCor(peca);
            }
        }

        private static void DecideCor(Peca peca)
        {
            if (peca.PecaCor == Cor.Branca)
                Console.Write(peca);
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                if (peca.PecaCor == Cor.Preta)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else if (peca.PecaCor == Cor.Vermelha)
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
            Console.Write(" ");
        }

        private static void Capturadas(Tabuleiro T)
        {
            Console.WriteLine("\nPeças capturadas:");
            foreach (var thing in T.PecasCapturadas)
            {
                DecideCor(thing);
            }
        }

        private static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[ ");
            foreach(var thing in conjunto)
            {
                Console.Write($"{thing} ");
            }
            Console.WriteLine("]");
        }


        public static void Capturadas(Tabuleiro T, bool completo)
        {
            if (!completo)
                Capturadas(T);
            else
            {
                Console.WriteLine("Peças capturadas:");
                Console.Write("Brancas:");
                ImprimirConjunto(T.Capturadas(Cor.Branca));
                Console.Write("Pretas: ");
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                ImprimirConjunto(T.Capturadas(Cor.Preta));
                Console.ForegroundColor = aux;
            }
        }

        //*************************************************************************************

        public static void ImprimirComFutura(Peca peca)
        {
            var movimento = peca.MovimentosPossiveis();
            //peca.MatrizDePossiveis();

            for (int coluna = 0; coluna < peca.T.Linhas; coluna++)
            {
                for (int linha = 0; linha < peca.T.Colunas; linha++)
                {
                    if (movimento[linha, coluna])
                        if (peca.T.QualAPeca(linha, coluna) == null)
                        {
                            peca.T.ColocarPeca(new Futura(peca.PecaCor, peca.T), new Posicao(linha, coluna));
                        }
                        else
                        {
                            peca.T.QualAPeca(linha, coluna).SetCor(Cor.Vermelha);
                        }
                }
            }

            ImprimirTabuleiro(peca.T);

            for (int coluna = 0; coluna < peca.T.Linhas; coluna++)
            {
                for (int linha = 0; linha < peca.T.Colunas; linha++)
                {
                    if (movimento[linha, coluna])
                    {
                        if (peca.T.QualAPeca(linha, coluna) is Futura)
                        {
                            peca.T.RetirarPeca(new Posicao(linha, coluna));
                        }
                        else 
                        {
                            if (peca.PecaCor == Cor.Branca)
                                peca.T.QualAPeca(linha, coluna).SetCor(Cor.Preta);
                            else if (peca.PecaCor == Cor.Preta)
                                peca.T.QualAPeca(linha, coluna).SetCor(Cor.Branca);
                        }
                    } 
                }
            }
        } 
    }
}