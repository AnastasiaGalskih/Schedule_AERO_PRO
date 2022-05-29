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

namespace Schedule.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClocksPage.xaml
    /// </summary>
    public partial class ClocksPage : Page
    {
        public ClocksPage()
        {
            InitializeComponent();
            gridClocks.ItemsSource = Entitie.ScheduleEntities4.GetContext().Workers.ToList();    
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            Entitie.PrintClass.startDate = dpStartDate.SelectedDate.ToString();
            Entitie.PrintClass.endDate = dpEndDate.SelectedDate.ToString();
            Entitie.PrintClass.dataContext = gridClocks.ItemsSource;

            Entitie.Manager.StartFrame.Navigate(new Pages.PrintFormPage());
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entitie.ScheduleEntities4.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                gridClocks.ItemsSource = Entitie.ScheduleEntities4.GetContext().Workers.ToList();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Entitie.DateClass.startDate = dpStartDate.SelectedDate;
            Entitie.DateClass.endDate = dpEndDate.SelectedDate;
            Entitie.ScheduleEntities4.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            gridClocks.ItemsSource = Entitie.ScheduleEntities4.GetContext().Workers.ToList();
        }

        private void txtBoxWork_TextChanged(object sender, TextChangedEventArgs e)
        {
            var currentWorkers = Entitie.ScheduleEntities4.GetContext().Workers.ToList();

            currentWorkers = currentWorkers.Where(p => p.FullName.ToLower().Contains(txtBoxWork.Text.ToLower())).ToList();
            gridClocks.ItemsSource = currentWorkers.ToList();
        }
    }
}
