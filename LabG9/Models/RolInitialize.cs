using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabG9.Models
{
    public class RolInitialize
    {
        public static void Initializer()
        {
            var rolManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            List<string> roles = new List<string>();
            roles.Add("Admin");
            roles.Add("Usuario");

            foreach (var rol in roles)
            {
                if (!rolManager.RoleExists(rol))
                {
                    rolManager.Create(new IdentityRole(rol));

                }
            }
            var adminUser = new ApplicationUser { UserName = "adminTienda@gmail.com", Email = "adminTienda@gmail.com" };
            string Contraseña = "Admin123";

            if (userManager.FindByEmail(adminUser.Email) != null)
            {
                var creacion = userManager.Create(adminUser, Contraseña);
                if (creacion.Succeeded)
                {
                    userManager.AddToRole(adminUser.Id, "Admin");
                }
            }
        }
    }
}