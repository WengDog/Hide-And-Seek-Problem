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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        DFS graf;
        Tuple<double, double>[] historyDraw;
        List<int> Path_Answer;
        int[] painted;
        bool foundSolution;

        public Page2(List<List<int>> Adj, Tuple<int, int, int>[] query, int N, int Q, bool[] visited, int[] ancestor)
        {

            InitializeComponent();

            // inisialisasi map dari page sebelumnya
            graf = new DFS();
            graf.Adj = Adj;
            graf.query = query;
            graf.N = N;
            graf.Q = Q;
            graf.visited = visited;
            graf.ancestor = ancestor;

            double x = 10;
            double y = 10;

            historyDraw = new Tuple<double, double>[graf.N];
            historyDraw[0] = Tuple.Create(x, y);
            CreateEllipse(graf_canvas, 1, x, y, 0);
            painted = new int[graf.Q];
            DrawNode(0, x + 40, y);
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
                    foreach (Grid temp in graf_canvas.Children.OfType<Grid>())
                    {
                        double xx = Canvas.GetLeft(temp);
                        double yy = Canvas.GetTop(temp);

                        if (xx == tempx && yy == tempy)
                        {
                            tempy += 30;
                        }
                    }
                    tempx += 40;
                    DrawLine(graf_canvas, parentx - 20, parenty + 10, tempx - 30, tempy + 10);
                    CreateEllipse(graf_canvas, nxt + 1, tempx - 40, tempy, 0);
                    historyDraw[nxt] = Tuple.Create(tempx - 40, tempy);
                    DrawNode(nxt, tempx, tempy);
                    tempy += 30;
                }
            }
        }

        public void DrawLine(Canvas myCanvas, double xs, double ys, double xe, double ye)
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

        public async void Find_Path()
        {
            for (int i = 0; i < Path_Answer.Count(); i++)
            {
                int x = Path_Answer[i];
                CreateEllipse(graf_canvas, x + 1, historyDraw[x].Item1, historyDraw[x].Item2, 1);
                await Task.Delay(1000);
            }
        }

        public void Reset_Path()
        {

            for (int i = 0; i < Path_Answer.Count(); i++)
            {
                int x = Path_Answer[i];
                CreateEllipse(graf_canvas, x + 1, historyDraw[x].Item1, historyDraw[x].Item2, 0);
            }
        }

        public void num_sol(Tuple<int, int, int> q, int idx)
        {

            int t, a, b, x, y;
            t = q.Item1; a = q.Item2; b = q.Item3;
            if (!graf.Answer(t, a, b))
            {
                foundSolution = false;
            }
            else
            {
                foundSolution = true;
                if (t == 0)
                {
                    x = b; y = a;
                }
                else
                {
                    x = a; y = b;
                }
                bool found = false;
                while (x != 0 && !found)
                {
                    painted[idx]++;
                    Path_Answer.Add(x);
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
                        Path_Answer.Add(x);
                        found = true;
                    }
                }
                if (t == 1)
                    Path_Answer.Reverse();
            }

        }

        public async void Solve()
        {
            for (int i = 0; i < graf.Q; i++)
            {
                Path_Answer = new List<int>();
                num_sol(graf.query[i], i);
                Find_Path();
                await Task.Delay(1000 * painted[i]);
                Reset_Path();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            for(int i = 0; i < graf.Q; i++)
            {
                int t = graf.query[i].Item1;
                int a = graf.query[i].Item2;
                int b = graf.query[i].Item3;

                if (graf.Answer(t, a, b))
                    TxtBox.Text = "Dengan query " + t + " " + a + " " + b + " Jose berhasil menemukan Ferdiant.";
                else
                    TxtBox.Text = "Dengan query " + t + " " + a + " " + b + " Jose tidak berhasil menemukan Ferdiant.";
                TxtBox.Text = "\n";
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e) //back
        {
            newWindow baru = new newWindow();
            ((MainWindow)Application.Current.MainWindow).Content = baru;
        }
    }
}
