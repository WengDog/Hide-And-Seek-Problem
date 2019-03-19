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
using Microsoft.Win32;
using System.IO;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        DFS map = new DFS();
        public Boolean boolMap = false;
        public Boolean boolQuery = false;

        public Page1()
        {
            InitializeComponent();
        }

        public void Button_Click(object sender, RoutedEventArgs e) //buat masukin peta
        {
            OpenFileDialog file = new OpenFileDialog();
            file.DefaultExt = ".txt";
            file.Filter = "Text Document (.txt)|*.txt";

            file.ShowDialog();

            if (file.FileName != "")
            {
                map.getInputGraph(file.FileName);
                boolMap = true;
            }
        }

        public void Button_Click_1(object sender, RoutedEventArgs e) //buat query dari file eksternal
        {
            OpenFileDialog fileQuery = new OpenFileDialog();
            fileQuery.DefaultExt = ".txt";
            fileQuery.Filter = "Text Document (.txt)|*.txt";

            fileQuery.ShowDialog();

            if (fileQuery.FileName != "")
            {
                map.getInputQuery(fileQuery.FileName);
                boolQuery = true;
            }
        }

        public void Button_Click_2(object sender, RoutedEventArgs e) //button query manual
        {
            Page3 Input_Query = new Page3(map.Adj,map.N,map.visited,map.ancestor,boolMap);
            ((MainWindow)Application.Current.MainWindow).Content = Input_Query;
        }

        public void Button_Click_3(object sender, RoutedEventArgs e) //button jawaban
        {
            if (!boolMap)
            {
                MessageBox.Show("You haven't upload the map file", "Map File", MessageBoxButton.OK);
            }
            if (!boolQuery)
            {
                MessageBox.Show("You haven't upload the query file", "Query", MessageBoxButton.OK);
            }
            if (boolMap && boolQuery)
            {
                Page2 solution_page = new Page2(map.Adj, map.query, map.N, map.Q, map.visited, map.ancestor);
                ((MainWindow)Application.Current.MainWindow).Content = solution_page;
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e) //back
        {
            newWindow baru = new newWindow();
            ((MainWindow)Application.Current.MainWindow).Content = baru;
        }
    }
}
