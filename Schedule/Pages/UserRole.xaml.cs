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
    /// Логика взаимодействия для UserRole.xaml
    /// </summary>
    public partial class UserRole : Page
    {
        public UserRole()
        {
            InitializeComponent();
            txtFIO.Text = Entitie.UserName.userLast + " " + Entitie.UserName.userFirst + " " + Entitie.UserName.userPathr;
            txtRole.Text = Entitie.UserName.userRole;

            if (txtRole.Text != "Администратор")
            {
               btnUsers.Visibility = Visibility.Hidden;
               btnHistory.Visibility = Visibility.Hidden;
            }
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            Entitie.Manager.StartFrame.Navigate(new Pages.UsersListPage());
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            Entitie.Manager.StartFrame.Navigate(new Pages.HistoryPage());
        }
    }
}
