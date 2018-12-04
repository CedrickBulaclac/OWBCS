using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OWBCS.Controllers
{
    public class SavingsController : Controller
    {
        public ActionResult Savings()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Savings(Savings contribution,FormCollection fc)
        {
            bool status = false;
            List<Savings> contr = new List<Savings>();
            contr = SavingsCotrollerSql.GetAll();
            int id=contr.Count+1;
            int mid = Convert.ToInt32(fc["MemberId"]);
            string sid = "C" + id.ToString();
            Savings model = new Savings
            {
                SavingsId = sid,
                SavingsDate = DateTime.Now,
                SavingsAmt = contribution.SavingsAmt,
                MemberId=mid
            };
            status = SavingsCotrollerSql.Insert(model);
            if(status==true)
            {
                Response.Write("<script>alert('You have successfully add the savings');</script>");
            }
            return View();
        }
    }
}