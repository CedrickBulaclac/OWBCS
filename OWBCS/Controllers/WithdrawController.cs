using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OWBCS.Controllers
{
    public class WithdrawController : Controller
    {
        // GET: Withdraw
        public ActionResult Withdraw()
        {
            int id = Convert.ToInt32(Session["MemberId"]);
            List<Withdraw> with1 = new List<Withdraw>();
            with1 = WithdrawControllerSql.Check(id);
            if (with1.Count == 0)
            {
                decimal totalsavings = 0;
                List<Savings> savings = new List<Savings>();
                savings = SavingsCotrollerSql.GetAll(id);
                if (savings.Count > 0)
                {
                    for (int i = 0; i <= savings.Count - 1; i++)
                    {
                        totalsavings += savings[i].SavingsAmt;
                    }
                }
                List<Withdraw> with = new List<Withdraw>();
                with = WithdrawControllerSql.GetAll(id);
                if (with.Count > 0)
                {
                    for (int i = 0; i <= with.Count - 1; i++)
                    {
                        totalsavings -= with[i].WithdrawAmt;
                    }
                }
                ViewBag.Fullname = Convert.ToString(Session["Fullname"]);
                ViewBag.Total = totalsavings;
                return View();
            }
            else
            {
                Session["withdrawstatus"] = "1";
                return RedirectToAction("Home","Member");
            }
        }
        public JsonResult Withdrawal(Withdraw data)
        {
            int id = Convert.ToInt32(Session["MemberId"]);
            bool status = false;
            List<Withdraw> withdraws = new List<Withdraw>();
            int i = withdraws.Count + 1;
            string wid = "WD" + i.ToString();
            Withdraw withdraw = new Withdraw
            {
                WithdrawAmt =data.WithdrawAmt,
                WithdrawDate=DateTime.Now,
                Status="Pending",
                MemberId=id,
                WithdrawId=wid,
                ApprovedBy="none"
            };
            status = WithdrawControllerSql.Insert(withdraw);
            return new JsonResult {Data= status, JsonRequestBehavior=JsonRequestBehavior.AllowGet};
        }
        public ActionResult GetWithdrawal(Withdraw data)
        {
            List<WithdrawList> ret = new List<WithdrawList>();
            ret = WithdrawListControllerSql.GetAll(data.Status);
            return Json(new { data = ret }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult WithdrawalView()
        {
            return View();
        }
        public JsonResult Update(Withdraw data)
        {
            string name = Convert.ToString(Session["Fullname"]);
            bool status = false;
            Withdraw withdraw = new Withdraw
            {
                Id=data.Id,
                Status=data.Status,
                ApprovedBy=name
            };
            status = WithdrawControllerSql.UpdateStatus(withdraw);
            return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        }
}