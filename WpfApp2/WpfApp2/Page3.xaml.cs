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
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        DFS map = new DFS();

        public Page3(List<List<int>> Adj)
        {
            InitializeComponent();

            map.Adj = Adj;
        }

        //buat input query secara manual dengan cara diubah ke tes.txt baru dibaca
        private void Button_Click_3(object sender, RoutedEventArgs e) 
        {
            File.WriteAllText("tes.txt", TxtBox.Text); //inputan manual diubah ke dalam bentuk tes.txt

            map.getInputQuery("C:\\Users\\user\\Desktop\\WpfApp2\\WpfApp2\\bin\\tes.txt");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

            Page2 solution_page = new Page2(map.Adj,map.query);
            ((MainWindow)Application.Current.MainWindow).Content = solution_page;
        }
    }
}
