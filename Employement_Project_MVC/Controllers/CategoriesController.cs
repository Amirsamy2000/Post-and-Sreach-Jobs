﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Employement_Project_MVC.Models;
using Microsoft.AspNet.Identity;

namespace Employement_Project_MVC.Controllers
{
    [Authorize]

    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            var userId = User.Identity.GetUserId();
            ViewBag.Role = db.Users.Where(x => x.Id == userId).Select(x => x.UserType).FirstOrDefault();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            ViewBag.Role = db.Users.Where(x => x.Id == userId).Select(x => x.UserType).FirstOrDefault();
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryName,CategoryDiscraption")] Category category)
        {
            var userId = User.Identity.GetUserId();
            ViewBag.Role = db.Users.Where(x => x.Id == userId).Select(x => x.UserType).FirstOrDefault();
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                

                    return RedirectToAction("admin", "admin");

               
            }
            
            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            var userId = User.Identity.GetUserId();
            ViewBag.Role = db.Users.Where(x => x.Id == userId).Select(x => x.UserType).FirstOrDefault();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryName,CategoryDiscraption")] Category category)
        {
            var userId = User.Identity.GetUserId();
            ViewBag.Role = db.Users.Where(x => x.Id == userId).Select(x => x.UserType).FirstOrDefault();
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                

                    return RedirectToAction("admin", "admin");

                
                
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            var userId = User.Identity.GetUserId();
            ViewBag.Role = db.Users.Where(x => x.Id == userId).Select(x => x.UserType).FirstOrDefault();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var userId = User.Identity.GetUserId();
            ViewBag.Role = db.Users.Where(x => x.Id == userId).Select(x => x.UserType).FirstOrDefault();
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
           

                return RedirectToAction("admin", "admin");

           
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
