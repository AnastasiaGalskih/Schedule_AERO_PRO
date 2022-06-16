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
    /// Логика взаимодействия для AddWorkerPage.xaml
    /// </summary>
    public partial class AddWorkerPage : Page
    {
        public Entitie.Worker currentWorker = new Entitie.Worker();
        public AddWorkerPage(Entitie.Worker selectedWorker)
        {
            InitializeComponent();

            if (selectedWorker != null)
                currentWorker = selectedWorker;

            DataContext = currentWorker;
            comBoxMashine.ItemsSource = Entitie.ScheduleEntities4.GetContext().TypeMachines.ToList();

            if (Entitie.UserName.userRole == "Бухгалтер" || Entitie.UserName.userRole == "Заместитель начальника производства")
            {
                btnSave.IsEnabled = false;
                btnSave.ToolTip = "Данная функция не доступна для вашей роли пользователя";
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(currentWorker.LastName))
                errors.AppendLine("Введите фамилию сотрудника");
            if (string.IsNullOrWhiteSpace(currentWorker.FirstName))
                errors.AppendLine("Введите имя сотрудника");
            if (Convert.ToDouble(currentWorker.Phone) > 89999999999 || Convert.ToDouble(currentWorker.Phone) < 70000000000)
                errors.AppendLine("Введен некорректный номер телефона");
            if (currentWorker.Passport != null) 
            {
                if (currentWorker.Passport.Length > 10 || currentWorker.Passport.Length < 10)
                errors.AppendLine("Введен некорректный номер паспорта");
            }
            if (currentWorker.Passport == null)
                errors.AppendLine("Введите паспортные данные");
            if (currentWorker.Birthday == null)
                errors.AppendLine("Введите дату рождения");
            if (currentWorker.TypeMachine == null)
                errors.AppendLine("Выберите тип рабочего станка");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (currentWorker.Id == 0)
                Entitie.ScheduleEntities4.GetContext().Workers.Add(currentWorker);

            try
            {
                Entitie.ScheduleEntities4.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена"); 
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
