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
    /// Логика взаимодействия для Warning.xaml
    /// </summary>
    public partial class Warning : Window
    {
        public Warning ()
        {
            InitializeComponent();
        }
        public Warning(string warning)
        {
            InitializeComponent();
            WarningText.Text = warning;
        }

        private void ButtonOk_Click (object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
