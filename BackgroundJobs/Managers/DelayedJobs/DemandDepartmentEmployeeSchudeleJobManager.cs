using System.Threading.Tasks;
using BusinessLayer.Concrete;
using BusinessLayer.Util;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;

namespace BackgroundJobs.Managers.DelayedJobs
{
    public class DemandDepartmentEmployeeSchudeleJobManager
    {
        private DemandAnswerManager _demandAnswerManager = new DemandAnswerManager(new EfDemandAnswerRepository());
        private DemandManager _demandManager = new DemandManager(new EfDemandRepository());
        private DepartmentManager _departmentManager = new DepartmentManager(new EfDepartmentRepository());

        private DepartmentEmployeeManager _departmentEmployeeManager =
            new DepartmentEmployeeManager(new EfDepartmentEmployeeRepository());
        private readonly UserManager<CustomUser> _userManager;

        public DemandDepartmentEmployeeSchudeleJobManager(UserManager<CustomUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task Proccess(int demandId)
        {
            SendMail sendMail = new SendMail();
            var isDemandAnswerExist = _demandAnswerManager.GetDemandAnswerByDemandId(demandId);
            var selectedDemand = _demandManager.TGetById(demandId);
            var selectedEmployeeListBySelectedDepartment =
                _departmentEmployeeManager.GetUserIdByDepartmentId(selectedDemand.DepartmentId);
            if (isDemandAnswerExist.Count == 0)
            {
                foreach (var item in selectedEmployeeListBySelectedDepartment)
                {
                    var selectedUser = await _userManager.FindByIdAsync(item.Id);
                    sendMail.SendMailForDepartmentNotAnswered(selectedUser.NameSurname,selectedUser.Email,selectedDemand.DemandTitle);
                }
            }
        }
    }
}