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
    public class DivisionController : Controller
    {
        private AssetContext db = new AssetContext();

        //
        // GET: /Division/
        [Authorize(Users = "admin")]
        public ActionResult Index()
        {
            return View(db.Divisions.ToList());
        }

        //
        // GET: /Division/Details/5
        [Authorize(Users = "admin")]
        public ActionResult Details(int id = 0)
        {
            Division division = db.Divisions.Find(id);
            if (division == null)
            {
                return HttpNotFound();
            }
            return View(division);
        }

        //
        // GET: /Division/Create
        [Authorize(Users = "admin")]
        public ActionResult Create()
        {
            //ViewBag.employeeid = new SelectList(db.Employees, "EmployeeID", "EmployeeFirstName");
            var query = from c in db.Employees
                        select new {employeeid = c.EmployeeID,
                                    FullNames = c.EmployeeFirstName + " " + c.EmployeeLastName};
            ViewBag.employeeid = new SelectList(query, "employeeid", "FullNames");
            return View();
        }

        //
        // POST: /Division/Create
        [Authorize(Users = "admin")]
        [HttpPost]
        public ActionResult Create(Division division)
        {
            if (ModelState.IsValid)
            {
                db.Divisions.Add(division);

                division.LastUpdatedOn = DateTime.Now;
                division.CreatedOn = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //var query = from c in db.Employees
            //            select new { employeeid = c.EmployeeID, Names = c.EmployeeFirstName + " " + c.EmployeeLastName };
            //ViewBag.employeeid = new SelectList(query, "employeeid", "Names",division.EmployeeID);
            return View(division);
        }

        //
        // GET: /Division/Edit/5
        [Authorize(Users = "admin")]
        public ActionResult Edit(int id = 0)
        {
            Division division = db.Divisions.Find(id);
            if (division == null)
            {
                return HttpNotFound();
            }
            //var query = from c in db.Employees
            //            select new { employeeid = c.EmployeeID, Names = c.EmployeeFirstName + " " + c.EmployeeLastName };
            //ViewBag.employeeid = new SelectList(query, "employeeid", "Names",division.EmployeeID);
            return View(division);
        }

        //
        // POST: /Division/Edit/5
        [Authorize(Users = "admin")]
        [HttpPost]
        public ActionResult Edit(Division division)
        {
            if (ModelState.IsValid)
            {
                db.Entry(division).State = EntityState.Modified;
                division.LastUpdatedOn = DateTime.Now;
                division.CreatedOn = division.CreatedOn;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //var query = from c in db.Employees
            //            select new { employeeid = c.EmployeeID, Names = c.EmployeeFirstName + " " + c.EmployeeLastName };
            //ViewBag.employeeid = new SelectList(query, "employeeid", "Names",division.EmployeeID);
            return View(division);
        }

        //
        // GET: /Division/Delete/5
        [Authorize(Users = "admin")]
        public ActionResult Delete(int id = 0)
        {
            Division division = db.Divisions.Find(id);
            if (division == null)
            {
                return HttpNotFound();
            }
            return View(division);
        }

        //
        // POST: /Division/Delete/5
        [Authorize(Users = "admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Division division = db.Divisions.Find(id);
            db.Divisions.Remove(division);
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