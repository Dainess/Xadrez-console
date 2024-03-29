namespace AreaDeJogo
{
    class Tabuleiro
    {
        public int Linhas {get; set;}
        public int Colunas {get; set;}
        private Peca[,] Espacos {get; set;}

        public Tabuleiro (int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Espacos = new Peca[linhas, colunas];
        }

        public Peca MandaPeca(int linha, int coluna)
        {
            return Espacos[linha, coluna];
        }

        public Peca MandaPeca(Posicao pos)
        {
            return Espacos[pos.Linha, pos.Coluna];
        }

        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return MandaPeca(pos) != null;
        }

        public void ColocarPeca(Peca p, Posicao pos)
        {
            if (ExistePeca(pos))
            {
                throw new TabuleiroException("Já existe uma peça nesse lugar!");
            }
            Espacos[pos.Linha, pos.Coluna] = p;
            p.PecaPosicao = pos;
        }
        
        public void ValidarPosicao(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha > this.Linhas || pos.Coluna < 0 || pos.Coluna > this.Colunas)
            {
                throw new TabuleiroException("Posição no tabuleiro não existe!");
            }
        }
    }
}