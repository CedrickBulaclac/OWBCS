using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Net;
using System.Text;
using System.Net.Mail;
namespace OWBCS.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult AdminView()
        {
            if (Session["status"] == null)
            {
                Session["status"] = 0;
            }
        
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminAdd(Admin adm)
        {
            Session["status"] = null;
            List<Login> loglist = new List<Login>();
            loglist = LoginControllerSql.GetAll(adm.Email);
            if (loglist.Count == 0)
            {
                Admin ret = new Admin
                {
                    Fname = adm.Fname,
                    Mname = adm.Mname,
                    Lname = adm.Lname,
                    Bdate = adm.Bdate,
                    Deleted = "0",
                    Email = adm.Email,
                    Url = "---",
                    Position = adm.Position,
                    LoginId = 0
                };
                bool status = false;
                status = AdminControllerSql.Insert(ret);
                if (status == true)
                {

                    int level = 0;
                    if (adm.Position == "Finance")
                    {
                        level = 2;
                    }
                    else if (adm.Position == "Accountant")
                    {
                        level = 3;
                    }
                    string pass = Hash((adm.Bdate.ToShortDateString()));
                    Login log = new Login
                    {
                        Username = adm.Email,
                        Hash = pass,
                        CreatedBy = "none",
                        ModifyBy = "none",
                        Level = level
                    };
                    status = LoginControllerSql.Insert(log);
                    Session["status"] = 1;
                }
            }
            else
            {
                Session["status"] = 2;
                Response.Write("<script type='text/javascript'>alert('Email is already exist');</script>");
            }
                                     
            return RedirectToAction("AdminView","Admin");
        }
        [HttpGet]
        public JsonResult AdminEdit(int Id)
        {
            Admin a = new Admin();
            a = AdminControllerSql.GetById(Id);
            return new JsonResult { Data = a, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public ActionResult AdminEdit(Admin adm)
        {
            Session["status"] = null;
            Admin ret = new Admin
            {
                Id=adm.Id,
                Fname = adm.Fname,
                Mname = adm.Mname,
                Lname = adm.Lname,
                Bdate = adm.Bdate,
                Email = adm.Email,
                Position = adm.Position,              
            };
            bool status = false;
            status = AdminControllerSql.Update(ret);
            if (status == true)
            {
                Session["status"] = 1;
            }
           
            return Redirect("~/Admin/AdminView");
        }
        public ActionResult GetAdmin()
        {
            List<Admin> ret = new List<Admin>();
            ret = AdminControllerSql.GetAll();
            return Json(new { data = ret }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Deleted(int Id)
        {
            Admin ret = new Admin
            {
                Id = Id
            };
            bool status = false;
            status = AdminControllerSql.Delete(ret);
            return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        private string Hash(string p)
        {
            string hash = "";
            SHA1CryptoServiceProvider sh = new SHA1CryptoServiceProvider();
            UTF8Encoding utf8 = new UTF8Encoding();
            hash = BitConverter.ToString(sh.ComputeHash(utf8.GetBytes(p.ToString())));
            return hash;
        }

        private void SendEmail(string email1, string pass)
        {
            try
            {
                var fromAddress = new MailAddress("uresidence04@gmail.com", "URESIDENCE");
                var toAddress = new MailAddress(email1, "To Name");
                const string fromPassword = "uresidence";
                const string subject = "PalmDale Heights Condominium";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = "Account Information" + "\n" + "Username :" + email1 + "\n"
                    + "Password :" + pass
                })
                {

                    smtp.Send(message);

                }
            }
            catch (Exception)
            {
                string script = "<script type = 'text/javascript'>alert('The email account that you tried to reach does not exist');</script>";
                Response.Write(script);
            }
        }

    }
}