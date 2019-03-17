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
        int[] painted;

        public Page2(List<List<int>> Adj, Tuple<int, int, int>[] query)
        {
            
            InitializeComponent();

            // inisialisasi map
            graf = new DFS();
            graf.Adj = Adj;
            graf.query = query;

            double x = 10;
            double y = 10;

            historyDraw = new Tuple<double, double>[graf.N];
            historyDraw[0] = Tuple.Create(x, y);
            CreateEllipse(graf_canvas, 1, x, y, 0);
            painted = new int[graf.Q];
            DrawNode(0, x + 40, y);
            Solve();
        }

        /*private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (graf.neff == 0)
            {
                //tidak ada jawaban
            }
            else
            {
                int i = 0;
                while (i < graf.neff)
                {
                    //jawaban per line
                }
            }
        }*/

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
            myEllipse.Width = 30;
            myEllipse.Height = 30;

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

        public async void Find_Path(Tuple<int, int, int> q)
        {

            int t = q.Item1;
            int x, y;
            if (t == 0)
            {
                x = q.Item3;
                y = q.Item2;
            }
            else
            {
                x = q.Item2;
                y = q.Item3;
            }
            bool found = false;
            while (x != 0 && !found)
            {

                CreateEllipse(graf_canvas, x + 1, historyDraw[x].Item1, historyDraw[x].Item2, 1);
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

                CreateEllipse(graf_canvas, x + 1, historyDraw[x].Item1, historyDraw[x].Item2, 1);
                if (x == y)
                {
                    found = true;
                }
                await Task.Delay(1000);
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
            }
            else
            {
                x = q.Item2;
                y = q.Item3;
            }
            bool found = false;
            while (x != 0 && !found)
            {
                CreateEllipse(graf_canvas, x + 1, historyDraw[x].Item1, historyDraw[x].Item2, 0);
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
                CreateEllipse(graf_canvas, x + 1, historyDraw[x].Item1, historyDraw[x].Item2, 0);
                if (x == y)
                {
                    found = true;
                }
            }

        }

        public void num_sol(Tuple<int, int, int> q, int idx)
        {

            int t = q.Item1;
            int x, y;
            if (t == 0)
            {
                x = q.Item3;
                y = q.Item2;
            }
            else
            {
                x = q.Item2;
                y = q.Item3;
            }
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

        public async void Solve()
        {

            for (int i = 0; i < graf.Q; i++)
            {
                num_sol(graf.query[i], i);
                Find_Path(graf.query[i]);
                await Task.Delay(1000 * painted[i]);
                Reset_Path(graf.query[i]);
            }
        }
    }
}
