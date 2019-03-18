using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class DFS
    {
        //Number of node
        public int N;
        //Number of Query
        public int Q;
        //Graph representation
        public List<List<int>> Adj;
        //ancestor of a node
        public int[] ancestor;
        //visited variables
        public bool[] visited;
        //query
        public Tuple<int, int, int>[] query;
        //stack
        public Stack<int> st;

        public void getInputGraph(string s)
        {
            string[] lines = System.IO.File.ReadAllLines(s);
            int cnt = 0;
            foreach (string line in lines)
            {
                if (cnt == 0)
                {
                    cnt++;
                    N = Convert.ToInt32(line);
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
                }
                else if (cnt < N)
                {
                    string[] inp = line.Split(' ');
                    int a, b;
                    a = Convert.ToInt32(inp[0]);
                    b = Convert.ToInt32(inp[1]);
                    a--; b--;
                    Adj[a].Add(b);
                    Adj[b].Add(a);
                    cnt++;
                }
            }
        }

        public void getInputQuery(string s)
        {
            string[] lines = System.IO.File.ReadAllLines(s);
            int cnt = 0;
            int qcnt = 0;
            foreach(string line in lines)
            {
                if (cnt == 0)
                {
                    Q = Convert.ToInt32(line);
                    cnt++;
                    query = new Tuple<int, int, int>[Q];
                }
                else
                {
                    string[] inp = line.Split(' '); 
                    int t, a, b;
                    t = Convert.ToInt32(inp[0]);
                    a = Convert.ToInt32(inp[1]);
                    b = Convert.ToInt32(inp[2]);
                    a--; b--;
                    query[qcnt] = Tuple.Create(t, a, b);
                    qcnt++;
                }
            }
        }
        public void generate(int node)
        {
            st = new Stack<int>();
            st.Push(node);
            visited[node] = true;
            while (st.Count() != 0)
            {
                int now = st.Peek();
                st.Pop();
                for (int i = 0; i < Adj[now].Count(); i++)
                {
                    int nxt = Adj[now][i];
                    if (!visited[nxt])
                    {
                        ancestor[nxt] = now;
                        st.Push(nxt);
                        visited[nxt] = true;
                    }
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
