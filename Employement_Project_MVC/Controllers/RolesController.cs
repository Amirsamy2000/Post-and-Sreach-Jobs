﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Employement_Project_MVC.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Employement_Project_MVC.Controllers
{
    [Authorize(Roles ="admins")]
    public class RolesController : Controller
    {
        ApplicationDbContext db=new ApplicationDbContext();
        // GET: Roles
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // GET: Roles/Details/5
        public ActionResult Details(string id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {
            
                if (ModelState.IsValid)
                {
                   db.Roles.Add(role);
                   db.SaveChanges();
                   return RedirectToAction("Admin","Admin");
                }
                return View(role);

           
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(string id)
        {
            var role=db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }

            return View(role);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include ="Id,Name")] IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("admin","admin");
            }
            return View(role);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(string id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);

        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(IdentityRole role)
        {
            try
            {
                var myrole = db.Roles.Find(role.Id);
                db.Roles.Remove(myrole);
                db.SaveChanges();

                return RedirectToAction("admin","admin");
            }
            catch
            {
                return View(role);
            }
            
        }
    }
}
