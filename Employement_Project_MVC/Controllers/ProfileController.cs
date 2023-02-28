using Employement_Project_MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employement_Project_MVC.Controllers
{ 
    public class ProfileController : Controller
    {
        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}
        ApplicationDbContext db = new ApplicationDbContext();
        //private ApplicationUserManager _userManager;
        //public ProfileController(ApplicationUserManager userManager)
        //{
        //    UserManager = userManager;

        //}


        // GET: Profile
        [Authorize]
        public ActionResult Publisher(string Id)
        {
            var User = db.Users.Where(u => u.Id == Id).FirstOrDefault();
            ViewBag.Cat = db.Categories;
            return View(User);

        }
        public ActionResult SreacherProfile(string Id )
        {

            var applies = db.ApplayForJobs.Where(x => x.Userid == Id).ToList();
           TempData["Srearcher"] = db.Users.Find(Id) as ApplicationUser;
           
             return View(applies);
        }

        public ActionResult loadJobs()
        {
            var userId = User.Identity.GetUserId();
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.Jobs.Where(u => u.CategoryId == 10 && u.UaerID == userId);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public JsonResult DataJobs(int id)
        {
            var userId = User.Identity.GetUserId();
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.Jobs.Where(u => u.CategoryId == id && u.UaerID == userId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult Applies_Profile(int id,int jobid=0,int accpet=0)
        {
            if (jobid!=0 && accpet==0)
            {
                var Rejected = db.ApplayForJobs.Find(id);
                db.ApplayForJobs.Remove(Rejected);
                db.SaveChanges();
                List<ApplayForJob> applies = db.ApplayForJobs.Where(x => x.JobId == jobid).ToList();
                return PartialView("_Applies", applies);

            }
            if(jobid != 0 && accpet != 0)
            {
                var accpted = db.ApplayForJobs.Find(accpet) ;
                accpted.Accept = true;
                db.SaveChanges();
                List<ApplayForJob> applies = db.ApplayForJobs.Where(x => x.JobId == jobid).ToList();
                return PartialView("_Applies", applies);

            }
            else
            {
                List<ApplayForJob> applies = db.ApplayForJobs.Where(x => x.JobId == id).ToList();
                return PartialView("_Applies", applies);
            }
           

            
        }

        public ActionResult Reject(int JobId, int applayId)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var Rejected = db.ApplayForJobs.Find(applayId);
            if (Rejected == null)
            {
                return HttpNotFound();
            }
            else
            {
                db.ApplayForJobs.Remove(Rejected);
                db.SaveChanges();
                List<ApplayForJob> applies = db.ApplayForJobs.Where(x => x.JobId == JobId).ToList();

                return PartialView("_Applies", applies);

            }


        }

        public ActionResult DeleteApplay(int id)
        {
            var DeletedApplies = db.ApplayForJobs.Find(id);
            string Id = DeletedApplies.Userid;
            db.ApplayForJobs.Remove(DeletedApplies);
            db.SaveChanges();

            
           
            return RedirectToAction("SreacherProfile","profile",new {id=Id});
            
        }

        //[HttpGet]
        //public ActionResult EditProfile(string id)
        //{
        //    ViewBag.UserType = new SelectList(db.Roles.Where(x => !x.Name.Contains("admins")).ToList(), "Name", "Name");

        //    ViewBag.Country = new SelectList(new[] { "مصر", "الامارات", "الكويت", "السعودية", "اخري" });
        //    var user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        return View(user);
        //    }

        //}
        //[HttpPost]
        //public ActionResult EditProfile(ApplicationUser user, string CurrentPassword, string NewPassword, string code, HttpPostedFileBase image)
        //{
        //    ViewBag.UserType = new SelectList(db.Roles.Where(x => !x.Name.Contains("admins")).ToList(), "Name", "Name");

        //    ViewBag.Country = new SelectList(new[] { "مصر", "الامارات", "الكويت", "السعودية", "اخري" });
        //    var olduser = db.Users.Find(user.Id);
        //    if (!UserManager.CheckPassword(olduser, CurrentPassword))
        //    {
        //        ViewBag.Message = "كلمة السر الحالية غير صحيحة";
        //    }

        //    else
        //    {
        //        var pass = UserManager.PasswordHasher.HashPassword(NewPassword);
        //        olduser.PasswordHash = pass;
        //        olduser.Country = user.Country;
        //        olduser.PhoneVisitor = code + user.PhoneVisitor;
        //        if (image == null)
        //        {

        //            db.Entry(olduser).State = System.Data.Entity.EntityState.Modified;
        //            db.SaveChanges();
        //        }
        //        else
        //        {

        //        }


        //    }

        //    return View();

        //}


       

    }
}