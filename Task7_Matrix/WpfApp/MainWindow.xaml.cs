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
using MatrixSpace;

namespace WpfApp
{
  
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MatrixSpace.Matrix m;
        string s = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
           m = new MatrixSpace.Matrix(5);
            m.Create();
            for (int i = 0; i < 5; i++)
            {
                s += "\n ";
                for (int j = 0; j < 5; j++)
                    s += m[i, j] + " ";
            }
            tbxText.Text = s;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            m = m.Inverse();
            s += "\n";
            for (int i = 0; i < 5; i++)
            {
                s += "\n ";
                for (int j = 0; j < 5; j++)
                    s += m[i, j] + " ";
            }
            tbxText.Text = s;
        }
    }
}
