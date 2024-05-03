using AreaDeJogo;

namespace Xadrez;

class Peao : Peca 
{
    public Peao(Cor cor, Tabuleiro T) : base (cor, T)
    {

    }

    public override string ToString()
    {
        return "P";
    }

    public override bool[,] MovimentosPossiveis()
    {
        bool[,] retorno = new bool[T.Linhas,T.Colunas];

        if (PecaPosicao == null)
            throw new TabuleiroException("A peça não pode criar matriz de movimentos pois sua posição é nula");

        if (Movimentos == 0)
        {
            Posicao doisQuadrados = new Posicao(PecaPosicao.Linha - 2 * -1 * (int)PecaCor, PecaPosicao.Coluna);
            if (T.ExistePeca(doisQuadrados) == false)
            {
                Console.WriteLine("Derp");
                retorno[doisQuadrados.Linha, doisQuadrados.Coluna] = true;
            }
        }

        Posicao proximoQuadrado = new Posicao(PecaPosicao.Linha - 1 * -1 * (int)PecaCor, PecaPosicao.Coluna);
        if (T.ExistePeca(proximoQuadrado) == false)
        {
            retorno[proximoQuadrado.Linha, proximoQuadrado.Coluna] = true;
        }

        Posicao ataqueEsquerda = new Posicao(proximoQuadrado.Linha, proximoQuadrado.Coluna - 1);
        if (T.ExistePeca(ataqueEsquerda) && T.QualAPeca(ataqueEsquerda).PecaCor != PecaCor)
        {
            retorno[ataqueEsquerda.Linha, ataqueEsquerda.Coluna] = true;
        }

        Posicao ataqueDireita = new Posicao(proximoQuadrado.Linha, proximoQuadrado.Coluna + 1);
        if (T.ExistePeca(ataqueDireita) && T.QualAPeca(ataqueDireita).PecaCor != PecaCor)
        {
            retorno[ataqueDireita.Linha, ataqueDireita.Coluna] = true;
        }

        return retorno;
    }
}