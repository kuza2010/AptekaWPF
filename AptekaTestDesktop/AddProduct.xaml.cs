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
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace AptekaTestDesktop
{
    /// <summary>
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public AddProduct ()
        {
            InitializeComponent();
        }

        private void ButtonCancle_Click (object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonOk_Click (object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NameProduct_PreviewTextInput (object sender, TextCompositionEventArgs e)
        {
            string  validChar = @"([0-9а-я])|[А-Я]|[.]";           //Цифры, русские буквы и символ '.'
            if (!Regex.IsMatch(e.Text.ToString(), validChar))
                e.Handled = true; 
        }

       
    }
}
