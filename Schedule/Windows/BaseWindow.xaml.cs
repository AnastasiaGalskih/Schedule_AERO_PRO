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

namespace Schedule
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class BaseWindow : Window
    {
        public BaseWindow()
        {
            InitializeComponent();
            Entitie.Manager.StartFrame = StartFrame;
            Entitie.Manager.StartFrame.Navigate(new Pages.UserRole());
        }

        private void StartFrame_ContentRendered(object sender, EventArgs e)
        {
            if (StartFrame.CanGoBack)
            {
                btnBack.Visibility = Visibility.Visible;
            }
            else
            {
                btnBack.Visibility = Visibility.Hidden;
            }

            if (Entitie.UserName.userRole == "Бухгалтер")
            {
                btnScheme.IsEnabled = false;
                btnScheme.ToolTip = "Данная функция не доступна для вашей роли пользователя";
            }
            if (Entitie.UserName.userRole == "Заместитель начальника производства")
            {
                btnClock.IsEnabled = false;
                btnClock.ToolTip = "Данная функция не доступна для вашей роли пользователя";
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            StartFrame.GoBack();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnWork_Click(object sender, RoutedEventArgs e)
        {
            Entitie.Manager.StartFrame.Navigate(new Pages.WorkersListPage());
        }

        private void btnClock_Click(object sender, RoutedEventArgs e)
        {
            Entitie.Manager.StartFrame.Navigate(new Pages.ClocksPage());
        }

        private void btnScheme_Click(object sender, RoutedEventArgs e)
        {
            Entitie.Manager.StartFrame.Navigate(new Pages.SchemePage());
        }

        private void btnSchedule_Click(object sender, RoutedEventArgs e)
        {
            Entitie.Manager.StartFrame.Navigate(new Pages.SchedulePage());
        }

        private void btnProf_Click(object sender, RoutedEventArgs e)
        {
            Entitie.Manager.StartFrame.Navigate(new Pages.UserRole());
        }
    }
}
