using System.Runtime.Intrinsics.Arm;

namespace Wuziqi
{
    class BoardManager
    {
        private static readonly int Size = 16;

        public ConsoleColor c1 = ConsoleColor.Green;
        public ConsoleColor c2 = ConsoleColor.Red;
        public ConsoleColor c3 = ConsoleColor.Yellow;
        public int[,] Grid2D = new int[Size,Size];

        public BoardManager()
        {
            for(int i = 0; i < Size; i++)
            {
                for(int j = 0; j < Size; j++)
                {
                    Grid2D[i,j] = 0;
                }
            }
        }

        public int GetSize()
        {
            return Size;
        }

        public void Display(int playerturn, int mouseX = -1, int mouseY = -1)
        {
            Console.ResetColor();

            //Console.WriteLine();
            Console.Write("      ");

            Console.ForegroundColor = (playerturn == 1) ? c1 : c2;
            for(int i = 0; i < Size; i++)
            {
                Console.Write($"{i:D2}  ");
            }
            Console.ResetColor();
            Console.WriteLine();
            Console.Write("    ");
            for(int i = 0; i < Size; i++)
            {
                Console.Write("  | ");
            }

            // actual grid start
            for(int i = 0; i < Size; i++)
            {
                Console.WriteLine();
                Console.ForegroundColor = (playerturn == 1) ? c1 : c2;
                Console.Write($"{i:D2}");
                Console.ResetColor();
                Console.Write(" ---");

                for(int j = 0; j < Size; j++)
                {

                    // check if cursor is here
                    if ((mouseX == i) && (mouseY == j)){Console.ForegroundColor=c3;}
                    else{Console.ForegroundColor = (Grid2D[i,j] == 1) ? c1 : c2;}

                    if (Grid2D[i,j] == 0)
                    {
                        if ((mouseX == i) && (mouseY == j)){Console.Write("V");}
                        else{Console.Write(" ");}
                    }
                    else if (Grid2D[i,j] == 1)
                    {
                        Console.Write("O");
                    }
                    else if (Grid2D[i,j] == -1)
                    {
                        Console.Write("X");
                    }
                    Console.ResetColor();
                    Console.Write("---");
                }

                Console.ForegroundColor = (playerturn == 1) ? c1 : c2;
                Console.Write($" {i:D2}");
                Console.ResetColor();


                Console.WriteLine("");
                Console.Write("    ");
                for(int j = 0; j < Size; j++)
                {
                    Console.Write("  | ");
                }

            }
            Console.WriteLine();
            Console.Write("      ");

            Console.ForegroundColor = (playerturn == 1) ? c1 : c2;
            for(int i = 0; i < Size; i++)
            {
                Console.Write($"{i:D2}  ");
            }
            Console.ResetColor();
            Console.WriteLine("");
        }
    
        public int CheckWinner()
        {
            int size = Size;
            int winLength = 5;

            // directions: right, down, down-right, down-left
            int[][] directions = new int[][]
            {
                new int[] { 0, 1 },  // right
                new int[] { 1, 0 },  // down
                new int[] { 1, 1 },  // down-right
                new int[] { 1, -1 }  // down-left
            };

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int player = Grid2D[i, j];
                    if (player == 0) continue;

                    foreach (var dir in directions)
                    {
                        int count = 1;
                        int x = i;
                        int y = j;

                        for (int k = 1; k < winLength; k++)
                        {
                            x += dir[0];
                            y += dir[1];

                            if (x < 0 || x >= size || y < 0 || y >= size) break;
                            if (Grid2D[x, y] == player)
                            {
                                count++;
                            }
                            else break;
                        }

                        if (count == winLength)
                        {
                            return player;  // 1 or -1 depending on who won
                        }
                    }
                }
            }

            return 0; // No winner
        }

    
    }
}