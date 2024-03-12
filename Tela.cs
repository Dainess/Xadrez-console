using AreaDeJogo;

namespace Aplicacao
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro Tab)
        {
            for (int i = 0; i < Tab.Linhas; i++)
            {
                for (int j = 0; j < Tab.Colunas; j++)
                {
                    Peca proxima = Tab.MandaPeca(i, j);
                    if (proxima == null)
                    {
                        Console.Write("_ ");
                    }
                    else
                    {
                        Console.Write($"{proxima} ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}