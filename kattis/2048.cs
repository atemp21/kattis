using System;
using System.Linq;

namespace kattis
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var board = new int[4, 4];
            for (var i = 0; i < 4; i++)
            {
                var j = 0;
                foreach (var n in Console.ReadLine().Split(' ').Select(int.Parse))
                    board[i, j++] = n;
            }

            var result = new int[4, 4];
            var move = int.Parse(Console.ReadLine());
            
            if (move == 0)  result = Left(board);
            else if (move == 1) result = Up(board);
            else if (move == 2) result = Right(board);
            else result = Down(board);

            Print(result);
        }

        private static void Print(int[,] board)
        {
            for (var i = 0; i < 4; i++)
            {
                for(var j = 0; j< 4; j++)
                    Console.Write("{0} ", board[i,j]);
                Console.WriteLine();
            }
        }

        private static int[,] Left(int[,] board)
        {
            for (var row = 0; row < 4; row++)
            {
                var swapped = new bool[4];
                
                for (var col = 1; col < 4; col++)
                {
                    if (board[row, col] == 0) continue;

                    for (var i = col; i > 0; i--)
                    {
                        if (board[row, i - 1] == 0)
                        {
                            board[row, i - 1] = board[row, i];
                            board[row, i] = 0;
                            var temp = swapped[i];
                            swapped[i] = swapped[i - 1];
                            swapped[i - 1] = temp;
                        }
                        else if (board[row, i - 1] == board[row, i] && !(swapped[i] || swapped[i-1]))
                        {
                            board[row, i - 1] += board[row, i - 1];
                            board[row, i] = 0;
                            swapped[i - 1] = true;
                        }
                    }
                }
            }

            return board;
        }

        private static int[,] Up(int[,] board)
        {
            return Rotate(Left(Rotate(Rotate(Rotate(board)))));
        }

        private static int[,] Right(int[,] board)
        {
            return Rotate(Rotate(Left(Rotate(Rotate(board)))));
        }

        private static int[,] Down(int[,] board)
        {
            return Rotate(Rotate(Rotate(Left(Rotate(board)))));
        }

        private static int[,] Rotate(int[,] board)
        {
            var temp = new int[4, 4];
            for(var i = 0; i < 4; i++)
                for (var j = 0; j < 4; j++)
                 temp[i, j] = board[4 - j - 1, i];
            return temp;
        }
    }
}