using AreaDeJogo;

namespace Xadrez;

class PosicaoXadrez
{
    public char Coluna {get; set;}
    public int Linha {get; set;}

    public PosicaoXadrez(char coluna, int linha)
    {
        Coluna = coluna;
        Linha = linha;       
    }

    public Posicao ToPosition()
    {
        int converteColuna = Coluna - 65;
        int converteLinha = 8 - Linha; 
        return new Posicao(converteLinha, converteColuna);
    }

    public override string ToString()
    {
        return $"{Coluna}{Linha}";
    }
}