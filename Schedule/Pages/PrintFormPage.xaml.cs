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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Schedule.Pages
{
    /// <summary>
    /// Логика взаимодействия для PrintFormPage.xaml
    /// </summary>
    public partial class PrintFormPage : Page
    {
        public PrintFormPage()
        {
            InitializeComponent();

            txtStartDate.Text = Entitie.PrintClass.startDate;
            txtEndDate.Text = Entitie.PrintClass.endDate;
            gridClocksPrint.ItemsSource = Entitie.PrintClass.dataContext;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog p = new PrintDialog();
            if (p.ShowDialog() == true)
            {
                p.PrintVisual(gridPrint, "Печать");
            }
        }
    }
}
