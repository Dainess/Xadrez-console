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
                PartidaDeXadrez partida = new PartidaDeXadrez();
                partida.IniciarTabuleiro();
                                
                while (!partida.Terminada)
                {
                    partida.LoopDePartida();
                }
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
            char one = (char)(65 + 32);
            Console.WriteLine($"!{one}!");
        }
    }
}