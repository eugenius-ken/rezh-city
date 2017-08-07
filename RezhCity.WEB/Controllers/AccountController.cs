using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RezhCity.DAL.Entities;
using RezhCity.DAL.Interfaces;
using RezhCity.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RezhCity.WEB.Controllers
{
    public class AccountController : Controller
    {
        private IIdentityUnitOfWork uow
        {
            get
            {
                return HttpContext.GetOwinContext().Get<IIdentityUnitOfWork>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LoginForAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginForAdmin(AdminLoginView model)
        {
            await SetInitialData();

            if(ModelState.IsValid)
            {
                var user = await uow.UserManager.FindAsync(model.Username, model.Password);
                if(user != null)
                {
                    ClaimsIdentity claim = await uow.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    if (claim == null)
                    {
                        ModelState.AddModelError("Password", "Логин и/или пароль введены неверно");
                    }
                    else
                    {
                        AuthenticationManager.SignOut();
                        AuthenticationManager.SignIn(new AuthenticationProperties()
                        {
                            IsPersistent = true
                        }, claim);

                        return RedirectToAction("Advertisements", "Check");
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "Логин и/или пароль введены неверно");
                }
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private async Task SetInitialData()
        {
            var role = await uow.RoleManager.FindByNameAsync("Admin");
            if(role == null)
            {
                role = new ApplicationRole() { Name = "Admin" };
                await uow.RoleManager.CreateAsync(role);
            }

            ApplicationUser admin = await uow.UserManager.FindByEmailAsync("admin@rezh-city.ru");
            if(admin == null)
            {
                admin = new ApplicationUser() { Email = "admin@rezh-city.ru", UserName = "admin" };
                var result = await uow.UserManager.CreateAsync(admin, "r1t2y3As");
                await uow.UserManager.AddToRoleAsync(admin.Id, "Admin");
            }

            await uow.SaveAsync();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (uow != null)
                {
                    uow.Dispose();
                }
            }

            base.Dispose(disposing);
        }
    }
}