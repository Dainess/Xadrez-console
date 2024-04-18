using AreaDeJogo;

namespace Xadrez;

class Futura : Peca
{
    public Futura(Cor cor, Tabuleiro tab) : base(cor, tab)
    {

    }

    public override string ToString()
    {
        return "X";
    }

    public override bool[,] MovimentosPossiveis()
    {
        throw new NotImplementedException();
    }
}