using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using abcMarket.Models;

namespace abcMarket.Controllers
{
    public class UserController : Controller
    {
        abcMarketEntities db = new abcMarketEntities();

        public ActionResult Login()
        {
            UserAccount.LogOut();
            cvmLogin model = new cvmLogin()
            {
                User_Email = "",
                Password = "",
                Remember = false
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(cvmLogin model)
        {
            if (!ModelState.IsValid) return View(model);
            string str_password = model.Password;
            using (Cryptographys crpy = new Cryptographys())
            {
                str_password = crpy.SHA256Encode(str_password);
            }
            var users = db.Users
                .Where(m => m.user_email == model.User_Email)
                .Where(m => m.password == str_password)
                .FirstOrDefault();
            if (users == null)
            {
                ModelState.AddModelError("user_email", "登入帳號或密碼不符!!");
                return View(model);
            }

            UserAccount.Login(model.User_Email, users.mname, UserAccount.GetRoleNo(users.role_no));

            return RedirectToAction("RedirectToUserPage");
        }

        public ActionResult LogOut()
        {
            UserAccount.LogOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ResetPassword()
        {
            cvmResetPassword model = new cvmResetPassword()
            {
                UserEmail = UserAccount.UserEmail,
                CurrentPassword = "",
                NewPassword = "",
                ConfirmPassword = ""
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ResetPassword(cvmResetPassword model)
        {
            if (!ModelState.IsValid) return View(model);

            string str_password = "";
            using (Cryptographys cryp = new Cryptographys())
            { str_password = cryp.SHA256Encode(model.CurrentPassword); }
            bool bln_error = false;

            var check = db.Users
                .Where(m => m.user_email == model.UserEmail)
                .Where(m => m.password == str_password)
                .FirstOrDefault();

            if (check == null) { ModelState.AddModelError("CurrentPassword", "目前密碼輸入錯誤!!"); bln_error = true; }
            if (bln_error) return View(model);

            str_password = model.NewPassword;
            var user = db.Users.Where(m => m.user_email == model.UserEmail).FirstOrDefault();
            if (user != null)
            {
                //密碼加密
                using (Cryptographys cryp = new Cryptographys())
                { str_password = cryp.SHA256Encode(str_password); }

                user.password = str_password;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                db.Configuration.ValidateOnSaveEnabled = true;
            }
            return RedirectToAction("RedirectToUserPage");
        }
        public ActionResult RedirectToUserPage()
        {
            if (UserAccount.RoleNo == AppEnum.enUserRole.Admin) return RedirectToAction("Index", "Admin", new { area = "Admin" });
            if (UserAccount.RoleNo == AppEnum.enUserRole.Member) return RedirectToAction("Index", "Member", new { area = "Member" });
            //if (UserAccount.RoleNo == AppEnum.enUserRole.Vendor) return RedirectToAction("Index", "Vendor", new { area = "Vendor" });
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            cvmRegister model = new cvmRegister();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(cvmRegister model)
        {
            if (!ModelState.IsValid) return View(model);

            //自定義檢查
            bool bln_error = false;
            var check = db.Users.Where(m => m.user_email == model.email).FirstOrDefault();
            if (check != null) { ModelState.AddModelError("", "電子信箱重覆註冊!"); bln_error = true; }
            if (bln_error) return View(model);

            //密碼加密
            using (Cryptographys cryp = new Cryptographys())
            {
                model.password = cryp.SHA256Encode(model.password);
                model.ConfirmPassword = model.password;
            }

            Users user = new Users();
            user.mname = model.mname;
            user.password = model.password;
            user.user_email = model.email;
            user.birthday = model.birthday;
            user.remark = model.remark;
            user.role_no = "Member";  //設定角色代號為 User
            user.varify_code = UserAccount.GetNewVarifyCode(); //產生驗證碼
            user.isvarify = 0;

            //寫入資料庫
            try
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Users.Add(user);
                db.SaveChanges();
                db.Configuration.ValidateOnSaveEnabled = true;
            }
            catch (Exception ex)
            {
                string str_message = ex.Message;
            }

            //寄出驗證信
            SendVerifyMail(model.email, user.varify_code);
            return RedirectToAction("SendEmailResult");
        }
        private string SendVerifyMail(string userEmail, string varifyCode)
        {
            string str_app_name = "abcMarket";
            var str_url = string.Format("/User/VerifyEmail/{0}", varifyCode);
            var str_link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, str_url);
            string str_subject = str_app_name + " - 帳號成功建立通知!!";
            string str_body = "<br/><br/>";
            str_body += "很高興告訴您，您的 " + str_app_name + " 帳戶已經成功建立. <br/>";
            str_body += "請按下下方連結完成驗證您的帳號程序!!<br/><br/>";
            str_body += "<a href='" + str_link + "'>" + str_link + "</a> ";
            str_body += "<br/><br/>";
            str_body += "本信件由電腦系統自動寄出,請勿回信!!<br/><br/>";
            str_body += string.Format("{0} 系統開發團隊敬上", str_app_name);

            using (GmailService gmail = new GmailService())
            {
                gmail.ReceiveEmail = userEmail;
                gmail.Subject = str_subject;
                gmail.Body = str_body;
                gmail.Send();
                return gmail.MessageText;
            }
        }
        [HttpGet]
        public ActionResult SendEmailResult()
        {
            return View();
        }

        [HttpGet]
        public ActionResult VerifyEmail(string id)
        {
            bool Status = false;
            db.Configuration.ValidateOnSaveEnabled = false;
            var user = db.Users.Where(m => m.varify_code == id).FirstOrDefault();
            if (user == null)
            { ViewBag.Message = "驗證碼錯誤!!"; }
            else
            {
                if (user.isvarify == 1)
                { ViewBag.Message = "此電子信箱已完成驗證, 請勿重覆執行!!"; }
                else
                {
                    user.isvarify = 1;
                    db.SaveChanges();
                    Status = true;
                }
            }
            ViewBag.Status = Status;
            return View();
        }
        [HttpGet]
        public ActionResult ForgetPassword()
        {
            cvmForgetPassword model = new cvmForgetPassword();
            return View(model);
        }
        [HttpPost]
        public ActionResult ForgetPassword(cvmForgetPassword model)
        {
            if (!ModelState.IsValid) return View(model);

            //string str_password = "";
            //var user = db.Users.Where(m => m.user_email == model.user_email).FirstOrDefault();
            //if (user != null)
            //{
            //    //密碼加密
            //    using (Cryptographys cryp = new Cryptographys())
            //    { str_password = cryp.SHA256Encode(model.user_email); }

            //    user.password = str_password;
            //    db.Configuration.ValidateOnSaveEnabled = false;
            //    db.SaveChanges();
            //    db.Configuration.ValidateOnSaveEnabled = true;
            //}

            SendForgetPasswordMail(model.user_email);       
            return RedirectToAction("ForgetEmailResult");
        }

        [HttpGet]
        public ActionResult ForgetEmailResult()
        {
            return View();
        }
        private string SendForgetPasswordMail(string userEmail)
        {
            string str_app_name = "abcMarket";
            var str_url = string.Format("/User/ResetForgetPassword");
            var str_link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, str_url);
            string str_subject = str_app_name + " - 密碼重設通知!!";
            string str_body = "";

            str_body += "很高興告訴您，您的 " + str_app_name + " 密碼重設要求已允許. <br/>";
            str_body += "請按下下方連結,重新設您的新密碼!!<br/><br/>";
            str_body += "<br/><br/>";
            str_body += "<a href='" + str_link + "'>" + str_link + "</a> ";
            str_body += "本信件由電腦系統自動寄出,請勿回信!!<br/><br/>";
            str_body += string.Format("{0} 系統開發團隊敬上", str_app_name);

            using (GmailService gmail = new GmailService())
            {
                gmail.ReceiveEmail = userEmail;
                gmail.Subject = str_subject;
                gmail.Body = str_body;
                gmail.Send();
                return gmail.MessageText;
            }
        }
        [HttpGet]
        public ActionResult ResetForgetPassword()
        {
            cvmResetForgetPassword model = new cvmResetForgetPassword()
            {
                user_email = UserAccount.UserEmail,
                NewPassword = "",
                ConfirmPassword = ""
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult ResetForgetPassword(cvmResetForgetPassword model)
        {
            if (!ModelState.IsValid) return View(model);
            string str_password = "";
            var user = db.Users.Where(m => m.user_email == model.user_email).FirstOrDefault();
            if (user != null)
            {
                //密碼加密
                using (Cryptographys cryp = new Cryptographys())
                { str_password = cryp.SHA256Encode(model.NewPassword); }

                user.password = str_password;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                db.Configuration.ValidateOnSaveEnabled = true;
            }
            return RedirectToAction("RedirectToUserPage");
        }
    }
}