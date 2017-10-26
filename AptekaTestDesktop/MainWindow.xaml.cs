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
using System.Text.RegularExpressions;
using System.IO;

namespace AptekaTestDesktop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string path = @"C:\Users\Артём\Desktop\АРТЕМ\НГТУ\5семестр\ООП\Проект АПТЕКА\AptekaTestDesktop\Apteka.txt"; //расположение файла

        //счетчик продуктов
        private int countProduct = 0;
        private List<Label> listAmount = new List<Label>();
        List<TextBox> listAddorDownProduct = new List<TextBox>();
        List<Canvas> listCanvas = new List<Canvas>();
        List<Label> listNumberLine = new List<Label>();
        List<Button> listButtonAdd = new List<Button>();
        List<Button> listButtonDown = new List<Button>();
        List<Label> listNameProduct = new List<Label>();

        public MainWindow ()
        {
            InitializeComponent();
            InitializeComponent();
            if (File.Exists(path)) //проверка на существование файла
            {
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default)) //проверка на товары в файле
                {
                    if (sr.Peek() != -1)
                    {
                        sr.Close();
                        ProductFromFile();
                    }
                }
            }
            else using (FileStream file = new FileStream(path, FileMode.Create)) { };
        }

        //Извлечь записи из файла и добавить в программу
        private void ProductFromFile ()
        {
            string nameproduct, amountproduct;

            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    nameproduct = line.Remove(line.IndexOf('='), line.Length - line.IndexOf('='));
                    amountproduct = line.Remove(0, line.IndexOf('=') + 1);
                    AddProduct(nameproduct, amountproduct);
                }
            }
        }

        //Кнопка добавление продукта
        private void ButtonAddProduct_Click (object sender, RoutedEventArgs e)
        {
            AddProduct windowAddProduct = new AddProduct();
            windowAddProduct.Owner = this;  //Задали отцовское окно для дочернего
            windowAddProduct.ShowDialog();  //Отобрразить как диалоговое окно
        }

        //функция добавления продукта
        public void AddProduct (string nameProduct, string amountProduct="-1")
        {
            //Новая строчка в гриде
            RowDefinition rowDef = new RowDefinition();
            rowDef.Height = new GridLength(75);
            AllProducts.RowDefinitions.Add(rowDef);

            //Все элементы строчки вложены в канвас
            Canvas canvas = new Canvas();

            //Номер товара и имя товара, количество,добавляемое количетсво товара
            Label labelNumber = new Label();
            Label labelNameProduct = new Label();
            listNameProduct.Add(labelNameProduct);
            Label amount = new Label();

            listNumberLine.Add(labelNumber);

            //Количество списываемого\добавлемого товара
            TextBox AddorDownAmount = new TextBox();
            //ТекстБокс вложен в лейбл labelNameProduct
            TextBlock textblockNameProduct = new TextBlock();

            //добавили в листы
            listAmount.Add(amount);
            listAddorDownProduct.Add(AddorDownAmount);
            listCanvas.Add(canvas);

            //Кнопки списать и добавить
            Button buttonAdd = new Button();
            Button buttonDown = new Button();
            buttonAdd.Content = "Пополнить";
            buttonDown.Content = "Списать";
            //добавили их в листы
            listButtonAdd.Add(buttonAdd);
            listButtonDown.Add(buttonDown);

            //Добавление всех элементов в канвас
            canvas.Children.Add(labelNumber);
            canvas.Children.Add(listNameProduct[countProduct]);
            canvas.Children.Add(listAmount[countProduct]);
            canvas.Children.Add(listButtonDown[countProduct]);
            canvas.Children.Add(listAddorDownProduct[countProduct]);
            canvas.Children.Add(listButtonAdd[countProduct]);
            canvas.Style = (Style) FindResource("NonDedicatedCanvas");

            //установитиь для канваса строку в гриде
            Grid.SetRow(canvas, countProduct);

            //Обработка нажатия на канвас
            canvas.MouseDown += (Canvas_click);

            //установка свойст для счетчика товара(лейбл)
            Canvas.SetTop(labelNumber, 11);
            labelNumber.Content = (countProduct+1).ToString()+"."; //ИЗМЕНИТЬ --------------------------------------------------------
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
            if (amountProduct == "-1") amount.Content = "0"; //если не из файла
            else amount.Content = amountProduct; //из файла
            amount.Background = new SolidColorBrush(Colors.White);

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
            buttonAdd.Name = "IndexRow_" + countProduct.ToString();

            //Установка свойст для товара который хотим списать/положить (лейбл)
            Canvas.SetTop(AddorDownAmount, 13);
            Canvas.SetLeft(AddorDownAmount, 405);
            AddorDownAmount.Height = 54;
            AddorDownAmount.Width = 54;
            AddorDownAmount.Template = (ControlTemplate) FindResource("TextBoxTemplate");
            AddorDownAmount.FontSize = 25;
            AddorDownAmount.Text = "0";
            AddorDownAmount.PreviewTextInput += new TextCompositionEventHandler(AddorDownAmount_PreviewTextInput); //событие,для проверки ввода в textbox только цифр  
            AddorDownAmount.PreviewKeyDown += new KeyEventHandler(AddorDownAmount_PreviewKeyDown);
            AddorDownAmount.HorizontalContentAlignment = HorizontalAlignment.Center;
            AddorDownAmount.VerticalContentAlignment = VerticalAlignment.Center;
            AddorDownAmount.Background = new SolidColorBrush(Colors.White);
            AddorDownAmount.MaxLength = 3; //максимальное число 999
            AddorDownAmount.GotFocus += new RoutedEventHandler(AddorDownAmount_GotFocus);

            //Установка свойств для кнопки списать
            Canvas.SetTop(buttonDown, 23);
            Canvas.SetLeft(buttonDown, 459);
            buttonDown.Height = 30;
            buttonDown.Width = 100;
            buttonDown.Click += new RoutedEventHandler(ButtonDown_Click); //нажатие кнопки
            buttonDown.HorizontalContentAlignment = HorizontalAlignment.Center;
            buttonDown.VerticalContentAlignment = VerticalAlignment.Center;
            buttonDown.Background = new SolidColorBrush(Colors.White);
            buttonDown.Name = "IndexRow_" + countProduct.ToString();
            buttonDown.Template = (ControlTemplate) TryFindResource("ButtonAddorDownTemplate");

            AllProducts.Children.Add(canvas);

            countProduct++;
        }
  
        //Выделение строки для удаления
        private void Canvas_click (object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed&&((Canvas) sender).Style == (Style) FindResource("NonDedicatedCanvas"))
                //Выделили
                ((Canvas) sender).Style = (Style) FindResource("DedicatedCanvas");
            else
                //сняли выделение при повторном клике
                ((Canvas) sender).Style = (Style) FindResource("NonDedicatedCanvas");
        }

        //Кнопка удаления вызывает диалоговое окно
        private void ButtonDelProduct_Click (object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < listCanvas.Count; i++)
            {
                if (listCanvas[i].Style == (Style) FindResource("DedicatedCanvas"))
                {
                    DeleteProduct deleteProduct = new DeleteProduct();
                    deleteProduct.Owner = this;  //Задали отцовское окно для дочернего
                    deleteProduct.ShowDialog();  //Отобрразить как диалоговое окно
                }
            }
            
        }

        //Удаление стрчоик
        public void Delete ()
        {
            for (int i = 0; i < listCanvas.Count; i++)
            {
                if (listCanvas[i].Style == (Style) FindResource("DedicatedCanvas"))
                {
                    DeleteProductFromFile(listNameProduct[i].Content, i);

                    listCanvas.RemoveAt(i);
                    listAmount.RemoveAt(i);
                    listNameProduct.RemoveAt(i);
                    listAddorDownProduct.RemoveAt(i);
                    listButtonAdd.RemoveAt(i);
                    listButtonDown.RemoveAt(i);
                    listNumberLine.RemoveAt(i);
                    AllProducts.Children.RemoveAt(i);
                    
                    //Количество товара изменилось на -1
                    countProduct--;
                    //изменение строк
                    ChengeRow(listCanvas, i); 
                    break;
                }
            }
        }

        //Функция изменение строк после удаления
                        //Лист канвасов для изменения, индекс с которого происходит замена
        private void ChengeRow (List<Canvas> listCanv, int index)
        {
            for (int i = index+1; i <= countProduct; i++)
            {
                Grid.SetRow(listCanv[i - 1], i - 1);
                listNumberLine[i - 1].Content = (i).ToString()+".";
                listButtonAdd[i - 1].Name = "IndexRow_" + (i - 1).ToString();
                listButtonDown[i - 1].Name = "IndexRow_" + (i - 1).ToString();
            }
        }

        //запрет ввода в AddorDownAmount ' '
        void AddorDownAmount_PreviewKeyDown (object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        //функция,проверяющая ввод только цифр в AddorDownAmount
        void AddorDownAmount_PreviewTextInput (object sender, TextCompositionEventArgs e) 
        {
            if (!char.IsDigit(e.Text, 0) || ((TextBox) sender).Text == "0") e.Handled = true;
        }
        
        //Если 0, то стереть нуль в окне добавления/списания товара
        private void AddorDownAmount_GotFocus (object sender, RoutedEventArgs e)
                {
                    if (((TextBox) sender).Text == "0")
                        ((TextBox) sender).Text = "";
                }

        //Кнопка добавления товара
        private void ButtonAdd_Click (object sender, RoutedEventArgs e)
        {
            //Получение нужного нам индекса
            string name = ((Button) sender).Name;
            int indexRow = Int32.Parse(name.Remove(0, 9));
            //Проверка на пустой текст
            if (listAddorDownProduct[indexRow].Text == "")
            {
                Warning windowWarning = new Warning("Введите количетсво отличное от нуля!");
                windowWarning.Owner = this;  //Задали отцовское окно для дочернего
                windowWarning.ShowDialog();  //Отобрразить как диалоговое окно
            }
            else
            {
                //Максимальное количество товара 999
                //расчет кол-ва товара ведет функция GetAmountProduct
                int amountProduct = GetAmountProduct(Int32.Parse(listAmount[indexRow].Content.ToString()), Int32.Parse(listAddorDownProduct[indexRow].Text.ToString()), "Add");
                if (amountProduct >= 1)
                {
                    ChangeFileAmountProduct(indexRow, amountProduct); //изменение кол-ва товара в файле
                    listAmount[indexRow].Content = amountProduct.ToString();
                    listAddorDownProduct[indexRow].Text = "0";
                }
            }
        }

        //Кнопка списания товара
        private void ButtonDown_Click (object sender, RoutedEventArgs e)
        {
            //Получение нужного нам индекса
            string name = ((Button) sender).Name;
            int indexRow = Int32.Parse(name.Remove(0, 9));

            if (listAddorDownProduct[indexRow].Text == "")
            {
                Warning windowWarning = new Warning("Введите количетсво отличное от нуля!");
                windowWarning.Owner = this;  //Задали отцовское окно для дочернего
                windowWarning.ShowDialog();  //Отобрразить как диалоговое окно
            }
            else
            {
                //Максимальное количество товара 999
                //расчет кол-ва товара ведет функция GetAmountProduct
                int amountProduct = GetAmountProduct(Int32.Parse(listAmount[indexRow].Content.ToString()), Int32.Parse(listAddorDownProduct[indexRow].Text.ToString()), "Take");
                if (amountProduct >= 0)
                {
                    ChangeFileAmountProduct(indexRow, amountProduct); //изменение кол-ва товара в файле
                    listAmount[indexRow].Content = amountProduct.ToString();
                    listAddorDownProduct[indexRow].Text = "0";
                }
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
                    Warning windowWarning = new Warning("Превышен лимит товара!Введите меньшее число");
                    windowWarning.Owner = this;  //Задали отцовское окно для дочернего
                    windowWarning.ShowDialog();  //Отобрразить как диалоговое окно
                    return -1;
                }
                else
                    return amountProduct + addOrDown;
            }
            //Случай с отниманием товара
            if (operation == "Take")
                if (amountProduct - addOrDown < 0)
                {
                    Warning windowWarning = new Warning("Количество товара не может быть отрицательным!");
                    windowWarning.Owner = this;  //Задали отцовское окно для дочернего
                    windowWarning.ShowDialog();  //Отобрразить как диалоговое окно
                    return -1;
                }
                else
                    return amountProduct - addOrDown;
            return 0;
        }

        //удалеие из файла нужного продукта
        private void DeleteProductFromFile (object filter,int index)
        {
            string[] InputFile = File.ReadAllLines(path, System.Text.Encoding.Default);
            File.Delete(path);
            int i = 0;
            foreach (string line in InputFile)
            {
                if (line.Contains(((TextBlock) filter).Text)&&i==index)
                {
                    continue;
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(line);
                    }
                }
                i++;
            }
        }

        //метод, изменяющий кол-во товара в файле
        private void ChangeFileAmountProduct (int index, int amountProduct)
        {
            string[] InputFile = File.ReadAllLines(path, System.Text.Encoding.Default); //записывает в массив строк InputFile данные из файла
            File.Delete(path);
            for (int i = 0; i < InputFile.Length; i++)
            {
                if (i == index)
                {
                    InputFile[i] = ((TextBlock) listNameProduct[index].Content).Text + "=" + amountProduct.ToString(); //изменяем кол-во товара 
                }
                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(InputFile[i]); //записываем данные в файл
                }
            }

        }
    }

}
