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

namespace AptekaTestDesktop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      
        private Canvas can;
        int count = 0;

        public MainWindow ()
        {
            InitializeComponent();
            AllProducts.Background = new SolidColorBrush(Colors.Yellow);
        }

        private void ButtonAddProduct_Click (object sender, RoutedEventArgs e)
        { 
            AddProduct windowAddProduct = new AddProduct();
            windowAddProduct.Owner = this;  //Задали отцовское окно для дочернего
            windowAddProduct.ShowDialog();  //Отобрразить как диалоговое окно
        }

        public void AddProduct (string nameProduct)
        {
            AllProducts.RowDefinitions.Add(new RowDefinition());
            can = new Canvas();
            Label number = new Label();
            can.Children.Add(number);
            number.Content = count + 1;   //Поменять номер
            Canvas.SetTop(number,11);
            number.Height = 54;
            number.Width = 32;
            number.HorizontalContentAlignment = HorizontalAlignment.Center;
            number.VerticalContentAlignment = VerticalAlignment.Center;

            Grid.SetColumn(can, 0);
            Grid.SetRow(can, count);
            AllProducts.Children.Add(can);
            AllProducts.RowDefinitions[count].Height = new GridLength(75);
            count++; 
        }
    }
}
