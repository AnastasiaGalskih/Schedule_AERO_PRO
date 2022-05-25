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
    /// Логика взаимодействия для WorkersListPage.xaml
    /// </summary>
    public partial class WorkersListPage : Page
    {
        public WorkersListPage()
        {
            InitializeComponent();
            if (Entitie.UserName.userRole == "Бухгалтер" || Entitie.UserName.userRole == "Заместитель начальника производства")
            {
                btnAddWork.IsEnabled = false;
                btnAddWork.ToolTip = "Данная функция не доступна для вашей роли пользователя";
                btnDeleteWork.IsEnabled = false;
                btnDeleteWork.ToolTip = "Данная функция не доступна для вашей роли пользователя"; 
            }
        }

        private void btnAddWork_Click(object sender, RoutedEventArgs e)
        {
            Entitie.Manager.StartFrame.Navigate(new Pages.AddWorkerPage(null));
        }

        private void btnDeleteWork_Click(object sender, RoutedEventArgs e)
        {
            var workersForRemoving = gridWorkers.SelectedItems.Cast<Entitie.Worker>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {workersForRemoving.Count()} элементов?", "Внимание!",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Entitie.ScheduleEntities4.GetContext().Workers.RemoveRange(workersForRemoving);
                    Entitie.ScheduleEntities4.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    gridWorkers.ItemsSource = Entitie.ScheduleEntities4.GetContext().Workers.ToList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Entitie.Manager.StartFrame.Navigate(new Pages.AddWorkerPage((sender as Button).DataContext as Entitie.Worker));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entitie.ScheduleEntities4.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                gridWorkers.ItemsSource = Entitie.ScheduleEntities4.GetContext().Workers.ToList();
            }
        }

        private void txtBoxWork_TextChanged(object sender, TextChangedEventArgs e)
        {
            var currentWorkers = Entitie.ScheduleEntities4.GetContext().Workers.ToList();

            currentWorkers = currentWorkers.Where(p => p.FullName.ToLower().Contains(txtBoxWork.Text.ToLower())).ToList();
            gridWorkers.ItemsSource = currentWorkers.ToList();
        }
    }
}
