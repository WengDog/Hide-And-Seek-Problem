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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // main program pada main window
        public MainWindow()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }

        // Fungsi tombol start pada menu awal
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // ketika tombol di klik akan masuk ke page 1
            Page1 milihFile = new Page1();
            ((MainWindow)Application.Current.MainWindow).Content = milihFile;
        }
    }
}
