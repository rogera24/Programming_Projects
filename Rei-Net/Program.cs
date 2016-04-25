using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rei_Net
{
    class Program
    {
        static void Main(string[] args)
        {
            Player r = new Player("Roger");
            Player j = new Player("Jordan");
            Board board = new Board(8,8);
            int[] p1WinSpaces = {3, 4};
            int[] p2WinSpaces = {59, 60};
            board.setExist(p1WinSpaces);
            board.setExist(p2WinSpaces);
            board.setWinSpaces(p1WinSpaces, p2WinSpaces);
            board.firstprintBoard(r, j);
            progression();
        }
        static void progression()
        {
            Player r = new Player("Roger");
            Player j = new Player("Jordan");
            int[] p1g = new int[4];
            int[] p1b = new int[4];
            int[] p2g = new int[4];
            int[] p2b = new int[4];
            Console.Out.Write("Choose the unique spaces for your starting good cards and hit enter after each:");
            string g1 = Console.ReadLine();
            string g2 = Console.ReadLine();
            string g3 = Console.ReadLine();
            string g4 = Console.ReadLine();

        }
    }
}
