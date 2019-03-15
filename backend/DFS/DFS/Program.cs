using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DFS
{
    public class Query
    {
        public int tipe, nodeA, nodeB;

        public Query(int t, int A, int B)
        {
            tipe = t;
            nodeA = A;
            nodeB = B;
        }

        public void Print()
        {
            Console.WriteLine(tipe + " " + nodeA + " " + nodeB);
        }
    }

    class Program
    {
        /*this program is use zero indexing*/

        //Number of node
        static int N;
        //Number of Query
        static int Q;
        //Graph representation
        static List<List<int>> Adj;
        //ancestor of a node
        static int[] ancestor;
        //visited variables
        static bool[] visited;

        static void getInput()
        {
            N = Convert.ToInt32(Console.ReadLine());
            Adj = new List<List<int>>();
            for (int i = 0; i < N; i++)
            {
                List<int> tmp = new List<int>();
                Adj.Add(tmp);
            }
            ancestor = new int[N];
            visited = new bool[N];
            for (int i = 0; i < N; i++)
            {
                ancestor[i] = -1;
                visited[i] = false;
            }
            for (int i = 0; i < N-1; i++)
            {
                int a, b;
                string[] x = Console.ReadLine().Split(' ');
                a = Convert.ToInt32(x[0]);
                b = Convert.ToInt32(x[1]);
                a--; b--;
                Adj[a].Add(b);
                Adj[b].Add(a);
            }
        }

        static void generate(int node)
        {
            visited[node] = true;
            for (int i = 0; i < Adj[node].Count(); i++)
            {
                int nxt = Adj[node][i];
                if (!visited[nxt])
                {
                    ancestor[nxt] = node;
                    generate(nxt);
                }
            }
        }

        static bool Answer(int t, int goal, int from)
        {
            int x, y;
            if (t == 0){
                x = from; y = goal;
            }
            else{
                x = goal; y = from;
            }
            while (x != 0)
            {
                if (x == y) return true;
                x = ancestor[x];
            }
            if (x == y) return true;
            else return false;
        }


        static void Main(string[] args)
        {
            getInput();
            generate(0);
            /*for (int i = 0; i < N; i++)
            {
                Console.WriteLine(i + " " + ancestor[i]);
            }*/
            int Q = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < Q; i++)
            {
                string[] inp = Console.ReadLine().Split(' ');
                int t, a, b;
                t = Convert.ToInt32(inp[0]);
                a = Convert.ToInt32(inp[1]);
                b = Convert.ToInt32(inp[2]);
                a--; b--;
                if (Answer(t, a, b))
                    Console.WriteLine("Ya");
                else
                    Console.WriteLine("Tidak");
                //Console.WriteLine(t + " " + a + " " + b);
            }
        }
    }
}
