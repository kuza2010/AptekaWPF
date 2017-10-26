﻿using System;
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

namespace AptekaTestDesktop
{
    /// <summary>
    /// Логика взаимодействия для DeleteProduct.xaml
    /// </summary>
    public partial class DeleteProduct : Window
    {
        public DeleteProduct ()
        {
            InitializeComponent();
        }

        private void ButtonOk_Click (object sender, RoutedEventArgs e)
        {
            MainWindow main = this.Owner as MainWindow;
            if (main != null)
            {
                main.Delete();
            }
            this.Close();
        }

        private void ButtonCancle_Click (object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
