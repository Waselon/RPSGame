using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace RPSGame
{
    public class Table
    {
        public void GenerateTable(Dictionary<int, string> contents)
        {
            List<string> c = new List<string>();
            Rules rules = new Rules();
            c.Add("PC / USER");
            c.AddRange(contents.Values.ToArray());
            var table = new ConsoleTable(c.ToArray());
            for (int i = 1; i <= contents.Values.ToArray().Length; i++)
            {
                List<string> s = new List<string>();
                s.Add(c[i]);
                for (int j = 0; j <= contents.Values.ToArray().Length; j++)
                {
                    s.Add(rules.GetWinner(contents.Keys.ToArray(), contents.Keys.ToArray().Length, i, j));
                }
                s.RemoveAt(1);
                table.AddRow(s.ToArray());
            }
            table.Write(Format.Alternative);
        }
    }
}
