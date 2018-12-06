using Microsoft.Reporting.WebForms;
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
            List<Savings> save = new List<Savings>();
            save = SavingsCotrollerSql.GetAll();
            if (save.Count != 0)
            {
                decimal total=0;
                for(int i=0;i<=save.Count-1;i++)
                {
                    total += save[i].SavingsAmt;
                }
                if (total >= 15000)
                {
                    string memberid = Convert.ToString(Session["MemberId"]);
                    List<Loan> loanlist = new List<Loan>();
                    loanlist = LoanControllerSql.GetAll(memberid);
                    if (loanlist.Count == 0)
                    {
                        List<Member> mem = new List<Member>();
                        mem = MemberControllerSql.GetAll(Convert.ToInt32(Session["MemberId"]));
                        ViewBag.Id = Convert.ToInt32(Session["MemberId"]);
                        ViewBag.Fullname = Convert.ToString(Session["Fullname"]);
                        ViewBag.Members = mem;
                        return View();
                    }
                    else
                    {
                        Session["alert"] = "1";
                        return RedirectToAction("Home", "Member");
                    }
                }
                else
                {
                    Session["alert1"] = "1";
                    return RedirectToAction("Home", "Member");
                }

            }
            else
            {
                Session["alert1"] = "1";
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
            string w2 = Convert.ToString(fc["witness2"]);
            string w3 = Convert.ToString(fc["witness3"]);
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
                };
               status=WitnessControllerSql.Insert(w);
                Witness wm1 = new Witness
                {
                    LoanId = lid,
                    WitnessName = w2,
                };
                status = WitnessControllerSql.Insert(wm1);
                Witness wm2 = new Witness
                {
                    LoanId = lid,
                    WitnessName = w3,                   
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
                    List<Member> m = new List<Member>();
                    m = MemberControllerSql.Get(MID);
                    if (m.Count > 0)
                    {
                        Comaker co = new Comaker
                        {
                            LoanId = lid,
                            Name = name,
                            ContactNo = contact,
                            MemberId = MID
                        };
                        status = ComakerControllerSql.Insert(co);
                      
                    }
                    else
                    {                      
                        Response.Write("<script type='text/javascript'>alert(Member Id: "+MID+"' doesn't exist');</script>");
                        return View();
                    }                
                }
                if (status == true)
                {
                    Session["Success"] = "1";
                    return RedirectToAction("Home", "Member");
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
        public ActionResult Download()
        {
            int id = Convert.ToInt32(Session["MemberId"]);
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Views/Report/Loan.rdlc");
            ReportDataSource rd1 = new ReportDataSource();
            ReportDataSource rd2 = new ReportDataSource();
            ReportDataSource rd = new ReportDataSource();
            List<LoanReport> data = new  List<LoanReport>();
            data = LoanReportControllerSql.GetAll(id);
           
            rd.Name = "LoanReportt";
            rd.Value = data.ToList();
            List<Comaker> data1 = new List<Comaker>();
            data1 = ComakerControllerSql.GetCo(data[0].Id.ToString());
            localreport.DataSources.Add(rd);
            rd1.Name = "ComakerReportt";
            rd1.Value = data1.ToList();
            List<Witness> data2 = new List<Witness>();
            data2 = WitnessControllerSql.GetAll(data[0].Id.ToString());
            localreport.DataSources.Add(rd1);
            rd2.Name = "WitnessReportt";
            rd2.Value = data2.ToList();
            localreport.DataSources.Add(rd2);
            string reportType = "PDF";
            string mimetype;
            string encoding;
            string filenameExtension = "pdf";
            string[] streams;
            Warning[] warnings;
            byte[] renderbyte;
            string deviceInfo = "<DeviceInfo><OutputFormat>PDF</OutputFormat><PageWidth>8.5in</PageWidth><PageHeight>11in</PageHeight><MarginTop>0.5in</MarginTop><MarginLeft>11in</MarginLeft><MarginRight>11in</MarginRight><MarginBottom>0.5in</MarginBottom></DeviceInfo>";
            renderbyte = localreport.Render(reportType, deviceInfo, out mimetype, out encoding, out filenameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment;filename=LoanInfo." + filenameExtension);
            return File(renderbyte, filenameExtension);
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
        public ActionResult GetWitness(Loan data)
        {
            List<Witness> ret = new List<Witness>();
            ret = WitnessControllerSql.GetAll(data.LoanId);
            return Json(new { data = ret }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetComaker(Loan data)
        {
            List<Comaker> ret = new List<Comaker>();
            ret = ComakerControllerSql.GetAll(data.LoanId);
            return Json(new { data = ret }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetComake()
        {
            List<Comaker> ret = new List<Comaker>();
            ret = ComakerControllerSql.GetAll();
            return Json(new { data = ret }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Loaning(Loan data)
        {
            string id = Convert.ToString(Session["MemberId1"]);
            List<Loan> ret = new List<Loan>();
            ret = LoanControllerSql.GetAll(id);
            return Json(new { data = ret }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateApprove(Loan data)
        {
            Loan l = new Loan();
            l = LoanControllerSql.Get(data.Id);
            bool data1 = false;
            data1 = ComakerControllerSql.Update(data.Status,l.LoanId);
            List<Comaker> colist = new List<Comaker>();
            colist = ComakerControllerSql.GetAll(l.LoanId);

            return new JsonResult { Data = data1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}