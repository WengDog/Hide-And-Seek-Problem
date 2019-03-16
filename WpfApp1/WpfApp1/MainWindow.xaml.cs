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
        //List posisi graf
        private List<Point> GrafPos;

        public MainWindow()
        {
            InitializeComponent();
            canvas = new Canvas();
            this.Content = canvas;
            double x = 10;
            double y = 10;


            CreateEllipse(canvas,1, x, y);
            InitializeNode();
            DrawNode(0, x+40, y);

        }

        void CreateEllipse(Canvas canvas, int num_node, double desiredLeft, double desiredTop)
        {
            // Create a red Ellipse.
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

            myEllipse.Fill = Brushes.Blue;

            // Set the width and height of the Ellipse.
            myEllipse.Width = 20;
            myEllipse.Height = 20;

            element.Children.Add(myEllipse);
            element.Children.Add(num);
            canvas.Children.Add(element);
            Canvas.SetLeft(element, desiredLeft);
            Canvas.SetTop(element, desiredTop);
        }

        public void InitializeNode()
        {
            N = 10;
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

            Adj[0].Add(1);
            Adj[1].Add(0);
            Adj[1].Add(2);
            Adj[2].Add(1);
            Adj[2].Add(3);
            Adj[3].Add(2);
            Adj[2].Add(4);
            Adj[4].Add(2);
            Adj[1].Add(5);
            Adj[5].Add(1);
            Adj[5].Add(6);
            Adj[6].Add(5);
            Adj[1].Add(7);
            Adj[7].Add(1);
            Adj[7].Add(8);
            Adj[8].Add(7);
            Adj[6].Add(9);
            Adj[9].Add(6);
        }

        public void DrawNode(int node,double x, double y)
        {
            visited[node] = true;
            double tempy = y;
            double parentx = x;
            double parenty = y;
            //Point P;

            for (int i = 0; i < Adj[node].Count(); i++)
            {
                int nxt = Adj[node][i];
                double tempx = x;

                if (!visited[nxt])
                {
                    ancestor[nxt] = node;
                    foreach (Grid temp in canvas.Children.OfType<Grid>())
                    {
                        double xx = Canvas.GetLeft(temp);
                        double yy = Canvas.GetTop(temp);

                        if(xx == tempx && yy == tempy)
                        {
                            tempy += 30;
                        }
                    }
                    //P = new Point(tempx, tempy);
                    //GrafPos.Add(P);
                    tempx += 40;
                    DrawLine(canvas, parentx - 20, parenty + 10 , tempx - 30, tempy + 10);
                    CreateEllipse(canvas,nxt+1, tempx-40, tempy);
                    DrawNode(nxt,tempx,tempy);
                    tempy += 30;
                }
            }
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

        public bool Answer(int t, int goal, int from)
        {
            int x, y;
            if (t == 0)
            {
                x = goal; y = from;
            }
            else
            {
                x = from; y = goal;
            }
            while (x != 0)
            {
                if (x == y) return true;
                x = ancestor[x];
            }
            if (x == y) return true;
            else return false;
        }

    }
}
