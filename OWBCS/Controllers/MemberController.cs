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
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult MemberAdd()
        {
            return View();
        }
        public ActionResult Home()
        {
            string loanerr = Convert.ToString(Session["alert"]);
            if (loanerr == "1")
            {
                Response.Write("<script type='text/javascript'>alert('Your loan is in process');</script>");
                Session["alert"] = null;
                return View();
            }
            else
            {
                return View();
            }
        }
        private string Hash(string p)
        {
            string hash = "";
            SHA1CryptoServiceProvider sh = new SHA1CryptoServiceProvider();
            UTF8Encoding utf8 = new UTF8Encoding();
            hash = BitConverter.ToString(sh.ComputeHash(utf8.GetBytes(p.ToString())));
            return hash;
        }

        [HttpPost]
        public ActionResult MemberAdd(Member me)
        {
            bool status = false;
            DateTime day = DateTime.Now;
            Session["status"] = null;
            List<Login> loglist = new List<Login>();
            loglist = LoginControllerSql.GetAll(me.EmailAddress);
            List<Member> mem = new List<Member>();
            mem = MemberControllerSql.GetAll();
            int iid = mem.Count + 1;
            string memid = "M" + (iid).ToString();
            string salut = "";
            if (loglist.Count == 0)
            {   
                
                string pass = Hash((me.Birthdate.ToShortDateString()));
                Login log = new Login
                {
                    Username = me.EmailAddress,
                    Hash = pass,
                    CreatedBy = "none",
                    ModifyBy = "none",
                    Level = 4,
                    CreatedDate = day,
                    ModifiedDate = day,
                    Locked = 0,
                    LastLogin = day
                };
                status = LoginControllerSql.Insert(log);

                if (status == true)
                {
                    if(me.Gender=="Male")
                    {
                        salut = "Mr";
                    }
                    else if(me.Gender=="Female")
                    {
                        if(me.MaritalStatus=="Married")
                        {
                            salut = "Mrs";
                        }
                        else
                        {
                            salut = "Ms";
                        }
                    }
                    List<Login> ul = LoginControllerSql.GetAll(me.EmailAddress);
                    Member ret = new Member
                    {
                        Fname = me.Fname,
                        Mname = me.Mname,
                        Lname = me.Lname,
                        Birthdate = me.Birthdate,
                        Position = me.Position,
                        EmailAddress = me.EmailAddress,
                        Deleted = 0,
                        Url = "---",
                        LoginId = ul[0].Id,
                        ResidentialAddress = me.ResidentialAddress,
                        Gender = me.Gender,
                        ContactNo = me.ContactNo,
                        MaritalStatus = me.MaritalStatus,
                        EmployeeId = memid,
                        EmergencyContactNo = me.EmergencyContactNo,
                        SalaryAmt = me.SalaryAmt,
                        Salutation = salut
                    };
                    status = MemberControllerSql.Insert(ret);
                    Session["status"] = 1;
                }
            }
            else
            {
                Session["status"] = 2;
                Response.Write("<script type='text/javascript'>alert('Email is already exist');</script>");
            }
            Session["AddMessage"] = status;
            return RedirectToAction("MemberView", "Member", new { id = "" });
        }
        public ActionResult MemberView()
        {
            return View();
        }
        [HttpGet]
        public ActionResult MemberView(int? Id)
        {
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
                Member a = new Member();
                a = MemberControllerSql.GetById((int)Id);
                ViewBag.ModalView = 1;
                return View(a);
            }
           
        }

        [HttpPost]
        public ActionResult MemberView(Member me)
        {
            Session["status"] = null;
            Member ret = new Member
            {
                Id= me.Id,
                Fname = me.Fname,
                Mname = me.Mname,
                Lname = me.Lname,
                Birthdate = me.Birthdate,
                Position = me.Position,
                EmailAddress = me.EmailAddress,         
                ResidentialAddress = me.ResidentialAddress,
                Gender = me.Gender,
                ContactNo = me.ContactNo,
                MaritalStatus = me.MaritalStatus,            
                EmergencyContactNo = me.EmergencyContactNo,
                SalaryAmt = me.SalaryAmt,
            };
            bool status = false;
            status = MemberControllerSql.Update(ret);
            if (status == true)
            {
                Session["status"] = 1;
                //Response.Write("<script>alert('You have successfully updated the Admin');</script>");
            }
            Session["UpdateMess"] = status;
            return RedirectToAction("MemberView", "Member", new { id = "" });
        }

        public ActionResult GetMember()
        {
            List<Member> ret = new List<Member>();
            ret = MemberControllerSql.GetAll();
            return Json(new { data = ret }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Deleted(int Id)
        {
            Member ret = new Member
            {
                Id = Id
            };
            bool status = false;
            status = MemberControllerSql.Delete(ret);
            Session["DeleteStatus"] = status;
            return RedirectToAction("MemberView", "Member", new { id = "" });
        }
    }
}