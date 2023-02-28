using Employement_Project_MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Employement_Project_MVC.Startup))]
namespace Employement_Project_MVC
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRorleAndUserDefualt();
        }
         public void CreateRorleAndUserDefualt()
        {
            var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManger = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            if (!rolemanager.RoleExists("admins"))
            {
                role.Name = "admins";
                rolemanager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.Email = "Elameer.Azmay@gmail.com";
                user.UserName = "Alameer";
                var chack = userManger.Create(user,"98Amir@");
                if (chack.Succeeded)
                {
                    userManger.AddToRole(user.Id,role.Name);
                }

            }
            

        }
    }
}
