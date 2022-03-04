using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using BackgroundJobs.Schedules;
using BusinessLayer.Concrete;
using BusinessLayer.Util;
using CompanyPanelUI.Models.ChangeDemandServiceViewModel;
using CompanyPanelUI.Models.DemandListViewModel;
using CompanyPanelUI.Models.DemandReplyViewModel;
using CompanyPanelUI.Models.EmployeeDemandAssignmentViewModel;
using CompanyPanelUI.Models.EmployeeDemandDepartmentAssignmentViewModel;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CompanyPanelUI.Controllers
{
    [Authorize(Roles = "personel")]
    public class EmployeeController : Controller
    {
        private DemandManager _demandManager = new DemandManager(new EfDemandRepository());
        private readonly UserManager<CustomUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        DepartmentEmployeeManager departmentEmployeeManager =
            new DepartmentEmployeeManager(new EfDepartmentEmployeeRepository());

        private DepartmentManager _departmentManager = new DepartmentManager(new EfDepartmentRepository());

        private ServiceManager _serviceManager = new ServiceManager(new EfServiceRepository());

        private EmployeeServiceManager employeeServiceManager =
            new EmployeeServiceManager(new EfEmployeeServiceRepository());

        private DemandFileManager demandFileManager = new DemandFileManager(new EfDemandFileRepository());

        private DemandAnswerManager demandAnswerManager = new DemandAnswerManager(new EfDemandAnswerRepository());
        Context context = new Context();

        private EmployeeDemandManager _employeeDemandManager =
            new EmployeeDemandManager(new EfEmployeeDemandRepository());
        private readonly ILogger<EmployeeController> _logger;
        

        public EmployeeController(UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager,ILogger<EmployeeController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DemandList()
        {
            DemandListViewModel demandListViewModel = new DemandListViewModel();
            try
            {
var loggedUser = await _userManager.GetUserAsync(User);
            var assignedDemandForUser = _employeeDemandManager.GetDemandByEmployee(loggedUser.Id);
            var employeeDepartment = departmentEmployeeManager.GetDepartmentEmployeeByUserId(loggedUser.Id);
            var employeeServices = employeeServiceManager.GetEmployeeServiceByUserId(loggedUser.Id);
            List<Demand> employeeDemandFilterByAsignedStatusList = new List<Demand>();
            List<Demand> employeeDemandFilterByDepartmentList = new List<Demand>();
            List<Demand> employeeDemandFilterByServicesList = new List<Demand>();

            foreach (var item in assignedDemandForUser)
            {
                var employeeDemandFilterByAsignedStatus =
                    (from x in context.Demands where x.DemandId == item.DemandId select x).FirstOrDefault<Demand>();
                employeeDemandFilterByAsignedStatusList.Add(employeeDemandFilterByAsignedStatus);
            }


            foreach (var item in employeeDepartment)
            {
                var employeeDemandFilterByDepartment =
                    (from x in context.Demands where x.DepartmentId == item.DepartmentId select x)
                    .ToList();
                foreach (var data in employeeDemandFilterByDepartment)
                {
                    employeeDemandFilterByDepartmentList.Add(data);
                }
            }


            foreach (var item in employeeServices)
            {
                var employeeDemandFilterByServices =
                    (from x in context.Demands where x.ServiceId == item.ServiceId select x).ToList();
                if (employeeDemandFilterByServices != null)
                {
                    foreach (var data in employeeDemandFilterByServices)
                    {
                        employeeDemandFilterByServicesList.Add(data);
                    }
                }
            }

            
            List<Service> employeeServiceList = new List<Service>();
            foreach (var item  in employeeServices)
            {
                Service data = new Service();
                data = _serviceManager.TGetById(item.ServiceId);
                employeeServiceList.Add(data);
            }

            demandListViewModel.Services = employeeServiceList;
            demandListViewModel.DemandAssignedForUser = employeeDemandFilterByAsignedStatusList;
            demandListViewModel.DemandServicesForUser = employeeDemandFilterByServicesList;
            demandListViewModel.DemandDepartmentForUser = employeeDemandFilterByDepartmentList;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            
            return View(demandListViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DemandList(DemandListViewModel postData)
        {
            DemandListViewModel newData = new DemandListViewModel();
            try
            {
            var demands = await context.Demands.ToListAsync();
            
            var loggedUser = await _userManager.GetUserAsync(User);
            var assignedDemandForUser = _employeeDemandManager.GetDemandByEmployee(loggedUser.Id);
            var employeeDepartment = departmentEmployeeManager.GetDepartmentEmployeeByUserId(loggedUser.Id);
            var employeeServices = employeeServiceManager.GetEmployeeServiceByUserId(loggedUser.Id);
            List<Demand> employeeDemandFilterByAsignedStatusList = new List<Demand>();
            List<Demand> employeeDemandFilterByDepartmentList = new List<Demand>();
            List<Demand> employeeDemandFilterByServicesList = new List<Demand>();
            
            
            foreach (var item in assignedDemandForUser)
            {
                var employeeDemandFilterByAsignedStatus =
                    (from x in context.Demands where x.DemandId == item.DemandId select x).FirstOrDefault<Demand>();
                employeeDemandFilterByAsignedStatusList.Add(employeeDemandFilterByAsignedStatus);
            }


            foreach (var item in employeeDepartment)
            {
                var employeeDemandFilterByDepartment =
                    (from x in context.Demands where x.DepartmentId == item.DepartmentId select x)
                    .ToList();
                foreach (var data in employeeDemandFilterByDepartment)
                {
                    employeeDemandFilterByDepartmentList.Add(data);
                }
            }


            foreach (var item in employeeServices)
            {
                var employeeDemandFilterByServices =
                    (from x in context.Demands where x.ServiceId == item.ServiceId select x).ToList();
                if (employeeDemandFilterByServices != null)
                {
                    foreach (var data in employeeDemandFilterByServices)
                    {
                        employeeDemandFilterByServicesList.Add(data);
                    }
                }
            }
            
            if (postData != null)
            {
                if (postData.StartDate.HasValue)
                {
                    employeeDemandFilterByAsignedStatusList = employeeDemandFilterByAsignedStatusList.Where(x => x.DemandCreateTime.Date >= postData.StartDate.Value.Date).ToList();
                    employeeDemandFilterByDepartmentList = employeeDemandFilterByDepartmentList.Where(x => x.DemandCreateTime.Date >= postData.StartDate.Value.Date).ToList();
                    employeeDemandFilterByServicesList = employeeDemandFilterByServicesList.Where(x => x.DemandCreateTime.Date >= postData.StartDate.Value.Date).ToList();
                }

                if (postData.EndDate.HasValue)
                {
                    
                    employeeDemandFilterByAsignedStatusList = employeeDemandFilterByAsignedStatusList.Where(x => x.DemandCreateTime.Date <= postData.EndDate.Value.Date).ToList();
                    employeeDemandFilterByDepartmentList = employeeDemandFilterByDepartmentList.Where(x => x.DemandCreateTime.Date <= postData.EndDate.Value.Date).ToList();
                    employeeDemandFilterByServicesList = employeeDemandFilterByServicesList.Where(x => x.DemandCreateTime.Date <= postData.EndDate.Value.Date).ToList();
                }

                if (postData.ServiceId.HasValue)
                {
                    
                    employeeDemandFilterByAsignedStatusList = employeeDemandFilterByAsignedStatusList.Where(x => x.ServiceId == postData.ServiceId).ToList();
                    employeeDemandFilterByDepartmentList = employeeDemandFilterByDepartmentList.Where(x => x.ServiceId == postData.ServiceId).ToList();
                    employeeDemandFilterByServicesList = employeeDemandFilterByServicesList.Where(x => x.ServiceId == postData.ServiceId).ToList();
                }

                if (postData.CevaplanmamisTalep == false || postData.Yanitlanmistalep == false)
                {
                    if (postData.CevaplanmamisTalep == true)
                    {
                        
                        employeeDemandFilterByAsignedStatusList = employeeDemandFilterByAsignedStatusList.Where(x => x.DemandStatus == true).ToList();
                        employeeDemandFilterByDepartmentList = employeeDemandFilterByDepartmentList.Where(x => x.DemandStatus == true).ToList();
                        employeeDemandFilterByServicesList = employeeDemandFilterByServicesList.Where(x => x.DemandStatus == true).ToList();
                    }

                    if (postData.Yanitlanmistalep == true)
                    {
                        
                        employeeDemandFilterByAsignedStatusList = employeeDemandFilterByAsignedStatusList.Where(x => x.DemandStatus == false).ToList();
                        employeeDemandFilterByDepartmentList = employeeDemandFilterByDepartmentList.Where(x => x.DemandStatus == false).ToList();
                        employeeDemandFilterByServicesList = employeeDemandFilterByServicesList.Where(x => x.DemandStatus == false).ToList();
                    }
                }
            }

            newData.Services = _serviceManager.GetList();
            newData.DemandAssignedForUser = employeeDemandFilterByAsignedStatusList;
            newData.DemandServicesForUser = employeeDemandFilterByServicesList;
            newData.DemandDepartmentForUser = employeeDemandFilterByDepartmentList;
            

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            
            
            return View(newData);
        }

        [HttpGet]
        public async Task<IActionResult> DemandAnswerUser(int id)
        {
            ViewBag.id = id;
            DemandReplyViewModel model = new DemandReplyViewModel();
            try
            {
var demandFiles = demandFileManager.GetFileByDemandId(id);
            var demand = _demandManager.GetDemandById(id);
            var user = await _userManager.FindByIdAsync(demand[0].UserId.ToString());
            var demandAnswer = demandAnswerManager.GetDemandAnswerByDemandId(id);
            var employeeDemands = _employeeDemandManager.GetDemandByDemandId(id);
            
            if (demand[0].DemandReadTime == null)
            {
                var updatedDemand = demand[0];
                updatedDemand.DemandReadTime = DateTime.Now;
                _demandManager.TUpdate(updatedDemand);
                
            }
            
            var demandHistory = DemandHistory(employeeDemands);

            List<DemandAndAnswerWm> demandandanswermodel = new List<DemandAndAnswerWm>();
            if (demandAnswer.Count == 0)
            {
                demandandanswermodel.Add(new DemandAndAnswerWm
                {
                    EmployeeHistory = await demandHistory, DemandFilePath = demandFiles, UserId = demand[0].UserId,
                    DemandId = demand[0].DemandId, DemandTitle = demand[0].DemandTitle, ServiceId = demand[0].ServiceId,
                    DemandContent = demand[0].DemandContent, DemantCreateDate = demand[0].DemandCreateTime,
                    DemandStatus = demand[0].DemandStatus, UserName = user.NameSurname
                });
            }
            else
            {
                demandandanswermodel.Add(new DemandAndAnswerWm
                {
                    EmployeeHistory = await demandHistory, DemandFilePath = demandFiles, UserId = demand[0].UserId,
                    DemandId = demand[0].DemandId, DemandTitle = demand[0].DemandTitle, ServiceId = demand[0].ServiceId,
                    DemandContent = demand[0].DemandContent, DemantCreateDate = demand[0].DemandCreateTime,
                    DemandStatus = demand[0].DemandStatus, UserName = user.NameSurname, DemandAnswer = demandAnswer
                });
            }


            model.DemandAndAnswerWmList = demandandanswermodel;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            
            return View(model);
        }

        [HttpPost]
        public IActionResult DemandAnswerUser(DemandReplyViewModel data)
        {
            DemandAnswer demandAnswer = new DemandAnswer();
            EmployeeDemand employeeDemand = new EmployeeDemand();
            try
            {
                demandAnswer.DemandId = data.DemandIdNew;
                employeeDemand.DemandId = data.DemandIdNew;
                employeeDemand.EmployeeId = data.EmployeeID;
                demandAnswer.Answer = data.DemandAnswerNew;
                demandAnswer.DemandAnswerDate = DateTime.Now;
                demandAnswer.DemandAnswerType = 2;
                demandAnswerManager.TAdd(demandAnswer);
                var isExist = _employeeDemandManager.GetDemandByDemandId(data.DemandIdNew);
                if (isExist.Count != 0)
                {
                    _employeeDemandManager.TUpdate(isExist[0]);
                }
                else
                {
                    _employeeDemandManager.TAdd(employeeDemand);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            

            return RedirectToAction("DemandAnswerUser", "Employee");
        }

        [HttpGet]
        public IActionResult ChangeDemandService(int id)
        {
            ChangeDemandServiceViewModel model = new ChangeDemandServiceViewModel();
            try
            {
                model.Demand = _demandManager.GetDemandById(id);
                model.Demand[0].Service = _serviceManager.TGetById(model.Demand[0].ServiceId);
                model.Service = _serviceManager.GetList();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
           
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeDemandService(ChangeDemandServiceViewModel data)
        {
            SendMail sendMail = new SendMail();
            
            ChangeDemandServiceViewModel model = new ChangeDemandServiceViewModel();
            try
            {
                var demand = _demandManager.GetDemandById(data.demandId)[0];
                demand.ServiceId = data.serviceId;
                _demandManager.TUpdate(demand);
                model.Demand = _demandManager.GetDemandById(data.demandId);
                model.Demand[0].Service = _serviceManager.TGetById(model.Demand[0].ServiceId);
                model.Service = _serviceManager.GetList();
                var employee = await _userManager.GetUsersInRoleAsync("personel");
                var employeeServices = employeeServiceManager.GetListByServiceId(data.serviceId);
                foreach (var employeeService in employeeServices)
                {
                    foreach (var item in employee)
                    {
                        if (employeeService.Id == item.Id)
                        {
                            sendMail.SendMailForService(item.NameSurname, item.Email, model.Demand[0].Service.ServiceName,
                                model.Demand[0].DemandTitle);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            


            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeDemandAssignment(int id)
        {
            EmployeeDemandAssignmentViewModel model = new EmployeeDemandAssignmentViewModel();
            try
            {
                var selectedDemand = _demandManager.GetDemandById(id);
                var currentEmployee = await _userManager.GetUserAsync(User);
                List<string> employeeUserId = new List<string>();
                var currentEmployeeDepartment = departmentEmployeeManager.GetDepartmentEmployeeByUserId(currentEmployee.Id);
                var currentEmployeServices = employeeServiceManager.GetEmployeeServiceByUserId(currentEmployee.Id);

                var usersThatCanBeAssignedForDepartmetn =
                    departmentEmployeeManager.GetUserIdByDepartmentId(currentEmployeeDepartment[0].DepartmentId);
                var usersThatCanBeAssignedForService =
                    employeeServiceManager.GetUserIdByServiceId(currentEmployeServices[0].ServiceId);

                foreach (var item in usersThatCanBeAssignedForDepartmetn)
                {
                    if (item.Id != currentEmployee.Id)
                    {
                        employeeUserId.Add(item.Id);
                    }
                }

                foreach (var item in usersThatCanBeAssignedForService)
                {
                    if (item.Id != currentEmployee.Id)
                    {
                        if (!employeeUserId.Contains(item.Id))
                        {
                            employeeUserId.Add(item.Id);
                        }
                    }
                }

                var employees = await _userManager.GetUsersInRoleAsync("personel");
                IList<CustomUser> EmployesThatCanBeAssigned = new List<CustomUser>();
                foreach (var item in employees)
                {
                    foreach (var employeeId in employeeUserId)
                    {
                        if (item.Id == employeeId)
                        {
                            var getUser = await _userManager.FindByIdAsync(item.Id);
                            EmployesThatCanBeAssigned.Add(getUser);
                        }
                    }
                }

                model.Employees = EmployesThatCanBeAssigned;
                model.Demand = selectedDemand;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            
            return View(model);
        }

        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> EmployeeDemandAssignment(EmployeeDemandAssignmentViewModel data)
        {
            EmployeeDemand employeeDemand = new EmployeeDemand();
            SendMail sendMail = new SendMail();
            try
            {
                var isDemandExistinEmployeeDemand = _employeeDemandManager.GetDemandByDemandId(data.demandId);
                employeeDemand.DemandId = data.demandId;
                employeeDemand.EmployeeId = data.employeeId;
                employeeDemand.CreateTime = DateTime.Now;
                if (isDemandExistinEmployeeDemand.Count != 0)
                {
                    _employeeDemandManager.TDelete(isDemandExistinEmployeeDemand[0]);
                    _employeeDemandManager.TAdd(employeeDemand);
                }
                else
                {
                    _employeeDemandManager.TAdd(employeeDemand);
                }
                DelayedJobs.SendMailForAssignedDemandAndNotAnswered(data.employeeId,data.demandId);

                var user = await _userManager.FindByIdAsync(data.employeeId);
                var demand = _demandManager.GetDemandById(data.demandId)[0];
                sendMail.SendMailForEmployee(user.NameSurname, user.Email, demand.DemandTitle);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            
            return RedirectToAction("DemandList", "Employee");
        }

        [HttpGet]
        public IActionResult EmployeeDemandDepartmentAssignment(int id)
        {
            EmployeeDemandDepartmentAssignmentViewModel model = new EmployeeDemandDepartmentAssignmentViewModel();
            try
            {
                var seletectedDemand = _demandManager.GetDemandById(id);
                seletectedDemand[0].Department = _departmentManager.GetDepartmentById(seletectedDemand[0].DepartmentId);
                model.Demand = seletectedDemand;
                model.Department = _departmentManager.GetList();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeDemandDepartmentAssignment(
            EmployeeDemandDepartmentAssignmentViewModel data)
        {
            try
            {
                SendMail sendMail = new SendMail();
                var seletectedDemand = _demandManager.GetDemandById(data.demandId)[0];
                seletectedDemand.DepartmentId = data.departmentId;
                _demandManager.TUpdate(seletectedDemand);
                var employee = await _userManager.GetUsersInRoleAsync("personel");
                var employeeDepartment = departmentEmployeeManager.GetList();
                foreach (var employeeDepartments in employeeDepartment)
                {
                    foreach (var item in employee)
                    {
                        if (employeeDepartments.Id == item.Id)
                        {
                            var department = _departmentManager.GetDepartmentById(employeeDepartments.DepartmentId);
                            sendMail.SendMailForDepartment(item.NameSurname, item.Email, department.DepartmentName,
                                seletectedDemand.DemandTitle);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
           

            return RedirectToAction("EmployeeDemandDepartmentAssignment", "Employee");
        }

        public IActionResult ChangeDemandStatus(int id)
        {
            try
            {
                var selectedDemand = _demandManager.TGetById(id);
                selectedDemand.DemandStatus = false;
                _demandManager.ChangeStatusValue(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
           
            return RedirectToAction("DemandList", "Employee");
        }

        public async Task<List<EmployeeHistory>> DemandHistory(IEnumerable<EmployeeDemand> employeeDemands)
        {
            List<EmployeeHistory> demandHistory = new List<EmployeeHistory>();
            try
            {
                string beforeUserControl = null;
                foreach (var item in employeeDemands)
                {
                    if (beforeUserControl == null)
                    {
                        beforeUserControl = item.EmployeeId;
                    }

                    if (beforeUserControl != item.EmployeeId)
                    {
                        var afterUser = await _userManager.FindByIdAsync(item.EmployeeId);
                        var beforeUser = await _userManager.FindByIdAsync(beforeUserControl);
                        EmployeeHistory history = new EmployeeHistory();
                        history.AfterEmployee = afterUser.NameSurname;
                        history.BeforeEmployee = beforeUser.NameSurname;
                        history.ChangeDate = item.CreateTime;
                        demandHistory.Add(history);
                        beforeUserControl = item.EmployeeId;
                    }
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            
            return demandHistory;
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}