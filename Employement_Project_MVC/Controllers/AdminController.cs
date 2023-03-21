using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Employement_Project_MVC.Models;
using Microsoft.AspNet.Identity;

namespace Employement_Project_MVC.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        ////[Authorize(Roles = "admins")]
        public ActionResult Admin()
        {
            
            var userAdmin = User.Identity.GetUserId();
            var admin = db.Users.Find(userAdmin);
            ViewBag.all = db.Users.Count();
            ViewBag.sreacher = db.Users.Where(x => x.UserType == "باحث").Count();
            ViewBag.Publisher = db.Users.Where(x => x.UserType == "ناشر").Count();
            ViewBag.cats = db.Categories.Count();
            ViewBag.Jobs = db.Jobs.Count();
            ViewBag.applies = db.ApplayForJobs.Count();
            ViewBag.AllCats = db.Categories.ToList();
            return View(admin);




        }

        public PartialViewResult AllRoles()
        {
            var Roles = db.Roles.Select(x=>new RoleViewModel {Id=x.Id,Name=x.Name}).ToList();
            return PartialView("~/Views/Shared/_AllRoles.cshtml", Roles);
        }

      
    }
}