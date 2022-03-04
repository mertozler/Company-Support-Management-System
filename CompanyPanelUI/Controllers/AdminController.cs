using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using CompanyPanelUI.Data;
using CompanyPanelUI.DTOS;
using CompanyPanelUI.Models.DemandListViewModel;
using CompanyPanelUI.Models.DemandReplyViewModel;
using CompanyPanelUI.Models.GetFirmViewModel;
using CompanyPanelUI.Models.ServiceViewModel;
using CompanyPanelUI.Models.UserEditViewModel;
using CompanyPanelUI.Models.UserListViewModel;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackgroundJobs.Schedules;
using BusinessLayer.Util;
using CompanyPanelUI.Models.DepartmentRegisterForEmployeeViewModel;
using CompanyPanelUI.Models.EmployeeListViewModel;
using CompanyPanelUI.Models.ServiceListViewModel;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using X.PagedList;

namespace CompanyPanelUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        FirmManager firmManager = new FirmManager(new EfFirmsRepository());
        private readonly UserManager<CustomUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        UserRegisterManager userRegisterManager = new UserRegisterManager(new EfUserRepository());
        ServiceManager serviceManager = new ServiceManager(new EfServiceRepository());
        FirmServiceManager firmServiceManager = new FirmServiceManager(new EfFirmServiceRepository());
        DemandManager demandManager = new DemandManager(new EfDemandRepository());
        DemandAnswerManager demandAnswerManager = new DemandAnswerManager(new EfDemandAnswerRepository());
        DemandFileManager demandFileManager = new DemandFileManager(new EfDemandFileRepository());
        DepartmentManager departmentManager = new DepartmentManager(new EfDepartmentRepository());
        private EmployeeDemandManager _employeeDemandManager =
            new EmployeeDemandManager(new EfEmployeeDemandRepository());

        DepartmentEmployeeManager departmentEmployeeManager =
            new DepartmentEmployeeManager(new EfDepartmentEmployeeRepository());

        private EmployeeServiceManager employeeServiceManager =
            new EmployeeServiceManager(new EfEmployeeServiceRepository());

        private ServiceDepartmentManager _serviceDepartmentManager =
            new ServiceDepartmentManager(new EfServiceDepartmentRepository()); 
        
        Context context = new Context();
        private readonly ILogger<AdminController> _logger;

        private LogsManager _logsManager = new LogsManager(new EfLogsRepository());
        private SettingManager _settingManager = new SettingManager(new EfSettingRepository());
        

        

        public AdminController(UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context,ILogger<AdminController> logger)
        {
            _userManager = userManager;
            this._roleManager = roleManager;
            _context = context;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Firms()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Firms(FirmsDTO data)
        {
            try
            {

                FirmValidator firmValidator = new FirmValidator();
                var firmList = firmManager.GetFirmByMail(data.FirmMail);
                if (firmList.Count == 0)
                {
                    Firm newFirm = new Firm();
                    newFirm.FirmName = data.FirmName;
                    newFirm.FirmTaxNo = data.FirmTaxNo;
                    newFirm.FirmTelNo = "+90" + data.FirmTelNo;
                    newFirm.FirmMail = data.FirmMail;
                    newFirm.FirmStatus = true;
                    ValidationResult results = firmValidator.Validate(newFirm);
                    if (results.IsValid)
                    {
                        firmManager.TAdd(newFirm);
                        return RedirectToAction("FirmList", "Admin");
                    }
                    else
                    {
                        foreach (var item in results.Errors)
                        {
                            ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                        }
                    }

                }
                else
                {
                    ModelState.AddModelError("FirmMail",
                        "Bu mail başka bir firma tarafından kullanılmaktadır, lütfen kontrol ediniz.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return View();

        }

        public IActionResult FirmList()
        {
            List<Firm> list = new List<Firm>();
            try
            {
                list = firmManager.GetList();
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            return View(list);
        }

        public IActionResult UserApplicationConfirm()
        {
            List<User> users = new List<User>();
            try
            {
                var list = userRegisterManager.GetList();
                foreach (var user in list)
                {
                    if (user.UserStatus==true)
                    {
                        users.Add(user);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            return View(users); 
        }
        [HttpGet]
        public IActionResult UserRegister(int id)
        {
            var roles = _roleManager.Roles;
            UserRegisterModel getSelectedUser = new UserRegisterModel();
            List<IdentityRole > newList = new List<IdentityRole>();
            try
            {
                foreach (var item in roles)
                {
                    newList.Add(item);
                }
            
                var user = userRegisterManager.GetUserById(id);
                var firmList = firmManager.GetList();
                getSelectedUser.Firm = firmList;
                getSelectedUser.User = user;
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return View(getSelectedUser);
        }
        [HttpPost]
        public async Task<IActionResult> UserRegister(UserRegisterModel data)
        {
            SendMail sendMail = new SendMail();
            UserRegisterModel getSelectedUser = new UserRegisterModel();
            try
            {
                var getCurrentUnconfirmedUserData = userRegisterManager.GetUserById(data.UserId);
                var user = new CustomUser {PhoneNumber = data.UserPhone , UserName = getCurrentUnconfirmedUserData[0].UserMail, Email = getCurrentUnconfirmedUserData[0].UserMail, NameSurname = getCurrentUnconfirmedUserData[0].UserNameSurname, FirmId = data.FirmId };
                var result = await _userManager.CreateAsync(user, getCurrentUnconfirmedUserData[0].UserPassword);
                if (result.Succeeded)
                {
                    var roleresult = await _userManager.AddToRoleAsync(user, "musteri");
                    if(roleresult.Succeeded)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationLink = Url.Action("Confirm", "Login", new { token, email = user.Email }, Request.Scheme);
                        bool emailResponse = sendMail.SendEmailForConfirmMail(user.Email, confirmationLink);

                        if (emailResponse)
                            getCurrentUnconfirmedUserData[0].UserStatus = false;
                        userRegisterManager.TUpdate(getCurrentUnconfirmedUserData[0]);
                        userRegisterManager.ChangeStatusValue(data.UserId);
                        return RedirectToAction("UserList", "Admin");
                        
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                
                var firmList = firmManager.GetList();
                var currentUser = userRegisterManager.GetUserById(getCurrentUnconfirmedUserData[0].UserId);
                getSelectedUser.User = currentUser;
                getSelectedUser.Firm = firmList;
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return View(getSelectedUser);
        }
        public async Task<IActionResult> UserList()
        {
            UserListViewModel userListModel = new UserListViewModel();
            List<UserWm> Users = new List<UserWm>();
            try
            {
                var UserinEmployeeRole = await (from user in _context.Users
                    join userRole in _context.UserRoles
                        on user.Id equals userRole.UserId
                    join role in _context.Roles
                        on userRole.RoleId equals role.Id
                    where role.Name == "musteri"
                    select user).ToListAsync();
            
            
                foreach (var item in UserinEmployeeRole)
                {
                    if(item.FirmId !=null)
                    {

                        var value = firmManager.TGetById(Convert.ToInt32(item.FirmId));
                        Users.Add(new UserWm { UserId = item.Id, UserNameSurname=item.NameSurname, UserMail = item.Email, UserPhone = item.PhoneNumber, Firm = value });
                    }
                    else
                    {
                        Users.Add(new UserWm { UserId = item.Id, UserNameSurname=item.NameSurname, UserMail = item.Email, UserPhone = item.PhoneNumber, Firm = null });

                    }
                
                }
                userListModel.Users = Users;
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return View(userListModel);
        }

        public IActionResult ServiceDefine()
        {
            List<Firm> list = new List<Firm>();
            try
            {
                list = firmManager.GetList();
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return View(list);
        }

        [HttpGet]
        public IActionResult CreateService()
        {
            ServicesDTO servicesDto = new ServicesDTO();
            try
            {
                var departments = departmentManager.GetList();
                
                servicesDto.Departments = departments;
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
           
            return View(servicesDto);
        }

        [HttpPost]
        public IActionResult CreateService(ServicesDTO service)
        {
            Service newService = new Service();
            ServiceDepartment newServiceDepartment = new ServiceDepartment();
            ServiceValidator serviceValidator = new ServiceValidator();
            try
            {
                if (service.departmentId != 0)
                {
                    
                
                newService.ServiceName = service.ServiceName;
                newService.ServiceAbout = service.ServiceAbout;
            
           
                ValidationResult results = serviceValidator.Validate(newService);
                if (results.IsValid)
                {
                    serviceManager.TAdd(newService);
                    newServiceDepartment.DepartmentId = service.departmentId;
                    newServiceDepartment.ServiceId = serviceManager.TGetById(newService.ServiceId).ServiceId;
                    _serviceDepartmentManager.TAdd(newServiceDepartment);
                    return RedirectToAction("ServiceList", "Admin");
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
                }
                else
                {
                    ModelState.AddModelError("departmentId", "Bir departmen seçmediniz, lütfen departman kaydı olup olmadığından emin olun.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }

            ServicesDTO servicesDto = new ServicesDTO();
            var departments = departmentManager.GetList();
                
            servicesDto.Departments = departments;
            
            return View(servicesDto);
        }

        public IActionResult ServiceList()
        {
            ServiceListViewModel newServiceListViewModel = new ServiceListViewModel();
            List<ServiceListClass> serviceListClas = new List<ServiceListClass>();
           
            List<Department> departments = new List<Department>();
            try
            {
                var serviceList = serviceManager.GetList();
                foreach (var item in serviceList)
                {
                    ServiceListClass newServiceListClass = new ServiceListClass();
                    var selectedServiceDepartmentId = _serviceDepartmentManager.GetDepartmentIdByServiceId(item.ServiceId).DepartmentId;
                    var department = departmentManager.GetDepartmentById(selectedServiceDepartmentId);
                    newServiceListClass.ServiceId = item.ServiceId;
                    newServiceListClass.ServiceName = item.ServiceName;
                    newServiceListClass.ServiceAbout = item.ServiceAbout;
                    newServiceListClass.DepartmentName = department.DepartmentName;
                    serviceListClas.Add(newServiceListClass);
                }

                newServiceListViewModel.ServiceListClasses = serviceListClas;
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return View(newServiceListViewModel);
        }

        [HttpGet]
        public IActionResult ServiceDefineForFirm(int id)
        {
            ServiceViewModel serviceViewModel = new ServiceViewModel();
            try
            {
                var firm = firmManager.GetFirmById(id);
                var serviceList = serviceManager.GetList();
                serviceViewModel.Firms = firm;
                serviceViewModel.Services = serviceList;
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return View(serviceViewModel);
        }
        [HttpPost]
        public IActionResult ServiceDefineForFirm(ServiceViewModel data,IFormCollection formVal)
        {
            var selectedServiceList = formVal["ServiceId"][0].Split(",");
            ServiceViewModel returnValue = new ServiceViewModel();
            try
            {
                for (int i = 0; i < selectedServiceList.Length - 1; i++)
                {
                    FirmService newFirmService = new FirmService();
                    newFirmService.FirmId = data.FirmId;
                    newFirmService.ServiceId = Convert.ToInt32(selectedServiceList[i]);
                    newFirmService.FirmServiceCreateDate = DateTime.Now;
                    firmServiceManager.TAdd(newFirmService);
                }
                returnValue.Firms = firmManager.GetFirmById(data.FirmId);
                returnValue.Services = serviceManager.GetList();
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return RedirectToAction("FirmList", "Admin");
        }
        [HttpGet]
        public IActionResult DemandList()
        {
            DemandListViewModel demandListViewModel = new DemandListViewModel();
            List<Demand> allDemands = new List<Demand>();
            try
            {
                allDemands = demandManager.GetList();
                allDemands = allDemands.AsEnumerable().OrderByDescending(x => x.DemandCreateTime).ToList();
                demandListViewModel.Demands = allDemands;
                demandListViewModel.Services = serviceManager.GetList();
                demandListViewModel.Yanitlanmistalep = false;
                demandListViewModel.CevaplanmamisTalep = false;
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return View(demandListViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DemandList(DemandListViewModel data)
        {
            DemandListViewModel newData = new DemandListViewModel();
            try
            {
                var demands = await context.Demands.ToListAsync();
           
            

                if (data !=null)
                {
                    if(data.StartDate.HasValue)
                    {
                        demands = demands.Where(x => x.DemandCreateTime.Date >= data.StartDate.Value.Date).ToList();
                    
                    }
                    if (data.EndDate.HasValue)
                    {
                        demands = demands.Where(x => x.DemandCreateTime.Date <= data.EndDate.Value.Date).ToList();
                    }
                    if(data.ServiceId.HasValue)
                    {
                        demands = demands.Where(x => x.ServiceId == data.ServiceId).ToList();
                    }
                    if(data.CevaplanmamisTalep == false || data.Yanitlanmistalep==false)
                    {

                        if (data.CevaplanmamisTalep == true)
                        {
                            demands = demands.Where(x => x.DemandStatus == true).ToList();
                        }
                        if (data.Yanitlanmistalep == true)
                        {
                            demands = demands.Where(x => x.DemandStatus == false).ToList();
                        }
                    }
                
                    newData.Demands = demands;
                    newData.Services = serviceManager.GetList();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return View(newData);
        }

        [HttpGet]
        public async Task<IActionResult> DemandAnswerUser(int id)
        {
            DemandReplyViewModel model = new DemandReplyViewModel();
            try
            {
                ViewBag.id = id;
                
                var demandFiles = demandFileManager.GetFileByDemandId(id);
                var demand = demandManager.GetDemandById(id);
                var user =  await _userManager.FindByIdAsync(demand[0].UserId.ToString());
                var demandAnswer = demandAnswerManager.GetDemandAnswerByDemandId(id);
                List<DemandAndAnswerWm> demandandanswermodel = new List<DemandAndAnswerWm>();
                if(demandAnswer.Count==0)
                {
                    demandandanswermodel.Add(new DemandAndAnswerWm { DemandFilePath = demandFiles, UserId = demand[0].UserId, DemandId = demand[0].DemandId, DemandTitle= demand[0].DemandTitle, ServiceId= demand[0].ServiceId, DemandContent= demand[0].DemandContent, DemantCreateDate = demand[0].DemandCreateTime, DemandStatus = demand[0].DemandStatus, UserName = user.NameSurname});
                }
                else
                {
                    demandandanswermodel.Add(new DemandAndAnswerWm { DemandFilePath = demandFiles, UserId = demand[0].UserId, DemandId = demand[0].DemandId, DemandTitle = demand[0].DemandTitle, ServiceId = demand[0].ServiceId, DemandContent = demand[0].DemandContent, DemantCreateDate = demand[0].DemandCreateTime, DemandStatus = demand[0].DemandStatus, UserName = user.NameSurname, DemandAnswer = demandAnswer });
                }
            

                model.DemandAndAnswerWmList = demandandanswermodel;
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return View(model);
        }

        [HttpPost]
        public IActionResult DemandAnswerUser(DemandReplyViewModel data)
        {
            
            ServiceViewModel returnValue = new ServiceViewModel();
            DemandAnswer demandAnswer = new DemandAnswer();
            try
            {
                demandAnswer.DemandId = data.DemandIdNew;
                demandAnswer.Answer = data.DemandAnswerNew;
                demandAnswer.DemandAnswerDate = DateTime.Now;
                demandAnswer.DemandAnswerType = 2;
                demandAnswerManager.TAdd(demandAnswer);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return RedirectToAction("DemandAnswerUser", "Admin");
        }

        [HttpGet]
        public IActionResult GetFirm(int id)
        {
            GetFirmViewModel getFirmViewModel = new GetFirmViewModel();
            List<CustomUser> userlist = new List<CustomUser>();
            try
            {
                getFirmViewModel.firmService = firmServiceManager.GetServiceByFirmId(id);
                var list = _userManager.Users;
                getFirmViewModel.firm = firmManager.GetFirmById(id)[0];
                foreach (var item in list)
                {
                    if (item.FirmId == id)
                    {
                        userlist.Add(item);
                    }
                }
                getFirmViewModel.firmUsers = userlist;
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return View(getFirmViewModel);
        }

        [HttpPost]
        public IActionResult GetFirm(GetFirmViewModel data)
        {
            FirmValidator firmValidator = new FirmValidator();
            GetFirmViewModel getFirmViewModel = new GetFirmViewModel();
            try
            {
                var selectedFirm = firmManager.GetFirmById(data.firm.FirmId)[0];
                selectedFirm.FirmName = data.firm.FirmName;
                selectedFirm.FirmTaxNo = data.firm.FirmTaxNo;
                selectedFirm.FirmTelNo = data.firm.FirmTelNo;
                selectedFirm.FirmMail = data.firm.FirmMail;
                selectedFirm.FirmStatus = true;
                ValidationResult results = firmValidator.Validate(selectedFirm);
                
                List<CustomUser> userlist = new List<CustomUser>();
                if (results.IsValid)
                {
                    firmManager.TUpdate(selectedFirm);
                    return RedirectToAction("GetFirm", "Admin", new { @id = selectedFirm.FirmId });
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
                getFirmViewModel.firmService = firmServiceManager.GetServiceByFirmId(selectedFirm.FirmId);
                var list = _userManager.Users;
                getFirmViewModel.firm = firmManager.GetFirmById(selectedFirm.FirmId)[0];
                foreach (var item in list)
                {
                    if (item.FirmId == selectedFirm.FirmId)
                    {
                        userlist.Add(item);
                    }
                }
                getFirmViewModel.firmUsers = userlist;
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return View(getFirmViewModel);
        }

        public async Task<IActionResult> DeleteFirm(int id)
        {
            try
            {
                var firm = firmManager.GetFirmById(id)[0];
                List<CustomUser> userlist = new List<CustomUser>();
                var userList = await _userManager.Users.ToListAsync();
                foreach (var item in userList)
                {
                    if (item.FirmId == firm.FirmId)
                    {
                        item.FirmId = null;
                        await _userManager.UpdateAsync(item);
                    }
                }
                firmManager.TDelete(firm);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return RedirectToAction("FirmList", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            UserEditViewModel getSelectedUser = new UserEditViewModel();
            try
            {
                var user = await _userManager.FindByIdAsync(id);
               
                List<IdentityRole> newList = new List<IdentityRole>();
                var roles = _roleManager.Roles;
                foreach (var item in roles)
                {
                    newList.Add(item);
                }

                getSelectedUser.Roles = newList;
                var firmList = firmManager.GetList();
                getSelectedUser.Firm = firmList;
                getSelectedUser.User = user;
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            

            return View(getSelectedUser);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserEditViewModel data)
        {
            try
            {
                var getUser = await _userManager.FindByIdAsync(data.UserId);
                getUser.Email = data.UserMail;
                getUser.NameSurname = data.UserNameSurname;
                getUser.ApplicationFirm = data.UserApplicationFirm;
                getUser.FirmId = data.FirmId;
                getUser.PhoneNumber = data.UserPhone;
                var userRoles = _userManager.GetRolesAsync(getUser);
                await _userManager.RemoveFromRoleAsync(getUser, userRoles.Result[0]);
                var result = await _userManager.AddToRoleAsync(getUser, data.Role);
                if (result.Succeeded)
                {
                    await _userManager.UpdateAsync(getUser);
                    return RedirectToAction("UserList", "Admin");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return View();
        }

        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                var demands = demandManager.getDemandByUserId(id);
                foreach (var item in demands)
                {
                    demandManager.TDelete(item);
                }
                await _userManager.DeleteAsync(user);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return RedirectToAction("UserList", "Admin");
        }

        public IActionResult DeleteService(int id)
        {
            try
            {
                var firmServices = firmServiceManager.GetFirmServiceByServiceId(id);
                foreach (var item in firmServices)
                {
                    firmServiceManager.TDelete(item);
                }
                var service = serviceManager.TGetById(id);
                serviceManager.TDelete(service);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return RedirectToAction("ServiceList", "Admin");
        }

        [HttpGet]
        public IActionResult EditService(int id)
        {
            Service service = new Service();
            try
            {
                service = serviceManager.TGetById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return View(service);

        }
        [HttpPost]
        public IActionResult EditService(Service newService)
        {
            Service service = new Service();
            try
            {
                
                ServiceValidator serviceValidator = new ServiceValidator();
                service = serviceManager.TGetById(newService.ServiceId);
                service.ServiceName = newService.ServiceName;
                service.ServiceAbout = newService.ServiceAbout;
                ValidationResult results = serviceValidator.Validate(service);
                if (results.IsValid)
                {
                    serviceManager.TUpdate(service);
                    return RedirectToAction("ServiceList", "Admin");
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            return View(service);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DepartmentAdd()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult DepartmentAdd(Department data)
        {
            try
            {

                departmentManager.TAdd(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            return RedirectToAction("DepartmentList", "Admin");
        }

        public IActionResult DepartmentList()
        {
            List<Department> value = new List<Department>();
            try
            {
                value = departmentManager.GetList();
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return View(value);
        }

        public IActionResult SetDepartmentDefaultValue(int id)
        {
            try
            {
                var selectedDepartment = departmentManager.GetDepartmentById(id);
                selectedDepartment.DepartmentisDefault = true;
                var departmentList = departmentManager.GetList();
                foreach (var item in departmentList)
                {
                    if (item.DepartmentId != selectedDepartment.DepartmentId)
                    {
                        item.DepartmentisDefault = false;
                        departmentManager.TUpdate(item);
                    }
                }
                departmentManager.TUpdate(selectedDepartment);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            return RedirectToAction("DepartmentList", "Admin");
        }

        [HttpGet]
        public IActionResult CreateEmployee()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeRegisterModel employeeData)
        {
            try
            {
                SendMail sendMail = new SendMail();
                var user = new CustomUser {PhoneNumber = employeeData.PhoneNumber , UserName = employeeData.Email, Email = employeeData.Email, NameSurname =employeeData.NameSurname };
                var result = await _userManager.CreateAsync(user, employeeData.Password);
                if (result.Succeeded)
                {
                    var roleresult = await _userManager.AddToRoleAsync(user, "personel");
                    if(roleresult.Succeeded)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationLink = Url.Action("Confirm", "Login", new { token, email = user.Email }, Request.Scheme);
                        bool emailResponse = sendMail.SendEmailForConfirmMail(user.Email, confirmationLink);
             
                        if (emailResponse)
                            return RedirectToAction("EmployeeList", "Admin");
                  
                   
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        if (error.Code.Contains("Password"))
                        {
                            ModelState.AddModelError("Password", error.Description);
                        }
                        else if (error.Code.Contains("UserName"))
                        {
                            ModelState.AddModelError("Email", error.Description);
                        }
                    
                    }
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return View();
        }

        public async Task<IActionResult> EmployeeList(int page=1)
        {
            EmployeeListViewModel model = new EmployeeListViewModel();
            try
            {
               
                List<EmployeeWM> employees = new List<EmployeeWM>();
                var allUsers = _userManager.Users;
                List<Firm> firmList = new List<Firm>();
                var UserinEmployeeRole = await (from user in _context.Users
                    join userRole in _context.UserRoles
                        on user.Id equals userRole.UserId
                    join role in _context.Roles
                        on userRole.RoleId equals role.Id
                    where role.Name == "personel"
                    select user).ToListAsync();

                foreach (var item in UserinEmployeeRole)
                {
                    if (item.FirmId != null)
                    {
                        var value = firmManager.TGetById(Convert.ToInt32(item.FirmId));
                        employees.Add(new EmployeeWM
                        {
                            EmployeeId = item.Id, EmployeeNameSurname = item.NameSurname, EmployeeMail = item.Email,
                            Firm = value, EmployeePhone = item.PhoneNumber
                        });
                    }
                    else
                    {
                        employees.Add(new EmployeeWM
                        {
                            EmployeeId = item.Id, EmployeeNameSurname = item.NameSurname, EmployeeMail = item.Email,
                            EmployeePhone = item.PhoneNumber
                        });
                    }

                    model.Users = employees;
                }


            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return View(model);
        }

        public async Task<IActionResult> DefineDepartmentForEmployee()
        {
            IList<CustomUser> customUsers = new List<CustomUser>();
            try
            {
                var allUsers = _userManager.Users;
               
                var UserinEmployeeRole = await (from user in _context.Users
                    join userRole in _context.UserRoles
                        on user.Id equals userRole.UserId
                    join role in _context.Roles
                        on userRole.RoleId equals role.Id
                    where role.Name == "personel"
                    select user).ToListAsync();
                foreach (var item in UserinEmployeeRole)
                {
                    if (item.FirmId == null)
                    {
                        customUsers.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return View(customUsers);
        }

        [HttpGet]
        public async Task<IActionResult> DepartmentRegisterForEmployee(string userId)
        {
            DepartmentRegisterForEmployeeViewModel departmentRegisterForEmployeeViewModel =
                new DepartmentRegisterForEmployeeViewModel();
            try
            {
               
                var selectedUser = await _userManager.FindByIdAsync(userId);
                var allDepartment = departmentManager.GetList();
                var allServicesForSelectedDepartment = serviceManager.GetList();
                departmentRegisterForEmployeeViewModel.User = selectedUser;
                departmentRegisterForEmployeeViewModel.defaultDepartmentId = departmentManager.GetDefaultDepartment().DepartmentId;
                departmentRegisterForEmployeeViewModel.allDepartment = allDepartment;
                departmentRegisterForEmployeeViewModel.getServicesForSelectedDepartment = allServicesForSelectedDepartment;
                departmentRegisterForEmployeeViewModel.allFirms = firmManager.GetList();
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return View(departmentRegisterForEmployeeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DepartmentRegisterForEmployee(DepartmentRegisterForEmployeeViewModel data,IFormCollection formVal)
        {
            List<int> departmentIdArray = new List<int>();
            try
            {
                foreach (var item in formVal["DepartmentId"])
                {
                    departmentIdArray.Add(Convert.ToInt32(item));
                }
                List<int> serviceIdArray = new List<int>();
                foreach (var item in formVal["ServiceId"])
                {
                    serviceIdArray.Add(Convert.ToInt32(item));
                }
                var selectedUser = await _userManager.FindByIdAsync(data.UserId);
           
                selectedUser.FirmId = data.FirmId;
                await _userManager.UpdateAsync(selectedUser);

                foreach (var item in departmentIdArray)
                {
                    DepartmentEmployee departmentEmployee = new DepartmentEmployee();
                
                    departmentEmployee.DepartmentId = item;
                    departmentEmployee.Id = data.UserId;
                    departmentEmployeeManager.TAdd(departmentEmployee);
                }
            
                foreach (var item in serviceIdArray)
                {
                    EmployeeService employeeService = new EmployeeService();
                    employeeService.ServiceId = item;
                    employeeService.Id = data.UserId;
                    employeeServiceManager.TAdd(employeeService);
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            

            return RedirectToAction("EmployeeList", "Admin");
        }

        public async Task<IActionResult> DeleteEmployee(string id)
        {
            try
            {
                var selectedUser =await _userManager.FindByIdAsync(id);
                var employeeDemandList = _employeeDemandManager.GetDemandByEmployee(selectedUser.Id);
                foreach (var item in employeeDemandList)
                {
                    _employeeDemandManager.TDelete(item);
                }
                var result = await _userManager.DeleteAsync(selectedUser);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            
            return RedirectToAction("EmployeeList","Admin");
            
            
        }

        [HttpGet]
        public IActionResult EditDepartment(int id)
        {
            Department selectedDepartment = new Department();
            try
            {
                selectedDepartment = departmentManager.TGetById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return View(selectedDepartment);
        }

        [HttpPost]
        public IActionResult EditDepartment(Department data)
        {
            try
            {
                var selectedDepartment = departmentManager.TGetById(data.DepartmentId);
                selectedDepartment.DepartmentAbout = data.DepartmentAbout;
                selectedDepartment.DepartmentName = data.DepartmentName;
                departmentManager.TUpdate(selectedDepartment);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return RedirectToAction("DepartmentList", "Admin");
        }
        
        public IActionResult DeleteDepartment(int id)
        {
            try
            {
                var selectedDepartment = departmentManager.TGetById(id);
                departmentManager.TDelete(selectedDepartment);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return RedirectToAction("DepartmentList", "Admin");
        }

        public IActionResult LogList(int page=1)
        {
            var logs = _logsManager.GetList();
            var logsList  = logs.AsEnumerable().OrderByDescending(x => x.TimeStamp).ToList().ToPagedList(page,7);
            return View(logsList);
        }

        public IActionResult LogDetails(int id)
        {
            Logs getLog = new Logs();
            try
            {
                 getLog = _logsManager.TGetById(id);
                
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            return View(getLog);
        }

        public IActionResult Settings()
        {
            List<Setting> settingList = new List<Setting>();
            try
            {
                settingList = _settingManager.GetList();
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }

            return View(settingList);
        }
        
        [HttpGet]
        public IActionResult EditSettings(int id)
        {
            Setting selectedSetting = new Setting();
            try
            {
                selectedSetting = _settingManager.TGetById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            return View(selectedSetting);
        }

        [HttpPost]
        public IActionResult EditSettings(Setting settingData)
        {
            try
            {
                var selectedSettings = _settingManager.TGetById(settingData.Id);
                selectedSettings.SettingValue = settingData.SettingValue;
                _settingManager.TUpdate(selectedSettings);
                RecurringJobs.DeleteLogTable();
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            return RedirectToAction("Settings", "Admin");
        }

       
    }
}
