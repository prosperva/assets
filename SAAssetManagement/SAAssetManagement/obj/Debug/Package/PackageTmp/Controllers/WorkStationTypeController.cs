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
    public class WorkStationTypeController : Controller
    {
        private AssetContext db = new AssetContext();

        //
        // GET: /WorkStationType/
        [Authorize(Users = "admin")]
        public ActionResult Index()
        {
            return View(db.WorkStationTypes.ToList());
        }

        //
        // GET: /WorkStationType/Details/5
        [Authorize(Users = "admin")]
        public ActionResult Details(int id = 0)
        {
            WorkStationType workstationtype = db.WorkStationTypes.Find(id);
            if (workstationtype == null)
            {
                return HttpNotFound();
            }
            return View(workstationtype);
        }

        //
        // GET: /WorkStationType/Create
        [Authorize(Users = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /WorkStationType/Create
        [Authorize(Users = "admin")]
        [HttpPost]
        public ActionResult Create(WorkStationType workstationtype)
        {
            if (ModelState.IsValid)
            {
                db.WorkStationTypes.Add(workstationtype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workstationtype);
        }

        //
        // GET: /WorkStationType/Edit/5
        [Authorize(Users = "admin")]
        public ActionResult Edit(int id = 0)
        {
            WorkStationType workstationtype = db.WorkStationTypes.Find(id);
            if (workstationtype == null)
            {
                return HttpNotFound();
            }
            return View(workstationtype);
        }

        //
        // POST: /WorkStationType/Edit/5
        [Authorize(Users = "admin")]
        [HttpPost]
        public ActionResult Edit(WorkStationType workstationtype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workstationtype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workstationtype);
        }

        //
        // GET: /WorkStationType/Delete/5
        [Authorize(Users = "admin")]
        public ActionResult Delete(int id = 0)
        {
            WorkStationType workstationtype = db.WorkStationTypes.Find(id);
            if (workstationtype == null)
            {
                return HttpNotFound();
            }
            return View(workstationtype);
        }

        //
        // POST: /WorkStationType/Delete/5
        [Authorize(Users = "admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkStationType workstationtype = db.WorkStationTypes.Find(id);
            db.WorkStationTypes.Remove(workstationtype);
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