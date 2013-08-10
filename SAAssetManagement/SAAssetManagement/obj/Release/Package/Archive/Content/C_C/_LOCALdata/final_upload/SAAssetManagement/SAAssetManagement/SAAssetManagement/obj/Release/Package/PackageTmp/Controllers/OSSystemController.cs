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
    public class OSSystemController : Controller
    {
        private AssetContext db = new AssetContext();

        //
        // GET: /OSSystem/
        [Authorize(Users = "admin")]
        public ActionResult Index()
        {
            return View(db.OSSystems.ToList());
        }

        //
        // GET: /OSSystem/Details/5
        [Authorize(Users = "admin")]
        public ActionResult Details(int id = 0)
        {
            OSSystem ossystem = db.OSSystems.Find(id);
            if (ossystem == null)
            {
                return HttpNotFound();
            }
            return View(ossystem);
        }

        //
        // GET: /OSSystem/Create
        [Authorize(Users = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /OSSystem/Create
        [Authorize(Users = "admin")]
        [HttpPost]
        public ActionResult Create(OSSystem ossystem)
        {
            if (ModelState.IsValid)
            {
                db.OSSystems.Add(ossystem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ossystem);
        }

        //
        // GET: /OSSystem/Edit/5
        [Authorize(Users = "admin")]
        public ActionResult Edit(int id = 0)
        {
            OSSystem ossystem = db.OSSystems.Find(id);
            if (ossystem == null)
            {
                return HttpNotFound();
            }
            return View(ossystem);
        }

        //
        // POST: /OSSystem/Edit/5
        [Authorize(Users = "admin")]
        [HttpPost]
        public ActionResult Edit(OSSystem ossystem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ossystem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ossystem);
        }

        //
        // GET: /OSSystem/Delete/5
        [Authorize(Users = "admin")]
        public ActionResult Delete(int id = 0)
        {
            OSSystem ossystem = db.OSSystems.Find(id);
            if (ossystem == null)
            {
                return HttpNotFound();
            }
            return View(ossystem);
        }

        //
        // POST: /OSSystem/Delete/5
        [Authorize(Users = "admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            OSSystem ossystem = db.OSSystems.Find(id);
            db.OSSystems.Remove(ossystem);
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