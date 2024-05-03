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

        public Peca QualAPeca(int linha, int coluna)
        {
            return Espacos[linha, coluna];
        }

        public Peca QualAPeca(Posicao pos)
        {
            return Espacos[pos.Linha, pos.Coluna];
        }

        public bool ExistePeca(Posicao pos)
        {
            if (ValidarPosicao(pos) == false)
                throw new TabuleiroException($"Posição {pos} no tabuleiro não existe!");
            return QualAPeca(pos) != null;
        }

        public void ColocarPeca(Peca p, Posicao pos)
        {
            if (ExistePeca(pos))
            {
                Console.WriteLine($"{p}, {p.PecaPosicao}, {p.PecaCor} , {pos}");
                throw new TabuleiroException("Já existe uma peça nesse lugar!");
            }
            Espacos[pos.Linha, pos.Coluna] = p;
            p.SetPosicao(pos);
        }

        public Peca RetirarPeca(Posicao posicao)
        {
            if (QualAPeca(posicao) == null)
                return null!;
            else
            {   
                Peca aux = QualAPeca(posicao);
                aux.SetPosicao(null!);
                Espacos[posicao.Linha, posicao.Coluna] = null!;
                return aux;
            }       
        }
        
        public bool ValidarPosicao(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha >= this.Linhas  || pos.Coluna < 0 || pos.Coluna >= this.Colunas)
            {
                //throw new TabuleiroException($"Posição {pos} no tabuleiro não existe!");
                return false;
            }
            return true;
        }

        public void AtualizaMovimentosPossiveis()
        {
            foreach (var peca in PecasAtivas)
            {
                if (peca.PecaPosicao != null)
                    peca.ExisteMovimentoPossivel();
                else 
                    peca.DestinosLegais.Clear();
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