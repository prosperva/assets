using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAAssetManagement.Models;

namespace SAAssetManagement.Controllers
{
    public class SoftwareController : Controller
    {
        private AssetContext db = new AssetContext();

        //
        // GET: /Software/
        [Authorize(Users = "admin")]
        public ActionResult Index()
        {
            return View(db.Softwares.ToList());
        }

        //
        // GET: /Software/Details/5
        [Authorize(Users = "admin")]
        public ActionResult Details(int id = 0)
        {
            Software software = db.Softwares.Find(id);
            if (software == null)
            {
                return HttpNotFound();
            }
            return View(software);
        }

        //
        // GET: /Software/Create
        [Authorize(Users = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Software/Create
        [Authorize(Users = "admin")]
        [HttpPost]
        public ActionResult Create(Software software)
        {
            if (ModelState.IsValid)
            {
                db.Softwares.Add(software);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(software);
        }

        //
        // GET: /Software/Edit/5
        [Authorize(Users = "admin")]
        public ActionResult Edit(int id = 0)
        {
            Software software = db.Softwares.Find(id);
            if (software == null)
            {
                return HttpNotFound();
            }
            ViewBag.OSSystemID = new SelectList(db.OSSystems, "OSSystemID", "OSSystemname", software.OSSystemID);
            return View(software);
        }

        //
        // POST: /Software/Edit/5
        [Authorize(Users = "admin")]
        [HttpPost]
        public ActionResult Edit(Software software)
        {
            if (ModelState.IsValid)
            {
                db.Entry(software).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OSSystemID = new SelectList(db.OSSystems, "OSSystemID", "OSSystemname", software.OSSystemID);
            return View(software);
        }

        //
        // GET: /Software/Delete/5
        [Authorize(Users = "admin")]
        public ActionResult Delete(int id = 0)
        {
            Software software = db.Softwares.Find(id);
            if (software == null)
            {
                return HttpNotFound();
            }
            return View(software);
        }

        //
        // POST: /Software/Delete/5
        [Authorize(Users = "admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Software software = db.Softwares.Find(id);
            db.Softwares.Remove(software);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}