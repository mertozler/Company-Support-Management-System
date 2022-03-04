using System;
using BackgroundJobs.Managers.RecurringJobs;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Hangfire;

namespace BackgroundJobs.Schedules
{
    public class RecurringJobs
    {
        
        private static SettingManager _settingManager = new SettingManager(new EfSettingRepository());
        [Obsolete]
        public static void DeleteLogTable()
        {
            var currentDay = DateTime.Today.Day;
            var deleteLogSetting = _settingManager.TGetById(1);
            var cronnedDay = currentDay + Convert.ToInt16(deleteLogSetting.SettingValue);
            
            string CronOperation = deleteLogSetting.SettingValue + " 13 * * *";
            RecurringJob.RemoveIfExists(nameof(CleaningLogTableScheduleJobManager));
            RecurringJob.AddOrUpdate<CleaningLogTableScheduleJobManager>(nameof(CleaningLogTableScheduleJobManager),
                job => job.Proccess(),
                Cron.DayInterval(cronnedDay),
                TimeZoneInfo.Local
                );
        }
    }
}