using System.ComponentModel;
using System.Net.Sockets;

namespace Wuziqi
{
    class Program
    {

        public static ConsoleColor tc = ConsoleColor.Yellow;
        public static void Main(string[] args)
        {
            BoardManager board = new();
            int playerturn = 1;

            board.Display(playerturn);

            int mouseX = board.GetSize() / 2;
            int mouseY = board.GetSize() / 2;
            
            Console.ForegroundColor = (playerturn == 1) ? board.c1 : board.c2;
            Instruct();

            while (board.CheckWinner() == 0)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    switch(key)
                    {
                        case ConsoleKey.W:
                        case ConsoleKey.I:
                            mouseY = (mouseY == 0) ? mouseY : mouseY - 1;
                            board.Display(playerturn, mouseY, mouseX);
                            Console.ForegroundColor = (playerturn == 1) ? board.c1 : board.c2;
                            Instruct();
                            break;
                        case ConsoleKey.A:
                        case ConsoleKey.J:
                            mouseX = (mouseX == 0) ? mouseX : mouseX - 1;
                            board.Display(playerturn, mouseY, mouseX);
                            Console.ForegroundColor = (playerturn == 1) ? board.c1 : board.c2;
                            Instruct();
                            break;
                        case ConsoleKey.S:
                        case ConsoleKey.K:
                            mouseY = (mouseY == board.GetSize() -1) ? mouseY : mouseY + 1;
                            board.Display(playerturn, mouseY, mouseX);
                            Console.ForegroundColor = (playerturn == 1) ? board.c1 : board.c2;
                            Instruct();
                            break;
                        case ConsoleKey.D:
                        case ConsoleKey.L:
                            mouseX = (mouseX == board.GetSize() -1) ? mouseX : mouseX + 1;
                            board.Display(playerturn, mouseY, mouseX);
                            Console.ForegroundColor = (playerturn == 1) ? board.c1 : board.c2;
                            Instruct();
                            break;
                        case ConsoleKey.Enter:
                        case ConsoleKey.Spacebar:
                            if(board.Grid2D[mouseY, mouseX] != 0)
                            {
                                board.Display(playerturn, mouseY, mouseX);
                                Console.ForegroundColor = tc;
                                Console.WriteLine("\n>>                   That space is taken! Try Again!                   <<\n");
                            }
                            else
                            {
                                board.Grid2D[mouseY, mouseX] = playerturn; // flip for printing
                                if(board.CheckWinner() == 0)
                                {
                                    playerturn *= -1;
                                    board.Display(playerturn);
                                    Console.ForegroundColor = (playerturn == 1) ? board.c1 : board.c2;
                                    Instruct();
                                }

                            }
                            break;
                    }
                }
            }

            Console.ForegroundColor = (playerturn == 1) ? board.c1 : board.c2;
            Console.Write("==========|~ WINNER ~|====");
            Console.Write("====|~ WINNER ~|====");
            Console.WriteLine("====|~ WINNER ~|==========");
            board.Display(playerturn);
            Console.ForegroundColor = (playerturn == 1) ? board.c1 : board.c2;
            Console.Write("==========|~ WINNER ~|====");
            Console.Write("====|~ WINNER ~|====");
            Console.WriteLine("====|~ WINNER ~|==========");

            Console.ResetColor();

        }

        public static void Instruct()
        {
            Console.ForegroundColor = tc;
            Console.WriteLine("\n>> Use WASD/IJKL to select your location and ENTER/SPACEBAR to mark it <<\n");
            Console.ResetColor();
        }
    }
}