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
        DFS tes = new DFS();

        public Page1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //buat masukin peta
        {
            OpenFileDialog file = new OpenFileDialog();
            file.DefaultExt = ".txt";
            file.Filter = "Text Document (.txt)|*.txt";

            file.ShowDialog();

            tes.getInputGraph(file.FileName);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //buat query dari file eksternal
        {
            OpenFileDialog fileQuery = new OpenFileDialog();
            fileQuery.DefaultExt = ".txt";
            fileQuery.Filter = "Text Document (.txt)|*.txt";

            fileQuery.ShowDialog();

            tes.getInputQuery(fileQuery.FileName);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //buat input query secara manual dengan cara diubah ke tes.txt baru dibaca
        {
            File.WriteAllText("tes.txt", TxtBox.Text); //inputan manual diubah ke dalam bentuk tes.txt

            tes.getInputQuery("F:/Hide-And-Seek-Problem/WpfApp2/WpfApp2/bin/Debug/tes.txt");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) //button jawaban
        {
            tes.Solve();

            Page2 pg2 = new Page2(tes.arrJawaban, tes.neff);
            ((MainWindow)Application.Current.MainWindow).Content = pg2;
        }
    }
}
