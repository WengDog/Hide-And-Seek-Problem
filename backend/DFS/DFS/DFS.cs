using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFS
{
    class DFS
    {
        //Number of node
        private int N;
        //Number of Query
        private int Q;
        //Graph representation
        private List<List<int>> Adj;
        //ancestor of a node
        private int[] ancestor;
        //visited variables
        private bool[] visited;
        //query
        private Tuple<int, int, int>[] query;
        

        public void getInput()
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
            for (int i = 0; i < N - 1; i++)
            {
                int a, b;
                string[] x = Console.ReadLine().Split(' ');
                a = Convert.ToInt32(x[0]);
                b = Convert.ToInt32(x[1]);
                a--; b--;
                Adj[a].Add(b);
                Adj[b].Add(a);
            }
            Q = Convert.ToInt32(Console.ReadLine());
            query = new Tuple<int, int, int>[Q];
            for (int i = 0; i < Q; i++)
            {
                string[] inp = Console.ReadLine().Split(' ');
                int t, a, b;
                t = Convert.ToInt32(inp[0]);
                a = Convert.ToInt32(inp[1]);
                b = Convert.ToInt32(inp[2]);
                a--; b--;
                query[i] = Tuple.Create(t, a, b);
            }

        }

        public void generate(int node)
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

        public bool Answer(int t, int goal, int from)
        {
            int x, y;
            if (t == 0)
            {
                x = from; y = goal;
            }
            else
            {
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

        public void Solve()
        {
            generate(0);
            for (int i = 0; i < Q; i++)
            {
                int t = query[i].Item1;
                int a = query[i].Item2;
                int b = query[i].Item3;
                if (Answer(t, a, b))
                    Console.WriteLine("YA");
                else
                    Console.WriteLine("TIDAK");
            }
        }
    }
}
