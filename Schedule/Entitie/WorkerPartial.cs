using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Entitie
{
    public partial class Worker
    {
        public string FullName
        {
            get
            {
                var fullName = $"{LastName} {FirstName}";
                if (string.IsNullOrWhiteSpace(Patronymic) == false)
                {
                    fullName += $" {Patronymic}";
                }

                return fullName;
            }
        }

        public int CountHours
        {
            get
            {
                var currentPass = ScheduleEntities4.GetContext().Passes.ToList();
                currentPass = currentPass.Where(p => p.IdWorker == Id).ToList();
                var currentSchedule = ScheduleEntities4.GetContext().Schedules.ToList();
                currentSchedule = currentSchedule.Where(p => p.IdWorker == Id).ToList();
                if(DateClass.startDate != null && DateClass.endDate != null)
                {
                   currentSchedule = currentSchedule.Where(p => p.Date >= DateClass.startDate && p.Date <= DateClass.endDate).ToList();
                }  
                int countHours = currentSchedule.Count() - currentPass.Count();
                countHours = countHours * 10;
                if (countHours < 0)
                    countHours = 0;
                return countHours;
            }
        }

        public int CountPass
        {
            get
            {
                var currentPass = ScheduleEntities4.GetContext().Passes.ToList();
                currentPass = currentPass.Where(p => p.IdWorker == Id).ToList();
                if (DateClass.startDate != null && DateClass.endDate != null)
                {
                    currentPass = currentPass.Where(p => p.Date >= DateClass.startDate && p.Date <= DateClass.endDate).ToList();
                }
                int countPass = currentPass.Count();
                countPass = countPass * 10;
                return countPass;
            }
        }
    }
}
