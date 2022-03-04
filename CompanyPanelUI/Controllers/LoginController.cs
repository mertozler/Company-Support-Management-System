using System;
using BusinessLayer.Concrete;
using CompanyPanelUI.DTOS;
using CompanyPanelUI.Models.LoginViewModel;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BusinessLayer.Util;
using CompanyPanelUI.Models.ResetPasswordViewModel;
using Microsoft.Extensions.Logging;

namespace CompanyPanelUI.Controllers
{
    public class LoginController : Controller
    {
        UserRegisterManager userRegisterManager = new UserRegisterManager(new EfUserRepository());
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;
        
        private readonly ILogger<LoginController> _logger;

        public LoginController(UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager,ILogger<LoginController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        var loggedUser = await _userManager.FindByEmailAsync(user.Email);
                        var kullaniciRolleri = await _userManager.GetRolesAsync(loggedUser);
                        if (kullaniciRolleri[0] == "musteri")
                        {
                            return RedirectToAction("Index", "User");
                            //Burayı usersa yönlendir
                        }
                        else if(kullaniciRolleri[0]=="admin")
                        {

                            return RedirectToAction("Index", "Admin");
                        }
                        else if(kullaniciRolleri[0]=="personel")
                        {

                            return RedirectToAction("Index", "Employee");
                        }
                    

                    }

                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

                }
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return View(user);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserModel data)
        {
            User newUser = new User();
            try
            {
                newUser.UserMail = data.UserMail;
                newUser.UserNameSurname = data.UserName;
                newUser.UserPassword = data.UserPassword;  
                newUser.UserApplicationFirm = data.ApplicationFirm;
                newUser.UserStatus = true;
                userRegisterManager.TAdd(newUser);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return RedirectToAction("RegisterSuccessful", "Login");
        }
        public IActionResult RegisterSuccessful()
        {
            return View();
        }

        public IActionResult AccesDenied()
        {
            return View();
        }

        public async Task<IActionResult> Confirm(string token, string email)
        {
            IdentityResult result = new IdentityResult();
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                    return View("Error");
 
                 result = await _userManager.ConfirmEmailAsync(user, token);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return View(result.Succeeded ? "Confirm" : "Error");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var user = _userManager.FindByEmailAsync(email).Result;

                if (user == null || !(_userManager.
                        IsEmailConfirmedAsync(user).Result))
                {
                    ViewBag.Message = "Şifrenizi sıfırlarken bir sorun oluştu! Mail adresinizi kontrol edip tekrardan deneyiniz";
                    return View("ForgotPassword");
                }

                var token = _userManager.
                    GeneratePasswordResetTokenAsync(user).Result;

                var resetLink = Url.Action("ResetPassword", 
                    "Login", new { token = token }, 
                    protocol: HttpContext.Request.Scheme);

                SendMail sendMail = new SendMail();
                sendMail.SendResetPassword(email, resetLink);

                // code to email the above link
                // see the earlier article

                ViewBag.Message = "Şifre sıfırlama linkiniz mail adresinize gönderilmiştir!";
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
            return View("ForgotPassword");
        }
        
        [HttpGet]
        public IActionResult ResetPassword(string token)
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel data)
        {
            IdentityResult result = new IdentityResult();
            try
            {
                var user = _userManager.
                    FindByEmailAsync(data.Email).Result;

                result = _userManager.ResetPasswordAsync
                    (user, data.Token,data.Password).Result;
                
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            if (result.Succeeded)
            {
                ViewBag.Message = "Şifreniz başarıyla sıfırlanmıştır.! Lütfen yeni şifrenizle giriş yapınız.";
                return View("ResetPassword");
            }
            else
            {
                ViewBag.Message = "Şifreniz sıfırlanırken bir sorun yaşandı!";
                return View("ResetPassword");
            }
            
        }
        
    }
}
