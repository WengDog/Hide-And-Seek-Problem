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
        

        public MainWindow()
        {
            InitializeComponent();
            canvas = new Canvas();
            graf = new DFS();
            this.Content = canvas;
            double x = 10;
            double y = 10;


            CreateEllipse(canvas,1, x, y);
            graf.getInput("C:\\Users\\user\\source\\repos\\WpfApp1\\WpfApp1\\testing.txt");
            DrawNode(0, x+40, y);

        }

        void CreateEllipse(Canvas canvas, int num_node, double desiredLeft, double desiredTop)
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
            myEllipse.Fill = Brushes.Blue;
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
                    foreach (Grid temp in canvas.Children.OfType<Grid>())
                    {
                        double xx = Canvas.GetLeft(temp);
                        double yy = Canvas.GetTop(temp);

                        if (xx == tempx && yy == tempy)
                        {
                            tempy += 30;
                        }
                    }
                    //P = new Point(tempx, tempy);
                    //GrafPos.Add(P);
                    tempx += 40;
                    DrawLine(canvas, parentx - 20, parenty + 10, tempx - 30, tempy + 10);
                    CreateEllipse(canvas, nxt + 1, tempx - 40, tempy);
                    DrawNode(nxt, tempx, tempy);
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

    }
}
