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
    /// Логика взаимодействия для SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : Page
    {
        public SchedulePage()
        {
            InitializeComponent();
            gridSchedule.ItemsSource = Entitie.ScheduleEntities4.GetContext().Schedules.ToList();

            if (Entitie.UserName.userRole == "Бухгалтер")
            {
                btnAddSchedule.IsEnabled = false;
                btnAddSchedule.ToolTip = "Данная функция не доступна для вашей роли пользователя";
                btnDeleteSchedule.IsEnabled = false;
                btnDeleteSchedule.ToolTip = "Данная функция не доступна для вашей роли пользователя";
            }
        }

        

        private void btnAddSchedule_Click(object sender, RoutedEventArgs e)
        {
            Entitie.Manager.StartFrame.Navigate(new AddSchedulePage(null));
        }

        private void btnDeleteSchedule_Click(object sender, RoutedEventArgs e)
        {
            var schedulesForRemoving = gridSchedule.SelectedItems.Cast<Entitie.Schedule>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {schedulesForRemoving.Count()} элементов?", "Внимание!",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Entitie.ScheduleEntities4.GetContext().Schedules.RemoveRange(schedulesForRemoving);
                    Entitie.ScheduleEntities4.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    gridSchedule.ItemsSource = Entitie.ScheduleEntities4.GetContext().Schedules.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Entitie.Manager.StartFrame.Navigate(new AddSchedulePage((sender as Button).DataContext as Entitie.Schedule));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entitie.ScheduleEntities4.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                gridSchedule.ItemsSource = Entitie.ScheduleEntities4.GetContext().Schedules.ToList();
            }
        }

        private void txtBoxWork_TextChanged(object sender, TextChangedEventArgs e)
        {
            var currentSchedule = Entitie.ScheduleEntities4.GetContext().Schedules.ToList();
            var idWorker = Entitie.ScheduleEntities4.GetContext().Workers.Where(p => (p.LastName + " " + p.FirstName + " " + p.Patronymic).ToLower().Contains(txtBoxWork.Text.ToLower())).FirstOrDefault();
            if(idWorker != null) 
            {
                var id = idWorker.Id;
                currentSchedule = currentSchedule.Where(p => p.IdWorker == id).ToList();
                gridSchedule.ItemsSource = currentSchedule.ToList();
            }
            else
            {
                gridSchedule.ItemsSource = currentSchedule.ToList();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Entitie.DateClass.startDate = dpStartDate.SelectedDate;
            Entitie.DateClass.endDate = dpEndDate.SelectedDate;
            if (Entitie.DateClass.startDate != null && Entitie.DateClass.endDate != null)
            {
                gridSchedule.ItemsSource = Entitie.ScheduleEntities4.GetContext().Schedules.Where(p => p.Date >= Entitie.DateClass.startDate && p.Date <= Entitie.DateClass.endDate).ToList();
            }
        }
    }
}
