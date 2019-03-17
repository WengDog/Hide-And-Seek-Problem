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

            map.getInputGraph(file.FileName);
        }

        public void Button_Click_1(object sender, RoutedEventArgs e) //buat query dari file eksternal
        {
            OpenFileDialog fileQuery = new OpenFileDialog();
            fileQuery.DefaultExt = ".txt";
            fileQuery.Filter = "Text Document (.txt)|*.txt";

            fileQuery.ShowDialog();

            map.getInputQuery(fileQuery.FileName);
        }

        public void Button_Click_2(object sender, RoutedEventArgs e) 
        {
            Page3 Input_Query = new Page3(map.Adj);
            ((MainWindow)Application.Current.MainWindow).Content = Input_Query;
        }

        public void Button_Click_3(object sender, RoutedEventArgs e) //button jawaban
        {

            Page2 solution_page = new Page2(map.Adj,map.query);
            ((MainWindow)Application.Current.MainWindow).Content = solution_page;
        }
    }
}
