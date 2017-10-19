using System.Windows;
using System.Windows.Input;
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
            Close();
        }

        private void ButtonOk_Click (object sender, RoutedEventArgs e)
        {
            if (NameProduct.Text.ToString().Length != 0)
            {
                
                Visibility = Visibility.Hidden; //Скрыть
                MainWindow main = Owner as MainWindow; //Родитель для AddProduct это MainWindow?
                if (main != null)
                {                     
                    string strNameProduct = NameProduct.Text;
                    main.AddProduct(strNameProduct);
                }
                Close();

            }
            else MessageBox.Show("Это окно временное, потом тут будет ошибка!");
        }

        private void NameProduct_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string validChar = @"([0-9а-я])|[А-Я]|[.]";           //Цифры, русские буквы и символ '.'
            if (!Regex.IsMatch(e.Text.ToString(), validChar))
                e.Handled = true;
        }
    }
}
