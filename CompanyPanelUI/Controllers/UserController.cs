using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using CompanyPanelUI.DTOS;
using CompanyPanelUI.Models.DemandListViewModel;
using CompanyPanelUI.Models.DemandReplyViewModel;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BackgroundJobs.Schedules;

namespace CompanyPanelUI.Controllers
{
    [Authorize(Roles = "musteri")]
    public class UserController : Controller
    {
        DemandManager demandManager = new DemandManager(new EfDemandRepository());
        FirmServiceManager firmServiceManager = new FirmServiceManager(new EfFirmServiceRepository());
        DemandFileManager demandFileManager = new DemandFileManager(new EfDemandFileRepository());
        DemandAnswerManager demandAnswerManager = new DemandAnswerManager(new EfDemandAnswerRepository());
        ServiceManager serviceManager = new ServiceManager(new EfServiceRepository());
        private DepartmentManager _departmentManager = new DepartmentManager(new EfDepartmentRepository());
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;

        public UserController(UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DemandListUser()
        {
            CustomUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var values = demandManager.getDemandByUserId(currentUser.Id);
            values = values.AsEnumerable().OrderByDescending(x => x.DemandCreateTime).ToList();
            DemandListViewModel demandListModel = new DemandListViewModel();
            demandListModel.Demands = values;
            demandListModel.Services = serviceManager.GetList();
            demandListModel.Yanitlanmistalep = false;
            demandListModel.CevaplanmamisTalep = false;
            return View(demandListModel);
        }

        [HttpPost]
        public async Task<IActionResult> DemandListUser(DemandListViewModel data)
        {
            CustomUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var demands = demandManager.getDemandByUserId(currentUser.Id);
            DemandListViewModel newData = new DemandListViewModel();


            if (data != null)
            {
                
                if (data.StartDate.HasValue)
                {
                    demands = demands.Where(x => x.DemandCreateTime.Date >= data.StartDate.Value.Date).ToList();

                }
                if (data.EndDate.HasValue)
                {
                    demands = demands.Where(x => x.DemandCreateTime.Date <= data.EndDate.Value.Date).ToList();
                }
                if (data.ServiceId.HasValue)
                {
                    demands = demands.Where(x => x.ServiceId == data.ServiceId).ToList();
                }
                if (data.CevaplanmamisTalep == false || data.Yanitlanmistalep == false)
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
            else
                {
                    newData.Demands = demands;
                    newData.Services = serviceManager.GetList();
                }
            
            return View(newData);
        }

        [HttpGet]
        public async Task<IActionResult> AddDemand()
        {
            AddDemandModel addDemandModel = new AddDemandModel();   
            CustomUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var userService = firmServiceManager.GetServiceByFirmId(Convert.ToInt32(currentUser.FirmId));
            addDemandModel.Service = userService;
            return View(addDemandModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddDemand(AddDemandModel model)
        {
            Demand newDemand = new Demand();
            DemandValidator demandValidator = new DemandValidator();
            CustomUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            AddDemandModel addDemandModel = new AddDemandModel();
            newDemand.DemandTitle = model.DemandTitle;
            newDemand.DemandContent = model.DemandContent;
            newDemand.UserId = currentUser.Id;
            newDemand.DemandStatus = true;
            newDemand.ServiceId = model.ServiceId;
            newDemand.DemandCreateTime = DateTime.Now;
            newDemand.DepartmentId = _departmentManager.GetDefaultDepartment().DepartmentId;
            ValidationResult results = demandValidator.Validate(newDemand);
            


            if (results.IsValid)
            {
                
                if (model.DemandFile != null)
                {
                    foreach (var file in model.DemandFile)
                    {
                        DemandFile demandFile = new DemandFile();
                        var extension = Path.GetExtension(file.FileName);
                        var unsupportedTypes = new[] { "exe", "dll"};
                        var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                        if (unsupportedTypes.Contains(fileExt))
                        {
                            ModelState.AddModelError("DemandFile", "Bu dosyayı yükleyemezsiniz, lütfen uygun bir dosya yükleyiniz");
                        }
                        else
                        {
                        demandManager.TAdd(newDemand);
                        int demandId = newDemand.DemandId;
                        var newFileName = Guid.NewGuid() + extension;
                        var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/DemandFiles/", newFileName);
                        var stream = new FileStream(location, FileMode.Create);
                        file.CopyTo(stream);
                        demandFile.DemandFilePath = newFileName;
                        demandFile.DemandId = demandId;
                        demandFileManager.TAdd(demandFile);
                        DelayedJobs.SendMailForDepartmentIfNotAnswered(newDemand.DemandId);
                        return RedirectToAction("GoDemandChat", "User", new { @id = demandId });
                            }
                        }
                }
                else
                {
                    demandManager.TAdd(newDemand);
                    int demandId = newDemand.DemandId;
                    DelayedJobs.SendMailForDepartmentIfNotAnswered(newDemand.DemandId);
                    return RedirectToAction("GoDemandChat", "User", new { @id = demandId });
                }
                
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            //user ıd birden çok demand açabilir bu yüzden unique üstüne düşün
            
            var userService = firmServiceManager.GetServiceByFirmId(Convert.ToInt32(currentUser.FirmId));
            addDemandModel.Service = userService;
            return View(addDemandModel);
        }
        [HttpGet]
        public async Task<IActionResult> GoDemandChatAsync(int id)
        {
            ViewBag.id = id;
            DemandReplyViewModel model = new DemandReplyViewModel();
            var demand = demandManager.GetDemandById(id);
            var demandFiles = demandFileManager.GetFileByDemandId(id);
            var user = await _userManager.FindByIdAsync(demand[0].UserId.ToString());
            var demandAnswer = demandAnswerManager.GetDemandAnswerByDemandId(id);
            List<DemandAndAnswerWm> demandandanswermodel = new List<DemandAndAnswerWm>();
            if (demandAnswer.Count == 0)
            {
                demandandanswermodel.Add(new DemandAndAnswerWm {DemandReadStatus = demand[0].DemandReadTime, DemandFilePath = demandFiles, UserId = demand[0].UserId, DemandId = demand[0].DemandId, DemandTitle = demand[0].DemandTitle, ServiceId = demand[0].ServiceId, DemandContent = demand[0].DemandContent, DemantCreateDate = demand[0].DemandCreateTime, DemandStatus = demand[0].DemandStatus, UserName = user.NameSurname });
            }
            else
            {
                demandandanswermodel.Add(new DemandAndAnswerWm { DemandReadStatus = demand[0].DemandReadTime,DemandFilePath = demandFiles, UserId = demand[0].UserId, DemandId = demand[0].DemandId, DemandTitle = demand[0].DemandTitle, ServiceId = demand[0].ServiceId, DemandContent = demand[0].DemandContent, DemantCreateDate = demand[0].DemandCreateTime, DemandStatus = demand[0].DemandStatus, UserName = user.NameSurname, DemandAnswer = demandAnswer });
            }

            model.DemandAndAnswerWmList = demandandanswermodel;
            return View(model);
        }
        [HttpPost]
        public IActionResult GoDemandChat(DemandReplyViewModel data)
        {
            DemandAnswer demandAnswer = new DemandAnswer();
            demandAnswer.DemandId = data.DemandIdNew;
            demandAnswer.Answer = data.DemandAnswerNew;
            demandAnswer.DemandAnswerDate = DateTime.Now;
            demandAnswer.DemandAnswerType = 1;
            demandAnswerManager.TAdd(demandAnswer);
            return RedirectToAction("GoDemandChat", "User");
        }

        public IActionResult ChangeDemandStatus(int id)
        {
            var selectedDemand = demandManager.TGetById(id);
            selectedDemand.DemandStatus = false;
            demandManager.ChangeStatusValue(id);
            return RedirectToAction("DemandListUser", "User");
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
