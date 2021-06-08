using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using abcMarket.Models;

namespace abcMarket.Areas.Admin.Controllers
{
    public class StatusController : Controller
    {
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetDataList()
        {
            using (abcMarketEntities db = new abcMarketEntities())
            {
                var models = db.Status.OrderBy(m => m.mno).ToList();
                return Json(new { data = models }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult Edit(int id = 0)
        {
            using (abcMarketEntities db = new abcMarketEntities())
            {
                if (id == 0)
                {
                    Status new_model = new Status();
                    return View(new_model);
                }
                var models = db.Status.Where(m => m.rowid == id).FirstOrDefault();
                return View(models);
            }
        }

        [HttpPost]
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult Edit(Status models)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (abcMarketEntities db = new abcMarketEntities())
                {
                    if (models.rowid > 0)
                    {
                        //Edit 
                        var Status = db.Status.Where(m => m.rowid == models.rowid).FirstOrDefault();
                        if (Status != null)
                        {
                            Status.mno = models.mno;
                            Status.mname = models.mname;
                            Status.remark = models.remark;
                        }
                    }
                    else
                    {
                        //Save
                        db.Status.Add(models);
                    }
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult Delete(int id)
        {
            using (abcMarketEntities db = new abcMarketEntities())
            {
                var model = db.Status.Where(m => m.rowid == id).FirstOrDefault();
                if (model != null)
                {
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult DeleteData(int id)
        {
            bool status = false;
            using (abcMarketEntities db = new abcMarketEntities())
            {
                var model = db.Status.Where(m => m.rowid == id).FirstOrDefault();
                if (model != null)
                {
                    db.Status.Remove(model);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}