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
using MathObjects;

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
            string[] stringArray = tbxCoefficients.Text.Split(',');
            int[] intArray = stringArray.Select(i => int.Parse(i)).ToArray();
            Polynome m = new Polynome(intArray);
            tbxResult.Text = m.CalculatePolynome(double.Parse(tbxX.Text)).ToString();
            //TODO: 
            //Polynome m1 =  new Polynome(new int[4] { 1, 20, 30, 40 });
            //Polynome m2 =  new Polynome(new int[4] { 1, 20, 30, 40 });
            


            //if (m1 == m2)
            //    tbxResult.Text = "m1 != m2";
            ////else 
            ////    tbxResult.Text = "m1 == m2";
            //MathObjects.Vector m0 = new MathObjects.Vector(new int[4] { 0, 0, 0, 4 });
           
            //m0.Coordinate = new int[3] { 1, 20, 30 };
            //MathObjects.Vector m4 = new MathObjects.Vector(new int[5] { 5, 5, 5, 5, 5 });
            //m0.Coordinate = m4.Coordinate;
        }
    }
}
