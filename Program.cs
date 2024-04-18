using AreaDeJogo;
using Xadrez;

namespace Aplicacao
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Test();
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine($"Tabuleiro Exception raised: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Deu ruim com " + e.Message);
            }
        }

        static void Test()
        {
            Tabuleiro T = new Tabuleiro(8, 8);
            Rei reizinho = new Rei(Cor.Branca, T);
            T.ColocarPeca(reizinho, new Posicao(3, 1));

            Tela.ImprimirComFutura(reizinho);

            Tela.ImprimirTabuleiro(T);
        }

        static void Game()
        {
            PartidaDeXadrez partida = new PartidaDeXadrez();
            partida.IniciarTabuleiro();
                            
            while (!partida.Terminada)
            {
                partida.LoopDePartida();
            }
        }
    }
}

/*

int count = 0;
            foreach (var thing in reizinho.MovimentosPossiveis())
            {
                Console.Write($"{thing}, ");
                count++;
                if (count >= 8)
                {
                    count = 0;
                    Console.WriteLine();
                }
            }

*/