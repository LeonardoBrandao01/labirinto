using System;
using System.Collections.Generic;

public class Program
{
    private const int limit = 15;

    static void Main()
    {
        char[,] labirinto = new char[limit, limit];
        criaLabirinto(labirinto);
        mostrarLabirinto(labirinto, limit, limit);
        buscarQueijo(labirinto, 1, 1);
        Console.ReadKey();
    }

    static void mostrarLabirinto(char[,] array, int l, int c)
    {
        for (int i = 0; i < l; i++)
        {
            Console.WriteLine();
            for (int j = 0; j < c; j++)
            {
                Console.Write($" {array[i, j]} ");
            }
        }
        Console.WriteLine();
    }

    static void criaLabirinto(char[,] meuLab)
    {
        Random random = new Random();
        for (int i = 0; i < limit; i++)
        {
            for (int j = 0; j < limit; j++)
            {
                meuLab[i, j] = random.Next(4) == 1 ? '|' : '.';
            }
        }

        for (int i = 0; i < limit; i++)
        {
            meuLab[0, i] = '*';
            meuLab[limit - 1, i] = '*';
            meuLab[i, 0] = '*';
            meuLab[i, limit - 1] = '*';
        }

        int x = random.Next(1, limit - 1);
        int y = random.Next(1, limit - 1);
        meuLab[x, y] = 'Q';
    }

    static void buscarQueijo(char[,] meuLab, int i, int j)
    {
        Stack<(int, int)> minhaPilha = new Stack<(int, int)>();
        minhaPilha.Push((i, j));

        while (minhaPilha.Count > 0)
        {
            (i, j) = minhaPilha.Pop();
            if (meuLab[i, j] == 'Q')
            {
                Console.WriteLine("Queijo encontrado!");
                return;
            }

            meuLab[i, j] = 'v'; // marcando como visitado

            // Tentar mover para direita
            if (j + 1 < limit && (meuLab[i, j + 1] == '.' || meuLab[i, j + 1] == 'Q'))
                minhaPilha.Push((i, j + 1));

            // Tentar mover para baixo
            if (i + 1 < limit && (meuLab[i + 1, j] == '.' || meuLab[i + 1, j] == 'Q'))
                minhaPilha.Push((i + 1, j));

            // Tentar mover para esquerda
            if (j - 1 >= 0 && (meuLab[i, j - 1] == '.' || meuLab[i, j - 1] == 'Q'))
                minhaPilha.Push((i, j - 1));

            // Tentar mover para cima
            if (i - 1 >= 0 && (meuLab[i - 1, j] == '.' || meuLab[i - 1, j] == 'Q'))
                minhaPilha.Push((i - 1, j));

            System.Threading.Thread.Sleep(200);
            Console.Clear();
            mostrarLabirinto(meuLab, limit, limit);
        }

        Console.WriteLine("Queijo não encontrado. Impossível achar o queijo!");
    }
}
