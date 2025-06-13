using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PedidosComida.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PedidosComida
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CrearRolesYUsuarios();  // llamada al método

        }


        public void CrearRolesYUsuarios()
        {
            var context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Crear rol Administrador
            if (!roleManager.RoleExists("Administrador"))
            {
                var role = new IdentityRole("Administrador");
                roleManager.Create(role);
            }

            // Crear rol Cliente
            if (!roleManager.RoleExists("Cliente"))
            {
                var role = new IdentityRole("Cliente");
                roleManager.Create(role);
            }

            // Asignar rol Administrador a un usuario
            var user = userManager.FindByEmail("admin@buenprovecho.com");
            if (user != null && !userManager.IsInRole(user.Id, "Administrador"))
            {
                userManager.AddToRole(user.Id, "Administrador");
            }
        }
    }
}
