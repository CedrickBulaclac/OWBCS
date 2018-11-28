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
    
        public ActionResult Home()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminAdd(Admin adm)
        {
            bool status = false;
            DateTime day = DateTime.Now;
            Session["status"] = null;
            List<Login> loglist = new List<Login>();
            loglist = LoginControllerSql.GetAll(adm.EmailAddress);
            string aid = day.ToShortDateString() + adm.Bdate.ToShortDateString();
            aid = aid.Replace("/","");
            if (loglist.Count == 0)
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
                    Username = adm.EmailAddress,
                    Hash = pass,
                    CreatedBy = "none",
                    ModifyBy = "none",
                    Level = level,
                    CreatedDate = day,
                    ModifiedDate = day,
                    Locked = 0,
                    LastLogin = day
                };
                status = LoginControllerSql.Insert(log);
               
                if (status == true)
                {
                    List<Login> ul = LoginControllerSql.GetAll(adm.EmailAddress);
                    Admin ret = new Admin
                    {
                        Fname = adm.Fname,
                        Mname = adm.Mname,
                        Lname = adm.Lname,
                        Bdate = adm.Bdate,
                        Deleted = 0,
                        EmailAddress = adm.EmailAddress,
                        Url = "---",
                        Position = adm.Position,
                        MaritalStatus = adm.MaritalStatus,
                        ContactNo = adm.ContactNo,
                        EmergencyNo = adm.EmergencyNo,
                        AdminId = aid,
                        ResidentialAddress = adm.ResidentialAddress,
                        LoginId=ul[0].Id,
                        Gender=adm.Gender
                    };                    
                    status = AdminControllerSql.Insert(ret);
                    Session["status"] = 1;
                }
            }
            else
            {
                Session["status"] = 2;
                Response.Write("<script type='text/javascript'>alert('Email is already exist');</script>");
            }
            Session["AddMessage"] = status;
            return RedirectToAction("AdminView","Admin",new { id=""});
        }
        [HttpGet]
        public ActionResult AdminView(int? Id)
        {
            if (Session["UpdateMess"] != null)
            {
                ViewBag.UpdateMessage = Session["UpdateMess"];
                Session["UpdateMess"] = null;
            }
            if (Session["AddMessage"] != null)
            {
                ViewBag.AddMessage = Session["AddMessage"];
                Session["AddMessage"] = null;
            }
            if (Session["DeleteStatus"] != null)
            {
                ViewBag.DeleteStatus = Session["DeleteStatus"];
                Session["DeleteStatus"] = null;
            }
            if (Id == null)
            {
                if (Session["status"] == null)
                {
                    Session["status"] = 0;
                }
                ViewBag.ModalView = 0;
                return View();
            }
            else
            {
                Admin a = new Admin();
                a = AdminControllerSql.GetById((int)Id);
                ViewBag.ModalView = 1;
                return View(a);
            }           
        }
        [HttpPost]
        public ActionResult AdminView(Admin adm)
        {
            Session["status"] = null;
            Admin ret = new Admin
            {
                Id=adm.Id,
                Fname = adm.Fname,
                Mname = adm.Mname,
                Lname = adm.Lname,
                Bdate = adm.Bdate,          
                EmailAddress = adm.EmailAddress,          
                Position = adm.Position,           
                MaritalStatus = adm.MaritalStatus,
                ContactNo = adm.ContactNo,
                EmergencyNo = adm.EmergencyNo,              
                ResidentialAddress = adm.ResidentialAddress,
                Gender=adm.Gender
            };
            bool status = false;
            status = AdminControllerSql.Update(ret);
            if (status == true)
            {
                Session["status"] = 1;
                //Response.Write("<script>alert('You have successfully updated the Admin');</script>");
            }
            Session["UpdateMess"] = status;
            return RedirectToAction("AdminView","Admin",new { id = "" });
        }
        public ActionResult GetAdmin()
        {
            List<Admin> ret = new List<Admin>();
            ret = AdminControllerSql.GetAll();
            return Json(new { data = ret }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Deleted(int Id)
        {
            Admin ret = new Admin
            {
                Id = Id
            };
            bool status = false;
            status = AdminControllerSql.Delete(ret);
            Session["DeleteStatus"] = status;
            return RedirectToAction("AdminView", "Admin", new { id = "" });
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