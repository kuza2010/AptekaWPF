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
      
        private Canvas canvas;
        int count = 0;

        public MainWindow ()
        {
            InitializeComponent();
          
        }

        private void ButtonAddProduct_Click (object sender, RoutedEventArgs e)
        { 
            AddProduct windowAddProduct = new AddProduct();
            windowAddProduct.Owner = this;  //Задали отцовское окно для дочернего
            windowAddProduct.ShowDialog();  //Отобрразить как диалоговое окно
        }

        public void AddProduct (string nameProduct)
        {
            //Новая строчка в гриде
            RowDefinition rowDef = new RowDefinition();
            rowDef.Height = new GridLength(75);
            AllProducts.RowDefinitions.Add(rowDef);

            //Все элементы строчки вложены в канвас
            canvas = new Canvas();

            //Номер товара и имя товара, количество,добавляемое количетсво товара
            Label labelNumber = new Label();
            Label labelNameProduct = new Label();
            Label amount = new Label();

            TextBox AddorDownAmount = new TextBox();

            //ТекстБокс вложен в лейбл labelNameProduct
            TextBlock textblockNameProduct = new TextBlock();
         

            

            //Кнопки списать и добавить
            Button buttonAdd = new Button();
            Button buttonDown = new Button();
            buttonAdd.Content = "Пополнить";   
            buttonDown.Content = "Списать";

            //Добавление всех элементов в канвас
            canvas.Children.Add(labelNumber);
            canvas.Children.Add(labelNameProduct);
            canvas.Children.Add(amount);
            canvas.Children.Add(buttonAdd);
            canvas.Children.Add(AddorDownAmount);
            canvas.Children.Add(buttonDown);
            //установитиь для канваса строку в гриде
            Grid.SetRow(canvas, count);

            //установка свойст для счетчика товара(лейбл)
            Canvas.SetTop(labelNumber, 11);
            labelNumber.Content = (count + 1).ToString() + "."; //ИЗМЕНИТЬ --------------------------------------------------------
            labelNumber.Height = 54;
            labelNumber.Width = 32;
            labelNumber.HorizontalContentAlignment = HorizontalAlignment.Center;
            labelNumber.VerticalContentAlignment = VerticalAlignment.Center;

            //Установка свойст имени продукта(текстбокс)
            Canvas.SetTop(labelNameProduct, 11);
            Canvas.SetLeft(labelNameProduct, 33);
            //Имя товара
            labelNameProduct.Content = textblockNameProduct;
            labelNameProduct.Height = 54;
            labelNameProduct.Width = 175;
            labelNameProduct.HorizontalContentAlignment = HorizontalAlignment.Left;
            labelNameProduct.VerticalContentAlignment = VerticalAlignment.Center;
            textblockNameProduct.Text = nameProduct;
            textblockNameProduct.TextWrapping = TextWrapping.Wrap;

            //Установка свойств для  количества товара(лейбл)
            Canvas.SetTop(amount, 11);
            Canvas.SetLeft(amount, 233);
            amount.Height = 54;
            amount.Width = 54;
            amount.Template = (ControlTemplate)FindResource("TextBlockTemplate");
            amount.HorizontalContentAlignment = HorizontalAlignment.Center;
            amount.VerticalContentAlignment = VerticalAlignment.Center;
            amount.Background = new SolidColorBrush(Colors.White);

            //Установка свойств для кнопки добавить
            Canvas.SetTop(buttonAdd, 23);
            Canvas.SetLeft(buttonAdd, 305);
            buttonAdd.Height = 30;
            buttonAdd.Width = 100;
            buttonAdd.HorizontalContentAlignment = HorizontalAlignment.Center;
            buttonAdd.VerticalContentAlignment = VerticalAlignment.Center;
            buttonAdd.Background = new SolidColorBrush(Colors.White);

            //Установка свойст для товара который хотим списать/положить (лейбл)
            Canvas.SetTop(AddorDownAmount, 13);
            Canvas.SetLeft(AddorDownAmount, 405);
            AddorDownAmount.Height = 54;
            AddorDownAmount.Width = 54;
            AddorDownAmount.Template = (ControlTemplate)FindResource("TextBoxTemplate");
 
            AddorDownAmount.HorizontalContentAlignment = HorizontalAlignment.Center;
            AddorDownAmount.VerticalContentAlignment = VerticalAlignment.Center;
            AddorDownAmount.Background = new SolidColorBrush(Colors.White);
            
            //Установка свойств для кнопки списать
            Canvas.SetTop(buttonDown, 23);
            Canvas.SetLeft(buttonDown, 459);
            buttonDown.Height = 30;
            buttonDown.Width = 100;
            buttonDown.HorizontalContentAlignment = HorizontalAlignment.Center;
            buttonDown.VerticalContentAlignment = VerticalAlignment.Center;
            buttonDown.Background = new SolidColorBrush(Colors.White);

            

            AllProducts.Children.Add(canvas);

            

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

            if (Grid.GetRow(canvas)==0&& Grid.GetColumn(canvas) == 0)
            ButtonDelProduct.Content = "eewq";
        }
    }

}
