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
        Boolean cekFileMap = false;
        Boolean cekQuery = false;

        public Page3(List<List<int>> Adj, int N, bool[] visited, int[] ancestor, Boolean boolMap)
        {
            InitializeComponent();

            map.Adj = Adj;
            map.N = N;
            map.visited = visited;
            map.ancestor = ancestor;

            cekFileMap = boolMap;
        }

        //buat input query secara manual dengan cara diubah ke tes.txt baru dibaca
        private void Button_Click_3(object sender, RoutedEventArgs e) 
        {
            File.WriteAllText("tes.txt", TxtBox.Text); //inputan manual diubah ke dalam bentuk tes.txt

            map.getInputQuery("F:\\Hide-And-Seek-Problem\\WpfApp2\\WpfApp2\\bin\\Debug\\tes.txt");

            MessageBox.Show("Input Generated", "Info", MessageBoxButton.OK);

        }

        private void Button_Click_4(object sender, RoutedEventArgs e) //buat run
        {
            if(!cekFileMap)
            {
                MessageBox.Show("You haven't upload the map file", "Map File", MessageBoxButton.OK);
            }
            if(!cekQuery)
            {
                MessageBox.Show("Your query is error", "Query", MessageBoxButton.OK);
            }
            if(cekFileMap && cekQuery)
            {
                Page2 solution_page = new Page2(map.Adj, map.query, map.N, map.Q, map.visited, map.ancestor);
                ((MainWindow)Application.Current.MainWindow).Content = solution_page;
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Page1 chooseFile = new Page1();
            ((MainWindow)Application.Current.MainWindow).Content = chooseFile;
        }
    }
}
