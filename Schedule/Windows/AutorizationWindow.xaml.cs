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
using System.Windows.Shapes;

namespace Schedule.Windows
{
    /// <summary>
    /// Логика взаимодействия для AutorizationWindow.xaml
    /// </summary>
    public partial class AutorizationWindow : Window
    {
        public AutorizationWindow()
        {
            InitializeComponent();
            txtBoxPassword.Visibility = Visibility.Hidden;
        }

        private void passBoxPassword_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            txtBoxPassword.Text = passBoxPassword.Password;
        }

        private void TextBlock_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            passBoxPassword.Password = txtBlockPassword.Text;
        }

        private void checkBoxPass_Checked(object sender, RoutedEventArgs e)
        {
            passBoxPassword.Visibility = Visibility.Hidden;
            txtBoxPassword.Visibility = Visibility.Visible;
            txtBlockPassword.Text = "Скрыть пароль";
            txtBoxPassword.Text = passBoxPassword.Password;
            passBoxPassword.Password = txtBoxPassword.Text;
        }

        private void checkBoxPass_Unchecked(object sender, RoutedEventArgs e)
        {
            passBoxPassword.Visibility = Visibility.Visible;
            txtBoxPassword.Visibility = Visibility.Hidden;
            txtBlockPassword.Text = "Показать пароль";
            passBoxPassword.Password = txtBoxPassword.Text;
            txtBoxPassword.Text = passBoxPassword.Password;
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            Entitie.User authUser = null;
            using (Entitie.ScheduleEntities4 db = new Entitie.ScheduleEntities4())
            {
                authUser = db.Users.Where(b => b.Login == txtBoxLogin.Text && b.Password == txtBoxPassword.Text 
                || b.Password == passBoxPassword.Password).FirstOrDefault();

                if (authUser != null)
                {
                    Entitie.UserName.userRole = authUser.Role.Type;
                    Entitie.UserName.userLast = authUser.LastName;
                    Entitie.UserName.userFirst = authUser.FirstName;
                    Entitie.UserName.userPathr = authUser.Pathronymic;
                    var id = authUser.Id;
                    
                    SaveHistory(id);
                    BaseWindow baseWindow = new BaseWindow();
                    baseWindow.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Введен неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private static void SaveHistory(int id)
        {
            Entitie.History history = new Entitie.History();
            history.IdUser = id;
            history.Date = DateTime.Now;
            Entitie.ScheduleEntities4.GetContext().Histories.Add(history);
            Entitie.ScheduleEntities4.GetContext().SaveChanges();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
