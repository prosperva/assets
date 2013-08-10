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
            //var profiler = MiniProfiler.Current; // it's ok if this is null
            //using (profiler.Step("Load assets"))
            //{
            //    using (profiler.Step("Step A"))
            //    { // something more interesting here
            var assets = db.Assets.Include("AssetType");
            return View(assets.ToList());
            //    }
            //}
        }

        [Authorize(Users = "admin")]
        public ActionResult LoadAssets(jQueryDataTableParamModel param)
        {
            var all_assets = db.Assets;
            IEnumerable<Asset> filteredAssets;
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<Asset, string> orderingFunction = (c => sortColumnIndex == 1 ? c.AssetTag :
                                                        sortColumnIndex == 2 ? c.PONumber :
                                                        c.MakeModel);


            if (!string.IsNullOrEmpty(param.sSearch)){
                filteredAssets = all_assets
                                            .Where(c => c.AssetTag.Contains(param.sSearch)||c.PONumber.Contains(param.sSearch)||c.MakeModel.Contains(param.sSearch));
            }
            else{
                filteredAssets = all_assets;
            }

            var sortDirection = Request["sSortDir_0"]; // asc or desc
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

        private void GenerateFormSelectData(int id =0)
        {
            Asset asset = db.Assets.Find(id);
            string json = "";                
            
            var all_divisions = db.Divisions.Select(c => new { id = c.DivisionID, value = c.DivisionName }).ToArray();
            var all_branches = db.Branches.Select(b => new { id = b.BranchID, value = b.BranchName, division = b.DivisionID }).ToArray();
            var all_employees = db.Employees.Select(b => new { id = b.EmployeeID, value = b.UserID }).ToArray();
            
            json = JsonConvert.SerializeObject(all_divisions);
            ViewBag.all_divisions = json;
            json = JsonConvert.SerializeObject(all_branches);
            ViewBag.all_branches = json;
            json = JsonConvert.SerializeObject(all_employees);
            ViewBag.all_employees = json;

            //var query = from c in db.Employees select new {employeeid = c.EmployeeID,FullNames = c.EmployeeFirstName + " " + c.EmployeeLastName };

            if (asset == null || asset.AssetID == 0){
                //var ALLSOFTWARES_XP = db.Softwares.Select(c => new { id = c.SoftwareID, value = c.softwareName, OS = c.OSSystemID }).Where(c => c.OS == 1).ToArray();
                //var ALLSOFTWARES_WIN7 = db.Softwares.Select(c => new { id = c.SoftwareID, value = c.softwareName, OS = c.OSSystemID }).Where(c => c.OS == 2).ToArray();
                var ALLSOFTWARES = db.Softwares.Select(c => new { id = c.SoftwareID, value = c.softwareName, OS = c.OSSystemID }).ToArray(); ;
              
                json = JsonConvert.SerializeObject(ALLSOFTWARES);
                ViewBag.ALLSOFTWARES = json;
                //json = JsonConvert.SerializeObject(DBSOFTWARES);
                //ViewBag.DBSOFTWARES = json;
                //json = JsonConvert.SerializeObject(ASSETSOFTWARE);
                //ViewBag.ASSETSOFTWARES = json;

                ViewBag.OSSystemID = new SelectList(db.OSSystems.OrderBy(t => t.OSSystemname), "OSSystemID", "OSSystemname");
                ViewBag.DivisionID = new SelectList(db.Divisions.OrderBy(t => t.DivisionName), "DivisionID", "DivisionName");
                ViewBag.BranchID = new SelectList(db.Branches.OrderBy(t => t.BranchName), "BranchID", "BranchName");
                //ViewBag.employeeid = new SelectList(query.OrderBy(e => e.FullNames), "employeeid", "FullNames");
                ViewBag.employeeid = new SelectList(db.Employees.OrderBy(e => e.UserID), "employeeid", "UserID");
                ViewBag.AssetTypeID = new SelectList(db.AssetTypes.OrderBy(t => t.AssetTypeName), "AssetTypeID", "AssetTypeName");
                ViewBag.WorkStationTypeID = new SelectList(db.WorkStationTypes.OrderBy(t => t.WorkStationTypeName), "WorkStationTypeID", "WorkStationTypeName");
                ViewBag.AssetStatusID = new SelectList(db.AssetStatuses.OrderBy(t => t.AssetStatusName), "AssetStatusID", "AssetStatusName");
                ViewBag.BuildingID = new SelectList(db.Buildings.OrderBy(t => t.BuildingName), "BuildingID", "BuildingName");
            }

            else{

                if (asset.SoftwarePerAssets != null){
                    var DBSOFTWARES = asset.Softwares.Select(c => new { id = c.SoftwareID, value = c.softwareName }).ToArray();
                    var ASSETSOFTWARE = asset.SoftwarePerAssets.Select(c => new { id = c.ID, softwareid = c.SoftwareID, installtype = c.install, serial = c.SerialKey, PO = c.PONumber }).ToArray();
                    json = JsonConvert.SerializeObject(ASSETSOFTWARE);
                    ViewBag.ASSETSOFTWARES = json;
                }
                var ALLSOFTWARES_XP = db.Softwares.Select(c => new { id = c.SoftwareID, value = c.softwareName,OS = c.OSSystemID }).Where(c => c.OS == 1).ToArray();
                var ALLSOFTWARES_WIN7 = db.Softwares.Select(c => new { id = c.SoftwareID, value = c.softwareName, OS = c.OSSystemID }).Where(c => c.OS == 2).ToArray();

                object ALLSOFTWARES;

                if (asset.OSSystemID == 1) { ALLSOFTWARES = ALLSOFTWARES_XP; }
                else { ALLSOFTWARES = ALLSOFTWARES_WIN7; }
                
                json = JsonConvert.SerializeObject(ALLSOFTWARES);
                ViewBag.ALLSOFTWARES = json;
                //json = JsonConvert.SerializeObject(DBSOFTWARES);
                //ViewBag.DBSOFTWARES = json;
                

                ViewBag.OSSystemID = new SelectList(db.OSSystems.OrderBy(t => t.OSSystemname), "OSSystemID", "OSSystemname", asset.OSSystemID);
                ViewBag.DivisionID = new SelectList(db.Divisions.OrderBy(t => t.DivisionName), "DivisionID", "DivisionName", asset.DivisionID);
                ViewBag.BranchID = new SelectList(db.Branches.OrderBy(t => t.BranchName), "BranchID", "BranchName", asset.BranchID);
                //ViewBag.employeeid = new SelectList(query.OrderBy(e => e.FullNames), "employeeid", "FullNames", asset.EmployeeID);
                ViewBag.employeeid = new SelectList(db.Employees.OrderBy(e => e.UserID), "employeeid", "UserID", asset.EmployeeID);
                ViewBag.AssetTypeID = new SelectList(db.AssetTypes.OrderBy(t => t.AssetTypeName), "AssetTypeID", "AssetTypeName", asset.AssetTypeID);
                ViewBag.WorkStationTypeID = new SelectList(db.WorkStationTypes.OrderBy(t => t.WorkStationTypeName), "WorkStationTypeID", "WorkStationTypeName", asset.WorkStationTypeID);
                ViewBag.AssetStatusID = new SelectList(db.AssetStatuses.OrderBy(t => t.AssetStatusName), "AssetStatusID", "AssetStatusName", asset.AssetStatusID);
                ViewBag.BuildingID = new SelectList(db.Buildings.OrderBy(t => t.BuildingName), "BuildingID", "BuildingName", asset.BuildingID);
            }
        }

        public ActionResult _workstation_create()
        {
            GenerateFormSelectData();
            return View();
        }
        public ActionResult _workstation(int id=0)
        {
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            GenerateFormSelectData(asset.AssetID);

            return View(asset);
        }
        public ActionResult printer()
        {
            GenerateFormSelectData();
            return View();
        }
        [Authorize(Users = "admin")]
        public ActionResult Create()
        {
            GenerateFormSelectData();
            return View();
        }
        //
        // POST: /Asset/Create
        [Authorize(Users = "admin")]
        [HttpPost]
        public ActionResult Create(Asset asset){
            try
            {
                //if (ModelState.IsValid)
                //{
                    db.Assets.Add(asset);
                    db.SaveChanges();
                    return RedirectToAction("Edit/" + asset.AssetID);
                    //return RedirectToAction("Index");
                //}
            }
            catch (DbEntityValidationException ex)
            {
                var error = ex.EntityValidationErrors.First().ValidationErrors.First();
                this.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                //return View();
            }
            GenerateFormSelectData(asset.AssetID);
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
            GenerateFormSelectData(asset.AssetID);

            string json = "";

            var ALLCARS = db.Softwares.Select(c => new { id = c.SoftwareID, value = c.softwareName }).ToArray();
            var DBCARS = asset.Softwares.Select(c => new { id = c.SoftwareID, value = c.softwareName }).ToArray();

            json = JsonConvert.SerializeObject(ALLCARS);
            ViewBag.ALLCARS = json;

            json = JsonConvert.SerializeObject(DBCARS);
            ViewBag.DBCARS = json;

            PopulateAssignedSoftware(asset);

            return View(asset);
        }

        private void PopulateAssignedSoftware(Asset asset)
        {
            var allSoftwares = db.Softwares;
            var assetSoftwares = new HashSet<int>(asset.Softwares.Select(c => c.SoftwareID));

            var viewModel = new List<AssetSoftwares>();

            foreach (var soft in allSoftwares)
            {
                viewModel.Add(new AssetSoftwares
                {
                    SoftwareID = soft.SoftwareID,
                    SoftwareName = soft.softwareName,
                    LicenseKey = "",
                    Assigned = assetSoftwares.Contains(soft.SoftwareID)
                });
            }
            ViewBag.Softwares = viewModel;
        }

        [Authorize(Users = "admin")]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection formCollection, string[] selectedSoftwares)
        {
            var assetToUpdate = db.Assets
                .Include(i => i.Softwares)
                .Where(i => i.AssetID == id)
                .Single();

            if (TryUpdateModel(assetToUpdate, "", null, new string[] { "Softwares" }))
            {
                try
                {
                    UpdateAssetSoftwares(selectedSoftwares, assetToUpdate);
                    
                    string json = JSONHelper.ToJSON(formCollection);
                  
                    JObject jObject = JObject.Parse(json);
                    string indexes = (string)jObject["assetSoftware.Index"];

                    string softwareID = "";
                    string install = "";
                    string serial = "";
                    string po = "";

                    string currentVal = "";

                    var toDeletes = assetToUpdate.SoftwarePerAssets.ToList();
                    foreach (var soft in toDeletes)
                    {
                        db.SoftwarePerAssets.Remove(soft);   
                    }
                    
                    if(indexes != null)
                    {
                        List<string> splitIndexes = indexes.Split(',').Distinct().ToList<string>();
                        List<int> addedIndexes = new List<int>();

                        SoftwarePerAsset sa;
                        
                        for (int i = 0; i < splitIndexes.Count; i++)
                        {   
                            currentVal = splitIndexes[i];
                            sa = new SoftwarePerAsset();

                            for (int t = 0; t < jObject.Count; t++)
                            {
                                softwareID = (string)jObject["assetSoftware[" + currentVal + "].softName"];
                                serial = (string)jObject["assetSoftware[" + currentVal + "].serial"];
                                install = (string)jObject["assetSoftware[" + currentVal + "].installType"] == "" ? "1" : (string)jObject["assetSoftware[" + currentVal + "].installType"];
                                po = (string)jObject["assetSoftware[" + currentVal + "].PO"];

                                //only add if software is not null
                                if (softwareID != "")
                                {
                                    sa.SoftwareID = Convert.ToInt32(softwareID);
                                    sa.SerialKey = serial;
                                    sa.install = Convert.ToInt32(install);
                                    sa.AssetID = assetToUpdate.AssetID;
                                    sa.PONumber = po;

                                    //Make sure the software is not added more than once
                                    if (!addedIndexes.Contains(sa.SoftwareID)) { 
                                        assetToUpdate.SoftwarePerAssets.Add(sa);
                                        addedIndexes.Add(sa.SoftwareID);
                                    }
                                }

                             }
                         }
                    }
                    
                    db.Entry(assetToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    //return RedirectToAction("Index");
                    GenerateFormSelectData(assetToUpdate.AssetID);
                    return View(assetToUpdate);
                }
                catch (DataException)
                {
                    //Log the error (add a variable name after DataException)
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            PopulateAssignedSoftware(assetToUpdate);
            GenerateFormSelectData(assetToUpdate.AssetID);

            return View(assetToUpdate);
        }

        private void UpdateAssetSoftwares(string[] selectedSoftwares, Asset asset)
        {
            if (selectedSoftwares == null)
            {
                asset.Softwares = new List<Software>();
                return;
            }
            
            var selectedSoftwaresHS = new HashSet<string>(selectedSoftwares);
            var assetSoftwares = new HashSet<int>(asset.Softwares.Select(c => c.SoftwareID));

            foreach (var soft in db.Softwares)
            {
                if (selectedSoftwaresHS.Contains(soft.SoftwareID.ToString()))
                {
                    if (!assetSoftwares.Contains(soft.SoftwareID))
                    {
                        asset.Softwares.Add(soft);
                    }
                }
                else
                {
                    if (assetSoftwares.Contains(soft.SoftwareID))
                    {
                        asset.Softwares.Remove(soft);
                    }
                }
            }
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