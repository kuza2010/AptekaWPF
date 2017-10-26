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
using System.IO;

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
            NameProduct.Focus();
        }
        private string path = @"C:\Users\Артём\Desktop\АРТЕМ\НГТУ\5семестр\ООП\Проект АПТЕКА\AptekaTestDesktop\Apteka.txt"; //расположение файла

        //Кнопка отмены
        private void ButtonCancle_Click (object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Кнопка окей
        private void ButtonOk_Click (object sender, RoutedEventArgs e)
        {
            //если есть наименование
            if (NameProduct.Text.ToString().Length != 0)
            {
                //Скрыть
                this.Visibility = Visibility.Hidden;

                //Родитель для AddProduct это MainWindow?
                MainWindow main = this.Owner as MainWindow;
                if (main != null)
                {
                    string strNameProduct = NameProduct.Text;
                    using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                    {

                        sw.WriteLine(strNameProduct + "=0"); //запись в файл товара с нулевым количеством
                    }
                    main.AddProduct(strNameProduct);
                }
                this.Close();
            }
            else
            {
                Warning windowWarning = new Warning("Наименование товара не может быть пустым!");
                windowWarning.Owner = this;  //Задали отцовское окно для дочернего
                windowWarning.ShowDialog();  //Отобрразить как диалоговое окно
            }
        }

        //Валидация входа
        private void NameProduct_PreviewTextInput (object sender, TextCompositionEventArgs e)
        {
            string validChar = @"([0-9а-я])|[А-Я]|[.]";           //Цифры, русские буквы и символ '.'
            if (!Regex.IsMatch(e.Text.ToString(), validChar))
                e.Handled = true;
        }
  
    }
}
