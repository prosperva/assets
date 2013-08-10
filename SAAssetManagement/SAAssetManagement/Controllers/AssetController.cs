using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAAssetManagement.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.Entity.Validation;

//using StackExchange.Profiling;
//using System.Threading;

namespace SAAssetManagement.Controllers
{

    public static class JSONHelper 
    {
        public static string ToJSON(this System.Web.Mvc.FormCollection collection) 
        {
            var list = new Dictionary<string, string>();
            foreach (string key in collection.Keys) {
                list.Add(key, collection[key]);
            }
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(list);
        }
    }
    
    public class AssetController : Controller
    {
        private AssetContext db = new AssetContext();
        //
        // GET: /Asset/
        //
        [Authorize(Users = "admin")]
        public ActionResult Index()
        {
            var assets = db.Assets.Include("AssetType");
            return View(assets.ToList());
        }

        [Authorize(Users = "admin")]
        public ActionResult LoadAssets(jQueryDataTableParamModel param)
        {
            var all_assets = db.Assets;
            IEnumerable<Asset> filteredAssets;
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<Asset, string> orderingFunction = (c => sortColumnIndex == 1 ? c.AssetTag : sortColumnIndex == 2 ? c.PONumber : c.MakeModel);

            if (!string.IsNullOrEmpty(param.sSearch)){
                filteredAssets = all_assets
                                            .Where(c => c.AssetTag.Contains(param.sSearch)||c.PONumber.Contains(param.sSearch)||c.MakeModel.Contains(param.sSearch));
            }
            else{
                filteredAssets = all_assets;
            }

            var sortDirection = Request["sSortDir_0"];
            if (sortDirection == "asc")
                filteredAssets = filteredAssets.OrderBy(orderingFunction);
            else
                filteredAssets = filteredAssets.OrderByDescending(orderingFunction);

            var displayedAssets = filteredAssets
                .Skip(param.iDisplayStart)
                .Take(param.iDisplayLength);

            var result = displayedAssets
                .AsEnumerable()
                .Select(a => new List<string> { a.AssetID.ToString(),a.AssetTag, a.PONumber, a.MakeModel })
                .ToList();

            var jsonData = new {
                sEcho = param.sEcho,
                iTotalRecords = all_assets.Count(),
                iTotalDisplayRecords = all_assets.Count(),
                aaData = result
            };

            return Json(jsonData,JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Asset/Details/5
        [Authorize(Users = "admin")]
        public ActionResult Details(int id = 0)
        {
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

      
        [Authorize(Users = "admin")]
        public ActionResult Create()
        {
            return View();
        }
        //
        // POST: /Asset/Create
        [Authorize(Users = "admin")]
        [HttpPost]
        public ActionResult Create(Asset asset){
            try
            {
                if (ModelState.IsValid)
                {
                    db.Assets.Add(asset);
                    db.SaveChanges();
                    return RedirectToAction("Edit/" + asset.AssetID);
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException ex)
            {
                var error = ex.EntityValidationErrors.First().ValidationErrors.First();
                this.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                //return View();
            }
            return View(asset);
        }

        // GET: /Asset/Edit/5

        [Authorize(Users = "admin")]
        public ActionResult Edit(int id = 0)
        {
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            
            
            return View(asset);
        }

        private void PopulateAssignedSoftware(Asset asset)
        {
        }

        [Authorize(Users = "admin")]
        [HttpPost]
        public ActionResult Edit(Asset asset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asset);
        }

        // GET: /Asset/Delete/5

        [Authorize(Users = "admin")]
        public ActionResult Delete(int id = 0)
        {
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        //
        // POST: /Asset/Delete/5

        [Authorize(Users = "admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Asset asset = db.Assets.Find(id);
            db.Assets.Remove(asset);
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