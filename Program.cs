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
                Tabuleiro T = new Tabuleiro(8, 8);
                
                T.ColocarPeca(new Torre(Cor.Preta, T), new Posicao(0,0));
                T.ColocarPeca(new Torre(Cor.Preta, T), new Posicao(1,3));
                T.ColocarPeca(new Rei(Cor.Preta, T), new Posicao(2,4));
                T.ColocarPeca(new Torre(Cor.Preta, T), new Posicao(0,0));
                Tela.ImprimirTabuleiro(T);
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
    }
}