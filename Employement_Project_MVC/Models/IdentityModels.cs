using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Employement_Project_MVC.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string UserType { set; get; }
        public ICollection<Job> jobs { get; set; }
        public string Country { set; get; }
        public string PhoneVisitor { set; get; }
        public string PathImage { set; get; }
       
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Employement_Project_MVC.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Employement_Project_MVC.Models.Job> Jobs { get; set; }

        public System.Data.Entity.DbSet<Employement_Project_MVC.Models.ApplayForJob> ApplayForJobs { get; set; }
        public System.Data.Entity.DbSet<Employement_Project_MVC.Models.Accepted> Accpeties { get; set; }

    }
}