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
                Test1();
                //Game();
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine($"Tabuleiro Exception raised: " + e.Message + e.StackTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine("Deu ruim com " + e.Message + e.StackTrace);
            }
        }

        static void Game()
        {
            PartidaDeXadrez partida = new PartidaDeXadrez();
            partida.IniciarTabuleiro();
                            
            partida.LoopDePartida();
        }

        static void Test()
        {
            Tabuleiro T = new Tabuleiro(8, 8);
            Peao scadufax = new Peao(Cor.Preta, T);
            T.ColocarPeca(scadufax, new Posicao(3, 4));
            T.ColocarPeca(new Torre(Cor.Preta, T), new Posicao(4, 3));
            T.ColocarPeca(new Torre(Cor.Branca, T), new Posicao(4, 5));
            
            Tela.ImprimirTabuleiro(T, scadufax.MovimentosPossiveis());
        }

        static void Test1()
        {
            PartidaDeXadrez partida = new PartidaDeXadrez();
            partida.IniciarTabuleiro(true);
                            
            partida.LoopDePartida();
        }
    }
}

/*
    Posicao um = new Posicao(2, 3);
    Posicao dois = new Posicao(2, 2);
    Posicao tres = new Posicao(2, 3);

    Console.WriteLine($"Um e dois: {um} e {dois}: {um.Equals(dois)}");
    Console.WriteLine($"Um e dois: {um} e {tres}: {um.Equals(tres)}");
    Console.WriteLine($"Um e dois: {tres} e {dois}: {tres.Equals(dois)}");

    List<int> sumList = new List<int>() {1, 2, 3};
    Console.WriteLine(sumList.Sum());
*/