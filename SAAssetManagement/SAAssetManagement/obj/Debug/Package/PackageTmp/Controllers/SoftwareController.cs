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

        [Authorize(Users = "admin")]
        public ActionResult LoadSoftware(jQueryDataTableParamModel param)
        {
            var all_software = db.Softwares;
            IEnumerable<Software> filteredSoftware;
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<Software, string> orderingFunction = (c => sortColumnIndex == 1 ? c.softwareName :
                                                        sortColumnIndex == 2 ? c.InstallNumber.ToString() :
                                                        c.OSSystem.OSSystemname);

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredSoftware = all_software
                                            .Where(c => c.softwareName.Contains(param.sSearch));
            }
            else
            {
                filteredSoftware = all_software;
            }

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredSoftware = filteredSoftware.OrderBy(orderingFunction);
            else
                filteredSoftware = filteredSoftware.OrderByDescending(orderingFunction);

            var displayedSoftware = filteredSoftware
                .Skip(param.iDisplayStart)
                .Take(param.iDisplayLength);

            var result = displayedSoftware
                .AsEnumerable()
                .Select(a => new List<string> { a.SoftwareID.ToString(), a.softwareName, a.InstallNumber.ToString(), getOSName(a.OSSystemID) })
                .ToList();

            var jsonData = new{
                sEcho = param.sEcho,
                iTotalRecords = all_software.Count(),
                iTotalDisplayRecords = all_software.Count(),
                aaData = result
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private string getOSName(int id) {
            return db.OSSystems.Find(id).OSSystemname;
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