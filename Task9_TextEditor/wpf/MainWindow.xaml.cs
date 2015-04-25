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
using TextEditorBL;

namespace wpf
{

    public partial class MainWindow : Window
    {
        TextEditor editor;
        bool flag =false;
        
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "(*.txt)|*.txt";
            bool? oppened = file.ShowDialog();
            if (oppened == true)
            {
                tbxText.IsEnabled = true;
                editor = new TextEditor(file.FileName);
                tbxText.Text = editor.Text;
                flag = true;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (editor != null)
                editor.Close();
            Application.Current.Shutdown();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!flag)
                return;
            if (e.Changes.ElementAt(0).AddedLength != 0)
            {
                string newString = tbxText.Text.Substring(e.Changes.ElementAt(0).Offset, e.Changes.ElementAt(0).AddedLength);
                editor.AddingText(e.Changes.ElementAt(0).Offset, e.Changes.ElementAt(0).AddedLength,newString);
            }
            else if (e.Changes.ElementAt(0).RemovedLength != 0)
                  editor.RemovingText(e.Changes.ElementAt(0).Offset, e.Changes.ElementAt(0).RemovedLength);
            
        }

       
    } 
}
