using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace RemoteControl.Models
{
    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {

            
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
 
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
 
            // create two roles
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };
 
            // add them to db
            roleManager.Create(role1);
            roleManager.Create(role2);
 
            // create users
            var admin = new ApplicationUser { Email = "somemail@mail.ru", UserName = "admin" };
            string password = "123456";
            var result = userManager.Create(admin, password);
 
            
            if(result.Succeeded)
            {
             
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
                
            }
           
           
            var user = new ApplicationUser { Email = "somemail@mail.ru", UserName = "user1" };
            result = userManager.Create(user, password);
            if (result.Succeeded)
                userManager.AddToRole(user.Id, role2.Name);

            user = new ApplicationUser { Email = "somemail@mail.ru", UserName = "user2" };
            result = userManager.Create(user, password);
            if (result.Succeeded)
                userManager.AddToRole(user.Id, role2.Name);

            user = new ApplicationUser { Email = "somemail@mail.ru", UserName = "user3" };
            result = userManager.Create(user, password);
            if (result.Succeeded)
             userManager.AddToRole(user.Id, role2.Name);

            base.Seed(context);
        }
    }
}