using System;
using System.Runtime.CompilerServices;
#nullable disable

namespace JogoDaVelha
{
    class Jogador
    {
        public string nome { get; set; }
        public char simbolo { get; set; }
        public int pontuacao { get; set; }

        public Jogador(string Nome, char Simbolo)
        {
            nome = Nome;
            simbolo = Simbolo;
            pontuacao = 0;
        }
    }

    class JogoDaVelha
    {
        private char[,] tabuleiro;
        private Jogador jogador1;
        private Jogador jogador2;
        private Jogador jogadorAtual;

        public JogoDaVelha(Jogador jgdr1, Jogador jgdr2)
        {
            this.jogador1 = jgdr1;
            this.jogador2 = jgdr2;
            jogadorAtual = jogador1;
            tabuleiro = new char[3, 3];
            IniciarTabuleiro();
        }

        private void IniciarTabuleiro()
        {
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    tabuleiro[i, j] = ' ';
                }
            }
        }

        public void Jogar()
        {
            while (true)
            {
                ExibirTabuleiro();
                JogadaJogadorFisico();

                if (VerificarVitoria(jogador1.simbolo))
                {
                    ExibirTabuleiro();
                    Console.WriteLine($"{jogador1.nome} venceu!");
                    break;
                }

                if (VerificarEmpate())
                {
                    ExibirTabuleiro();
                    Console.WriteLine("O jogo empatou!");
                    break;
                }

                JogadaJogadorVirtual();

                if (VerificarVitoria(jogador2.simbolo))
                {
                    ExibirTabuleiro();
                    Console.WriteLine($"{jogador2.nome} venceu!");
                    break;
                }

                if (VerificarEmpate())
                {
                    ExibirTabuleiro();
                    Console.WriteLine("O jogo empatou!");
                    break;
                }
            }
        }

        private void JogadaJogadorFisico()
        {
            Console.WriteLine($"{jogador1.nome}, escolha a posição (linha e coluna de 1 a 3):");
            int linha = int.Parse(Console.ReadLine()) - 1;
            int coluna = int.Parse(Console.ReadLine()) - 1;

            if (tabuleiro[linha, coluna] == ' ')
            {
                tabuleiro[linha, coluna] = jogador1.simbolo;
            }
            else
            {
                Console.WriteLine("Posição inválida! Tente novamente.");
                JogadaJogadorFisico();
            }
        }

        private void JogadaJogadorVirtual()
        {
            Random rand = new Random();
            int linha, coluna;

            do
            {
                linha = rand.Next(0, 3);
                coluna = rand.Next(0, 3);
            } while (tabuleiro[linha, coluna] != ' ');

            Console.WriteLine(jogador2.nome + " jogou na posição " + (linha + 1) + ", " + (coluna + 1));
            tabuleiro[linha, coluna] = jogador2.simbolo;
        }

        private bool VerificarVitoria(char simbolo)
        {
            for (int i = 0; i < 3; i++)
            {
                if ((tabuleiro[i, 0] == simbolo && tabuleiro[i, 1] == simbolo && tabuleiro[i, 2] == simbolo) ||
                    (tabuleiro[0, i] == simbolo && tabuleiro[1, i] == simbolo && tabuleiro[2, i] == simbolo))
                {
                    return true;
                }
            }

            if ((tabuleiro[0, 0] == simbolo && tabuleiro[1, 1] == simbolo && tabuleiro[2, 2] == simbolo) ||
                (tabuleiro[0, 2] == simbolo && tabuleiro[1, 1] == simbolo && tabuleiro[2, 0] == simbolo))
            {
                return true;
            }

            return false;
        }

        private bool VerificarEmpate()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tabuleiro[i, j] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void TrocarJogador()
        {
            jogadorAtual = jogadorAtual == jogador1 ? jogador2 : jogador1;
        }

        private void ExibirTabuleiro()
        {
            Console.Clear();
            Console.WriteLine("  1 2 3");
            for (int i = 0; i < 3; i++)
            {
                Console.Write((i + 1) + " ");  
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(tabuleiro[i, j]);  
                    if (j < 2) Console.Write("|");  
                }
                Console.WriteLine();
                if (i < 2) Console.WriteLine("  -----");  
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Jogador[] jogadores = new Jogador[]
            {
                new Jogador("Alice", 'X'),
                new Jogador("Bob", 'O')
            };

            Console.WriteLine("Selecione o jogador físico: ");
            for(int i = 0; i <jogadores.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {jogadores[i].nome}");
            }
            int escolha = int.Parse(Console.ReadLine()) - 1;
            Jogador jogadorFisico = jogadores[escolha];

            char simboloVirtual = jogadorFisico.simbolo == 'X' ? 'O' : 'X';
            Jogador jogadorVirtual = new Jogador("Computador", simboloVirtual);

            JogoDaVelha jogo = new JogoDaVelha(jogadorFisico, jogadorVirtual);
            jogo.Jogar();
        }
    }
}
