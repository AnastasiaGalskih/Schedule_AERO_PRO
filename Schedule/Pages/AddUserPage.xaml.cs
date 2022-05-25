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
    /// Логика взаимодействия для AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        public Entitie.User currentUser = new Entitie.User();
        public AddUserPage(Entitie.User selectedUser)
        {
            InitializeComponent();

            if (selectedUser != null)
                currentUser = selectedUser;

            DataContext = currentUser;
            comBoxRole.ItemsSource = Entitie.ScheduleEntities4.GetContext().Roles.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(currentUser.Login))
                errors.AppendLine("Введите логин");
            if (string.IsNullOrWhiteSpace(currentUser.Password))
                errors.AppendLine("Введите пароль");
            if (string.IsNullOrWhiteSpace(currentUser.LastName))
                errors.AppendLine("Введите фамилию сотрудника");
            if (string.IsNullOrWhiteSpace(currentUser.FirstName))
                errors.AppendLine("Введите имя сотрудника");
            if (Convert.ToDouble(currentUser.Phone) > 89999999999 || Convert.ToDouble(currentUser.Phone) < 70000000000)
                errors.AppendLine("Введен некорректный номер телефона");
            if (currentUser.Role == null)
                errors.AppendLine("Выберите роль сотрудника");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (currentUser.Id == 0)
                Entitie.ScheduleEntities4.GetContext().Users.Add(currentUser);

            try
            {
                Entitie.ScheduleEntities4.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
