using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Text.RegularExpressions;
using System;

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
            buttonAdd.Template = (ControlTemplate)TryFindResource("ButtonAddorDownTemplate");
            buttonAdd.Click += new RoutedEventHandler(ButtonAdd_Click); //нажатие кнопки
            //buttonAdd.Click += (sender, args) => MessageBox.Show(this, "Ура!");

            TextBox AddorDownAmount = new TextBox();
            AddorDownAmount.FontSize = 25;
            AddorDownAmount.PreviewTextInput += new TextCompositionEventHandler(textBox_PreviewTextInput); //событие,для проверки ввода в textbox только цифр

            Button buttonDown = new Button();
            buttonDown.Content = "Списать";
            buttonDown.Template = (ControlTemplate)TryFindResource("ButtonAddorDownTemplate");

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
            Canvas.SetLeft(amount, 230);
            amount.Height = 54;
            amount.Width = 54;
            amount.HorizontalContentAlignment = HorizontalAlignment.Center;
            amount.VerticalContentAlignment = VerticalAlignment.Center;
            amount.Background = new SolidColorBrush(Colors.White);

            Canvas.SetTop(buttonAdd, 23);
            Canvas.SetLeft(buttonAdd, 312);

            Canvas.SetTop(AddorDownAmount, 13);
            Canvas.SetLeft(AddorDownAmount, 405);
            AddorDownAmount.Height = 54;
            AddorDownAmount.Width = 54;
            AddorDownAmount.HorizontalContentAlignment = HorizontalAlignment.Center;
            AddorDownAmount.VerticalContentAlignment = VerticalAlignment.Center;
            AddorDownAmount.Background = new SolidColorBrush(Colors.White);

            Canvas.SetTop(buttonDown, 23);
            Canvas.SetLeft(buttonDown, 467);

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

        void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e) //функция,проверяющая ввод только цифр в textbox
        {
            if (!char.IsDigit(e.Text, 0)) e.Handled = true;            
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
