using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RPSGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Rules rules = new Rules();
            Table table = new Table();
            Keygen keygen = new Keygen();
            Random random = new Random();
            int moves = args.Length;
            if (moves == 0 || moves % 2 == 0 || moves != args.Distinct().Count())
            {
                WriteErrorMessage();
                return;
            }
            Dictionary<int, string> movesDict = new Dictionary<int, string>();
            for (int i = 0; i < moves; i++)
            {
                movesDict.Add(i + 1, args[i]);
            }
            int cpuMove = random.Next(1, moves + 1);
            byte keysize = 16;
            string hmacKey = GenerateKey(keysize);
            Console.WriteLine($"HMAC: {keygen.GetHMAC(cpuMove.ToString(), hmacKey)}");
            int playerMove = -1;
            bool isValidInput = false;
            while (!(playerMove > 0) || !(playerMove <= moves) || !isValidInput)
            {
                WriteMenu();
                string input = Console.ReadLine();
                isValidInput = int.TryParse(input, out playerMove);
                if (input == "?") { table.GenerateTable(movesDict); playerMove = -1; }
                if (!isValidInput) playerMove = -1;
                if (playerMove == 0) { Console.WriteLine("Exiting program..."); Environment.Exit(0); }
            }
            Console.WriteLine($"Your move: {movesDict.GetValueOrDefault(playerMove)}");            
            Console.WriteLine($"Computer move: {movesDict.GetValueOrDefault(cpuMove)}");
            Console.WriteLine(rules.GetWinner(movesDict.Keys.ToArray(), moves, playerMove, cpuMove));
            Console.Write($"HMAC Key: {hmacKey}");

            void WriteErrorMessage()
            {
                Console.WriteLine("Incorrect input!\n" +
                    "Please write an odd number of distinct moves greater than 3 (Rock, Paper, Scissors, etc.).");
            }

            void WriteMenu()
            {              
                Console.WriteLine("Available moves: ");
                foreach (KeyValuePair<int, string> d in movesDict) 
                    Console.WriteLine($"{d.Key} - {d.Value}");
                Console.Write("0 - Exit\n? - Help\nEnter your move: ");
            }

            string GenerateKey(int size)
            {
                using (var generator = RandomNumberGenerator.Create())
                {
                    var key = new byte[size];
                    generator.GetBytes(key);
                    return BitConverter.ToString(key).Replace("-", "").ToLower();
                }
            }
        }
    }
}
