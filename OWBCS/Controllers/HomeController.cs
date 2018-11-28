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
    public class HomeController : Controller
    {
        string i;
        string email1;
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            try
            {
                string username = (fc["username"]).ToString();
                string pass = (fc["password"].ToString());
                string hash = Hash(pass);
                Login log = new Login();
                log = LoginControllerSql.Get(username, hash);
                if(username=="")
                {
                    return RedirectToAction("Home", "Admin");
                }
                if(log.Id!=0)
                {
                    return RedirectToAction("Home", "Admin");
                }
                else
                {
                    Response.Write("<script type = 'text/javascript'>alert('Wrong Username or Password');</script>");
                }
            }
            catch (Exception)
            {
              
                Response.Write("<script type = 'text/javascript'>alert('Wrong Username or Password');</script>");
            }
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        private string Hash(string p)
        {
            string hash = "";
            SHA1CryptoServiceProvider sh = new SHA1CryptoServiceProvider();
            UTF8Encoding utf8 = new UTF8Encoding();
            hash = BitConverter.ToString(sh.ComputeHash(utf8.GetBytes(p.ToString())));
            return hash;
        }

        private void SendEmail()
        {
            try
            {
                var fromAddress = new MailAddress("@gmail.com", "COOPERATIVE SYSTEM");
                var toAddress = new MailAddress(email1, "To Name");
                const string fromPassword = "";
                const string subject = "Reset Password";
                string body = Randomizer();
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
                    Body = "Your Reference Code is : " + body + "."
                })
                {
                    i = body;
                    smtp.Send(message);

                }
            }
            catch (Exception)
            {
                string script = "<script type = 'text/javascript'>alert('Submission Failed');</script>";
                Response.Write(script);
            }
        }

        public string Randomizer()
        {
            char[] letter = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
            Random rd = new Random();
            string Code = "";
            for (int i = 0; i <= 3; i++)
            {
                Code += letter[rd.Next(0, 62)].ToString();
            }

            return Code;
        }
    }
}