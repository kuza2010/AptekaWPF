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
        List<Label> listAmount = new List<Label>();
        List<TextBox> listAddorDownProduct = new List<TextBox>();


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

            canvas.MouseDown +=(canvac_click);
            //Номер товара и имя товара, количество,добавляемое количетсво товара
            Label labelNumber = new Label();
            Label labelNameProduct = new Label();
            Label amount = new Label();

            //Количество списываемого\добавлемого товара
            TextBox AddorDownAmount = new TextBox();
            //ТекстБокс вложен в лейбл labelNameProduct
            TextBlock textblockNameProduct = new TextBlock();

            //добавили в листы
            listAmount.Add(amount);
            listAddorDownProduct.Add(AddorDownAmount);

            //Кнопки списать и добавить
            Button buttonAdd = new Button();
            Button buttonDown = new Button();
            buttonAdd.Content = "Пополнить";
            buttonDown.Content = "Списать";

            //Добавление всех элементов в канвас
            canvas.Children.Add(labelNumber);
            canvas.Children.Add(labelNameProduct);
            canvas.Children.Add(listAmount[count]);
            canvas.Children.Add(buttonAdd);
            canvas.Children.Add(listAddorDownProduct[count]);
            canvas.Children.Add(buttonDown);
            //установитиь для канваса строку в гриде
            Grid.SetRow(canvas, count);

            //установка свойст для счетчика товара(лейбл)
            Canvas.SetTop(labelNumber, 11);
            labelNumber.Content = (count + 1).ToString(); //ИЗМЕНИТЬ --------------------------------------------------------
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
            amount.FontSize = 25;
            amount.Template = (ControlTemplate) FindResource("TextBlockTemplate");
            amount.HorizontalContentAlignment = HorizontalAlignment.Center;
            amount.VerticalContentAlignment = VerticalAlignment.Center;
            amount.Content = 0;
            amount.Background = new SolidColorBrush(Colors.White);
            amount.Name = "IndexRow_" + labelNumber.Content.ToString();

            //Установка свойств для кнопки добавить
            Canvas.SetTop(buttonAdd, 23);
            Canvas.SetLeft(buttonAdd, 305);
            buttonAdd.Height = 30;
            buttonAdd.Width = 100;
            buttonAdd.HorizontalContentAlignment = HorizontalAlignment.Center;
            buttonAdd.VerticalContentAlignment = VerticalAlignment.Center;
            buttonAdd.Background = new SolidColorBrush(Colors.White);
            buttonAdd.Template = (ControlTemplate) TryFindResource("ButtonAddorDownTemplate");
            buttonAdd.Click += new RoutedEventHandler(ButtonAdd_Click); //нажатие кнопки
            buttonAdd.Name = "IndexRow_" + labelNumber.Content.ToString();


            //Установка свойст для товара который хотим списать/положить (лейбл)
            Canvas.SetTop(AddorDownAmount, 13);
            Canvas.SetLeft(AddorDownAmount, 405);
            AddorDownAmount.Height = 54;
            AddorDownAmount.Width = 54;
            AddorDownAmount.Template = (ControlTemplate) FindResource("TextBoxTemplate");
            AddorDownAmount.FontSize = 25;
            AddorDownAmount.Text = "0";
            AddorDownAmount.PreviewTextInput += new TextCompositionEventHandler(textBox_PreviewTextInput); //событие,для проверки ввода в textbox только цифр  
            AddorDownAmount.HorizontalContentAlignment = HorizontalAlignment.Center;
            AddorDownAmount.VerticalContentAlignment = VerticalAlignment.Center;
            AddorDownAmount.Background = new SolidColorBrush(Colors.White);
            AddorDownAmount.MaxLength = 3; //максимальное число 999
            AddorDownAmount.Name = "IndexRow_" + labelNumber.Content.ToString();

            //Установка свойств для кнопки списать
            Canvas.SetTop(buttonDown, 23);
            Canvas.SetLeft(buttonDown, 459);
            buttonDown.Height = 30;
            buttonDown.Width = 100;
            buttonDown.Click += new RoutedEventHandler(ButtonDown_Click); //нажатие кнопки
            buttonDown.HorizontalContentAlignment = HorizontalAlignment.Center;
            buttonDown.VerticalContentAlignment = VerticalAlignment.Center;
            buttonDown.Background = new SolidColorBrush(Colors.White);
            buttonDown.Name = "IndexRow_" + labelNumber.Content.ToString();
            buttonDown.Template = (ControlTemplate) TryFindResource("ButtonAddorDownTemplate");

            AllProducts.Children.Add(canvas);

            count++;
        }

        private void Canvas_MouseDown (object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void canvac_click (object sender, MouseButtonEventArgs e)
        {
            ((Canvas) sender).Background = new SolidColorBrush(Colors.Yellow);
        }

        /// //////////////////////////////////////////////////////////////////////////////
        private void ButtonDelProduct_Click (object sender, RoutedEventArgs e)
        {
            /*
            foreach (var gridChild in AllProducts.Children)
            {
                var canv = gridChild as Canvas;
                if (canv != null&& Grid.GetRow(canv)==2)
                {
                   foreach(var q in canv.Children)
                    {
                        var labl = q as Label;
                        if (labl != null)
                        {
                         break;
                        }
                    }
                }
            }*/

            if (Grid.GetRow(canvas) == 0 && Grid.GetColumn(canvas) == 0)
                ButtonDelProduct.Content = "eewq";
        }
        /// //////////////////////////////////////////////////////////////////////// 
     

        //функция,проверяющая ввод только цифр в textbox
        void textBox_PreviewTextInput (object sender, TextCompositionEventArgs e) 
        {
            if (!char.IsDigit(e.Text, 0)) e.Handled = true;
            if (((TextBox) sender).Text == "0")
                e.Handled = true;
        }

        //Кнопка добавления товара
        private void ButtonAdd_Click (object sender, RoutedEventArgs e)
        {
            //Получение нужного нам индекса
            string name = ((Button) sender).Name;
            int indexRow = Int32.Parse(name.Remove(0, 9));

            //Максимальное количество товара 999
            //расчет кол-ва товара ведет функция GetAmountProduct
            int amountProduct = GetAmountProduct(Int32.Parse(listAmount[indexRow - 1].Content.ToString()), Int32.Parse(listAddorDownProduct[indexRow - 1].Text.ToString()), "Add");
            if (amountProduct >= 1)
            {
                listAmount[indexRow - 1].Content = amountProduct.ToString();
                listAddorDownProduct[indexRow - 1].Text = "";
            }
        }

        //Кнопка списания товара
        private void ButtonDown_Click (object sender, RoutedEventArgs e)
        {
            //Получение нужного нам индекса
            string name = ((Button) sender).Name;
            int indexRow = Int32.Parse(name.Remove(0, 9));

            //Максимальное количество товара 999
            //расчет кол-ва товара ведет функция GetAmountProduct
            int amountProduct = GetAmountProduct(Int32.Parse(listAmount[indexRow - 1].Content.ToString()), Int32.Parse(listAddorDownProduct[indexRow - 1].Text.ToString()), "Take");
            if (amountProduct >= 0)
            {
                listAmount[indexRow - 1].Content = amountProduct.ToString();
                listAddorDownProduct[indexRow - 1].Text = "";
            }
        }

        //проверка на списание\добавку товара
        private int GetAmountProduct (int amountProduct, int addOrDown, string operation)
        {
            //случай с добавленим товара
            if (operation == "Add")
            {
                if (amountProduct + addOrDown > 999)
                {
                    MessageBox.Show("Будет ошибка");
                    return -1;
                }
                else
                    return amountProduct + addOrDown;
            }
            //Случай с отниманием товара
            if (operation == "Take")
                if (amountProduct - addOrDown < 0)
                {
                    MessageBox.Show("Будет ошибка");
                    return -1;
                }
                else
                    return amountProduct - addOrDown;
            return 0;
        }
    }

}
