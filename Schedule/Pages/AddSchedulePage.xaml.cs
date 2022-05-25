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
    /// Логика взаимодействия для AddSchedulePage.xaml
    /// </summary>
    public partial class AddSchedulePage : Page
    {
        private Entitie.Schedule currentSchedule = new Entitie.Schedule();
        public AddSchedulePage(Entitie.Schedule selectedSchedule)
        {
            InitializeComponent();
            if (selectedSchedule != null)
                currentSchedule = selectedSchedule;

            DataContext = currentSchedule;
            cbWork.ItemsSource = Entitie.ScheduleEntities4.GetContext().Workers.ToList();
            cbType.ItemsSource = Entitie.ScheduleEntities4.GetContext().TypeMachines.ToList();

            if (Entitie.UserName.userRole == "Бухгалтер")
            {
                btnSave.IsEnabled = false;
                btnSave.ToolTip = "Данная функция не доступна для вашей роли пользователя";
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (cbWork.SelectedItem == null)
                errors.AppendLine("Выберите сотрудника");    
            if (dpWorkDate.Text == "01.01.0001")
                errors.AppendLine("Выберите дату");
            if (cbType.SelectedItem == null)
                errors.AppendLine("Выберите тип станка"); 
            if (txtNumber.Text == 0.ToString())
                errors.AppendLine("Выберите номер места");
            if (cbWork.SelectedItem != null)
            {
                Entitie.Worker worker = Entitie.ScheduleEntities4.GetContext().Workers
                    .Where(p => (p.LastName + " " + p.FirstName + " " + p.Patronymic) == cbWork.Text).FirstOrDefault();
                var idWorker = worker.Id;
                var idTypeMachine = worker.IdTypeMachine;
                Entitie.TypeMachine typeMachine = Entitie.ScheduleEntities4.GetContext()
                    .TypeMachines.Where(p => p.Id == idTypeMachine).FirstOrDefault();
                var typeId = typeMachine.Id;
                var type = typeMachine.Name;
                currentSchedule.IdWorker = idWorker;
                
                if (type != cbType.Text)
                    errors.AppendLine("Данный сотрудник не обучен работе за этим станком");

                var scheduleDate = Entitie.ScheduleEntities4.GetContext().Schedules
                    .Where(p => p.Date == dpWorkDate.DisplayDate && p.IdWorker == idWorker).FirstOrDefault();
                if(scheduleDate != null)
                    errors.AppendLine("Данный сотрудник уже назначен на рабочее место в этот день");

                var schedule = Entitie.ScheduleEntities4.GetContext().Schedules
                    .Where(p => p.Date == dpWorkDate.DisplayDate && p.PlaceNumber
                    .ToString() == txtNumber.Text && p.IdTypeMachine == typeId).FirstOrDefault();
                if (schedule != null)
                    errors.AppendLine("На это рабочее место уже назначен сотрудник");

                if(dpWorkDate.DisplayDate <= DateTime.Today)
                    errors.AppendLine("Вы не можете изменить уже прошедшее расписание");
            }


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (currentSchedule.Id == 0)
                Entitie.ScheduleEntities4.GetContext().Schedules.Add(currentSchedule);

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
