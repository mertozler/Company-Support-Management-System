using System;
using BackgroundJobs.Managers.DelayedJobs;

namespace BackgroundJobs.Schedules
{
    public static class DelayedJobs
    {
        [Obsolete]
        public static void SendMailForAssignedDemandAndNotAnswered(string employeeId, int demandId)
        {
            Hangfire.BackgroundJob.Schedule<DemandAssignEmployeeSchudeleJobManager>(
                job => job.Proccess(employeeId, demandId),
                TimeSpan.FromDays(3)
            );
        }
        
        public static void SendMailForDepartmentIfNotAnswered(int demandId)
        {
            Hangfire.BackgroundJob.Schedule<DemandDepartmentEmployeeSchudeleJobManager>(
                job => job.Proccess(demandId),
                TimeSpan.FromDays(5)
            );
        }
    }
}