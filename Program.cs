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
                //Game();
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine($"Tabuleiro Exception raised: " + e.Message);
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
            Peao scadufax = new Peao(Cor.Branca, T);
            T.ColocarPeca(scadufax, new Posicao(3, 4));
            
            Tela.ImprimirTabuleiro(T, scadufax.MovimentosPossiveis());
        }

        static void Test1()
        {
            Tabuleiro T = new Tabuleiro(8, 8);
            Rei reizinho = new Rei(Cor.Branca, T);
            Torre baradDur = new Torre(Cor.Preta, T);
            Rei prince = new Rei(Cor.Branca, T);
            Console.WriteLine();
            T.ColocarPeca(reizinho, new Posicao(5,5));
            T.ColocarPeca(baradDur, new Posicao(2,5));
            T.ColocarPeca(prince, new Posicao(3,4));
            T.ColocarPeca(new Rei(Cor.Branca, T), new Posicao(6,4));

            //Tela.ImprimirComFutura(baradDur);
            //Tela.ImprimirComFutura(prince);

            Tela.ImprimirTabuleiro(T);
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