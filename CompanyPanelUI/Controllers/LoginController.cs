using BusinessLayer.Concrete;
using CompanyPanelUI.DTOS;
using CompanyPanelUI.Models.LoginViewModel;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CompanyPanelUI.Controllers
{
    public class LoginController : Controller
    {
        UserRegisterManager userRegisterManager = new UserRegisterManager(new EfUserRepository());
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;

        public LoginController(UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel user)
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

                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

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
            newUser.UserMail = data.UserMail;
            newUser.UserNameSurname = data.UserName;
            newUser.UserPassword = data.UserPassword;  
            newUser.UserApplicationFirm = data.ApplicationFirm;
            newUser.UserStatus = true;
            userRegisterManager.TAdd(newUser);
            return RedirectToAction("RegisterSuccessful", "Login");
        }
        public IActionResult RegisterSuccessful()
        {
            return View();
        }
    }
}
