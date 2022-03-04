using System.Threading.Tasks;
using BusinessLayer.Concrete;
using BusinessLayer.Util;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;

namespace BackgroundJobs.Managers.DelayedJobs
{
    public class DemandAssignEmployeeSchudeleJobManager
    {
        private DemandAnswerManager _demandAnswerManager = new DemandAnswerManager(new EfDemandAnswerRepository());
        private DemandManager _demandManager = new DemandManager(new EfDemandRepository());
        private readonly UserManager<CustomUser> _userManager;

        public DemandAssignEmployeeSchudeleJobManager(UserManager<CustomUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task Proccess(string employeeId,int demandId)
        {
            SendMail sendMail = new SendMail();
            var selectedEmployee = await _userManager.FindByIdAsync(employeeId);
            var isDemandAnswerExist = _demandAnswerManager.GetDemandAnswerByDemandId(demandId);
            var selectedDemand = _demandManager.TGetById(demandId);
            if (isDemandAnswerExist.Count == 0)
            {
                sendMail.SendMailForEmployeeAnswerReminder(selectedEmployee.NameSurname,selectedEmployee.Email,selectedDemand.DemandTitle);
            }
        }
    }
}