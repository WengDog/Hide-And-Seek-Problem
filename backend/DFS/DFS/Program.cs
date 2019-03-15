using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFS
{
    class Program
    {
        static void Main(string[] args)
        {
            DFS tmp = new DFS();
            //testing file eksternal
            tmp.getInput("H:\\Testing.txt");
            tmp.Solve();
        }
    }
}
