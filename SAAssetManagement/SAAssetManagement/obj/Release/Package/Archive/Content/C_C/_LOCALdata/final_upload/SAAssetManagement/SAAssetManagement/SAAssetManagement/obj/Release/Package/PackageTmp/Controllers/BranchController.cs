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
    public class BranchController : Controller
    {
        private AssetContext db = new AssetContext();

        //
        // GET: /Branch/
        [Authorize(Users = "admin")]
        public ActionResult Index()
        {
            var branches = db.Branches.Include(b => b.Division);
            return View(branches.ToList());
        }

        //
        // GET: /Branch/Details/5
        [Authorize(Users = "admin")]
        public ActionResult Details(int id = 0)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        //
        // GET: /Branch/Create
        [Authorize(Users = "admin")]
        public ActionResult Create()
        {
            ViewBag.DivisionID = new SelectList(db.Divisions, "DivisionID", "DivisionName");
            return View();
        }

        //
        // POST: /Branch/Create
        [Authorize(Users = "admin")]
        [HttpPost]
        public ActionResult Create(Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Branches.Add(branch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DivisionID = new SelectList(db.Divisions, "DivisionID", "DivisionName", branch.DivisionID);
            return View(branch);
        }

        //
        // GET: /Branch/Edit/5
        [Authorize(Users = "admin")]
        public ActionResult Edit(int id = 0)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            ViewBag.DivisionID = new SelectList(db.Divisions, "DivisionID", "DivisionName", branch.DivisionID);
            return View(branch);
        }

        //
        // POST: /Branch/Edit/5
        [Authorize(Users = "admin")]
        [HttpPost]
        public ActionResult Edit(Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(branch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DivisionID = new SelectList(db.Divisions, "DivisionID", "DivisionName", branch.DivisionID);
            return View(branch);
        }

        //
        // GET: /Branch/Delete/5
        [Authorize(Users = "admin")]
        public ActionResult Delete(int id = 0)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        //
        // POST: /Branch/Delete/5
        [Authorize(Users = "admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Branch branch = db.Branches.Find(id);
            db.Branches.Remove(branch);
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