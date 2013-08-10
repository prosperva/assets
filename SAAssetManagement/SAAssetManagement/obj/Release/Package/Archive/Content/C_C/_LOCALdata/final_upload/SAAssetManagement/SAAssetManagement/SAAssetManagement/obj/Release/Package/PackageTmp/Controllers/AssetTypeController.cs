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
    public class AssetTypeController : Controller
    {
        private AssetContext db = new AssetContext();

        //
        // GET: /AssetType/
        [Authorize(Users = "admin")]
        public ActionResult Index()
        {
            return View(db.AssetTypes.ToList());
        }

        //
        // GET: /AssetType/Details/5
        [Authorize(Users = "admin")]
        public ActionResult Details(int id = 0)
        {
            AssetType assettype = db.AssetTypes.Find(id);
            if (assettype == null)
            {
                return HttpNotFound();
            }
            return View(assettype);
        }

        //
        // GET: /AssetType/Create
        [Authorize(Users = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AssetType/Create
        [Authorize(Users = "admin")]
        [HttpPost]
        public ActionResult Create(AssetType assettype)
        {
            if (ModelState.IsValid)
            {
                db.AssetTypes.Add(assettype);
                assettype.CreatedOn = DateTime.Now;
                assettype.LastUpdatedOn = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assettype);
        }

        //
        // GET: /AssetType/Edit/5
        [Authorize(Users = "admin")]
        public ActionResult Edit(int id = 0)
        {
            AssetType assettype = db.AssetTypes.Find(id);
            if (assettype == null)
            {
                return HttpNotFound();
            }
            return View(assettype);
        }

        //
        // POST: /AssetType/Edit/5
        [Authorize(Users = "admin")]
        [HttpPost]
        public ActionResult Edit(AssetType assettype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assettype).State = EntityState.Modified;
                assettype.CreatedOn = assettype.CreatedOn;
                assettype.LastUpdatedOn = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assettype);
        }

        //
        // GET: /AssetType/Delete/5
        [Authorize(Users = "admin")]
        public ActionResult Delete(int id = 0)
        {
            AssetType assettype = db.AssetTypes.Find(id);
            if (assettype == null)
            {
                return HttpNotFound();
            }
            return View(assettype);
        }

        //
        // POST: /AssetType/Delete/5
        [Authorize(Users = "admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            AssetType assettype = db.AssetTypes.Find(id);
            db.AssetTypes.Remove(assettype);
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