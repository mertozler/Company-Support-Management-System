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

namespace CompanyPanelUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        FirmManager firmManager = new FirmManager(new EfFirmsRepository());
        private readonly UserManager<CustomUser> _userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext _context;
        UserRegisterManager userRegisterManager = new UserRegisterManager(new EfUserRepository());
        ServiceManager serviceManager = new ServiceManager(new EfServiceRepository());
        FirmServiceManager firmServiceManager = new FirmServiceManager(new EfFirmServiceRepository());
        DemandManager demandManager = new DemandManager(new EfDemandRepository());
        DemandAnswerManager demandAnswerManager = new DemandAnswerManager(new EfDemandAnswerRepository());
        DemandFileManager demandFileManager = new DemandFileManager(new EfDemandFileRepository());
        Context context = new Context();

        public AdminController(UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            this.roleManager = roleManager;
            _context = context;
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
            FirmValidator firmValidator = new FirmValidator();
            Firm newFirm = new Firm();
            newFirm.FirmName = data.FirmName;
            newFirm.FirmTaxNo = data.FirmTaxNo;
            newFirm.FirmTelNo = data.FirmTelNo;
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
            return View();
        }

        public IActionResult FirmList()
        {
            var list = firmManager.GetList();
            return View(list);
        }

        public IActionResult UserApplicationConfirm()
        {
            List<User> users = new List<User>();    
            var list = userRegisterManager.GetList();
            foreach (var user in list)
            {
                if (user.UserStatus==true)
                {
                    users.Add(user);
                }
            }
            return View(users); 
        }
        [HttpGet]
        public IActionResult UserRegister(int id)
        {
            var roles = roleManager.Roles;
            UserRegisterModel getSelectedUser = new UserRegisterModel();
            List<IdentityRole > newList = new List<IdentityRole>();
            foreach (var item in roles)
            {
                newList.Add(item);
            }
            getSelectedUser.Roles = newList;
            var user = userRegisterManager.GetUserById(id);
            var firmList = firmManager.GetList();
            getSelectedUser.Firm = firmList;
            getSelectedUser.User = user;
            return View(getSelectedUser);
        }
        [HttpPost]
        public async Task<IActionResult> UserRegister(UserRegisterModel data)
        {
            var getCurrentUnconfirmedUserData = userRegisterManager.GetUserById(data.UserId);
            var user = new CustomUser {PhoneNumber = data.UserPhone , UserName = getCurrentUnconfirmedUserData[0].UserMail, Email = getCurrentUnconfirmedUserData[0].UserMail, NameSurname = getCurrentUnconfirmedUserData[0].UserNameSurname, FirmId = data.FirmId };
            var result = await _userManager.CreateAsync(user, getCurrentUnconfirmedUserData[0].UserPassword);
            if (result.Succeeded)
            {
                var roleresult = await _userManager.AddToRoleAsync(user, data.Role);
                if(roleresult.Succeeded)
                {
                    userRegisterManager.ChangeStatusValue(data.UserId);
                    return RedirectToAction("UserList", "Admin");
                }
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            UserRegisterModel getSelectedUser = new UserRegisterModel();
            var firmList = firmManager.GetList();
            var currentUser = userRegisterManager.GetUserById(getCurrentUnconfirmedUserData[0].UserId);
            getSelectedUser.User = currentUser;
            getSelectedUser.Firm = firmList;
            return View(getSelectedUser);
        }
        public IActionResult UserList()
        {
            UserListViewModel userListModel = new UserListViewModel();
            List<UserWm> Users = new List<UserWm>();
            var list = _userManager.Users;
            foreach (var item in list)
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
            return View(userListModel);
        }

        public IActionResult ServiceDefine()
        {
            var list = firmManager.GetList();
            return View(list);
        }

        [HttpGet]
        public IActionResult CreateService()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult CreateService(ServicesDTO service)
        {
            Service newService = new Service();
            ServiceValidator serviceValidator = new ServiceValidator();
            newService.ServiceName = service.ServiceName;
            newService.ServiceAbout = service.ServiceAbout;
            ValidationResult results = serviceValidator.Validate(newService);
            if (results.IsValid)
            {
                serviceManager.TAdd(newService);
                return RedirectToAction("ServiceList", "Admin");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public IActionResult ServiceList()
        {
            var serviceList = serviceManager.GetList();
            return View(serviceList);
        }

        [HttpGet]
        public IActionResult ServiceDefineForFirm(int id)
        {
            ServiceViewModel serviceViewModel = new ServiceViewModel();
            var firm = firmManager.GetFirmById(id);
            var serviceList = serviceManager.GetList();
            serviceViewModel.Firms = firm;
            serviceViewModel.Services = serviceList;
            return View(serviceViewModel);
        }
        [HttpPost]
        public IActionResult ServiceDefineForFirm(ServiceViewModel data)
        {
            ServiceViewModel returnValue = new ServiceViewModel();
            FirmService newFirmService = new FirmService();
            newFirmService.FirmId = data.FirmId;
            newFirmService.ServiceId = data.ServiceId;
            firmServiceManager.TAdd(newFirmService);
            returnValue.Firms = firmManager.GetFirmById(data.FirmId);
            returnValue.Services = serviceManager.GetList();
            return RedirectToAction("FirmList", "Admin");
        }
        [HttpGet]
        public IActionResult DemandList()
        {
            DemandListViewModel demandListViewModel = new DemandListViewModel();
            demandListViewModel.Demands = demandManager.GetList();
            demandListViewModel.Services = serviceManager.GetList();
            demandListViewModel.Yanitlanmistalep = false;
            demandListViewModel.CevaplanmamisTalep = false;
            return View(demandListViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DemandList(DemandListViewModel data)
        {

            var demands = await context.Demands.ToListAsync();
            DemandListViewModel newData = new DemandListViewModel();
            

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
            return View(newData);
        }

        [HttpGet]
        public async Task<IActionResult> DemandAnswerUser(int id)
        {
            ViewBag.id = id;
            DemandReplyViewModel model = new DemandReplyViewModel();
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
                demandandanswermodel.Add(new DemandAndAnswerWm { DemandFilePath = demandFiles, UserId = demand[0].UserId, DemandId = demand[0].DemandId, DemandTitle = demand[0].DemandTitle, ServiceId = demand[0].ServiceId, DemandContent = demand[0].DemandContent, DemantCreateDate = demand[0].DemandCreateTime, DemandStatus = demand[0].DemandStatus, UserName = user.NameSurname, DemandAnswer = demandAnswer[0].Answer, DemandAnswerDate = demandAnswer[0].DemandAnswerDate });
            }
            

            model.DemandAndAnswerWmList = demandandanswermodel;
            return View(model);
        }

        [HttpPost]
        public IActionResult DemandAnswerUser(DemandReplyViewModel data)
        {
            //int id = ViewBag.id;
            ServiceViewModel returnValue = new ServiceViewModel();
            DemandAnswer demandAnswer = new DemandAnswer();
            demandAnswer.DemandId = data.DemandIdNew;
            demandAnswer.Answer = data.DemandAnswerNew;
            demandAnswer.DemandAnswerDate = DateTime.Now;
            demandAnswerManager.TAdd(demandAnswer);
            demandManager.ChangeStatusValue(data.DemandIdNew);
            return RedirectToAction("DemandAnswerUser", "Admin");
        }

        [HttpGet]
        public IActionResult GetFirm(int id)
        {
            GetFirmViewModel getFirmViewModel = new GetFirmViewModel();
            List<CustomUser> userlist = new List<CustomUser>();
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
            return View(getFirmViewModel);
        }

        [HttpPost]
        public IActionResult GetFirm(GetFirmViewModel data)
        {
            FirmValidator firmValidator = new FirmValidator();
            var selectedFirm = firmManager.GetFirmById(data.firm.FirmId)[0];
            selectedFirm.FirmName = data.firm.FirmName;
            selectedFirm.FirmTaxNo = data.firm.FirmTaxNo;
            selectedFirm.FirmTelNo = data.firm.FirmTelNo;
            selectedFirm.FirmMail = data.firm.FirmMail;
            selectedFirm.FirmStatus = true;
            ValidationResult results = firmValidator.Validate(selectedFirm);
            GetFirmViewModel getFirmViewModel = new GetFirmViewModel();
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
            return View(getFirmViewModel);
        }

        public async Task<IActionResult> DeleteFirm(int id)
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
            return RedirectToAction("FirmList", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            UserEditViewModel getSelectedUser = new UserEditViewModel();
            List<IdentityRole> newList = new List<IdentityRole>();
            var roles = roleManager.Roles;
            foreach (var item in roles)
            {
                newList.Add(item);
            }

            getSelectedUser.Roles = newList;
            var firmList = firmManager.GetList();
            getSelectedUser.Firm = firmList;
            getSelectedUser.User = user;

            return View(getSelectedUser);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserEditViewModel data)
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
            return View();
        }

        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var demands = demandManager.getDemandByUserId(id);
            foreach (var item in demands)
            {
                demandManager.TDelete(item);
            }
            await _userManager.DeleteAsync(user);
            return RedirectToAction("UserList", "Admin");
        }

        public IActionResult DeleteService(int id)
        {
            var firmServices = firmServiceManager.GetFirmServiceByServiceId(id);
            foreach (var item in firmServices)
            {
              firmServiceManager.TDelete(item);
            }
            var service = serviceManager.TGetById(id);
            serviceManager.TDelete(service);
            return RedirectToAction("ServiceList", "Admin");
        }

        [HttpGet]
        public IActionResult EditService(int id)
        {
            var service = serviceManager.TGetById(id);
            return View(service);

        }
        [HttpPost]
        public IActionResult EditService(Service newService)
        {
            ServiceValidator serviceValidator = new ServiceValidator();
            var service = serviceManager.TGetById(newService.ServiceId);
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
            return View(service);
        }

        public IActionResult Contact()
        {
            return View();
        }

    }
}
