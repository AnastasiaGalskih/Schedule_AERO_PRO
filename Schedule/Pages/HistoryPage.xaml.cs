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
    /// Логика взаимодействия для HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        public HistoryPage()
        {
            InitializeComponent();
            gridHistory.ItemsSource = Entitie.ScheduleEntities4.GetContext().Histories.ToList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var stringForRemoving = gridHistory.SelectedItems.Cast<Entitie.History>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие " +
                $"{stringForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Entitie.ScheduleEntities4.GetContext().Histories.RemoveRange(stringForRemoving);
                    Entitie.ScheduleEntities4.GetContext().SaveChanges();
                    MessageBox.Show("Данные были удалены");

                    gridHistory.ItemsSource = Entitie.ScheduleEntities4.GetContext().Histories.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnDelHis_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы точно хотите очистить историю входов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Entitie.ScheduleEntities4.GetContext().Histories.RemoveRange(Entitie.ScheduleEntities4.GetContext().Histories.ToList());
                    Entitie.ScheduleEntities4.GetContext().SaveChanges();
                    MessageBox.Show("Данные были удалены");

                    gridHistory.ItemsSource = Entitie.ScheduleEntities4.GetContext().Histories.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
