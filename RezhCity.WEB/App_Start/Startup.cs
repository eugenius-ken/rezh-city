using Microsoft.Owin;
using Owin;
using RezhCity.DAL.Interfaces;
using RezhCity.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(RezhCity.WEB.App_Start.Startup))]

namespace RezhCity.WEB.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }

        private IIdentityUnitOfWork Create()
        {
            return IdentityUnitOfWork.Create("RezhCityConnection");
        }
    }
}