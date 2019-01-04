using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;
using Blog.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class LoginController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;

        private readonly ApplicationDbContext db;

        public LoginController(ApplicationDbContext _db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.db = _db;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Index(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                LoginFrom lf = new LoginFrom();
                lf.returnUrl = returnUrl;
                return View(lf);
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Name,Pw,returnUrl")]LoginFrom user)
        {
            if (ModelState.IsValid)
            {
                //User tempUser = db.users.FirstOrDefault(a => a.Name == user.Name && a.Pw == Md5(user.Pw));
                //if (user != null)
                //{
                //    HttpContext.Session.SetInt32("ID", user.Id);
                //    HttpContext.Session.SetString("UserName", user.Name);
                //    return RedirectToAction("Index", "Home");

                //}


                var um = await userManager.FindByNameAsync(user.Name);

                if (um != null)
                {

                    var result = await signInManager.PasswordSignInAsync(um, user.Pw, true, false);

                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(um, isPersistent: true);

                        if (!string.IsNullOrEmpty(user.returnUrl))
                            return Redirect(user.returnUrl);

                        return RedirectToAction("Index", "Home");

                    }
                }

                return View(user);
            }

            return View(user);
        }


        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn([Bind("Name,Pw,Cpw")]SignInFrom loginFrom)
        {
            if (ModelState.IsValid)
            {
                //User user = new User();
                //user.Name = loginFrom.Name;
                //user.Pw = Md5(loginFrom.Pw);
                //user.CreatTime = DateTime.Now;

                //db.users.Add(user);
                //db.SaveChanges();

                //return RedirectToAction("Index", "Login");


                var appUser = new ApplicationUser
                {
                    UserName = loginFrom.Name
                };

                var result = await userManager.CreateAsync(appUser, loginFrom.Pw);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Login");
                }


            }

            return View(loginFrom);
        }


        [AcceptVerbs("Get", "Post")]
        public JsonResult VerifyUserName(string name)
        {
            ApplicationUser u = db.Users.FirstOrDefault(a => a.UserName == name);
            if (u != null)
                return Json($"用户名 {name} 已经被占用");

            return Json(true);
        }



        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }




        public string Md5(string str)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }

    }
}