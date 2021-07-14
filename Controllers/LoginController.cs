using Microsoft.AspNetCore.Mvc;
using SmartSense.Models;
using System.Net.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartSense.Identity;

namespace SmartSense.Controllers
{
    public class LoginController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public  IActionResult Login()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }
          var  user = await _userManager.FindByEmailAsync(model.email);
            if (user == null)
            {
                ViewBag.loginError = "kullanıcı bulunamadı!";
                return View(model);
                
            }

            else
            {
                user.EmailConfirmed = true;
                var result = await _signInManager.PasswordSignInAsync(user, model.sifre,false,false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.loginError = "Hatalı Sifre!";
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult UserSetting()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> resetPasswordAsync(string currentPass, string newPass, string newPassRe)
        {
            if (newPass != newPassRe)
            {
                ViewBag.message = "Şifreler Eşleşmiyor";
                return View("UserSetting");
            }
            var userName = User.Identity.Name;
            var db = new SmartSenseDB();
            var eposta = db.AspNetUsers.Where(i => i.UserName == userName).FirstOrDefault();
            string ep = eposta.Email;
            Identity.User user = await _userManager.FindByEmailAsync(ep);
            if (user != null)
            {
                var result =await _userManager.ChangePasswordAsync(user, currentPass, newPass);
                if (result.Succeeded)
                {
                    ViewBag.message = "Şifreniz başarıyla değiştirildi";
                }
                else
                {
                    ViewBag.message = "Hatalı Şifre!";
                }
            }

   

            return View("UserSetting");
        }
    }


}
