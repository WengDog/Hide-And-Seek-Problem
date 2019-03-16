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
        string[] arrJawaban;
        int banyakJawaban = 0;

        public Page2(string[] jawaban, int neff)
        {
            InitializeComponent();
            
            for(int i = 0; i < neff; i++)
            {
                arrJawaban[i] = jawaban[i];
                banyakJawaban++;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) //untuk menampilkan jawaban dari query
        {
            int i = 0;
            while (i < banyakJawaban)
            {
                
            }
        }
    }
}
