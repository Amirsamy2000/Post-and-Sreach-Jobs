using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Employement_Project_MVC.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace Employement_Project_MVC.Controllers
{
    [Authorize]
    public class JobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Jobs
        public ActionResult Index()
        {
            var jobs = db.Jobs.Include(j => j.cat);
            return View(jobs.ToList());
        }

        // GET: Jobs/Details/5
        public ActionResult Details(int? id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Jobs/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName");
           var userId = User.Identity.GetUserId();
            ViewBag.Role = db.Users.Where(x => x.Id == userId).Select(x => x.UserType).FirstOrDefault();
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Job job, HttpPostedFileBase upload)
        {
            var userId = User.Identity.GetUserId();
            ViewBag.Role = db.Users.Where(x => x.Id == userId).Select(x => x.UserType).FirstOrDefault();
            if (ModelState.IsValid)
            {
                string filename = Guid.NewGuid() + upload.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"),filename);
                upload.SaveAs(path);
                job.JobImage = filename;
                job.UaerID = User.Identity.GetUserId();
                db.Jobs.Add(job);
                db.SaveChanges();
                if (ViewBag.Role == "admins")
                {

                    return RedirectToAction("admin","admin");

                }
                else
                {
                    return RedirectToAction("Publisher", "profile", new {Id=userId});
                }
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", job.CategoryId);
            return View(job);
        }

        // GET: Jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            var userId = User.Identity.GetUserId();
            ViewBag.Role = db.Users.Where(x => x.Id == userId).Select(x => x.UserType).FirstOrDefault();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);

            if (job == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", job.CategoryId);
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Job job, HttpPostedFileBase upload)
        {
            var userId = User.Identity.GetUserId();
            ViewBag.Role = db.Users.Where(x => x.Id == userId).Select(x => x.UserType).FirstOrDefault();
            if (ModelState.IsValid)
            {
               
                if (upload != null)
                {
                    string pathold = Path.Combine(Server.MapPath("~/Uploads"), job.JobImage);

                    System.IO.File.Delete(pathold);
                    string filename = Guid.NewGuid() + upload.FileName;
                    string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                    upload.SaveAs(path);
                    job.JobImage = filename;
                

                }
                
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                if (ViewBag.Role == "admins")
                {

                    return RedirectToAction("admin", "admin");

                }
                else
                {
                    return RedirectToAction("Publisher", "profile", new { Id = userId });
                }
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", job.CategoryId);
            return View(job);
        }

        // GET: Jobs/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var userId = User.Identity.GetUserId();
            ViewBag.Role = db.Users.Where(x => x.Id == userId).Select(x => x.UserType).FirstOrDefault();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Job job = db.Jobs.Find(id);

                return View(job);
            }
            
          
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

           
            var job = db.Jobs.Find(id);
                db.Jobs.Remove(job);
                db.SaveChanges();
            var userId = User.Identity.GetUserId();
            ViewBag.Role = db.Users.Where(x => x.Id == userId).Select(x => x.UserType).FirstOrDefault();

            if (ViewBag.Role == "admins")
            {

                return RedirectToAction("admin", "admin");

            }
            else
            {
                return RedirectToAction("Publisher", "Profile", new { Id = userId });

            }


        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
