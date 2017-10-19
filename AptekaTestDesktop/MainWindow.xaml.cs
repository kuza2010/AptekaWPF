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

        public void AddProduct (string pNameProduct)
        {
            RowDefinition rowDef = new RowDefinition();
            AllProducts.RowDefinitions.Add(rowDef);
            rowDef.Height = new GridLength(75);
            can = new Canvas();
            Label number = new Label();
            TextBlock textbox = new TextBlock();
            Label nameProduct = new Label();
            TextBlock amount = new TextBlock();
            
            //Добавить в Canvas элементы одного товарного наименования
            can.Children.Add(nameProduct);
            can.Children.Add(number);
            can.Children.Add(amount);
            
            //Номер 
            number.Content = count + 1;   //Поменять номер
            Canvas.SetTop(number,11);
            number.Height = 54;
            number.Width = 32;
            number.HorizontalContentAlignment = HorizontalAlignment.Center;
            number.VerticalContentAlignment = VerticalAlignment.Center;
            
            //Наименование
            Canvas.SetTop(nameProduct,11);
            Canvas.SetLeft(nameProduct, 33);
            nameProduct.Height = 54;
            nameProduct.Width = 175;
            nameProduct.VerticalContentAlignment= VerticalAlignment.Center;
            nameProduct.HorizontalContentAlignment = HorizontalAlignment.Left;
            nameProduct.Background = new SolidColorBrush(Colors.Green);
            nameProduct.Content = textbox;
            //Текст в label устанавливаем через TextBlock
            textbox.Text = pNameProduct;
            textbox.TextWrapping = TextWrapping.Wrap;

            //Количество


            //Добавить канвас в грид           
            Grid.SetRow(can, count);
            AllProducts.Children.Add(can);
            
            count++;

          
        }

        private void ButtonDelProduct_Click (object sender, RoutedEventArgs e)
        {
            foreach (var gridChild in AllProducts.Children)
            {
                var canv = gridChild as Canvas;
                if (canv != null&& Grid.GetRow(canv)==2)
                {
                   foreach(var q in canv.Children)
                    {
                        var labl = q as Label;
                        if (q != null)
                            labl.Content = "qwe";
                    }
                }
            }

            if (Grid.GetRow(can)==0&& Grid.GetColumn(can) == 0)
            ButtonDelProduct.Content = "eewq";
        }
    }

}
