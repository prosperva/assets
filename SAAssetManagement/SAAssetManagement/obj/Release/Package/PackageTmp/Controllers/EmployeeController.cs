using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAAssetManagement.Models;
using Newtonsoft.Json;

namespace SAAssetManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private AssetContext db = new AssetContext();

        //
        // GET: /Employee/
        [Authorize(Users = "admin")]
        public ActionResult Index()
        {
            return View(db.Employees.ToList().OrderBy(e => e.EmployeeFirstName));
        }

        //
        // GET: /Employee/Details/5
        [Authorize(Users = "admin")]
        public ActionResult Details(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            
            return View(employee);
        }

       private void CreateEmployeeDropDowns(int? employeeid=0,int? divisionid=0,int? branchid=0,int? buildingid=0) {
            
            var query = from c in db.Employees
                        select new { employeeid = c.EmployeeID, FullNames = c.EmployeeFirstName + " " + c.EmployeeLastName };
            ViewBag.manager = new SelectList(query, "employeeid", "FullNames",employeeid);
            
            var division_query = from c in db.Divisions
                                 select new { divisionid = c.DivisionID, divisionname = c.DivisionName };
            ViewBag.divisionid = new SelectList(division_query, "divisionid", "divisionname",divisionid);

            var branch_query = from c in db.Branches
                               select new { branchid = c.BranchID, branchname = c.BranchName };
            ViewBag.branchid = new SelectList(branch_query, "branchid", "branchname",branchid);

            var building_query = from c in db.Buildings
                               select new { buildingid = c.BuildingID, buildingname = c.BuildingName };
            ViewBag.buildingid = new SelectList(building_query, "buildingid", "buildingname", buildingid);
        }
        [Authorize(Users = "admin")]
        public ActionResult Create()
        {
            CreateEmployeeDropDowns();
            CreateDivisionArray();
            return View();
        }

        //
        // POST: /Employee/Create
        [Authorize(Users = "admin")]
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                //var currentTime = DateTime.Now;
                //employee.CreatedOn = currentTime;
                //employee.LastUpdatedOn = currentTime;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            CreateEmployeeDropDowns();
            CreateDivisionArray();
            return View(employee);
        }

        // all divisions and branches
        private void CreateDivisionArray()
        {
            string json = "";

            var all_divisions = db.Divisions.Select(c => new { id = c.DivisionID, value = c.DivisionName }).ToArray();
            var all_branches = db.Branches.Select(b => new { id = b.BranchID,value = b.BranchName,division = b.DivisionID}).ToArray();

            json = JsonConvert.SerializeObject(all_divisions);
            ViewBag.all_divisions = json;

            json = JsonConvert.SerializeObject(all_branches);
            ViewBag.all_branches = json;
        }
        [Authorize(Users = "admin")]
        public ActionResult Edit(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            CreateEmployeeDropDowns(employee.Manager,employee.DivisionID,employee.BranchID,employee.BuildingID);
            
            CreateDivisionArray();
            return View(employee);
        }

        //
        // POST: /Employee/Edit/5
        [Authorize(Users = "admin")]
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                //employee.LastUpdatedOn = DateTime.Now;
                //employee.CreatedOn = employee.CreatedOn;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            CreateEmployeeDropDowns(employee.Manager,employee.DivisionID,employee.BranchID,employee.BuildingID);
            CreateDivisionArray();
            return View(employee);
        }

        //
        // GET: /Employee/Delete/5
        [Authorize(Users = "admin")]
        public ActionResult Delete(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // POST: /Employee/Delete/5
        [Authorize(Users = "admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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