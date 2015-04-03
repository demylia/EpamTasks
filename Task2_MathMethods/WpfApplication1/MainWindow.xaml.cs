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
using MathClass;



namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
           int n,d;
           double a;
           var number = tbxNumber1.Text;
           Int32.TryParse(number, out n);
           var degree = tbxDegree.Text;
           Int32.TryParse(degree, out d);
           var eps = tbxAccuracy.Text.Replace(".",",");
           Double.TryParse(eps, out a);
          
           tbxResult1.Text = Calculate.CalculateRoot(n,d,a).ToString();
         }

        private void ConvertString_Click(object sender, RoutedEventArgs e)
        {
            int x;
            var number = tbxNumber2.Text;
            Int32.TryParse(number, out x);

            tbxResult2.Text = Calculate.ConvertToBinary(x).ToString();
        }

       
    }
}
