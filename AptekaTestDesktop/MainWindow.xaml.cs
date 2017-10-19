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

        public MainWindow()
        {
            InitializeComponent();
        }

        Canvas canvas;
        int count = 0;

        public void AddProduct(string nameProduct)
        {
            RowDefinition rowDef = new RowDefinition();
            AllProducts.RowDefinitions.Add(rowDef);
            canvas = new Canvas();

            Label labelNumber = new Label();
            labelNumber.Content = (count+1).ToString()+".";

            Label labelNameProduct = new Label();
            TextBlock textblockNameProduct = new TextBlock();
            textblockNameProduct.Text = nameProduct;
            textblockNameProduct.TextWrapping = TextWrapping.Wrap;
            labelNameProduct.Content = textblockNameProduct;

            Label amount = new Label();

            Button buttonAdd = new Button();
            buttonAdd.Content = "Пополнить";

            Label AddorDownAmount = new Label();

            Button buttonDown = new Button();
            buttonDown.Content = "Списать";



            canvas.Children.Add(labelNumber);
            canvas.Children.Add(labelNameProduct);
            canvas.Children.Add(amount);
            canvas.Children.Add(buttonAdd);
            canvas.Children.Add(AddorDownAmount);
            canvas.Children.Add(buttonDown);

            Canvas.SetTop(labelNumber, 11);
            labelNumber.Height = 54;
            labelNumber.Width = 32;
            labelNumber.HorizontalContentAlignment = HorizontalAlignment.Center;
            labelNumber.VerticalContentAlignment = VerticalAlignment.Center;

            Canvas.SetTop(labelNameProduct, 11);
            Canvas.SetLeft(labelNameProduct, 33);
            labelNameProduct.Height = 54;
            labelNameProduct.Width = 175;
            labelNameProduct.HorizontalContentAlignment = HorizontalAlignment.Left;
            labelNameProduct.VerticalContentAlignment = VerticalAlignment.Center;

            Canvas.SetTop(amount, 11);
            Canvas.SetLeft(amount, 267);
            amount.Height = 54;
            amount.Width = 54;
            amount.HorizontalContentAlignment = HorizontalAlignment.Center;
            amount.VerticalContentAlignment = VerticalAlignment.Center;
            amount.Background = new SolidColorBrush(Colors.White);

            Canvas.SetTop(buttonAdd, 23);
            Canvas.SetLeft(buttonAdd, 305);
            buttonAdd.Height = 30;
            buttonAdd.Width = 100;
            buttonAdd.HorizontalContentAlignment = HorizontalAlignment.Center;
            buttonAdd.VerticalContentAlignment = VerticalAlignment.Center;
            buttonAdd.Background = new SolidColorBrush(Colors.White);

            Canvas.SetTop(AddorDownAmount, 13);
            Canvas.SetLeft(AddorDownAmount, 405);
            AddorDownAmount.Height = 54;
            AddorDownAmount.Width = 54;
            AddorDownAmount.HorizontalContentAlignment = HorizontalAlignment.Center;
            AddorDownAmount.VerticalContentAlignment = VerticalAlignment.Center;
            AddorDownAmount.Background = new SolidColorBrush(Colors.White);

            Canvas.SetTop(buttonDown, 23);
            Canvas.SetLeft(buttonDown, 459);
            buttonDown.Height = 30;
            buttonDown.Width = 100;
            buttonDown.HorizontalContentAlignment = HorizontalAlignment.Center;
            buttonDown.VerticalContentAlignment = VerticalAlignment.Center;
            buttonDown.Background = new SolidColorBrush(Colors.White);

            Grid.SetRow(canvas, count);

            AllProducts.Children.Add(canvas);

            rowDef.Height = new GridLength(75);

            count++;
        }

        private void ButtonAddProduct_Click (object sender, RoutedEventArgs e)
        {           
            AddProduct windowAddProduct = new AddProduct();
            windowAddProduct.Owner = this;  //Задали отцовское окно для дочернего
            windowAddProduct.ShowDialog();  //Отобрразить как диалоговое окно           
        }

        private void ButtonDelProduct_Click(object sender, RoutedEventArgs e)
        {
            foreach (var gridChild in AllProducts.Children)
            {
                var canv = gridChild as Canvas;
                if (canv != null && Grid.GetRow(canv) == 2)
                {
                    foreach (var q in canv.Children)
                    {
                        var labl = q as Label;
                        if (q != null)
                            labl.Content = "qwe";
                    }
                }
            }
        }
    }
}
