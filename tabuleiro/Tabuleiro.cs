namespace AreaDeJogo
{
    class Tabuleiro
    {
        public int Linhas {get; set;}
        public int Colunas {get; set;}
        private Peca[,] Espacos {get; set;}
        public HashSet<Peca> PecasCapturadas { get; private set; } = new HashSet<Peca>();
        public HashSet<Peca> PecasAtivas { get; private set;} = new HashSet<Peca>();

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
            p.SetPosicao(pos);
        }

        public Peca RetirarPeca(Posicao posicao)
        {
            if (MandaPeca(posicao) == null)
                return null;
            else
            {   
                Peca aux = MandaPeca(posicao);
                aux.SetPosicao(null);
                Espacos[posicao.Linha, posicao.Coluna] = null;
                return aux;
            }       
        }
        
        public void ValidarPosicao(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha >= this.Linhas  || pos.Coluna < 0 || pos.Coluna >= this.Colunas)
            {
                throw new TabuleiroException($"Posição {pos} no tabuleiro não existe!");
            }
        }

        /*** HASH SET ***/

        public HashSet<Peca> Capturadas(Cor cor)
        {
            return PecasCapturadas.Where(peca => peca.PecaCor == cor).ToHashSet();
            /*HashSet<Peca> aux = new HashSet<Peca>();
            var outroAux = T.PecasCapturadas.Where(peca => peca.PecaCor == cor);
            aux = outroAux.ToHashSet();
            return aux;*/
        }
        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            return PecasAtivas.Where(peca => peca.PecaCor == cor).ToHashSet();
        }
    }
}