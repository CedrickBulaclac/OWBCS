using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OWBCS.Controllers
{
    public class LoanController : Controller
    {
        // GET: Loan
        public ActionResult Loan()
        {
            string memberid = Convert.ToString(Session["MemberId"]);
            List<Loan> loanlist = new List<Loan>();
            loanlist = LoanControllerSql.GetAll(memberid);
            if (loanlist.Count == 0)
            {
                ViewBag.Id = Convert.ToInt32(Session["MemberId"]);
                ViewBag.Fullname = Convert.ToString(Session["Fullname"]);
                return View();
            }
            else
            {
                Session["alert"] = "1";
                return RedirectToAction("Home", "Member");
            }
        }
        [HttpPost]
        public ActionResult Loan(FormCollection fc)
        {
            bool status = false;
            decimal loanamt = Convert.ToDecimal(fc["amt"]);
            int terms = Convert.ToInt32(fc["mtp"]);
            decimal totalwinterest = Convert.ToDecimal(fc["totalwinterest"]);
            int mid= Convert.ToInt32(Session["MemberId"]);

            string w1 = Convert.ToString(fc["witness1"]);
            string wc1 = Convert.ToString(fc["witnesscno1"]);
            string w2 = Convert.ToString(fc["witness2"]);
            string wc2 = Convert.ToString(fc["witnesscno2"]);
            string w3 = Convert.ToString(fc["witness3"]);
            string wc3 = Convert.ToString(fc["witnesscno3"]);
            int no = Convert.ToInt32(fc["no"]);
            List<Loan> listloan = new List<Loan>();
            listloan = LoanControllerSql.GetAll();
            int cid = listloan.Count + 1;
            string lid = "L" + cid.ToString();
            DateTime d = DateTime.Now;
            Loan loan = new Loan
            {
                LoanId = lid,
                MemberId = mid,
                Status = "Pending",
                CreatedDate = d,
                ApprovalDate= d,
                LoanAmt= loanamt,
                Terms=terms,
                InterestRate=1,
                TotalPaymentwInterest= totalwinterest,
                ApprovedBy="none"
            };
            status = LoanControllerSql.Insert(loan);
            if(status==true)
            {
                Witness w = new Witness
                {
                    LoanId =lid,
                    WitnessName=w1,
                    ContactNo=wc1
                };
               status=WitnessControllerSql.Insert(w);
                Witness wm1 = new Witness
                {
                    LoanId = lid,
                    WitnessName = w2,
                    ContactNo =wc2
                };
                status = WitnessControllerSql.Insert(wm1);
                Witness wm2 = new Witness
                {
                    LoanId = lid,
                    WitnessName = w3,
                    ContactNo = wc3
                };
                status=WitnessControllerSql.Insert(wm2);
                Response.Write("<script>alert('Success');</script>");
                for (int i=0;i<=no-1;i++)
                {
                    string name = Convert.ToString(fc["co"+i]);
                    string contact = Convert.ToString(fc["cno" + i]);
                    string MID= Convert.ToString(fc["CMID" + i]);
                    if(MID=="")
                    {
                        MID = "0";
                    }
                

                    Comaker co = new Comaker
                    {
                        LoanId=lid,
                        Name=name,
                        ContactNo=contact,
                        MemberId=MID
                    };
                    status = ComakerControllerSql.Insert(co);
                    if(status==true)
                    {
                        Session["Success"] = "1";
                        return RedirectToAction("Home","Member");
                    }
                }
            }
            
            return View();
        }
        public ActionResult ViewLoan()
        {
            Loan loan = new Loan();
            string memberid = Convert.ToString(Session["MemberId"]);
            List<Loan> loanlist = new List<Loan>();
            loanlist = LoanControllerSql.GetAll(memberid);
            if (loanlist.Count == 0)
            {
                return View();
            }
            else
            {
                loan = LoanControllerSql.Get(memberid);
                return View(loan);
            }
                
        }

        public ActionResult Savings()
        {
            return View();
        }
        public ActionResult GetLoan(Loan data)
        {
            List<Loan> ret = new List<Loan>();
            ret = LoanControllerSql.GetStatus(data.Status);
            return Json(new { data = ret }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Loan data)
        {
            string name = Convert.ToString(Session["Fullname"]);
            bool status = false;
            Loan loan = new Loan
            {
                Id = data.Id,
                Status = data.Status,
                ApprovedBy = name,
                ApprovalDate=DateTime.Now
            };
            status = LoanControllerSql.UpdateStatus(loan);
            return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult LoanView()
        {
            return View();
        }
    }
}