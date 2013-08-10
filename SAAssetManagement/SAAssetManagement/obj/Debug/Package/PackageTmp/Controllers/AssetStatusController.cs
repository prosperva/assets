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
    public class AssetStatusController : Controller
    {
        private AssetContext db = new AssetContext();

        //
        // GET: /AssetStatus/
        [Authorize(Users = "admin")]
        public ActionResult Index()
        {
            return View(db.AssetStatuses.ToList());
        }

        //
        // GET: /AssetStatus/Details/5
        [Authorize(Users = "admin")]
        public ActionResult Details(int id = 0)
        {
            AssetStatus assetstatus = db.AssetStatuses.Find(id);
            if (assetstatus == null)
            {
                return HttpNotFound();
            }
            return View(assetstatus);
        }

        //
        // GET: /AssetStatus/Create
        [Authorize(Users = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AssetStatus/Create

        [Authorize(Users = "admin")]
        [HttpPost]
        public ActionResult Create(AssetStatus assetstatus)
        {
            if (ModelState.IsValid)
            {
                db.AssetStatuses.Add(assetstatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assetstatus);
        }

        //
        // GET: /AssetStatus/Edit/5
        [Authorize(Users = "admin")]
        public ActionResult Edit(int id = 0)
        {
            AssetStatus assetstatus = db.AssetStatuses.Find(id);
            if (assetstatus == null)
            {
                return HttpNotFound();
            }
            return View(assetstatus);
        }

        //
        // POST: /AssetStatus/Edit/5

        [Authorize(Users = "admin")]
        [HttpPost]
        public ActionResult Edit(AssetStatus assetstatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assetstatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assetstatus);
        }

        //
        // GET: /AssetStatus/Delete/5
        [Authorize(Users = "admin")]
        public ActionResult Delete(int id = 0)
        {
            AssetStatus assetstatus = db.AssetStatuses.Find(id);
            if (assetstatus == null)
            {
                return HttpNotFound();
            }
            return View(assetstatus);
        }

        //
        // POST: /AssetStatus/Delete/5
        [Authorize(Users = "admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            AssetStatus assetstatus = db.AssetStatuses.Find(id);
            db.AssetStatuses.Remove(assetstatus);
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