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
using CalcMethods;

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
            
            string[] stringArray = tbxNumber1.Text.Split(',');
            int[] intArray = stringArray.Select(i => int.Parse(i)).ToArray();
            tbxResult1.Text = CalculateBL.CalculateNOD(intArray).ToString();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int x, y;
            double t;
            var a = Int32.TryParse(tbxNumber2.Text, out x);
            a = Int32.TryParse(tbxNumber3.Text, out y);


            tbxResult2.Text = CalculateBL.CalculateNODAndTimeStain(x, y, out t).ToString();
            tbxResult3.Text = t.ToString() + "ms";
        }
    }
}
