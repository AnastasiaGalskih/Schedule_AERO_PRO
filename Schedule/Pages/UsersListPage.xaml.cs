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
    /// Логика взаимодействия для UsersListPage.xaml
    /// </summary>
    public partial class UsersListPage : Page
    {
        public UsersListPage()
        {
            InitializeComponent();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Entitie.Manager.StartFrame.Navigate(new Pages.AddUserPage((sender as Button).DataContext as Entitie.User));
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            Entitie.Manager.StartFrame.Navigate(new Pages.AddUserPage(null));
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var usersForRemoving = gridUsers.SelectedItems.Cast<Entitie.User>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {usersForRemoving.Count()} элементов?", "Внимание!",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Entitie.ScheduleEntities4.GetContext().Users.RemoveRange(usersForRemoving);
                    Entitie.ScheduleEntities4.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    gridUsers.ItemsSource = Entitie.ScheduleEntities4.GetContext().Users.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entitie.ScheduleEntities4.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                gridUsers.ItemsSource = Entitie.ScheduleEntities4.GetContext().Users.ToList();
            }
        }
    }
}
