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

        // main program pada page 1
        public Page1()
        {
            InitializeComponent();
        }

        // Procedur untuk untuk memasukkan input peta dari file eksternal jika tombol upload map di klik
        public void Button_Click(object sender, RoutedEventArgs e) 
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

        // Procedur untuk untuk memasukkan input query dari file eksternal jika tombol upload query di klik
        public void Button_Click_1(object sender, RoutedEventArgs e)
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

        // Procedur untuk mengalihkan ke page 3 jika tombol masukan input secara manual di klik
        public void Button_Click_2(object sender, RoutedEventArgs e) 
        {
            Page3 Input_Query = new Page3(map.Adj,map.N,map.visited,map.ancestor,boolMap);
            ((MainWindow)Application.Current.MainWindow).Content = Input_Query;
        }

        // Procedur untuk pergi ke page 2 untuk ditampilkan jawabannya ketika tombol answer di klik
        public void Button_Click_3(object sender, RoutedEventArgs e) //button jawaban
        {
            // cek apakah map sudah di input
            if (!boolMap)
            {
                MessageBox.Show("You haven't upload the map file", "Map File", MessageBoxButton.OK);
            }
            // cek apakah query sudah diinput
            if (!boolQuery)
            {
                MessageBox.Show("You haven't upload the query file", "Query", MessageBoxButton.OK);
            }
            // cek apakah keduanya sudah di input
            if (boolMap && boolQuery)
            {
                Page2 solution_page = new Page2(map.Adj, map.query, map.N, map.Q, map.visited, map.ancestor);
                ((MainWindow)Application.Current.MainWindow).Content = solution_page;
            }
        }

        // Procedur untuk kembali ke window utama ketika tombol back di klik
        private void Button_Click_4(object sender, RoutedEventArgs e) //back
        {
            newWindow baru = new newWindow();
            ((MainWindow)Application.Current.MainWindow).Content = baru;
        }
    }
}
