using System;

namespace Zgadnij_liczbę_2;
internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        GameManager game = new();
            game.run();
    }
}
