using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Employement_Project_MVC.Models;
using Microsoft.AspNet.Identity;
using System.Net;
using System.IO;

namespace Employement_Project_MVC.Controllers
{
    public class HomeController : Controller
    {
       
        private ApplicationDbContext Db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var list_cats = Db.Categories.ToList();
           
            return View(list_cats);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactUsModel contact)
        {
            var mail = new MailMessage();
            var loginfo = new NetworkCredential("alameer.samy99@gmail.com", "uwsgjeaqiqcqfniu");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("alameer.samy99@gmail.com"));
            mail.IsBodyHtml = true;
            string body = ":اسم المرسل" + contact.Name + "<br>" +
                "بريد المرسل:" + contact.Email + "<br>" +
                "عنوان الرسالة:" + contact.Subject + "<br>" +
                "نص الرسالة:" +"<b>"+ contact.Message+"</b>" + "<br>";
            mail.Subject=contact.Subject;
            mail.Body = body;
           
            var smtpClient=new SmtpClient("smtp.gmail.com",587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = loginfo;
            smtpClient.Send(mail);




            return RedirectToAction("index");
        }

        public ActionResult Details(int? id)
        {

            var job = Db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            Session["job"] = job.Id;
          
            ViewBag.PublisherJob = Db.Users.Where(x => x.Id == job.UaerID).SingleOrDefault();
          
            var Userid = User.Identity.GetUserId();
           Session["Visitor"]= Db.Users.Where(x => x.Id == Userid).SingleOrDefault();
            if (Session["Visitor"] == null)
            {
                Session["Visitor"] = new ApplicationUser();
            }

                ViewBag.AppliesJob = Db.ApplayForJobs.Where(x => x.JobId == job.Id).Count();
            return View(job);
        }
        [HttpGet]
        [Authorize]
        public ActionResult Applay() {

            return View();
        }
        [HttpPost]
        public ActionResult Applay(ApplayForJob Applay,HttpPostedFileBase CV)
        {
            string filename = Guid.NewGuid() + CV.FileName;
            string path = Path.Combine(Server.MapPath("~/CVS"), filename);
           
            var userId = User.Identity.GetUserId();
            var jobId = (int)Session["job"];
            var checkJob = Db.Jobs.Where(x => x.UaerID == userId);

            Applay.AplayOn = DateTime.Now;

            Applay.JobId = jobId;
            Applay.Userid = userId;
            Applay.CvApplay = filename;

            var check = Db.ApplayForJobs.Where(x => x.JobId == jobId && x.Userid == userId);

            if (checkJob.Count() > 1)
            {
                ViewBag.result = "لا تسطتيع التقدم لانك انت الناشر";
            }
            else
            {
                if (check.Count() < 1)
                {
                    CV.SaveAs(path);
                    Db.ApplayForJobs.Add(Applay);
                    Db.SaveChanges();
                    ViewBag.result = "تمت التقديم بنجاح";
                }
                else
                {
                    ViewBag.result = "المعذرة!! تم التقديم مسبقا";
                }
               
            }
            return RedirectToAction("Index");

        }

        public ActionResult GetJob()
        {
            string id = User.Identity.GetUserId();
            var jobSApplied = Db.ApplayForJobs.Where(x => x.Userid == id);
            return View(jobSApplied.ToList());
        }

        public ActionResult DetialsJob(int id)
        {
            var ApplayJob = Db.ApplayForJobs.Find(id);
            if (ApplayJob==null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(ApplayJob);
            }
           
        }
        public ActionResult Edit(int id)
        {
            var job = Db.ApplayForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }

            return View(job);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit(ApplayForJob job)
        {
            if (ModelState.IsValid)
            {

                job.AplayOn = DateTime.Now;
                Db.Entry(job).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("GetJob");
            }
            return View(job);
        }

        public ActionResult Delete(int id)
        {
            var job = Db.ApplayForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);

        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(ApplayForJob job)
        {
            
                var job1 = Db.ApplayForJobs.Find(job.Id);
                Db.ApplayForJobs.Remove(job1);
                Db.SaveChanges();
                return RedirectToAction("GetJob");
            

        }
        [Authorize]
        public ActionResult GetJobPublisher()
        {
            string id = User.Identity.GetUserId();
            var jobs = from app in Db.ApplayForJobs
                       join job in Db.Jobs
                       on app.JobId equals job.Id
                       where job.UaerID == id
                       select app;
            var groups = from j in jobs
                         group j by j.job.JobTtile
                        into gr
                         select new JobsViewModel
                         {
                             JobTitle = gr.Key,
                             Applies = gr
                         };
            return View(groups.ToList());
        }

        [Authorize]
        public ActionResult PublishJob()
        {
            string id = User.Identity.GetUserId();

            var jobs = from app in Db.ApplayForJobs
                       join app2 in Db.Jobs
                       on app.JobId equals app2.Id
                       where app2.UaerID == id
                       select app;
            
           var groups = jobs.GroupBy(x => x.job.JobTtile);

            List<JobsViewModel> l = new List<JobsViewModel>(); 
            foreach (var item in groups)
            {
                l.Add(new JobsViewModel() { JobTitle = item.Key, Applies = item.Select(x => x) });
            }
            if (jobs == null)
            {
                return HttpNotFound();
            }

            return View(l);

        }
       
        public ActionResult Sreiveces(int id)
        {
            var Category = Db.Categories.Find(id);
            if (Category == null)
            {
                return HttpNotFound();
            }
            return View(Category);

        }
        [HttpGet]
        public ActionResult sreach()
        {
            return View();
        }
        [HttpPost]
        public ActionResult sreach(string SreachName)
        {
            var job = Db.Jobs.Where(x => x.JobTtile.Contains(SreachName) || x.JobContent.Contains(SreachName)
        || x.cat.CategoryName.Contains(SreachName) || x.cat.CategoryDiscraption.Contains(SreachName) ||
        x.SreachEngigJob.Contains(SreachName));
            if (job == null)
            {
                var jobs = new List<Job>();
                return View(jobs);
            }
            return View(job.ToList());
        }

    }
}