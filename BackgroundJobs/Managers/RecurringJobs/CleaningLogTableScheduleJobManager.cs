using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;

namespace BackgroundJobs.Managers.RecurringJobs
{
    public class CleaningLogTableScheduleJobManager
    {
        private LogsManager _logsManager = new LogsManager(new EfLogsRepository());
        public void Proccess()
        {
            var list = _logsManager.GetList();
            foreach (var item in list)
            {
                _logsManager.TDelete(item);
            }
        }
    }
}