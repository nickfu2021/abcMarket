﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using abcMarket.Models;

namespace abcMarket.Areas.Member.Controllers
{
    public class MemberController : Controller
    {
        [LoginAuthorize(RoleNo = "Member")]
        public ActionResult Index()
        {
            UserAccount.UploadImageMode = false;
            using (Sale sale = new Sale(DateTime.Today))
            {
                sale.CountAmount("M");
                ViewBag.MonthAmount = sale.AmountData;
                ViewBag.MonthAmountPercent = sale.PercentData;

                sale.CountQty("M");
                ViewBag.MonthCount = sale.AmountData; ;
                ViewBag.MonthCountPercent = sale.PercentData;

                sale.CountAmount("Y");
                ViewBag.YearAmount = sale.AmountData;
                ViewBag.YearAmountPercent = sale.PercentData;

                sale.CountQty("Y");
                ViewBag.YearCount = sale.AmountData; ;
                ViewBag.YearCountPercent = sale.PercentData;

                List<string> arrYearMonthName = sale.GetYearMonthNameList();
                List<int> arrYearMonthAmount = sale.GetYearMonthAmountList("Base");
                ViewBag.BaseYearAmountRank = sale.GetSaleRank("Base", "Amount", 20);
                ViewBag.BaseYearQtyRank = sale.GetSaleRank("Base", "Qty", 20);
                ViewBag.BaseYearMonthName = Newtonsoft.Json.JsonConvert.SerializeObject(arrYearMonthName);
                ViewBag.BaseYearMonthAmount = Newtonsoft.Json.JsonConvert.SerializeObject(arrYearMonthAmount);

                sale.CountAmount("W");
                List<string> arrPriorWeekName = sale.GetWeekNameList("Prior");
                List<int> arrPriorWeekAmount = sale.GetWeekAmountList("Prior");
                ViewBag.PriorWeekName = Newtonsoft.Json.JsonConvert.SerializeObject(arrPriorWeekName);
                ViewBag.PriorWeekAmount = Newtonsoft.Json.JsonConvert.SerializeObject(arrPriorWeekAmount);

                List<string> arrBaseWeekName = sale.GetWeekNameList("Base");
                List<int> arrBaseWeekAmount = sale.GetWeekAmountList("Base");
                ViewBag.BaseWeekName = Newtonsoft.Json.JsonConvert.SerializeObject(arrBaseWeekName);
                ViewBag.BaseWeekAmount = Newtonsoft.Json.JsonConvert.SerializeObject(arrBaseWeekAmount);

                return View();
            }
        }
        [LoginAuthorize(RoleNo = "Member")]
        public ActionResult MemberProfile()
        {
            using ( abcMarketEntities db = new abcMarketEntities())
            {
                var model = db.Users
                    .Where(m => m.user_email == UserAccount.UserEmail)
                    .FirstOrDefault();
                return View(model);
            }
        }
        [HttpGet]
        [LoginAuthorize(RoleNo = "Member")]
        public ActionResult EditProfile()
        {
            using (abcMarketEntities db = new abcMarketEntities())
            {
                var model = db.Users
                .Where(m => m.user_email == UserAccount.UserEmail)
                .FirstOrDefault();
                return View(model);
            }
        }
        [HttpPost]
        [LoginAuthorize(RoleNo = "Member")]
        public ActionResult EditProfile(Users model)
        {
            if (!ModelState.IsValid) return View(model);

            using (abcMarketEntities db = new abcMarketEntities())
            {
                var user = db.Users
                .Where(m => m.rowid == model.rowid)
                .FirstOrDefault();

                if (user != null)
                {
                    user.mname = model.mname;
                    user.phone = model.phone;
                    user.user_email = model.user_email;
                    user.birthday = model.birthday;
                    user.remark = model.remark;
                    db.SaveChanges();
                }

                return RedirectToAction("MemberProfile");
            }
        }
        [LoginAuthorize(RoleNo = "Member")]
        public ActionResult UploadImage()
        {
            UserAccount.UploadImageMode = true;
            ImageService.ImageTitle = String.Format("{0}{1}會員圖片上傳", UserAccount.UserEmail, UserAccount.UserName);
            ImageService.ImageFolder = "~/Images";
            ImageService.ImageSubFolder = "Member";
            ImageService.ImageName = UserAccount.UserEmail;
            ImageService.ImageExtention = "jpg";
            ImageService.UploadImageMode = true;
            return RedirectToAction("MemberProfile");
        }

        //[HttpPost]
        //[LoginAuthorize(RoleNo = "Member")]
        //public ActionResult Upload(HttpPostedFileBase file)
        //{
        //    if (file != null)
        //    {
        //        if (file.ContentLength > 0)
        //        {
        //            var fileName = UserAccount.UserEmail + ".jpg";
        //            var path = Path.Combine(Server.MapPath("~/Images/user"), fileName);
        //            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
        //            file.SaveAs(path);
        //        }
        //    }
        //    UserAccount.UploadImageMode = false;
        //    return RedirectToAction("MemberProfile");
        //}

        //[LoginAuthorize(RoleNo = "Member")]
        //public ActionResult UploadCancel()
        //{
        //    UserAccount.UploadImageMode = false;
        //    return RedirectToAction("MemberProfile");
        //}
    }
}