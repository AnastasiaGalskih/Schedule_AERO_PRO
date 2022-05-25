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
    /// Логика взаимодействия для SchemePage.xaml
    /// </summary>
    public partial class SchemePage : Page
    {
        private DateTime date = DateTime.Today;
        private Entitie.Pass currentPass = new Entitie.Pass();
        private static string[] tips1 = new string[8];
        private static string[] tips2 = new string[4];
        private static string[] tips3 = new string[3];
        public SchemePage()
        {
            InitializeComponent();
            
            txtHeader.Text = "Список отсутствующих " + date;
            var currentSchedule = Entitie.ScheduleEntities4.GetContext().Schedules.ToList();
            currentSchedule = currentSchedule.Where(p => p.Date == date).ToList();

            gridPasses.ItemsSource = Entitie.ScheduleEntities4.GetContext().Passes.ToList().Where(p => p.Date == date);

            var type1 = currentSchedule.Where(p => p.IdTypeMachine == 1).ToList();
            var type2 = currentSchedule.Where(p => p.IdTypeMachine == 2).ToList();
            var type3 = currentSchedule.Where(p => p.IdTypeMachine == 3).ToList();

            NameWorker(8, type1, tips1);

            number11.ToolTip = tips1[0];
            number12.ToolTip = tips1[1];
            number13.ToolTip = tips1[2];
            number14.ToolTip = tips1[3];
            number15.ToolTip = tips1[4];
            number16.ToolTip = tips1[5];
            number17.ToolTip = tips1[6];
            number18.ToolTip = tips1[7];

            NameWorker(4, type2, tips2);

            number21.ToolTip = tips2[0];
            number22.ToolTip = tips2[1];
            number23.ToolTip = tips2[2];
            number24.ToolTip = tips2[3];

            NameWorker(3, type3, tips3);

            number31.ToolTip = tips3[0];
            number32.ToolTip = tips3[1];
            number33.ToolTip = tips3[2];
        }

        private void NameWorker(int lengths, List<Entitie.Schedule> type, string[] tips)
        {
            var currentWorker = Entitie.ScheduleEntities4.GetContext().Workers.ToList();
            int i, j;
            for (i = 1, j = 0; i <= lengths; i++, j++)
            {
                var schedule = type.Where(p => p.PlaceNumber == i).FirstOrDefault();

                if (schedule != null)
                {
                    int idWorker = schedule.IdWorker;
                    var worker = currentWorker.Where(p => p.Id == idWorker).FirstOrDefault();
                    tips[j] = worker.FullName;
                }
                else
                {
                    tips[j] = "На данное место не назначен работник";
                }
            }
        }

        private void SavePass(string[] tips, int number)
        {
            var currentWorker = Entitie.ScheduleEntities4.GetContext().Workers.ToList();
            var worker = currentWorker.Where(p => p.FullName == tips[number]).FirstOrDefault();
            currentPass.IdWorker = worker.Id;

            currentPass.Date = DateTime.Today;

            var currentSchedule = Entitie.ScheduleEntities4.GetContext().Schedules.ToList();
            var schedule = currentSchedule.Where(p => p.Date == DateTime.Today && p.IdWorker == worker.Id).FirstOrDefault();
            currentPass.IdSchedule = schedule.Id;

            if (MessageBox.Show($"Подтверждаете что сотрудник {tips[number]} отсутствует на рабочем месте?", "Внимание!",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (currentPass.Id == 0)
                Entitie.ScheduleEntities4.GetContext().Passes.Add(currentPass);
                
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

            gridPasses.ItemsSource = Entitie.ScheduleEntities4.GetContext().Passes.ToList().Where(p => p.Date == date);
        }

        private void number11_Click(object sender, RoutedEventArgs e)
        {
            SavePass(tips1, 0);
            number11.Background = Brushes.Red;
        }

        private void number12_Click(object sender, RoutedEventArgs e)
        {
            SavePass(tips1, 1);
            number12.Background = Brushes.Red;
        }

        private void number13_Click(object sender, RoutedEventArgs e)
        {
            SavePass(tips1, 2);
            number13.Background = Brushes.Red;
        }

        private void number14_Click(object sender, RoutedEventArgs e)
        {
            SavePass(tips1, 3);
            number14.Background = Brushes.Red;
        }

        private void number18_Click(object sender, RoutedEventArgs e)
        {
            SavePass(tips1, 7);
            number18.Background = Brushes.Red;
        }

        private void number17_Click(object sender, RoutedEventArgs e)
        {
            SavePass(tips1, 6);
            number17.Background = Brushes.Red;
        }

        private void number16_Click(object sender, RoutedEventArgs e)
        {
            SavePass(tips1, 5);
            number16.Background = Brushes.Red;
        }

        private void number15_Click(object sender, RoutedEventArgs e)
        {
            SavePass(tips1, 4);
            number15.Background = Brushes.Red;
        }

        private void number21_Click(object sender, RoutedEventArgs e)
        {
            SavePass(tips2, 0);
            number21.Background = Brushes.Red;
        }

        private void number22_Click(object sender, RoutedEventArgs e)
        {
            SavePass(tips2, 1);
            number22.Background = Brushes.Red;
        }

        private void number24_Click(object sender, RoutedEventArgs e)
        {
            SavePass(tips2, 3);
            number24.Background = Brushes.Red;
        }

        private void number23_Click(object sender, RoutedEventArgs e)
        {
            SavePass(tips2, 2);
            number23.Background = Brushes.Red;
        }

        private void number33_Click(object sender, RoutedEventArgs e)
        {
            SavePass(tips3, 2);
            number33.Background = Brushes.Red;
        }

        private void number32_Click(object sender, RoutedEventArgs e)
        {
            SavePass(tips3, 1);
            number32.Background = Brushes.Red;
        }

        private void number31_Click(object sender, RoutedEventArgs e)
        {
            SavePass(tips3, 0);
            number31.Background = Brushes.Red;
        }

    }
}
