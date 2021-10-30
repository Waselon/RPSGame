using System;

namespace RPSGame
{
    public class Rules
    {        
        public string GetWinner(int[] arr, int n, int x, int y)
        {
            int halfMoves = (n - 1) / 2;
            int i, j;
            int dst = int.MaxValue;
            for (i = 0; i < n; i++)
            {
                for (j = i + 1; j < n; j++)
                {
                    if ((x == arr[i] && y == arr[j] || y == arr[i] && x == arr[j]) && dst > Math.Abs(i - j))
                        dst = Math.Abs(i - j);
                }
            }
            if (x == y)  return "Draw!";
            if ((dst > halfMoves && x < y) || (dst <= halfMoves && x > y))  return "You Lost!";
            else return "You Won!";
        }
    }
}
