using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Canvas canvas;
        DFS graf;
        Tuple<double,double>[] historyDraw;
        int[] painted;
        int[] NumberChild;
        int[] ordered;
        int order;
        bool foundSolution;
        Queue<int>[] waitingTime;

        public MainWindow()
        {
            InitializeComponent();
            canvas = new Canvas();
            graf = new DFS();
            this.Content = canvas;
            double x = 10;
            double y = 10;

            //directory sesuaikan!!!
            graf.getInputGraph("H:\\small.txt");
            //directory sesuaikan!!!
            graf.getInputQuery("H:\\query.txt");
            historyDraw = new Tuple<double, double>[graf.N];
            historyDraw[0] = Tuple.Create(x, y);
            CreateEllipse(canvas, 1, x, y, 0);
            painted = new int[graf.Q];
            NumberChild = new int[graf.N];
            ordered = new int[graf.N];
            order = 0;
            DrawNode(0, x+40, y);
            Solve();
        }

        void CreateEllipse(Canvas canvas, int num_node, double desiredLeft, double desiredTop, int tipe)
        {
            // Inisialisasi elemen2 graf
            Grid element = new Grid();
            Ellipse myEllipse = new Ellipse();
            TextBlock num = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Text = num_node.ToString(),
                Foreground = Brushes.White,
                FontSize = 10
            };

            // Setting tampilan graf
            if (tipe == 0)
            {
                myEllipse.Fill = Brushes.Blue;
            }
            else
            {
                myEllipse.Fill = Brushes.Red;
            }
            myEllipse.Width = 20;
            myEllipse.Height = 20;

            // Gambar elemen pada canvas
            element.Children.Add(myEllipse);
            element.Children.Add(num);
            canvas.Children.Add(element);
            Canvas.SetLeft(element, desiredLeft);
            Canvas.SetTop(element, desiredTop);
        }

        public void DrawNode(int node, double x, double y)
        {
            int tchild = 0;
            graf.visited[node] = true;
            double tempy = y;
            double parentx = x;
            double parenty = y;
            //Point P;

            for (int i = 0; i < graf.Adj[node].Count(); i++)
            {
                int nxt = graf.Adj[node][i];
                double tempx = x;

                if (!graf.visited[nxt])
                {
                    graf.ancestor[nxt] = node;
                    foreach (Grid temp in canvas.Children.OfType<Grid>())
                    {
                        double xx = Canvas.GetLeft(temp);
                        double yy = Canvas.GetTop(temp);

                        if (xx == tempx && yy == tempy)
                        {
                            tempy += 30;
                        }
                    }
                    tempx += 40;
                    DrawLine(canvas, parentx - 20, parenty + 10, tempx - 30, tempy + 10);
                    CreateEllipse(canvas, nxt + 1, tempx - 40, tempy, 0);
                    historyDraw[nxt] = Tuple.Create(tempx - 40, tempy);
                    DrawNode(nxt, tempx, tempy);
                    tchild += NumberChild[nxt];
                    tempy += 30;
                }
            }
            NumberChild[node] = tchild;
        }
        
        public void DrawLine(Canvas myCanvas,double xs, double ys, double xe, double ye)
        {
            Line line = new Line();
            line.Stroke = Brushes.Black;

            line.X1 = xs;
            line.X2 = xe;
            line.Y1 = ys;
            line.Y2 = ye;

            line.StrokeThickness = 1;
            myCanvas.Children.Add(line);
        }

        public void find_time(int from)
        {
            graf.visited[from] = true;
            ordered[from] = order; 
            order++;
            for (int i = 0; i < graf.Adj[from].Count(); i++)
            {
                int nxt = graf.Adj[from][i];
                if (!graf.visited[nxt] && nxt != graf.ancestor[from])
                {
                    find_time(nxt);
                    int prv = 0;
                    prv = ordered[from];
                    waitingTime[from].Enqueue(order-prv);
                    ordered[from] = order;
                    order++;
                }
            }
        }

        public async void find_path_query_one(int from, int goal)
        {
            if (foundSolution) return;
            graf.visited[from] = true;
            CreateEllipse(canvas, from + 1, historyDraw[from].Item1, historyDraw[from].Item2, 1);
            await Task.Delay(1000);
            if (from == goal)
            {
                foundSolution = true;
                return;
            }
            else {
                for (int i = 0; i < graf.Adj[from].Count(); i++)
                {
                    int nxt = graf.Adj[from][i];
                    if (!graf.visited[nxt] && nxt != graf.ancestor[from])
                    {
                        find_path_query_one(nxt, goal);
                        int waited = waitingTime[from].Dequeue();
                        await Task.Delay(1000*(waited));
                    }
                }
                if (!foundSolution)
                {
                    CreateEllipse(canvas, from + 1, historyDraw[from].Item1, historyDraw[from].Item2, 0);
                    await Task.Delay(1000);
                }
            }
        }

        public async void Find_Path(Tuple<int,int,int> q)
        {
            
            int t = q.Item1;
            int x, y;
            if (t == 0){
                x = q.Item3;
                y = q.Item2;
                bool found = false;
                while (x != 0 && !found)
                {

                    CreateEllipse(canvas, x + 1, historyDraw[x].Item1, historyDraw[x].Item2, 1);
                    if (x == y)
                    {
                        found = true;
                    }
                    else
                    {
                        x = graf.ancestor[x];
                    }
                    await Task.Delay(1000);
                }
                if (!found)
                {

                    CreateEllipse(canvas, x + 1, historyDraw[x].Item1, historyDraw[x].Item2, 1);
                    if (x == y)
                    {
                        found = true;
                    }
                    await Task.Delay(1000);
                }
            }
            else {
                x = q.Item2;
                y = q.Item3;
                foundSolution = false;
                for (int i = 0; i < graf.N; i++)
                    graf.visited[i] = false;
                find_path_query_one(y, x);
            }
            
        }

        public void Reset_Path_Query_One(int from,int goal)
        {
            if (foundSolution) return;
            graf.visited[from] = true;
            CreateEllipse(canvas, from + 1, historyDraw[from].Item1, historyDraw[from].Item2, 0);
            if (from == goal)
            {
                foundSolution = true;
                return;
            }
            else
            {
                for (int i = 0; i < graf.Adj[from].Count(); i++)
                {
                    int nxt = graf.Adj[from][i];
                    if (!graf.visited[nxt] && nxt != graf.ancestor[from])
                    {
                        Reset_Path_Query_One(nxt, goal);
                    }
                }
            }
        }

        public void Reset_Path(Tuple<int, int, int> q)
        {

            int t = q.Item1;
            int x, y;
            if (t == 0)
            {
                x = q.Item3;
                y = q.Item2;
                bool found = false;
                while (x != 0 && !found)
                {
                    CreateEllipse(canvas, x + 1, historyDraw[x].Item1, historyDraw[x].Item2, 0);
                    if (x == y)
                    {
                        found = true;
                    }
                    else
                    {
                        x = graf.ancestor[x];
                    }
                }
                if (!found)
                {
                    CreateEllipse(canvas, x + 1, historyDraw[x].Item1, historyDraw[x].Item2, 0);
                    if (x == y)
                    {
                        found = true;
                    }
                }
            }
            else
            {
                x = q.Item2;
                y = q.Item3;
                for (int i = 0; i < graf.N; i++)
                    graf.visited[i] = false;
                foundSolution = false;
                Reset_Path_Query_One(y, x);
            }
        }
        
        public void num_sol_query_one(int from,int goal, int idx)
        {
            if (foundSolution) return;
            graf.visited[from] = true;
            painted[idx]++;
            if (from == goal)
            {
                foundSolution = true;
                return;
            }
            else
            {
                for (int i = 0; i < graf.Adj[from].Count(); i++)
                {
                    int nxt = graf.Adj[from][i];
                    if (!graf.visited[nxt] && nxt != graf.ancestor[from])
                    {
                        num_sol_query_one(nxt, goal, idx);
                        int waited = waitingTime[from].Dequeue();
                       // painted[idx]++;
                    }
                }
                if (!foundSolution)
                {
                    painted[idx]++;
                }
            }
        }

        public void num_sol(Tuple<int, int, int> q,int idx)
        {

            int t = q.Item1;
            int x, y;
            if (t == 0)
            {
                x = q.Item3;
                y = q.Item2;
                bool found = false;
                while (x != 0 && !found)
                {
                    painted[idx]++;
                    if (x == y)
                    {
                        found = true;
                    }
                    else
                    {
                        x = graf.ancestor[x];
                    }
                }
                if (!found)
                {
                    painted[idx]++;
                    if (x == y)
                    {
                        found = true;
                    }
                }
            }
            else
            {
                x = q.Item2;
                y = q.Item3;

                for (int i = 0; i < graf.N; i++)
                    graf.visited[i] = false;
                order = 1;
                waitingTime = new Queue<int>[graf.N];
                for (int i = 0; i < graf.N; i++)
                    waitingTime[i] = new Queue<int>();
                find_time(y);

                for (int i = 0; i < graf.N; i++)
                    graf.visited[i] = false;
                foundSolution = false;
                num_sol_query_one(y, x, idx);

                for (int i = 0; i < graf.N; i++)
                    graf.visited[i] = false;
                foundSolution = false;
                order = 1;
                for (int i = 0; i < graf.N; i++)
                    waitingTime[i] = new Queue<int>();
                find_time(y);
            }
        }

        public async void Solve()
        {

            for (int i = 0; i < graf.Q; i++)
            {
                num_sol(graf.query[i], i);
                Find_Path(graf.query[i]);
                await Task.Delay(1000 * (painted[i]));
                Reset_Path(graf.query[i]);
            }
        }

    }
}
